using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("InformeInspeccion")]
public partial class InformeInspeccion
{
    [Key]
    public int IdInspeccion { get; set; }

    public int? IdUsuario { get; set; }

    public string? FirmaSupervisorImagen { get; set; }

    public string? FirmaCoordinadorImagen { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [ForeignKey("IdInspeccion")]
    [InverseProperty("InformeInspeccion")]
    public virtual FichaInspeccion IdInspeccionNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("InformeInspeccions")]
    public virtual Usuario IdUsuarioNavigation { get; set; }
}
