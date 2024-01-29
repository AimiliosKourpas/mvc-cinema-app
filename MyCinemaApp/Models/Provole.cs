using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[PrimaryKey("MoviesId", "CinemasId", "MoviesName")]
[Table("PROVOLES")]
[Index("CinemasId", Name = "IX_PROVOLES_cinemas_id")]
[Index("ContentAdminId", Name = "IX_PROVOLES_content_admin_id")]
[Index("MoviesId", "MoviesName", Name = "IX_PROVOLES_movies_id_movies_name")]
public partial class Provole
{
    [Key]
    [Column("movies_id")]
    public int MoviesId { get; set; }

    [Key]
    [Column("cinemas_id")]
    public int CinemasId { get; set; }

    [Key]
    [Column("movies_name")]
    [StringLength(45)]
    [Unicode(false)]
    public string MoviesName { get; set; } = null!;

    [Column("content_admin_id")]
    public int ContentAdminId { get; set; }

    [Column("ID")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Id { get; set; }

    [Column("movie_date")]
    [Display(Name = "Date and time")]
    public DateTime? MovieDate { get; set; }


    [ForeignKey("CinemasId")]
    [InverseProperty("Provoles")]
    public virtual Cinema? Cinemas { get; set; } 

    [ForeignKey("ContentAdminId")]
    [InverseProperty("Provoles")]
    public virtual ContentAdmin? ContentAdmin { get; set; } 

    [ForeignKey("MoviesId, MoviesName")]
    [InverseProperty("Provoles")]
    public virtual Movie? Movie { get; set; } 

    [InverseProperty("Provole")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
