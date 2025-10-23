using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("dni")]
    public int Dni { get; set; }

    [Required]
    [Column("nombreUsuario")]
    [StringLength(50)]
    public string NombreUsuario { get; set; }

    [Required]
    [Column("correo")]
    [StringLength(100)]
    public string Correo { get; set; }

    [Required]
    [Column("password")]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    [Column("rol")]
    [StringLength(20)]
    public string Rol { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<InformeInspeccion> InformeInspeccions { get; set; } = new List<InformeInspeccion>();
}
