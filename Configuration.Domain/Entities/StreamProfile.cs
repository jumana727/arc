using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arc.Common.Enums;

namespace Configuration.Domain.Entities
{

    public class StreamProfileConfig
    {

        /// <summary>
        /// Unique identifier for the stream profile, auto-incremented by the database.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        /// <summary>
        /// Identifier of the component to which this stream profile belongs.
        /// </summary>
        public int ComponentId { get; set; }

        [Key]
        /// <summary>
        /// Identifier of the device to which this component belongs.
        /// This is also part of the primary key.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Stream profile number, used to distinguish between multiple profiles.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Stream profile number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stream profile number must be a positive integer.")]
        public int No { get; set; }

        /// <summary>
        /// Name of the stream profile. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Stream profile name is required.")]
        [StringLength(100, ErrorMessage = "Stream profile name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Video codec used in the stream profile.
        /// </summary>
        [Required(ErrorMessage = "Video codec is required.")]
        public VideoCodec VideoCodec { get; set; }

        /// <summary>
        /// Resolution of the video stream.
        /// </summary>
        [Required(ErrorMessage = "Video resolution is required.")]
        public VideoResolution Resolution { get; set; }

        /// <summary>
        /// Audio codec used in the stream profile.
        /// </summary>
        [Required(ErrorMessage = "Audio codec is required.")]
        public AudioCodec AudioCodec { get; set; }

        /// <summary>
        /// Bitrate control mode for the video stream.
        /// </summary>
        [Required(ErrorMessage = "Bitrate control is required.")]
        [Range(0, byte.MaxValue, ErrorMessage = "Bitrate control must be between 0 and 255.")]
        public byte BitrateControl { get; set; }

        /// <summary>
        /// Bitrate of the video stream.
        /// </summary>
        [Required(ErrorMessage = "Streaming bitrate is required.")]
        public StreamingBitrate Bitrate { get; set; }

        /// <summary>
        /// Frames per second (FPS) for the video stream.
        /// </summary>
        [Required(ErrorMessage = "FPS is required.")]
        [Range(1, 120, ErrorMessage = "FPS must be between 1 and 120.")]
        public int FPS { get; set; }

        /// <summary>
        /// Quality level of the video stream, typically between 1 and 100.
        /// </summary>
        [Required(ErrorMessage = "Quality is required.")]
        [Range(1, 100, ErrorMessage = "Quality must be between 1 and 100.")]
        public int Quality { get; set; }

        /// <summary>
        /// GOP (Group of Pictures) size for the video stream.
        /// </summary>
        [Required(ErrorMessage = "GOP size is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "GOP size must be a positive integer.")]
        public int GOP { get; set; }

        /// <summary>
        /// Indicates if audio is enabled in the stream profile.
        /// </summary>
        public bool EnableAudio { get; set; }

        /// <summary>
        /// URL of the stream.
        /// </summary>
        [Required(ErrorMessage = "Stream URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for Component.
        /// </summary>
        [ForeignKey("ComponentId,DeviceId")]
        [Required(ErrorMessage = "Component is required.")]
        public virtual ComponentConfig? Component { get; set; }

    }

}