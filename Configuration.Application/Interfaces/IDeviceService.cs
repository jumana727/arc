using Configuration.Application.DTOs;

namespace Configuration.Application.Interfaces
{

    public interface IDeviceService
    {

        List<ComponentDevice> GetDeviceComponents();

        List<Device> GetDevices();

        Device GetDevice(int id);

        bool AddUpdateDevice(Device device);

        bool DeleteDevice(int id);

    }

}