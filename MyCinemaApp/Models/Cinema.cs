using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[Table("CINEMAS")]
public partial class Cinema
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("seats")]
    [StringLength(45)]
    [Unicode(false)]
    public string Seats { get; set; } = null!;

    [Column("three_d")]
    [StringLength(45)]
    [Unicode(false)]
    [Display(Name = "3D")]
    public string ThreeD { get; set; } = null!;

    [InverseProperty("Cinemas")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();
}
