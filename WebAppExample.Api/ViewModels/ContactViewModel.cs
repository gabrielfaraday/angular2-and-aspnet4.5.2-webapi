using System;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Api.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public static class ContactExtensions
    {
        public static Contact ToContactModel(this ContactViewModel c)
        {
            var contact = new Contact
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone
            };

            if (!string.IsNullOrWhiteSpace(c.BirthDate))
                contact.BirthDate = DateTime.Parse(c.BirthDate);

            return contact;
        }

        public static ContactViewModel ToContactViewModel(this Contact c)
        {
            var contactViewModel = new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
            };

            if (c.BirthDate.HasValue)
                contactViewModel.BirthDate = c.BirthDate.Value.ToShortDateString();

            return contactViewModel;
        }

    }
}