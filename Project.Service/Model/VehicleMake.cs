using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Model
{
    /// <summary>
    /// POKO za VehicleMake
    /// POCO for VehicleMake
    /// </summary>
    public class VehicleMake
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Maximum allowed nubmer of characters = 80")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Maximum allowed number of characters = 20")]
        public string Abrv { get; set; }

        public ICollection<VehicleModel> Models { get; set; }
    }
}
