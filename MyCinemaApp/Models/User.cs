using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

[Table("USERS")]
public partial class User
{
    [Key]
    [Column("username")]
    [StringLength(32)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(32)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [InverseProperty("UserUsernameNavigation")]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [InverseProperty("UsernameNavigation")]
    public virtual ICollection<ContentAdmin> ContentAdmins { get; set; } = new List<ContentAdmin>();

    [InverseProperty("UserUsernameNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
