using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibCafeApp.Model;

namespace LibCafeApp.Data
{
    public class LibCafeAppContext : DbContext
    {
        public LibCafeAppContext (DbContextOptions<LibCafeAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; } = default!;
        public DbSet<BookReservation> BookReservation { get; set; } = default!;
     
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;
        public DbSet<Payment> Payment { get; set; } = default!;
        public DbSet<LibCafeApp.Model.Book> Book { get; set; } = default!;
    }
}
