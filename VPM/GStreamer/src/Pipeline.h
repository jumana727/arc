#pragma once
#include <gst/gst.h>
#include <iostream>
class PipelineException : public std::runtime_error {
public:
	explicit PipelineException(const std::string& message) : std::runtime_error(message) {}
};
class CMx_Pipeline
{
	
	GstElement *Pipeline;
	GstElement *source;
	GstElement *decoder;
	GstElement *Parse;
	GstElement *sink;
	GstStateChangeReturn ret; // return state of pipeline failure success 
	std::string m_webrtURL;
public:

	CMx_Pipeline();
	~CMx_Pipeline();

	void InitPipeline(int ipipelineID,const char  * rtspURL);
	void StartLiveView();
	void PauseLiveView();
	void GetLiveURL(char *GetLiveURL);
private:
	std::string GetCurrentWorkingDirectory();
};