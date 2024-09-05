#pragma once

#ifndef STREAMING_API_H
#define STREAMING_API_H

#define EXPORTFUNCT __attribute__((visibility("default")))

extern "C"
{
    EXPORTFUNCT int InitPipeline(int iPipelineId, const char *rtspURL);
    EXPORTFUNCT int PlayLive(int iCameraID);
    EXPORTFUNCT int StopLive(int iCameraID);
    EXPORTFUNCT int PauseLiveView(int iPipelineId);
    EXPORTFUNCT void GetLiveURL(int iPipelineId, char *GetLiveURL);
}

#endif // !STREAMING_API_H
