using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("ActaInspeccion")]
public partial class ActaInspeccion
{
    [Key]
    public int IdInspeccion { get; set; }

    public string Observaciones { get; set; }

    public string Recomendaciones { get; set; }

    public string Conclusiones { get; set; }

    public string? FirmaSupervisorImagen { get; set; }
    public string? FirmaAdministradoImagen { get; set; }


    [ForeignKey("IdInspeccion")]
    [InverseProperty("ActaInspeccion")]
    public virtual FichaInspeccion IdInspeccionNavigation { get; set; }
}
