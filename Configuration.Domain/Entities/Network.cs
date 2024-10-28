using System.ComponentModel.DataAnnotations.Schema;
using Arc.Common.Entities;

namespace Configuration.Domain.Entities
{

    public class NetworkConfig : ConnectionInfo
    {

        // Navigation property
        [ForeignKey("DeviceId")]
        public virtual DeviceConfig? Device { get; set; }

    }

}
