using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models;
using CapstoneProject.Models.Repository;
using MongoDB.Driver;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {

        private ContactCollectionWorkSpace _contacts = new ContactCollectionWorkSpace();
        public ActionResult Index()
        {
            return View(_contacts.SelectAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateWP()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateWP(WorkSpace contact)
        {
            this._contacts.InsertContact(contact);
            return RedirectToAction("Index", _contacts.SelectAll());
        }

        public ActionResult Edit(string contactId)
        {
            return View(_contacts.Get(contactId));
        }

        [HttpPost]
        public ActionResult Edit(string _id, WorkSpace contact)
        {
            this._contacts.UpdateContact(_id, contact);

            return RedirectToAction("Index", _contacts.SelectAll());
        }
    }
}