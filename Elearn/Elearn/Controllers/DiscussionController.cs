﻿using Elearn.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearn.Models;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class DiscussionController : Controller
    {
        valdation valdation = new valdation();
        DbCon dbCon = new DbCon();

        // GET: Discussion
        public ActionResult Index(int? id)
        {

            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valda(Convert.ToString(Session["Role"]),Convert.ToString(Session["UserName"]),iduser,token)==false){
                return RedirectToAction("Index","Home");
            }

            int idexam =Convert.ToInt16(id);
            int idstu = Convert.ToInt16(Session["id"]);
            if (valdation.isinexame(idstu, idexam) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<post> Posts = dbCon.Con.posts.Where(m => m.idexame == id).ToList();
            List<post_qu> post_qus = new List<post_qu>();
            post_qu pq;

            foreach (post item in Posts)
            {
                pq = new post_qu(item);
                post_qus.Add(pq);
            }
            ViewBag.post_qus = post_qus;
            return View();
        }
    }

    public class post_qu
    {
        public post post = new post();
        public qu qu = new qu();
        public Ueaser Ueaser = new Ueaser();
        public List<comment> comm = new List<comment>();
        DbCon dbCon = new DbCon();
        public post_qu(post ps)
        {
            post = ps;
            qu = dbCon.Con.qus.Find(ps.idqu);
            Ueaser = dbCon.Con.Ueasers.Find(ps.idstu);
            comm = ps.comments.ToList();
        }
    }
    
}