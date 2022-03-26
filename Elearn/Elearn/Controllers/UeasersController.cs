using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Elearn.Models;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class UeasersController : Controller
    {
        DbCon dbCon = new DbCon();
        valdation valdation = new valdation();
        // GET: Ueasers
        public ActionResult Index()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(dbCon.Con.Ueasers.ToList());
        }

        // GET: Ueasers/Details/5
        public ActionResult Details(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ueaser ueaser = dbCon.Con.Ueasers.Find(id);
            if (ueaser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ueaser);
        }

        // GET: Ueasers/Create
        public ActionResult Create()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Ueasers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Fname,Lname,phone,role,UserName,Pass,actv,sex")] Ueaser ueaser)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                dbCon.Con.Ueasers.Add(ueaser);
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ueaser);
        }

        // GET: Ueasers/Edit/5
        public ActionResult Edit(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ueaser ueaser = dbCon.Con.Ueasers.Find(id);
            if (ueaser == null)
            {
                return HttpNotFound();
            }
            return View(ueaser);
        }

        // POST: Ueasers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Fname,Lname,phone,role,UserName,Pass,actv,sex")] Ueaser ueaser)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                dbCon.Con.Entry(ueaser).State = EntityState.Modified;
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ueaser);
        }

        // GET: Ueasers/Delete/5
        public ActionResult Delete(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ueaser ueaser = dbCon.Con.Ueasers.Find(id);
            if (ueaser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ueaser);
        }

        // POST: Ueasers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            Ueaser ueaser = dbCon.Con.Ueasers.Find(id);
            dbCon.Con.Ueasers.Remove(ueaser);
            dbCon.Con.markexames.RemoveRange(dbCon.Con.markexames.Where(m => m.idstu == id));
            dbCon.Con.UserInGroups.RemoveRange(dbCon.Con.UserInGroups.Where(m=>m.iduser==id));
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
