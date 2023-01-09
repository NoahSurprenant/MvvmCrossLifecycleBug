using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;

namespace MvvmCrossLifecycleBug.Core.Services
{
    public class ContactService : IContactService
    {
        private List<Contact> contacts;
        public ContactService()
        {
            contacts = new List<Contact>();
        }


        public IEnumerable<Contact> GetContacts()
        {
            if (contacts.Any())
            {
                return contacts;
            }

            for (var i = 0; i < 50; i++)
            {
                contacts.Add(new Contact()
                {
                    FirstName = { Value = Faker.Name.First() },
                    LastName = { Value = Faker.Name.Last() },
                    Phone = { Value = Faker.Phone.Number() },
                    Email = { Value = Faker.Internet.Email() },
                    City = { Value = Faker.Address.City() },
                    Country = { Value = Faker.Address.Country() },
                });
            }

            return contacts;
        }
    }
}
