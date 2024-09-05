using Arc.Common.Enums;

namespace Configuration.Application.DTOs
{

    public class Network
    {

        /// <summary>
        /// Unique identifier for the connection info.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the network.
        /// </summary>
        public int NetworkId { get; set; }

        /// <summary>
        /// Identifier of the device associated with this connection info.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Name of the connection.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of connection.
        /// </summary>
        public ConnectionType Type { get; set; }

        /// <summary>
        /// Address of the connection.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// HTTP port for the connection.
        /// </summary>
        public int HttpPort { get; set; }

        /// <summary>
        /// RTSP port for the connection.
        /// </summary>
        public int RtspPort { get; set; }

    }

}