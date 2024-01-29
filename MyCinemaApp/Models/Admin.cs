using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[Table("ADMINS")]
[Index("UserUsername", Name = "IX_ADMINS_user_username")]
public partial class Admin
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
    public string UserUsername { get; set; } = null!;

    [ForeignKey("UserUsername")]
    [InverseProperty("Admins")]
    public virtual User UserUsernameNavigation { get; set; } = null!;
}
