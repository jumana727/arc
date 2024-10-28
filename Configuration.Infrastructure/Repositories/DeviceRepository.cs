using Arc.Common.Enums;
using Configuration.Domain.Entities;
using Configuration.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Infrastructure.Repositories
{

    public class DeviceRepository : IDeviceRepository
    {

        #region Declaration

        private readonly DatabaseContext dbContext;

        #endregion

        #region Ctor

        public DeviceRepository(DatabaseContext _dbContext)
        {
            dbContext = _dbContext;
        }

        #endregion

        #region Methods

        #region Public Methods

        public List<DeviceConfig> GetDeviceComponents() => dbContext.Devices
            .Include(d => d.Components)
            .Select(device => new DeviceConfig()
            {
                Id = device.Id,
                Name = device.Name,
                Components = device.Components.Where(x=>x.Type == ComponentType.Camera).Select(component => new ComponentConfig()
                {
                    Id = component.Id,
                    ComponentId = component.ComponentId,
                    Name = component.Name
                }).ToList()
            }).ToList();

        public List<DeviceConfig> GetDevices() => dbContext.Devices.Include(x => x.Components)?.ThenInclude(x => x.Profiles).ToList();

        public DeviceConfig? GetDevice(int id) => dbContext.Devices.Where(x => x.Id == id)?.Include(x => x.Components).ThenInclude(x => x.Profiles).FirstOrDefault();

        public int AddUpdateDevice(DeviceConfig device) => AddUpdate(device);

        public bool DeleteDevice(int id) => Delete(id);

        #endregion

        #region Private Methods

        private int AddUpdate(DeviceConfig device)
        {
            int result = 0;

            try
            {
                DeviceConfig? _device = dbContext.Devices.Find(device.Id);

                if (_device == null)
                    dbContext.Devices.Add(device);
                else
                {
                    _device.Id = device.Id;
                    _device.Name = device.Name;
                    _device.Address = device.Address;
                    _device.HttpPort = device.HttpPort;
                    _device.RtspPort = device.RtspPort;
                    _device.Type = device.Type;
                    _device.UserName = device.UserName;
                    _device.Password = device.Password;
                    _device.Protocol = device.Protocol;
                    _device.Version = device.Version;
                    _device.Status = device.Status;
                }

                dbContext.SaveChanges();

                result = device.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private bool Delete(int id)
        {
            bool result = false;

            try
            {
                DeviceConfig? _device = dbContext.Devices.Find(id);

                if (_device != null)
                    dbContext.Devices.Remove(_device);

                dbContext.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion

        #endregion

    }

}