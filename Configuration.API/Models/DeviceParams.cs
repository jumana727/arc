using Arc.Common.Enums;

namespace Configuration.API.Models
{

    public class DeviceParams
    {

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; } = string.Empty;

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
        /// Username for the device.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password for the device.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Type of device.
        /// </summary>
        public DeviceType Type { get; set; } = DeviceType.IpCamera;

    }

}