using System.Runtime.InteropServices;

namespace VPM.Application.Wrapper
{

    public static class GStreamerWrapper
    {

        [DllImport("/src/lib/libstreaming.so", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitPipeline(int pipeLineId, byte[] rtspUrl);

        [DllImport("/src/lib/libstreaming.so", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PlayLive(int pipeLineId);

        [DllImport("/src/lib/libstreaming.so", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PauseLiveView(int pipeLineId);

        [DllImport("/src/lib/libstreaming.so", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopLive(int pipeLineId);

        [DllImport("/src/lib/libstreaming.so", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLiveURL(int pipeLineId, byte[] webRTCURL);

    }

}