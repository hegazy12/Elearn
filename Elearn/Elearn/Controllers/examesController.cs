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
    public class examesController : Controller
    {
        private DbCon dbCon = new DbCon();
        valdation valdation =new valdation();
        // GET: exames
        public ActionResult Index()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(dbCon.Con.exames.ToList());
        }

        // GET: exames/Details/5
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
            exame exame = dbCon.Con.exames.Find(id);
            if (exame == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(exame);
        }

        // GET: exames/Create
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

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,exame exame)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        exame.photo = "/UploadedFiles/" + _FileName;
                        exame.id_emp = Convert.ToInt32(Session["id"]);
                        file.SaveAs(_path);

                        if (ModelState.IsValid)
                        {
                            dbCon.Con.exames.Add(exame);
                            dbCon.Con.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View(exame);
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View(exame);
            }
            return View(exame);
        }
        // GET: exames/Edit/5
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
                return RedirectToAction("Index", "Home");
            }
            exame exame = dbCon.Con.exames.Find(id);
            if (exame == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(exame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file,exame exame)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        exame.photo = "/UploadedFiles/" + _FileName;
                        exame.id_emp = Convert.ToInt32(Session["id"]);
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
                dbCon.Con.Entry(exame).State = EntityState.Modified;
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
                }
            return View(exame);
        }

        // GET: exames/Delete/5
        public ActionResult Delete(int? id)
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
            exame exame = dbCon.Con.exames.Find(id);
            if (exame == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(exame);
        }

        // POST: exames/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),id,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            exame exame = dbCon.Con.exames.Find(id);
            dbCon.Con.exames.Remove(exame);
            dbCon.Con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
