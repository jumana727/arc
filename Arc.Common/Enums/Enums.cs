using System.ComponentModel;

namespace Arc.Common.Enums
{

    public enum HttpStatusCode
    {
        // 1xx Informational
        [Description("Continue")]
        Continue = 100,

        [Description("Switching Protocols")]
        SwitchingProtocols = 101,

        [Description("Processing")]
        Processing = 102,

        // 2xx Success
        [Description("OK")]
        OK = 200,

        [Description("Created")]
        Created = 201,

        [Description("Accepted")]
        Accepted = 202,

        [Description("Non-Authoritative Information")]
        NonAuthoritativeInformation = 203,

        [Description("No Content")]
        NoContent = 204,

        [Description("Reset Content")]
        ResetContent = 205,

        [Description("Partial Content")]
        PartialContent = 206,

        [Description("Multi-Status")]
        MultiStatus = 207,

        [Description("Already Reported")]
        AlreadyReported = 208,

        [Description("IM Used")]
        IMUsed = 226,

        // 3xx Redirection
        [Description("Multiple Choices")]
        MultipleChoices = 300,

        [Description("Moved Permanently")]
        MovedPermanently = 301,

        [Description("Found")]
        Found = 302,

        [Description("See Other")]
        SeeOther = 303,

        [Description("Not Modified")]
        NotModified = 304,

        [Description("Use Proxy")]
        UseProxy = 305,

        [Description("Switch Proxy")]
        SwitchProxy = 306,

        [Description("Temporary Redirect")]
        TemporaryRedirect = 307,

        [Description("Permanent Redirect")]
        PermanentRedirect = 308,

        // 4xx Client Errors
        [Description("Bad Request")]
        BadRequest = 400,

        [Description("Unauthorized")]
        Unauthorized = 401,

        [Description("Payment Required")]
        PaymentRequired = 402,

        [Description("Forbidden")]
        Forbidden = 403,

        [Description("Not Found")]
        NotFound = 404,

        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,

        [Description("Not Acceptable")]
        NotAcceptable = 406,

        [Description("Proxy Authentication Required")]
        ProxyAuthenticationRequired = 407,

        [Description("Request Timeout")]
        RequestTimeout = 408,

        [Description("Conflict")]
        Conflict = 409,

        [Description("Gone")]
        Gone = 410,

        [Description("Length Required")]
        LengthRequired = 411,

        [Description("Precondition Failed")]
        PreconditionFailed = 412,

        [Description("Payload Too Large")]
        PayloadTooLarge = 413,

        [Description("URI Too Long")]
        URITooLong = 414,

        [Description("Unsupported Media Type")]
        UnsupportedMediaType = 415,

        [Description("Range Not Satisfiable")]
        RangeNotSatisfiable = 416,

        [Description("Expectation Failed")]
        ExpectationFailed = 417,

        [Description("I'm a teapot")]
        ImATeapot = 418,

        [Description("Misdirected Request")]
        MisdirectedRequest = 421,

        [Description("Unprocessable Entity")]
        UnprocessableEntity = 422,

        [Description("Locked")]
        Locked = 423,

        [Description("Failed Dependency")]
        FailedDependency = 424,

        [Description("Too Early")]
        TooEarly = 425,

        [Description("Upgrade Required")]
        UpgradeRequired = 426,

        [Description("Precondition Required")]
        PreconditionRequired = 428,

        [Description("Too Many Requests")]
        TooManyRequests = 429,

        [Description("Request Header Fields Too Large")]
        RequestHeaderFieldsTooLarge = 431,

        [Description("Unavailable For Legal Reasons")]
        UnavailableForLegalReasons = 451,

        // 5xx Server Errors
        [Description("Internal Server Error")]
        InternalServerError = 500,

        [Description("Not Implemented")]
        NotImplemented = 501,

        [Description("Bad Gateway")]
        BadGateway = 502,

        [Description("Service Unavailable")]
        ServiceUnavailable = 503,

        [Description("Gateway Timeout")]
        GatewayTimeout = 504,

        [Description("HTTP Version Not Supported")]
        HTTPVersionNotSupported = 505,

        [Description("Variant Also Negotiates")]
        VariantAlsoNegotiates = 506,

        [Description("Insufficient Storage")]
        InsufficientStorage = 507,

        [Description("Loop Detected")]
        LoopDetected = 508,

        [Description("Not Extended")]
        NotExtended = 510,

        [Description("Network Authentication Required")]
        NetworkAuthenticationRequired = 511
    }

    public enum DeviceType : byte
    {

        [Description("Ip Camera")]
        IpCamera = 1,

        [Description("NVR")]
        NVR = 2

    }

    public enum ConnectionType : byte
    {

        [Description("Ip or Server Name")]
        Ip = 1,

        [Description("Matrix DNS - Host Name")]
        HostName = 2,

        [Description("Matrix DNS - MAC Address")]
        MacAddress = 3

    }

