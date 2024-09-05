using Arc.Common.Enums;

namespace Configuration.Application.DTOs
{

    public class Device
    {

        /// <summary>
        /// Unique identifier for the device.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        ///// <summary>
        ///// List of preferred networks for the device.
        ///// </summary>
        //public ICollection<Network> PreferedNetworks { get; set; } = new List<Network>();

        /// <summary>
        /// Address of the connection. It is required and cannot be empty.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// HTTP port for the connection. It is required and must be between 1 and 65535.
        /// </summary>
        public int HttpPort { get; set; } = 80;

        /// <summary>
        /// RTSP port for the connection. It is required and must be between 1 and 65535.
        /// </summary>
        public int RtspPort { get; set; } = 554;

        /// <summary>
        /// Type of the device.
        /// </summary>
        public DeviceType Type { get; set; }

        /// <summary>
        /// Username for the device.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password for the device.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Protocol used by the device.
        /// </summary>
        public string Protocol { get; set; } = string.Empty;

        /// <summary>
        /// Version of the device.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Count of camera components.
        /// </summary>
        public int CameraCount { get; set; }

        /// <summary>
        /// Count of alarm components.
        /// </summary>
        public int AlarmCount { get; set; }

        /// <summary>
        /// Count of sensor components.
        /// </summary>
        public int SensorCount { get; set; }

        /// <summary>
        /// Status of the device.
        /// </summary>
        public DeviceStatus Status { get; set; }

        /// <summary>
        /// List of components associated with the device.
        /// </summary>
        public ICollection<Component> Components { get; set; } = new List<Component>();

    }

}