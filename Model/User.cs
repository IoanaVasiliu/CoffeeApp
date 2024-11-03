using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LibCafeApp.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }  // Primary Key

        [Required]
        [StringLength(255)]      
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }  // Securely hashed password
     
        public string RoleName { get; set; }  // Foreign Key to Roles

        [StringLength(255)]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Timestamp for when the user was created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // Timestamp for last update

   

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual ICollection<BookReservation> BookReservations { get; set; } = new List<BookReservation>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();


    }
}
