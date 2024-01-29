using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MyCinemaApp.Models;

namespace MyCinemaApp.Models;

[Table("CONTENT_ADMINS")]
public partial class ContentAdmin
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("user_username")]
    [StringLength(32)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [InverseProperty("ContentAdmin")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [InverseProperty("ContentAdmin")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();

    [ForeignKey("Username")]
    [InverseProperty("ContentAdmins")]
    [Display(Name = "Username")]
    public virtual User? UsernameNavigation { get; set; }
}
