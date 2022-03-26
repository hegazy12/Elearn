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
    public class qusController : Controller
    {
        private DbCon dbCon = new DbCon();
        private valdation valdation = new valdation();
        // GET: qus
        public ActionResult Index()
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token)==false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(dbCon.Con.qus.ToList());
        }

        // GET: qus/Details/5
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
            qu qu = dbCon.Con.qus.Find(id);
            if (qu == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(qu);
        }

        // GET: qus/Create
        public ActionResult Create(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: qus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int? id,qu qu)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                qu.idexame = Convert.ToInt32(id);
                qu.id_emp =Convert.ToInt32(Session["id"]);
                dbCon.Con.qus.Add(qu);
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qu);
        }

        // GET: qus/Edit/5
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
            qu qu = dbCon.Con.qus.Find(id);
            if (qu == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(qu);
        }

        // POST: qus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(qu qu)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                dbCon.Con.Entry(qu).State = EntityState.Modified;
                dbCon.Con.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qu);
        }

        // GET: qus/Delete/5
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
            qu qu = dbCon.Con.qus.Find(id);
            if (qu == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(qu);
        }

        // POST: qus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            qu qu = dbCon.Con.qus.Find(id);
            dbCon.Con.qus.Remove(qu);
            dbCon.Con.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
