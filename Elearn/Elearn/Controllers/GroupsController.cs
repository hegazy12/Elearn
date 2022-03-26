using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Elearn.Models;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class GroupsController : Controller
    {
        private DbCon dbCon = new DbCon();
        valdation valdation = new valdation();
        // GET: Groups
        public ActionResult Index()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(dbCon.Con.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Group group = dbCon.Con.Groups.Find(id);
            if (group == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Group group)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (file != null){
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);

                        if (ModelState.IsValid)
                        {
                            group.photo = "/UploadedFiles/" + _FileName;
                            group.idemp = Convert.ToInt32(Session["id"]);


                            dbCon.Con.Groups.Add(group);
                            dbCon.Con.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    } 
                }

            }catch
            {
                ViewBag.Message = "File upload failed!!";
                return View(group);
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = dbCon.Con.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file,Group group)
        {
            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        group.photo = "/UploadedFiles/" + _FileName;
                        group.idemp = Convert.ToInt32(Session["id"]);
                        file.SaveAs(_path);
                    }
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                dbCon.Con.Entry(group).State = EntityState.Modified;
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = dbCon.Con.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = dbCon.Con.Groups.Find(id);
            dbCon.Con.Groups.Remove(group);
            dbCon.Con.SaveChanges();
            List<UserInGroup> userInGroups = dbCon.Con.UserInGroups.Where(m => m.idgroup == id).ToList();
            foreach (UserInGroup user in userInGroups)
            {
                dbCon.Con.UserInGroups.Remove(user);
                dbCon.Con.SaveChanges();
            }
            dbCon.Con.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbCon.Con.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
