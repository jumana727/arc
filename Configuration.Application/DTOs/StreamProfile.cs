using Arc.Common.Enums;

namespace Configuration.Application.DTOs
{

    public class StreamProfile
    {

        /// <summary>
        /// Unique identifier for the stream profile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the device to which this stream profile belongs.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Identifier of the component to which this stream profile belongs.
        /// </summary>
        public int ComponentId { get; set; }

        /// <summary>
        /// Stream profile number, used to distinguish between multiple profiles.
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// Name of the stream profile.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Video codec used in the stream profile.
        /// </summary>
        public VideoCodec VideoCodec { get; set; }

        /// <summary>
        /// Resolution of the video stream.
        /// </summary>
        public VideoResolution Resolution { get; set; }

        /// <summary>
        /// Audio codec used in the stream profile.
        /// </summary>
        public AudioCodec AudioCodec { get; set; }

        /// <summary>
        /// Bitrate control mode for the video stream.
        /// </summary>
        public byte BitrateControl { get; set; }

        /// <summary>
        /// Bitrate of the video stream.
        /// </summary>
        public StreamingBitrate Bitrate { get; set; }

        /// <summary>
        /// Frames per second (FPS) for the video stream.
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// Quality level of the video stream.
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// GOP (Group of Pictures) size for the video stream.
        /// </summary>
        public int GOP { get; set; }

        /// <summary>
        /// Indicates if audio is enabled in the stream profile.
        /// </summary>
        public bool EnableAudio { get; set; }

        /// <summary>
        /// URL of the stream.
        /// </summary>
        public string Url { get; set; } = string.Empty;

    }

}