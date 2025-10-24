using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoADS1.Models;

[Table("MemorandumInspeccion")]
[Index("IdInspeccion", Name = "UQ__Memorand__2929253E6844E20C", IsUnique = true)]
public partial class MemorandumInspeccion
{
    [Key]
    public int IdMemo { get; set; }

    public int IdInspeccion { get; set; }

    [StringLength(255)]
    public string Asunto { get; set; }

    [Column(TypeName = "text")]
    public string Cuerpo { get; set; }

    public string? FirmaCoordinadorGeneral { get; set; }  
    public string? FirmaCoordinador { get; set; }    
    public string? FirmaDirectorGeneral { get; set; }    

    [Column(TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaActualizada { get; set; }

    [ForeignKey("IdInspeccion")]
    [InverseProperty("MemorandumInspeccion")]
    public virtual FichaInspeccion IdInspeccionNavigation { get; set; }
}
