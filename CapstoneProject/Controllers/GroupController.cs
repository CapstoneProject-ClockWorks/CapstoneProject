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
    [Authorize]
    public class GroupController : BaseController
    {
        PMSEntities db = new PMSEntities();
        // GET: Group
        public ActionResult Index()
        {
            var listgroup = db.WorkSpaces.OrderByDescending(m => m.ID).ToList();
            ViewData["ListGroup"] = listgroup;
            return View();
        }
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(WorkSpace ws, HttpPostedFileBase ImageWS)
        {

            string userid = Session["UserId"].ToString();
            if (ImageWS != null)
            {
                string avatar = "";
                if (ImageWS.ContentLength > 0)
                {
                    var filename = Path.GetFileName(ImageWS.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/workspace/"), filename);
                    ImageWS.SaveAs(path);
                    avatar = filename;
                }
                ws.ImageWS = avatar;
            }
            else
            {
                ws.ImageWS = "default.png";
            }
            ws.Createdate = DateTime.Now;
            ws.User_ID = userid;
            WS_User_Roles wsp = new WS_User_Roles();
            wsp.User_ID = userid;
            wsp.Role_Admin = true;
            wsp.Role_Member = true;
            db.WS_User_Roles.Add(wsp);
            db.WorkSpaces.Add(ws);
            db.SaveChanges();
            return RedirectToAction("InviteMember", "Group", new { id = ws.ID });
        }
        public ActionResult InviteMember(int? id)
        {
            string userid = Session["UserId"].ToString();
            WorkSpace addmem = db.WorkSpaces.Find(id);
            return View(addmem);
        }
        [HttpPost]
        public ActionResult InviteMember(WorkSpace model, List<string> adduser)
        {
            WorkSpace wp = db.WorkSpaces.Find(model.ID);
            foreach (var user in adduser)
            {
                WS_User_Roles wsuser = new WS_User_Roles();
                wsuser.User_ID = db.AspNetUsers.SingleOrDefault(x => x.Email == user).Id;
                wsuser.WorkSpace_ID = wp.ID;
                wsuser.Role_Member = true;
                db.WS_User_Roles.Add(wsuser);
                db.SaveChanges();

            }
            return RedirectToAction("ShowGroup", new { id = model.ID });
        }
        public ActionResult DeleteGroup(WorkSpace ws)
        {
            WorkSpace worksp = db.WorkSpaces.Find(ws.ID);
            var iduser = db.WS_User_Roles.Where(x => x.WorkSpace_ID == ws.ID);
            db.WS_User_Roles.RemoveRange(iduser);
            db.WorkSpaces.Remove(worksp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMember(WS_User_Roles model)
        {
            WS_User_Roles wp = db.WS_User_Roles.Find(model.ID);
            db.WS_User_Roles.Remove(wp);
            db.SaveChanges();
            return RedirectToAction("AddMemberWS", new { id = wp.WorkSpace_ID });
        }
        public ActionResult MemberOutGroup(WS_User_Roles model)
        {
            WS_User_Roles wp = db.WS_User_Roles.Find(model.ID);
            db.WS_User_Roles.Remove(wp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowGroup(int? id)
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
            ViewBag.user = db.AspNetUsers.OrderByDescending(x => x.Id).ToList();
            return View(addmem);
        }

        [HttpPost]
        public ActionResult AddMemberWS(WorkSpace model, List<string> adduser)
        {
            WorkSpace wp = db.WorkSpaces.Find(model.ID);
            foreach (var user in adduser)
            {
                WS_User_Roles wsuser = new WS_User_Roles();
                wsuser.User_ID = db.AspNetUsers.SingleOrDefault(x => x.Email == user).Id;
                wsuser.WorkSpace_ID = wp.ID;
                wsuser.Role_Member = true;
                db.WS_User_Roles.Add(wsuser);
            }
            db.SaveChanges();
            return RedirectToAction("AddMemberWS", new { id = model.ID });
        }

        public ActionResult SettingGroup(int? id)
        {
            WorkSpace editws = db.WorkSpaces.Find(id);
            return View(editws);
        }

        [HttpPost]
        public ActionResult SettingGroup(WorkSpace model, HttpPostedFileBase ImageWS)
        {
            WorkSpace ws = db.WorkSpaces.Find(model.ID);
            if (ImageWS != null)
            {
                string avatar = "";
                if (ImageWS.ContentLength > 0)
                {
                    var filename = Path.GetFileName(ImageWS.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/workspace/"), filename);
                    ImageWS.SaveAs(path);
                    avatar = filename;
                }
                ws.ImageWS = avatar;
            }
            ws.Description = model.Description;
            ws.WorkSpaceName = model.WorkSpaceName;
            db.SaveChanges();
            return RedirectToAction("ShowGroup", new { id = model.ID });
        }

        public ActionResult EditRoleMemberWS(int? id)
        {
            WS_User_Roles ws = db.WS_User_Roles.Find(id);
            return View(ws);
        }

        [HttpPost]
        public ActionResult EditRoleMemberWS(WS_User_Roles model, List<string> chk1)
        {
            WS_User_Roles wsp = db.WS_User_Roles.Find(model.ID);
            wsp.Role_Admin = model.Role_Admin;
            wsp.Role_Manager = model.Role_Manager;
            wsp.Role_Member = model.Role_Member;

            db.Entry(wsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AddMemberWS", new { id = wsp.WorkSpace_ID });
        }


        public ActionResult CreateProcess(int? id)
        {
            Process ws = db.Processes.Find(id);
            return View();
        }

        [HttpPost]
        public ActionResult CreateProcess(WorkSpace model, Process proo)
        {
            WorkSpace ws = db.WorkSpaces.Find(model.ID);
            Process ps = new Process();
            ps.ProcessName = proo.ProcessName;
            ps.Description = proo.Description;
            ps.Group_ID = model.ID;
            db.Processes.Add(ps);

            db.SaveChanges();
            return RedirectToAction("DrawProcess", new { id = ps.ID });
        }

        public ActionResult DrawProcess(int? id)
        {
            Process ws = db.Processes.Find(id);
            return View(ws);
        }
        [HttpPost]
        public JsonResult DrawProcess(int processId ,string data/*, Array[] linkDataArray, Array[] nodeDataArray*/)
        {
            Process ws = db.Processes.Find(processId);
            ws.DataJson = data.ToString();
            db.Entry(ws).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("ShowGroup", new { id = ws.Group_ID });
            return Json(new { id = ws.Group_ID });
            //return Json(new { data = data , processId = processId, linkDataArray = linkDataArray, nodeDataArray = nodeDataArray });
        }

    }
}