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
        public ActionResult Index()
        {
            return View();   
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

        //public ActionResult CreateWorkSpace()
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        string user = User.Identity.GetUserName();
        //        Session["UserName"] = user;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        //[HttpPost]
        //public ActionResult CreateWorkSpace(WorkSpace ws, HttpPostedFileBase ImageWS)
        //{
        //    if (ImageWS != null)
        //    {
        //        string avatar = "";
        //        if (ImageWS.ContentLength > 0)
        //        {
        //            var filename = Path.GetFileName(ImageWS.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Images/"), filename);
        //            ImageWS.SaveAs(path);
        //            avatar = filename;
        //        }
        //        ws.ImageWS = avatar;
        //    }

        //    ws.Createdate = DateTime.Now;
        //    WS_User_Roles wsp = new WS_User_Roles();
        //    string userid = User.Identity.GetUserId();
        //    Session["UserId"] = userid;
        //    wsp.User_ID = userid;
        //    wsp.Role_Admin = true;
        //    wsp.Role_Member = true;
        //    db.WS_User_Roles.Add(wsp);
        //    db.WorkSpaces.Add(ws);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");


        //}

        //public ActionResult DeleteWorkSpace(WorkSpace ws)
        //{
        //    WorkSpace worksp = db.WorkSpaces.Find(ws.ID);
        //    var iduser = db.WS_User_Roles.Where(x => x.WorkSpace_ID == ws.ID);
        //    db.WS_User_Roles.RemoveRange(iduser);
        //    db.WorkSpaces.Remove(worksp);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public ActionResult ShowWorkSpace(int? id)
        //{
        //    WorkSpace showws = db.WorkSpaces.Find(id);
        //    if (showws == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(showws);
        //}

        //public ActionResult AddMemberWS(int? id)
        //{
        //    WorkSpace addmem = db.WorkSpaces.Find(id);
        //    ViewBag.listuser = db.AspNetUsers.OrderByDescending(x => x.Id).ToList();
        //    return View(addmem);
        //}

        //[HttpPost]
        //public ActionResult AddMemberWS(WorkSpace model, List<string> adduser)
        //{
        //    WorkSpace wp = db.WorkSpaces.Find(model.ID);
        //    //var idwp = db.WS_User_Roles.Where(x => x.WorkSpace_ID == model.ID);
        //    //db.WS_User_Roles.RemoveRange(idwp);
        //    foreach (var user in adduser)
        //    {
        //        WS_User_Roles wsuser = new WS_User_Roles();
        //        var id = db.AspNetUsers.SingleOrDefault(x => x.Id == user);
        //        var rs = db.WS_User_Roles.ToList();
        //        foreach (var item in rs)
        //        {
        //            if (item.User_ID.Equals(id) == true)
        //            {

        //            }
        //            else
        //            {
        //                wsuser.User_ID = db.AspNetUsers.SingleOrDefault(x => x.Email == user).Id;

        //                wsuser.WorkSpace_ID = wp.ID;
        //                wsuser.Role_Member = true;
        //                db.WS_User_Roles.Add(wsuser);
        //                db.SaveChanges();
        //            }
        //        }

        //    }

        //    return RedirectToAction("AddMemberWS", new { id = model.ID });
        //}

        //public ActionResult SettingWorkSpace(int? id)
        //{
        //    WorkSpace editws = db.WorkSpaces.Find(id);
        //    return View(editws);
        //}

        //[HttpPost]
        //public ActionResult SettingWorkSpace(WorkSpace model)
        //{
        //    WorkSpace ws = db.WorkSpaces.Find(model.ID);
        //    ws.WorkSpaceName = model.WorkSpaceName;
        //    ws.Description = model.Description;
        //    db.SaveChanges();
        //    return RedirectToAction("ShowWorkSpace", new { id = model.ID });
        //}

        //public ActionResult EditRoleMemberWS(int? id)
        //{
        //    WS_User_Roles ws = db.WS_User_Roles.Find(id);
        //    return View(ws);
        //}

        //[HttpPost]
        //public ActionResult EditRoleMemberWS(WS_User_Roles model, List<string> chk1)
        //{
        //    WS_User_Roles wsp = db.WS_User_Roles.Find(model.ID);
        //    wsp.Role_Admin = model.Role_Admin;
        //    wsp.Role_Manager = model.Role_Manager;
        //    wsp.Role_Member = model.Role_Member;

        //    db.Entry(wsp).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("AddMemberWS", new { id = wsp.WorkSpace_ID });
        //}

    }
}