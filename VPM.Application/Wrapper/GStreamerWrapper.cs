using System.Runtime.InteropServices;

namespace VPM.Application.Wrapper
{

    public static class GStreamerWrapper
    {

        [DllImport("VideoProcessor.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitPipeline(int pipeLineId, byte[] rtspUrl);

        [DllImport("VideoProcessor.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PlayLive(int pipeLineId);

        [DllImport("VideoProcessor.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PauseLiveView(int pipeLineId);

        [DllImport("VideoProcessor.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopLive(int pipeLineId);

        [DllImport("VideoProcessor.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLiveURL(int pipeLineId, byte[] webRTCURL);

    }

}