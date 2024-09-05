using VPM.Application.Interfaces;
using VPM.Application.Models;

namespace VPM.Application.Services
{

    public class StreamService : IStreamService
    {

        #region Declaration

        static StreamManager streamManager = new StreamManager();

        #endregion

        #region Ctor

        public StreamService()
        {
        }

        #endregion

        #region Methods

        public string GetStreamUrl(string url)
        {
            string responseUrl = string.Empty;

            try
            {
                if (streamManager.CreatePipe(url))
                {
                    StremMapping? mapping = streamManager.GetMapping(url);
                    Console.WriteLine(mapping);

                    if (mapping != null)
                    {
                        mapping.WebRtcUrl = streamManager.GetWebRTCURL(mapping.PipeLineId);
                        if (mapping.WebRtcUrl != null)
                            streamManager.Play(mapping.PipeLineId);

                        responseUrl = mapping.WebRtcUrl ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return responseUrl;
        }

        public bool StartStream(string url)
        {
            bool response = false;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    streamManager.Play(streamManager.GetMapping(url)?.PipeLineId ?? 0);
                    response = true;
                }
            }
            catch (Exception)
            {
            }

            return response;
        }

        public bool PauseStream(string url)
        {
            bool response = false;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    streamManager.Play(streamManager.GetMapping(url)?.PipeLineId ?? 0);
                    response = true;
                }
            }
            catch (Exception)
            {
            }

            return response;
        }

        public bool StopStream(string url)
        {
            bool response = false;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    streamManager.Stop(streamManager.GetMapping(url)?.PipeLineId ?? 0);
                    response = true;
                }
            }
            catch (Exception)
            {
            }

            return response;
        }

        #endregion

    }

}