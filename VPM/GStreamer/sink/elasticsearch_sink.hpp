#pragma once

#include <iostream>
#include <curl/curl.h>
#include "spdlog/spdlog.h"
#include "spdlog/sinks/base_sink.h"
#include "spdlog/details/null_mutex.h"
#include <mutex>
#include "spdlog/common.h"
#include <chrono>
#include <iomanip>
#include <sstream>
#include <string_view>
#include <type_traits>

template<typename Mutex>
class elasticsearch_sink : public spdlog::sinks::base_sink<Mutex>
{
public:
    elasticsearch_sink(const std::string& url, const std::string& index_name)
        : url_(url + "/" + index_name + "/_doc")
    {
        curl_global_init(CURL_GLOBAL_ALL);
    }

    ~elasticsearch_sink()
    {
        curl_global_cleanup();
    }

protected:
    void sink_it_(const spdlog::details::log_msg& msg) override
    {
        try {
            // Format the log message
            spdlog::memory_buf_t formatted;
            spdlog::sinks::base_sink<Mutex>::formatter_->format(msg, formatted);
            
            // Get current time
            auto now = std::chrono::system_clock::now();
            auto now_c = std::chrono::system_clock::to_time_t(now);
            std::stringstream ss;
            ss << std::put_time(std::localtime(&now_c), "%Y-%m-%d %H:%M:%S");
            
            // Prepare JSON payload
            std::string json_payload = fmt::format(
                R"({{"filename":"{}","log_level":"{}","log":"{}","timestamp":"{}"}})",
                msg.source.filename ? escape_json(msg.source.filename) : "unknown",
                escape_json(spdlog::level::to_string_view(msg.level)),
                escape_json(fmt::to_string(formatted)),
                ss.str()
            );

            // Send to Elasticsearch
            CURL *curl = curl_easy_init();
            if(curl) {
                struct curl_slist *headers = NULL;
                headers = curl_slist_append(headers, "Content-Type: application/json");
                
                curl_easy_setopt(curl, CURLOPT_URL, url_.c_str());
                curl_easy_setopt(curl, CURLOPT_POSTFIELDS, json_payload.c_str());
                curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);

                CURLcode res = curl_easy_perform(curl);
                if(res != CURLE_OK)
                    std::cerr << "curl_easy_perform() failed: " << curl_easy_strerror(res) << std::endl;

                curl_slist_free_all(headers);
                curl_easy_cleanup(curl);
            }
        } catch (const std::exception& e) {
            std::cerr << "Error in elasticsearch_sink: " << e.what() << std::endl;
        }
    }

    void flush_() override
    {
        // Elasticsearch doesn't require explicit flushing
    }

private:
    std::string url_;

     // Add this new overload
    std::string escape_json(spdlog::string_view_t input) {
        return escape_json(std::string_view(input.data(), input.size()));
    }
    
    // Function to escape special characters in JSON
    std::string escape_json(const char* input) {
        return escape_json(std::string_view(input));
    }

    std::string escape_json(const std::string& input) {
        return escape_json(std::string_view(input));
    }

    std::string escape_json(std::string_view input) {
        std::string output;
        output.reserve(input.length());
        for (char c : input) {
            switch (c) {
                case '"': output += "\\\""; break;
                case '\\': output += "\\\\"; break;
                case '\b': output += "\\b"; break;
                case '\f': output += "\\f"; break;
                case '\n': output += "\\n"; break;
                case '\r': output += "\\r"; break;
                case '\t': output += "\\t"; break;
                default:
                    if ('\x00' <= c && c <= '\x1f') {
                        output += fmt::format("\\u{:04X}", static_cast<int>(c));
                    } else {
                        output += c;
                    }
            }
        }
        return output;
    }
};

using elasticsearch_sink_mt = elasticsearch_sink<std::mutex>;
using elasticsearch_sink_st = elasticsearch_sink<spdlog::details::null_mutex>;