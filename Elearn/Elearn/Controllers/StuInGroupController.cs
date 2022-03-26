using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Elearn.Models;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class StuInGroupController : ApiController
    {
        DbCon dbCon = new DbCon();
        public string Get(){
            return "goood";
            }
        
        // POST: api/StuInGroup
        public string Post(int id)
        {
            UserInGroup userInGroup = new UserInGroup();
            int idstu =Convert.ToInt32(HttpContext.Current.Request.Form["idstu"]);

            var x = dbCon.Con.UserInGroups.Where(m => m.idgroup == id && m.iduser == idstu).FirstOrDefault();
            if (x != null)
            {
                return "he\\she is in";
            }

            userInGroup.idgroup = id;
            userInGroup.iduser =idstu;
            dbCon.Con.UserInGroups.Add(userInGroup);
            dbCon.Con.SaveChanges();
            return "good jop";
        }

        // DELETE: api/StuInGroup/5
        public string Delete(int id)
        {
            int idstu =Convert.ToInt32(HttpContext.Current.Request.Form["idstu"]);
            UserInGroup userInGroup = new UserInGroup();
            userInGroup = dbCon.Con.UserInGroups.Where(m => m.iduser == idstu && m.idgroup == id).FirstOrDefault();
            dbCon.Con.UserInGroups.Remove(userInGroup);
            dbCon.Con.SaveChanges();
            return "good jop";
        }
    }
}
