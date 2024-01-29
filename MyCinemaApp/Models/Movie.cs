using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[PrimaryKey("Id", "Name")]
[Table("MOVIES")]
[Index("ContentAdminId", Name = "IX_MOVIES_content_admin_id")]
public partial class Movie
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Key]
    [Column("name")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("content")]
    [StringLength(45)]
    [Unicode(false)]
    public string Content { get; set; } = null!;

    [Column("length")]
    public int Length { get; set; }

    [Column("type")]
    [StringLength(45)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("summary")]
    [StringLength(45)]
    [Unicode(false)]
    public string Summary { get; set; } = null!;

    [Column("director")]
    [StringLength(45)]
    [Unicode(false)]
    public string Director { get; set; } = null!;

    [Column("content_admin_id")]
    public int ContentAdminId { get; set; }

    [ForeignKey("ContentAdminId")]
    [InverseProperty("Movies")]
    public virtual ContentAdmin? ContentAdmin { get; set; } = null!;

    [InverseProperty("Movie")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();
}
