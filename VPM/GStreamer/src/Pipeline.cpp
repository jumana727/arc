#include <map>
#include "Pipeline.h"
#include <string>
#include <iostream>
#include <filesystem>
#include <unistd.h>
#include <limits.h>
#include <gst/rtsp/rtsp.h>
#include <cstdlib>

//
CMx_Pipeline::CMx_Pipeline()
{
	//here init gstreamer 
	//debug enable 
	std::string path = GetCurrentWorkingDirectory();
	auto pos = path.find_last_of("\\/");
	if (pos != std::string::npos)
		path = path.substr(0, pos);
	std::string pluginPath = "/usr/lib/x86_64-linux-gnu/gstreamer-1.0";
	//path = newpath;
	//std::string pluginPath =  //"D:\\POC_Gstreamer\\1.0\\msvc_x86_64\\lib\\gstreamer-1.0";

	// std::string rootPath = "/usr/lib";
	std::string gioPath = "/usr/lib/x86_64-linux-gnu/gio";
	g_setenv("GST_PLUGIN_PATH", pluginPath.c_str(), TRUE);
	// g_setenv("GSTREAMER_1_0_ROOT_x86_64", rootPath.c_str(), TRUE);
	g_setenv("GIO_MODULE_DIR", gioPath.c_str(), TRUE);

	char level[4] = { 0 };

	snprintf(level, 4, "%d", 3);

	g_setenv("GST_DEBUG", level, TRUE);
	g_setenv("GST_DEBUG_FILE", "/src/log.txt", TRUE);
	gst_init(NULL, NULL);

	//GstDebugLevel temp = 3;
	gst_debug_set_default_threshold(GST_LEVEL_FIXME);
}

CMx_Pipeline::~CMx_Pipeline()
{
}

std::string CMx_Pipeline::GetCurrentWorkingDirectory()
{
    char buffer[PATH_MAX];

    if (getcwd(buffer, sizeof(buffer)))
    {
        return std::string(buffer);
    }

    return "";
}

static void on_rtsp_pad_added(GstElement *src, GstPad *new_pad, GstElement *decoder) {

	static int count = 1;

	GstPad *sink_pad = gst_element_get_static_pad(decoder, "sink");
	if (gst_pad_is_linked(sink_pad)) {
		g_object_unref(sink_pad);
		return;
	}

	GstPadLinkReturn ret = gst_pad_link(new_pad, sink_pad);
	if (GST_PAD_LINK_FAILED(ret)) {
		g_print("link failed");
		//std::cerr << "Type is '" << " is " << "' but link failed." << std::endl;
	}
	else {
		g_print("link_success");
		//std::cout << "Link succeeded.  " << count << std::endl;
	}
	++count;
	g_object_unref(sink_pad);
}

