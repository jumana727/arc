
namespace Configuration.Application.DTOs
{

    public class ComponentDevice
    {

        /// <summary>
        /// Unique identifier for the device.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// List of components associated with the device.
        /// </summary>
        public ICollection<DeviceComponent> Components { get; set; } = new List<DeviceComponent>();

    }

}