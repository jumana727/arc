using System.Runtime.InteropServices;
using System.Text;
using VPM.Application.Models;
using VPM.Application.Wrapper;

namespace VPM.Application.Services
{

    public class StreamManager
    {

        #region Declaration

        private List<StremMapping> lstMapping;
        private int mappingCounter = 1;

        #endregion

        #region Ctor

        public StreamManager()
        {
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            lstMapping = new List<StremMapping>();
        }

        public bool CreatePipe(string rtspUrl)
        {
            bool result = false;

            try
            {
                if (rtspUrl != null)
                {
                    StremMapping? mapping = lstMapping.FirstOrDefault(x => x.RtspUrl.Equals(rtspUrl, StringComparison.OrdinalIgnoreCase));

                    Console.WriteLine("RTSP URL :" + rtspUrl + "Mapping : " + mapping);
                    if (mapping == null)
                    {
                        InitPipeline(mappingCounter, rtspUrl);
                        lstMapping.Add(new StremMapping() { PipeLineId = mappingCounter, RtspUrl = rtspUrl });
                        mappingCounter += 1;
                        Console.WriteLine("mappingCounter : " + mappingCounter);
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public bool RemovePipe(string rtspUrl)
        {
            bool result = false;

            try
            {
                if (rtspUrl != null)
                {
                    StremMapping? mapping = lstMapping.FirstOrDefault(x => x.RtspUrl.Equals(rtspUrl, StringComparison.OrdinalIgnoreCase));

                    if (mapping != null)
                    {
                        StopLive(mappingCounter);
                        lstMapping.Remove(mapping);
                    }

                    result = true;
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        public bool RemovePipe(int pipeLineId)
        {
            bool result = false;

            try
            {
                if (pipeLineId > 0)
                {
                    StremMapping? mapping = lstMapping.FirstOrDefault(x => x.PipeLineId == pipeLineId);

                    if (mapping != null)
                    {
                        StopLive(mappingCounter);
                        lstMapping.Remove(mapping);
                    }

                    result = true;
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        public StremMapping? GetMapping(string rtspUrl)
        {
            StremMapping? result = null;

            if (rtspUrl != null)
                result = lstMapping.FirstOrDefault(x => x.RtspUrl.Equals(rtspUrl, StringComparison.OrdinalIgnoreCase));

            return result;
        }

        public StremMapping? GetMapping(int pipeLineId)
        {
            StremMapping? result = null;

            if (pipeLineId > 0)
                result = lstMapping.FirstOrDefault(x => x.PipeLineId == pipeLineId);

            return result;
        }

        public string GetWebRTCURL(int pipeLineId)
        {
            return GetLiveURL(pipeLineId);
        }

        public void Play(int pipeLineId)
        {
            PlayLive(pipeLineId);
        }

        public void Pause(int pipeLineId)
        {
            PauseLiveView(pipeLineId);
        }

        public void Stop(int pipeLineId)
        {
            StopLive(pipeLineId);
        }

        #region WrapperMethod

        private void InitPipeline(int pipeLineId, string rtspUrl)
        {
            GStreamerWrapper.InitPipeline(pipeLineId, Encoding.ASCII.GetBytes(rtspUrl));
        }

        private string GetLiveURL(int pipeLineId)
        {
            // stream url buffer
            byte[] webrtcURL = new byte[100];
            GStreamerWrapper.GetLiveURL(pipeLineId, webrtcURL);
            return Encoding.UTF8.GetString(webrtcURL).Trim('\0') ?? string.Empty;
        }

        private void PlayLive(int pipeLineId)
        {
            GStreamerWrapper.PlayLive(pipeLineId);
        }

        private void PauseLiveView(int pipeLineId)
        {
            GStreamerWrapper.PauseLiveView(pipeLineId);
        }

        private void StopLive(int pipeLineId)
        {
            GStreamerWrapper.StopLive(pipeLineId);
        }

        #endregion

        #endregion

    }

}