    public enum DeviceStatus : byte
    {

        [Description("Enabled")]
        Enabled = 1,

        [Description("Disabled")]
        Disabled = 2,

        [Description("Undermaintainance")]
        Undermaintainance = 3

    }

    public enum ComponentType : byte
    {

        [Description("Alarm")]
        Alarm = 1,

        [Description("Camera")]
        Camera = 2,

        [Description("Sensor")]
        Sensor = 3

    }

    public enum VideoCodec : byte
    {

        [Description("H.264")]
        H264 = 1,

        [Description("H.265")]
        H265 = 2,

        [Description("MJPEG")]
        MJPEG = 3

    }

    public enum VideoResolution
    {

        // Standard Resolutions
        [Description("160x120")]
        QQVGA = 0, // Quarter Quarter VGA

        [Description("320x240")]
        QVGA = 1, // Quarter VGA

        [Description("640x480")]
        VGA = 2, // Video Graphics Array

        [Description("720x480")]
        SD = 3, // Standard Definition

        // HD Resolutions
        [Description("1280x720")]
        HD = 4, // High Definition 720p

        [Description("1920x1080")]
        FullHD = 5, // Full High Definition 1080p

        // Ultra HD Resolutions
        [Description("2560x1440")]
        QHD = 6, // Quad High Definition 1440p

        [Description("3840x2160")]
        UHD = 7, // Ultra High Definition 4K

        [Description("7680x4320")]
        UHD8K = 8, // Ultra High Definition 8K

        // Cinema Resolutions
        [Description("2048x1080")]
        _2K = 9, // 2K Cinema

        [Description("4096x2160")]
        _4KCinema = 10, // 4K Cinema

        [Description("8192x4320")]
        _8KCinema = 11 // 8K Cinema

    }

    public enum AudioCodec
    {

        // Lossy Audio Codecs
        [Description("MP3 - MPEG Layer 3")]
        MP3 = 1,

        [Description("AAC - Advanced Audio Coding")]
        AAC = 2,

        [Description("Ogg Vorbis")]
        OggVorbis = 3,

        [Description("WMA - Windows Media Audio")]
        WMA = 4,

        [Description("Opus")]
        Opus = 5,

        // Lossless Audio Codecs
        [Description("FLAC - Free Lossless Audio Codec")]
        FLAC = 6,

        [Description("ALAC - Apple Lossless Audio Codec")]
        ALAC = 7,

        [Description("WAV - Waveform Audio File Format")]
        WAV = 8,

        [Description("AIFF - Audio Interchange File Format")]
        AIFF = 9,

        [Description("APE - Monkey's Audio")]
        APE = 10,

        // Speech Codecs
        [Description("G.711")]
        G711 = 11,

        [Description("G.722")]
        G722 = 12,

        [Description("G.726")]
        G726 = 13,

        [Description("G.729")]
        G729 = 14,

        // Other Audio Codecs
        [Description("AC-3 - Dolby Digital")]
        AC3 = 15,

        [Description("DTS - Digital Theater Systems")]
        DTS = 16,

        [Description("Vorbis")]
        Vorbis = 17,

        [Description("AMR - Adaptive Multi-Rate")]
        AMR = 18

    }

    public enum StreamingBitrate
    {

        // Low Bitrates
        [Description("32 kbps - Low quality")]
        Kbps32 = 32,

        [Description("64 kbps - Low quality")]
        Kbps64 = 64,

        [Description("96 kbps - Low quality")]
        Kbps96 = 96,

        // Medium Bitrates
        [Description("128 kbps - Standard quality")]
        Kbps128 = 128,

        [Description("192 kbps - Standard quality")]
        Kbps192 = 192,

        [Description("256 kbps - Standard quality")]
        Kbps256 = 256,

        // High Bitrates
        [Description("320 kbps - High quality")]
        Kbps320 = 320,

        [Description("512 kbps - High quality")]
        Kbps512 = 512,

        // Very High Bitrates
        [Description("1000 kbps - High quality")]
        Kbps1000 = 1000,

        [Description("1500 kbps - High quality")]
        Kbps1500 = 1500,

        [Description("2000 kbps - High quality")]
        Kbps2000 = 2000,

        // Ultra High Bitrates
        [Description("3000 kbps - Ultra high quality")]
        Kbps3000 = 3000,

        [Description("4000 kbps - Ultra high quality")]
        Kbps4000 = 4000,

        [Description("5000 kbps - Ultra high quality")]
        Kbps5000 = 5000,

        [Description("8000 kbps - Ultra high quality")]
        Kbps8000 = 8000,

        // Professional Quality Bitrates
        [Description("10000 kbps - Professional quality")]
        Kbps10000 = 10000,

        [Description("15000 kbps - Professional quality")]
        Kbps15000 = 15000,

        [Description("20000 kbps - Professional quality")]
        Kbps20000 = 20000

    }

}