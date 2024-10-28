using Configuration.Domain.Entities;

namespace Configuration.Domain.Interfaces
{

    public interface IDeviceRepository
    {

        #region Device

        List<DeviceConfig> GetDeviceComponents();

        public List<DeviceConfig> GetDevices();

        public DeviceConfig? GetDevice(int id);

        public int AddUpdateDevice(DeviceConfig device);

        public bool DeleteDevice(int id);

        #endregion

    }

}