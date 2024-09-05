#include "Streaming_api.h"
#include "Pipeline.h"
#include <map>

std::map<int, CMx_Pipeline*> m_MapPipeline;

int EXPORTFUNCT InitPipeline(int iPipelineId, const char * rtspURL)
{

	if (m_MapPipeline.find(iPipelineId) != m_MapPipeline.end())
	{
		//here allready pipeline added
		return 0;
	}

	m_MapPipeline[iPipelineId] = new CMx_Pipeline();

	m_MapPipeline[iPipelineId]->InitPipeline(iPipelineId,rtspURL);
	return 1;
}

int  EXPORTFUNCT PlayLive(int iPipelineId)
{
	m_MapPipeline[iPipelineId]->StartLiveView();
	return 1;
}
int  EXPORTFUNCT StopLive(int iPipelineId)
{
	return 1;
}
int  EXPORTFUNCT PauseLiveView(int iPipelineId)
{
	m_MapPipeline[iPipelineId]->PauseLiveView();
	return 1;
}
void  EXPORTFUNCT GetLiveURL(int iPipelineId,char *GetLiveURL)
{
	if (m_MapPipeline.find(iPipelineId) == m_MapPipeline.end())
	{
		//here allready pipeline added
		return ;
	}
	
	m_MapPipeline[iPipelineId]->GetLiveURL(GetLiveURL);
}