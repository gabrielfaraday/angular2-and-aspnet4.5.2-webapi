using System;

namespace WebAppExample.Lib.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
