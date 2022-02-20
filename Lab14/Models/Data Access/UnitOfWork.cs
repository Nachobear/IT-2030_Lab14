using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab14.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private ContactContext context { get; set; }
        public UnitOfWork(ContactContext ctx) => context = ctx;

        private IRepository<Contact> contacts;
        public IRepository<Contact> Contacts
        {
            get
            {
                if (contacts == null) contacts = new Repository<Contact>(context);
                return contacts;
            }
        }

        private IRepository<Category> categories;
        public IRepository<Category> Categories
        {
            get
            {
                if (categories == null) categories = new Repository<Category>(context);
                return categories;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
