using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Arc.Common.Enums;

namespace Configuration.Domain.Entities
{

    public class DeviceConfig
    {
        
        /// <summary>
        /// Unique identifier for the device, auto-incremented by the database.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the device. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Device name is required.")]
        [StringLength(100, ErrorMessage = "Device name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        ///// <summary>
        ///// List of preferred networks for the device. It is required and cannot be null.
        ///// </summary>
        //[Required(ErrorMessage = "Preferred networks are required.")]
        //public virtual NetworkConfig? Network { get; set; }

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
        public int HttpPort { get; set; } = 80;

        /// <summary>
        /// RTSP port for the connection. It is required and must be between 1 and 65535.
        /// </summary>
        [Required(ErrorMessage = "RTSP port is required.")]
        [Range(1, 65535, ErrorMessage = "RTSP port must be between 1 and 65535.")]
        public int RtspPort { get; set; } = 554;

        /// <summary>
        /// Type of the device. It is required.
        /// </summary>
        [Required(ErrorMessage = "Device type is required.")]
        public DeviceType Type { get; set; }

        /// <summary>
        /// Username for the device. It is required.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password for the device. It is required.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Protocol used by the device. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Protocol is required.")]
        [StringLength(50, ErrorMessage = "Protocol cannot exceed 50 characters.")]
        public string Protocol { get; set; } = string.Empty;

        /// <summary>
        /// Version of the device. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Version is required.")]
        [StringLength(50, ErrorMessage = "Version cannot exceed 50 characters.")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Count of camera components. This property is computed and not mapped to the database.
        /// </summary>
        [NotMapped]
        public int CameraCount => Components?.Count(x => x.Type == ComponentType.Camera) ?? 0;

        /// <summary>
        /// Count of alarm components. This property is computed and not mapped to the database.
        /// </summary>
        [NotMapped]
        public int AlarmCount => Components?.Count(x => x.Type == ComponentType.Alarm) ?? 0;

        /// <summary>
        /// Count of sensor components. This property is computed and not mapped to the database.
        /// </summary>
        [NotMapped]
        public int SensorCount => Components?.Count(x => x.Type == ComponentType.Sensor) ?? 0;

        /// <summary>
        /// Status of the device. It is required.
        /// </summary>
        [Required(ErrorMessage = "Device status is required.")]
        public DeviceStatus Status { get; set; }

        /// <summary>
        /// List of components associated with the device. It is required and cannot be null.
        /// </summary>
        [Required(ErrorMessage = "Components are required.")]
        public virtual ICollection<ComponentConfig>? Components { get; set; }

    }

}