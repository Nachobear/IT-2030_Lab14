using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab14.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; }

        public string Organization { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1, 100000000, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Slug => this.FirstName?.Replace(' ', '-').ToLower()
            + '-' + this.LastName?.Replace(' ', '-').ToLower();
    }
}
