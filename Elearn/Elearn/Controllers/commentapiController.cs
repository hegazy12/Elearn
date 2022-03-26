using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Elearn.Models;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class commentapiController : ApiController
    {
        
        valdation valdation = new valdation();
        DbCon DbCon = new DbCon();
        public List<c> Get(int id)
        {
            comms comms = new comms(id);
            return comms.cmmnts;
        }

        // POST: api/commentapi
        public string Post()
        {
            int idstu= Convert.ToInt16(HttpContext.Current.Request.Form["idstu"]);
            if (valdation.valda(idstu) == false)
            {
                return "goback";
            }

            int idpost = Convert.ToInt16(HttpContext.Current.Request.Form["idpost"]);
            string comment = HttpContext.Current.Request.Form["comment"];
            comment c = new comment();
            c.comment1 = comment;
            c.idpost = idpost;
            c.idstu = idstu;
            DbCon.Con.comments.Add(c);
            DbCon.Con.SaveChanges();
            return "good jop";
        }

        // PUT: api/commentapi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/commentapi/5
        public void Delete(int id)
        {
        }
    }
    public class c
    {
        private DbCon DbCon = new DbCon();
        public string comment;
        public int idstu;
        comment comm;
        public c(int idcoment)
        {
            comm = DbCon.Con.comments.Find(idcoment);
            idstu = Convert.ToInt16(comm.idstu);
            comment = comm.comment1;
        }
    }

    public class comms{
        private DbCon DbCon = new DbCon();
        private List<comment> comments;
        public List<c> cmmnts = new List<c>();
        c c;
        public comms(int idpost)
        {
            comments =DbCon.Con.comments.Where(m => m.idpost == idpost).ToList();
            foreach (comment item in comments){
                c= new c(item.id);
                cmmnts.Add(c);
            }
        }
    }

}
