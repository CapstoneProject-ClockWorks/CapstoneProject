using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CapstoneProject.Controllers
{
    public class HomeController : BaseController
    {
        CapstoneProjectModelEntities db = new CapstoneProjectModelEntities();
        public ActionResult Index()
        {
            var viewlistworkspace = db.WorkSpaces.OrderByDescending(m => m.ID).ToList();
            return View(viewlistworkspace);
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

        public ActionResult CreateWorkSpace()
        {
            if (Request.IsAuthenticated)
            {
                string user = User.Identity.GetUserName();
                Session["UserName"] = user;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult CreateWorkSpace(WorkSpace ws)
        {
			if (!ws.Equals(null))
			{
				ws.Createdate = DateTime.Now;
				//ws.Bilimail = Session["UserName"].ToString();
				db.WorkSpaces.Add(ws);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
				return View();
        }

        public ActionResult DeleteWorkSpace(WorkSpace ws)
        {
            WorkSpace worksp = db.WorkSpaces.Find(ws.ID);
            db.WorkSpaces.Remove(worksp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowWorkSpace(int? id)
        {
            WorkSpace showws = db.WorkSpaces.Find(id);
            if (showws == null)
            {
                return HttpNotFound();
            }
            return View(showws);
        }

        public ActionResult AddMemberWS(int? id)
        {
            WorkSpace addmem = db.WorkSpaces.Find(id);
            return View(addmem);
        }

        [HttpPost]
        public ActionResult AddMemberWS(WorkSpace model,List<string> adduser)
        {
            WorkSpace wp = db.WorkSpaces.Find(model.ID);
            foreach (var user in adduser)
            {
                WS_User_Roles wsuser = new WS_User_Roles();
                wsuser.User_ID = db.AspNetUsers.SingleOrDefault(x => x.Email == user).Id;
                wsuser.WorkSpace_ID = wp.ID;
                wsuser.Role_ID = 3;//3 là id của RoleWorkSpace
                db.WS_User_Roles.Add(wsuser);
            }
            db.SaveChanges();
            return RedirectToAction("ShowWorkSpace", new { id = model.ID });
        }

        public ActionResult SettingWorkSpace(int? id)
        {
            WorkSpace editws = db.WorkSpaces.Find(id);
            return View(editws);
        }

        [HttpPost]
        public ActionResult SettingWorkSpace(WorkSpace model)
        {
            WorkSpace ws = db.WorkSpaces.Find(model.ID);
            ws.WorkSpaceName = model.WorkSpaceName;
            ws.Description = model.Description;
            db.SaveChanges();
            return RedirectToAction("ShowWorkSpace");
        }

        public ActionResult EditRoleMemberWS()
        {
            return View();
        }


    }
}