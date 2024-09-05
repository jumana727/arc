
namespace VPM.Application.Interfaces
{

    public interface IStreamService
    {

        string GetStreamUrl(string url);

        bool StartStream(string url);

        bool PauseStream(string url);

        bool StopStream(string url);

    }

}