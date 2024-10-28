using Arc.Common.Enums;

namespace Configuration.Application.DTOs
{

    public class Component
    {

        /// <summary>
        /// Unique identifier for the component.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the component.
        /// </summary>
        public int ComponentId { get; set; }

        /// <summary>
        /// Identifier of the device to which this component belongs.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Name of the component.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of the component.
        /// </summary>
        public ComponentType Type { get; set; }

        /// <summary>
        /// Count of stream profiles associated with the component.
        /// </summary>
        public int ProfileCount { get; set; }

        /// <summary>
        /// List of stream profiles associated with the component.
        /// </summary>
        public List<StreamProfile> Profiles { get; set; } = new List<StreamProfile>();

    }

}