using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearn.dbCon;
using Elearn.Users;

namespace Elearn.Controllers
{
    public class mistakController : Controller
    {
        DbCon db = new DbCon();
        valdation valdation = new valdation();
        // GET: mistak
        public ActionResult Index(int? id,int? exame)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valdaemp(Convert.ToString(Session["Role"]), Convert.ToString(Session["UserName"]), iduser, token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.Con.mistaks.Where(m => m.idstu == id && m.idexam == exame).ToList());
        }
    }
}