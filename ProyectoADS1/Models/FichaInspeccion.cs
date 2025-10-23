using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("FichaInspeccion")]
public partial class FichaInspeccion
{
    [Key]
    public int IdInspeccion { get; set; }

    public int IdConcesionario { get; set; }

    [Required]
    [Column("RUC")]
    [StringLength(11)]
    [Unicode(false)]
    public string Ruc { get; set; }

    [StringLength(100)]
    public string NombreComercial { get; set; }

    [StringLength(100)]
    public string RazonSocial { get; set; }

    [StringLength(150)]
    public string Direccion { get; set; }

    [StringLength(50)]
    public string Departamento { get; set; }

    [StringLength(50)]
    public string Provincia { get; set; }

    [StringLength(20)]
    public string Telefono { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string AreaSupervisada { get; set; }

    public DateOnly FechaProgramada { get; set; }

    [StringLength(200)]
    public string MotivoInspeccion { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    [StringLength(25)]
    public string Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizada { get; set; }

    [InverseProperty("IdInspeccionNavigation")]
    public virtual ActaInspeccion ActaInspeccion { get; set; }

    [ForeignKey("IdConcesionario")]
    [InverseProperty("FichaInspeccions")]
    public virtual Concesionario IdConcesionarioNavigation { get; set; }

    [InverseProperty("IdInspeccionNavigation")]
    public virtual InformeInspeccion InformeInspeccion { get; set; }

    [InverseProperty("IdInspeccionNavigation")]
    public virtual MemorandumInspeccion MemorandumInspeccion { get; set; }
}
