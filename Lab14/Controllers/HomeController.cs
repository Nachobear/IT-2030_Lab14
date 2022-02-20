using Lab14.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Controllers
{
    public class HomeController : Controller
    {
        
        
        
        
        private IRepository<Contact> data { get; set; }
        public HomeController(IRepository<Contact> rep)
        {
            this.data = rep;
        }
        
        /*
        //UnitOfWork way of connecting to database (names it data)
        private Repository<Contact> data { get; set; }

        public HomeController(ContactContext ctx)
        {
            this.data = new Repository<Contact>(ctx);
        }
        */

        
        
        
        public IActionResult Index()
        {
            //UnitOfWork way of querying database (replaces the code below it)
            //(first makes an options then uses List method of Repository)
            
            var options = new QueryOptions<Contact>
            {
                Includes = "Category",
                OrderBy = c => c.FirstName
            };
            var contacts = data.List(options);
            return View(contacts);
            
            
            /*
            var contacts = context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.FirstName)
                .ToList();
            return View(contacts);
            */
        }
    }
}
