using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arc.Common.Enums;

namespace Arc.Common.Entities
{

    public class ConnectionInfo
    {

        /// <summary>
        /// Unique identifier for the connection info, auto-incremented by the database.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NetworkId { get; set; }

        /// <summary>
        /// Identifier of the device associated with this connection info.
        /// This is also part of the primary key.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Device ID is required.")]
        public int DeviceId { get; set; }

        /// <summary>
        /// Name of the connection. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Connection name is required.")]
        [StringLength(100, ErrorMessage = "Connection name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of connection. It is required.
        /// </summary>
        [Required(ErrorMessage = "Connection type is required.")]
        public ConnectionType Type { get; set; }

        /// <summary>
        /// Address of the connection. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Connection address is required.")]
        [StringLength(255, ErrorMessage = "Connection address cannot exceed 255 characters.")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// HTTP port for the connection. It is required and must be between 1 and 65535.
        /// </summary>
        [Required(ErrorMessage = "HTTP port is required.")]
        [Range(1, 65535, ErrorMessage = "HTTP port must be between 1 and 65535.")]
        public int HttpPort { get; set; }

        /// <summary>
        /// RTSP port for the connection. It is required and must be between 1 and 65535.
        /// </summary>
        [Required(ErrorMessage = "RTSP port is required.")]
        [Range(1, 65535, ErrorMessage = "RTSP port must be between 1 and 65535.")]
        public int RtspPort { get; set; }

    }

}