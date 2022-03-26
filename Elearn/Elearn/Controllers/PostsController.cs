using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearn.Exama;
using Elearn.Models;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class PostsController : Controller
    {
        valdation valdation = new valdation();
        DbCon dbCon = new DbCon();

        [HttpGet]
        public ActionResult Creatpost(int? id, int? exame)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valda(Convert.ToString(Session["Role"]),Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index","Home");
            }
            if (id==0)
            {
                return RedirectToAction("Index", "Discussion", new {id=exame});
            }

            
            qu qu = new qu();
            qu = dbCon.Con.qus.Find(Convert.ToInt32(id));
            ViewBag.qu = qu;
            ViewBag.exame = Convert.ToInt32(exame);
            return View();
        }


        [HttpPost]
        public ActionResult Creatpost(int? id, int? exame,string Post)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valda(Convert.ToString(Session["Role"]),Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            post ps = new post();
            ps.idqu = Convert.ToInt32(id);
            ps.idstu = Convert.ToInt32(Session["id"]);
            
            ps.idexame = Convert.ToInt32(exame);
            ps.post1 = Post;
            dbCon.Con.posts.Add(ps);
            dbCon.Con.SaveChanges();
            qu qu = new qu();
            qu = dbCon.Con.qus.Find(Convert.ToInt32(id));
            ViewBag.qu = qu;
            ViewBag.exame = Convert.ToInt32(exame);
            return RedirectToAction("Index", "Discussion", new{id=exame} );
        }
    }
}