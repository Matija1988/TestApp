using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Model
{
    /// <summary>
    /// Nepromjenjiv record kojim definiram DTO
    /// Immutable records to define DTOs
    /// Record - Class or Struct 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Abrv"></param>
    public record VehicleMakeDTORead(int Id, string Name, string Abrv);

    public record VehicleMakeDTOInsert(

        [Required]
        [StringLength(80, ErrorMessage ="Maximum allowed number of characters = 80")]
        string Name,

        [Required]
        [StringLength(20, ErrorMessage ="Maximum allowed number of characters = 20")]
        string Abrv);

    public record VehicleMakeDTOReadWithoutID(string Name, string Abrv);

    public record VehicleModelDTORead(int Id, string Name, string Abrv, string Maker);

    public record VehicleModelDTOInsert(

        [Required]
        [StringLength(80, ErrorMessage ="Maximum allowed number of characters = 80")]
        string Name,

        [Required]
        [StringLength(20, ErrorMessage ="Maximum allowed number of characters = 20")]
        string Abrv,

        int MakeId

        );

    public record VehicleModelDTOReadWithoutID(string Name, string Abrv, int Maker);


}
