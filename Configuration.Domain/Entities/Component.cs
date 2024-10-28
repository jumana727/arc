using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Arc.Common.Enums;

namespace Configuration.Domain.Entities
{

    public class ComponentConfig
    {

        /// <summary>
        /// Unique identifier for the component, auto-incremented by the database.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id of the component. It is required and cannot be 0.
        /// This is also part of the primary key.
        /// </summary>
        [Key]
        public int ComponentId { get; set; }

        /// <summary>
        /// Identifier of the device to which this component belongs.
        /// This is also part of the primary key.
        /// </summary>
        [Key]
        public int DeviceId { get; set; }

        /// <summary>
        /// Name of the component. It is required and cannot be empty.
        /// </summary>
        [Required(ErrorMessage = "Component name is required.")]
        [StringLength(100, ErrorMessage = "Component name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of the component. It is required.
        /// </summary>
        [Required(ErrorMessage = "Component type is required.")]
        public ComponentType Type { get; set; }

        /// <summary>
        /// Count of stream profiles associated with the component.
        /// This property is computed and not mapped to the database.
        /// </summary>
        [NotMapped]
        public int ProfileCount => Profiles?.Count ?? 0;

        /// <summary>
        /// List of stream profiles associated with the component. It is initialized to an empty list.
        /// </summary>
        public virtual List<StreamProfileConfig> Profiles { get; set; } = new List<StreamProfileConfig>();

        // Navigation property for Device
        [ForeignKey("Id")]
        public DeviceConfig? Device { get; set; }

    }

}