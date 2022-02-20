using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" },
                new Category { CategoryId = 4, Name = "Other" }


            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Charles",
                    LastName = "Everhart",
                    PhoneNumber = "757-111-1777",
                    Email = "ceverhart21@gmail.com",
                    CategoryId = 1,
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Jebodiah",
                    LastName = "Doggersworth",
                    PhoneNumber = "757-499-2333",
                    Email = "jdoggersworth@yahoo.com",
                    CategoryId = 1,
                },
                new Contact
                {
                    ContactId = 3,
                    FirstName = "Bearemy",
                    LastName = "Nachingham",
                    PhoneNumber = "757-333-3214",
                    Email = "bnachingham@aol.com",
                    CategoryId = 2,
                }
            );










        }
    }
}