void CMx_Pipeline::InitPipeline(int ipipelineID, const char * rtspURL)
{

	Pipeline = gst_pipeline_new("Pipeline");
	if (!Pipeline)
	{
		gst_print("Failed to create pipeline for ID");
	}

	source   = gst_element_factory_make("rtspsrc", "source");
	decoder  = gst_element_factory_make("rtph264depay", "decoder");
	Parse	 = gst_element_factory_make("h264parse", "parse");
	sink	 = gst_element_factory_make("rtspclientsink", "sink");


	if (!source || !decoder ||  !Parse || !sink)
	{
		g_print("Failed to create element for pipeline ID");
	}

	g_object_set(source, "location", rtspURL, nullptr);
	g_object_set(sink, "protocols", GST_RTSP_LOWER_TRANS_TCP, nullptr);


	std::string mediaMtxIP = "localhost";

	const char* env_var = std::getenv("ASPNETCORE_ENVIRONMENT");

	if(env_var == nullptr || env_var == "Development")
	{
		mediaMtxIP = "localhost";
	}

	else
	{
		if(std::string(env_var) == "Docker")
		{
			mediaMtxIP = "mediamtx";
		}

		else if(std::string(env_var) == "Production")
		{
			mediaMtxIP = "mediamtx.default.svc.cluster.local";
		}
	}
	

	std::string mediaMtxURL = "rtsp://"  + mediaMtxIP + ":8554/" + "live" + std::to_string(ipipelineID);

	m_webrtURL = "http://"  + mediaMtxIP + ":8889/" + "live" + std::to_string(ipipelineID);
	g_object_set(sink, "location",/*"rtsp://192.168.27.164:8554/live1"*/mediaMtxURL.c_str()/*"rtsp://127.0.0.1:8554/live"*/, nullptr);
	
	gst_bin_add_many(GST_BIN(Pipeline), source, decoder,Parse,sink, nullptr);
	if (gst_element_link(decoder, Parse) != TRUE || gst_element_link(Parse, sink) != TRUE)
	{
		g_print("element link is failed");
	}
	g_signal_connect(source, "pad-added", G_CALLBACK(on_rtsp_pad_added), decoder);

	////connect to pad added to signal of the decoder
	//g_signal_connect(decoder, "pad-added", G_CALLBACK(on_pad_added), sink);
}

void CMx_Pipeline::StartLiveView()
{

	ret = gst_element_set_state(Pipeline, GST_STATE_PLAYING);
	if (ret == GST_STATE_CHANGE_FAILURE) {
		g_print("Failed to start live view.");
	}
	else {
		g_print("Live view started successfully.\n");
	}

	//
	//ret = gst_element_set_state(Pipeline, GST_STATE_PLAYING);
	//if (ret == GST_STATE_CHANGE_FAILURE)
	//{
	//	g_print("failed to state change ");
	//}
	//else
	//{
	//	g_print("change state successfully");
	//}
}
void CMx_Pipeline::PauseLiveView()
{
	ret = gst_element_set_state(Pipeline, GST_STATE_PAUSED);

	if (ret == GST_STATE_CHANGE_FAILURE)
	{
		g_print("failed to state change ");
	}
	else
	{
		g_print("change state successfully");
	}
}

void CMx_Pipeline::GetLiveURL(char * GetLiveURL)
{
	if (GetLiveURL != nullptr)
	{
		memcpy(GetLiveURL, m_webrtURL.c_str(), m_webrtURL.size());
	}
}
//void CMx_Pipeline::StartRecording()
//{
//	if (isRecording)
//	{
//		g_print("Recording is already in progress.");
//		return;
//	}
//
//	gst_bin_add(GST_BIN(Pipeline), appsink_record);
//
//	if (!gst_element_link(queue_record, appsink_record)) {
//		g_print("Failed to link recording.");
//		return;
//	}
//
//	std::string ffmpegCmd = "ffmpeg -i - -c:v copy -c:a copy -f hls -hls_time 10 -hls_list_size 0 -hls_segment_filename \"output_%03d.ts\" output.m3u8";
//	ffmpegPipe = _popen(ffmpegCmd.c_str(), "wb");
//
//	if (!ffmpegPipe) {
//		g_print("Failed to start FFmpeg for recording.");
//		return;
//	}
//
//	isRecording = true;
//	g_print("Recording started successfully.");
//}
//void CMx_Pipeline::StopRecording()
//{
//	if (!isRecording) {
//		g_print("Recording is not running.");
//		return;
//	}
//
//	stopFFmpeg();
//
//	gst_element_set_state(appsink_record, GST_STATE_NULL);
//	gst_bin_remove(GST_BIN(Pipeline), appsink_record);
//
//	isRecording = false;
//	g_print("Recording stopped successfully.");
//}
//void CMx_Pipeline::stopFFmpeg() {
//	if (ffmpegPipe) {
//		_pclose(ffmpegPipe);
//		ffmpegPipe = nullptr;
//		isRecording = false;
//		g_print("FFmpeg stopped.");
//	}
//}