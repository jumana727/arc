using AutoMapper;
using Arc.Common.Enums;
using Arc.Common.Extentions;
using Configuration.Application.DTOs;
using Configuration.Application.Interfaces;
using Configuration.Domain.Entities;
using Configuration.Domain.Interfaces;

namespace Configuration.Application.Services
{

    public class DeviceService : IDeviceService
    {

        #region Declaration

        private readonly IDeviceRepository devicRepository;
        private readonly IComponentService componentService;
        private readonly IMapper mapper;

        #endregion

        #region Ctor

        public DeviceService(IDeviceRepository _devicRepository, IComponentService _componentService, IMapper _mapper)
        {
            devicRepository = _devicRepository;
            componentService = _componentService;
            mapper = _mapper;
        }

        #endregion

        #region Methods

        #region Public Methods

        public List<ComponentDevice> GetDeviceComponents() => mapper.Map<List<ComponentDevice>>(devicRepository.GetDeviceComponents());

        public List<Device> GetDevices() => mapper.Map<List<Device>>(devicRepository.GetDevices());

        public Device? GetDevice(int id) => mapper.Map<Device>(devicRepository.GetDevice(id));

        public bool AddUpdateDevice(Device device) => AddUpdate(device);

        public bool DeleteDevice(int id) => Delete(id);

        #endregion

        #region Private Methods

        private bool AddUpdate(Device device)
        {
            bool result = false;
            bool status = true;
            bool isAddOperation = device.Id == 0;
            int id = 0;
            Random _random = new Random();

            try
            {
                device.Status = DeviceStatus.Enabled;
                device.Version = "1.0.0";
                device.Protocol = device.Type == DeviceType.IpCamera ? "ONVIF" : "BM";
                device.Id = devicRepository.AddUpdateDevice(mapper.Map<DeviceConfig>(device));

                if (isAddOperation)
                {
                    if (device.Components?.Count <= 0)
                        if (device.Type == DeviceType.IpCamera)
                            device.Components = GenerateComponents(device.Id, string.Format("{0}:{1}", device.Address, device.RtspPort), 1, _random.Next(1, 8), _random.Next(1, 8));
                        else if (device.Type == DeviceType.NVR)
                            device.Components = GenerateComponents(device.Id, string.Format("{0}:{1}", device.Address, device.RtspPort), _random.Next(1, 99), _random.Next(1, 99), _random.Next(1, 99));
                }

                if (status)
                {
                    if (device.Components != null)
                    {
                        foreach (var component in device.Components)
                        {
                            id = componentService.AddUpdateComponent(component);

                            if (id > 0)
                            {
                                if (component.Type == ComponentType.Camera)
                                    foreach (var profile in component.Profiles)
                                    {
                                        id = componentService.AddUpdateStreamProfile(profile);

                                        if (id > 0)
                                            continue;
                                        else
                                            break;
                                    }

                                continue;
                            }
                            else
                                break;
                        }
                    }
                }

                if (!status)
                    Delete(device.Id);

                result = status;
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
                componentService.DeleteStreamProfiles(id);
                componentService.DeleteComponents(id);
                devicRepository.DeleteDevice(id);

                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private List<Component> GenerateComponents(int deviceId, string address, int cameraCount = 1, int alarmCount = 1, int sensorCount = 1)
        {
            List<Component> result = new List<Component>();

            try
            {
                int componentCount = 1;

                for (int i = 1; i <= cameraCount; i++)
                {
                    result.Add(new Component()
                    {
                        Id = 0,
                        ComponentId = componentCount,
                        DeviceId = deviceId,
                        Name = string.Format("{0}-{1}", ComponentType.Camera.GetDescription(), i),
                        Type = ComponentType.Camera,
                        Profiles = GenerateStreamProfiles(deviceId, componentCount, address)
                    });
                    componentCount += 1;
                }

                for (int i = 1; i <= alarmCount; i++)
                {
                    result.Add(new Component()
                    {
                        Id = 0,
                        ComponentId = componentCount,
                        DeviceId = deviceId,
                        Name = string.Format("{0}-{1}", ComponentType.Alarm.GetDescription(), i),
                        Type = ComponentType.Alarm
                    });
                    componentCount += 1;
                }

                for (int i = 1; i <= sensorCount; i++)
                {
                    result.Add(new Component()
                    {
                        Id = 0,
                        ComponentId = componentCount,
                        DeviceId = deviceId,
                        Name = string.Format("{0}-{1}", ComponentType.Sensor.GetDescription(), i),
                        Type = ComponentType.Sensor
                    });
                    componentCount += 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private List<StreamProfile> GenerateStreamProfiles(int deviceId, int componentId, string address, int profileCount = 4)
        {
            List<StreamProfile> result = new List<StreamProfile>();
            Random _random = new Random();

            try
            {
                for (int i = 1; i <= profileCount; i++)
                    result.Add(new StreamProfile()
                    {
                        Id = i,
                        DeviceId = deviceId,
                        ComponentId = componentId,
                        No = i,
                        Name = $"Profile {i}",
                        VideoCodec = (VideoCodec)_random.Next(1, Enum.GetValues(typeof(VideoCodec)).Length),
                        Resolution = (VideoResolution)_random.Next(1, Enum.GetValues(typeof(VideoResolution)).Length),
                        AudioCodec = (AudioCodec)_random.Next(1, Enum.GetValues(typeof(AudioCodec)).Length),
                        BitrateControl = (byte)_random.Next(1, 2),
                        Bitrate = (StreamingBitrate)_random.Next(1, Enum.GetValues(typeof(StreamingBitrate)).Length),
                        FPS = _random.Next(1, 30), // FPS between 1 and 30
                        Quality = _random.Next(1, 4), // Quality between 1 and 4
                        GOP = _random.Next(1, 100), // GOP size between 1 and 100
                        EnableAudio = _random.Next(0, 2) == 1,
                        Url = $"rtsp://{address}/unicaststream/{i}"
                    });
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