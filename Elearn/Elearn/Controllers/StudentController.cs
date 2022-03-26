using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearn.Models;
using Elearn.ShortOrder;
using Elearn.Exama;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class StudentController : Controller
    {
        DbCon dbCon = new DbCon();
        Examago examago = new Examago();
        valdation valdation = new valdation();
        // GET: Student
        
        public ActionResult myExamas(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valda(Convert.ToString(Session["Role"]),Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            int idexam = Convert.ToInt16(id);
            int idstu = Convert.ToInt16(Session["id"]);
            if (valdation.ingroup(idstu, idexam) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<exame> exame = dbCon.Con.exames.Where(m =>m.idgroup ==id).ToList();
            ViewBag.exame = exame;
            return View();
        }
        
        public ActionResult Exama(int? id)
        {
            string token = Convert.ToString(Session["Token"]);
            int iduser = Convert.ToInt32(Session["id"]);
            if (valdation.valda(Convert.ToString(Session["Role"]),Convert.ToString(Session["UserName"]),iduser,token) == false)
            {
                return RedirectToAction("Index", "Home");
            }
            int idexam =Convert.ToInt16(id);
            int idstu =Convert.ToInt16(Session["id"]);
            if(valdation.isinexame(idstu,idexam) == false)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.qus = dbCon.Con.qus.Where(m => m.idexame == id).ToList();

            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Exama(int?id,FormCollection form)
        {
            corexame corexame = new corexame();
            List<qu> qus = examago.GetQus(Convert.ToInt16(id));
            int x = corexame.ExamCorrection(Convert.ToInt16(id),form);
            List<mastikqu> mastikqus = corexame.GetQusMastik(Convert.ToInt16(id),form);

            //التأكد من اعدم وجود الدرجه قبل ذالك في الداتا بيز
            if (examago.is_mark_in_table(Convert.ToInt32(Session["id"]),Convert.ToInt16(id))==0){
                examago.putmark(x,Convert.ToInt32(Session["id"]),Convert.ToInt16(id));
                examago.putmastkqu(mastikqus, Convert.ToInt16(id),Convert.ToInt32(Session["id"]));
            }

            ViewBag.qus = qus;
            ViewBag.x = x;
            ViewBag.y = examago.is_mark_in_table(Convert.ToInt32(Session["id"]),Convert.ToInt16(id));
            int? idgroup=dbCon.Con.exames.Find(id).idgroup;
            return RedirectToAction("myExamas", "Student", new {id=idgroup});
        }
    }
}