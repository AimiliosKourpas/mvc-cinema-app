using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MyCinemaApp.Models;

namespace MyCinemaApp.Models;

[PrimaryKey("MoviesId", "CinemasId", "CustomersId")]
[Table("RESERVATIONS")]
public partial class Reservation
{
    [Key]
    [Column("movies_id")]
    public int MoviesId { get; set; }

    [Column("movies_name")]
    [StringLength(45)]
    [Unicode(false)]
    public string MoviesName { get; set; } = null!;

    [Key]
    [Column("cinemas_id")]
    public int CinemasId { get; set; }

    [Key]
    [Column("customers_id")]
    public int CustomersId { get; set; }

    [Column("number_of_seats")]
    [Display(Name = "Number of seats")]
    public int NumberOfSeats { get; set; }

    [ForeignKey("CustomersId")]
    [InverseProperty("Reservations")]
    public virtual Customer? Customers { get; set; }

    [ForeignKey("MoviesId, CinemasId, MoviesName")]
    [InverseProperty("Reservations")]
    public virtual Provole? Provole { get; set; }

}
