using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("Concesionario")]
public partial class Concesionario
{
    [Key]
    public int IdConcesionario { get; set; }

    [StringLength(100)]
    public string NombreComercial { get; set; }

    [Required]
    [StringLength(100)]
    public string RazonSocial { get; set; }

    [Required]
    [Column("RUC")]
    [StringLength(11)]
    [Unicode(false)]
    public string Ruc { get; set; }

    [StringLength(150)]
    public string Direccion { get; set; }

    [StringLength(50)]
    public string Departamento { get; set; }

    [StringLength(50)]
    public string Provincia { get; set; }

    [StringLength(50)]
    public string Distrito { get; set; }

    [StringLength(20)]
    public string Telefono { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public bool Estado { get; set; }

    [InverseProperty("IdConcesionarioNavigation")]
    public virtual ICollection<FichaInspeccion> FichaInspeccions { get; set; } = new List<FichaInspeccion>();
}
