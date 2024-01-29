using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[PrimaryKey("ProvolesMoviesId", "ProvolesCinemasId", "CustomersId")]
[Table("RESERVATIONS")]
[Index("CustomersId", Name = "IX_RESERVATIONS_customers_id")]
[Index("ProvolesMoviesId", "ProvolesCinemasId", "ProvolesMoviesName", Name = "IX_RESERVATIONS_provoles_movies_id_provoles_cinemas_id_provoles_movies_name")]
public partial class Reservation
{
    [Key]
    [Column("provoles_movies_id")]
    public int ProvolesMoviesId { get; set; }

    [Column("provoles_movies_name")]
    [StringLength(45)]
    [Unicode(false)]
    public string ProvolesMoviesName { get; set; } = null!;

    [Key]
    [Column("provoles_cinemas_id")]
    public int ProvolesCinemasId { get; set; }

    [Key]
    [Column("customers_id")]
    public int CustomersId { get; set; }

    [Column("number_of_seats")]
    public int NumberOfSeats { get; set; }

    [ForeignKey("CustomersId")]
    [InverseProperty("Reservations")]
    public virtual Customer Customers { get; set; } = null!;

    [ForeignKey("ProvolesMoviesId, ProvolesCinemasId, ProvolesMoviesName")]
    [InverseProperty("Reservations")]
    public virtual Provole Provole { get; set; } = null!;
}
