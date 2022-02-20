using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab14.Models;

namespace Lab14.Controllers
{
    public class ContactController : Controller
    {

        //connects to database and names it data
        
        private IUnitOfWork data { get; set; }
        public ContactController(IUnitOfWork rep)
        {
            this.data = rep;
        }
        
        /*
        private UnitOfWork data { get; set; }

        public ContactController(ContactContext ctx)
        {
            this.data = new UnitOfWork(ctx);
        }
        */
        


        
        public ViewResult Details (int id)
        {
            //UnitOfWork way of querying database (replaces the code below it)
            
            var options = new QueryOptions<Contact>
            {
                Includes = "Category",
                Where = c => c.ContactId == id
            };
            var contact = data.Contacts.Get(options);

            return View(contact);
            
            
            
            /*
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);

            return View(contact);
            */
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            
            //UnitOfWork way of accessing database (replaces the code below it)
            //(just for getting list of categories)
            var options = new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Categories = data.Categories.List(options);

            /*
            ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();
            */
            
            return View("Edit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            var categoryOptions = new QueryOptions<Category>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Categories = data.Categories.List(categoryOptions);
            /*ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();*/
            var options = new QueryOptions<Contact>
            {
                Includes = "Category",
                Where = c => c.ContactId == id
            };
            var contact = data.Contacts.Get(options);
            /*
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);
            */

            return View("Edit", contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            string action = (contact.ContactId == 0) ? "Add" : "Edit";

            if (ModelState.IsValid)
            {
                if(action == "Add")
                {
                    contact.DateAdded = DateTime.Now;
                    data.Contacts.Insert(contact);
                    //context.Contacts.Add(contact);
                }
                else //update
                {
                    data.Contacts.Update(contact);
                    //context.Contacts.Update(contact);
                }

                data.Save();
                //context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = action;
                var categoryOptions = new QueryOptions<Category>
                {
                    OrderBy = c => c.Name
                };
                ViewBag.Categories = data.Categories.List(categoryOptions);
                //ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();
                return View(contact);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var options = new QueryOptions<Contact>
            {
                Includes = "Category",
                Where = c => c.ContactId == id
            };
            var contact = data.Contacts.Get(options);
            /*
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);
            */

            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            data.Contacts.Delete(contact);
            //context.Contacts.Remove(contact);
            data.Save();
            //context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
