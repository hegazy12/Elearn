using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elearn.Models;
using Elearn.dbCon;

namespace Elearn.useringroup
{
    public class us_in_gr
    {
        public List<Ueaser> ueasers = new List<Ueaser>();
        DbCon dbCon = new DbCon();
        public List<Ueaser> us_in_group(int id)
        {
            ueasers.Clear();
            List<UserInGroup> u =dbCon.Con.UserInGroups.Where(m => m.idgroup == id).ToList();
            foreach (UserInGroup x in u)
            {
                ueasers.Add(x.Ueaser);
            }
            return ueasers;
        }
        public List<Ueaser> us_out_group(int id)
        {
            List<UserInGroup> UserInGroups = dbCon.Con.UserInGroups.Where(m => m.idgroup == id).ToList();
            ueasers = dbCon.Con.Ueasers.ToList();
            foreach (UserInGroup userInGroup in UserInGroups)
            {
                ueasers.Remove(dbCon.Con.Ueasers.Find(userInGroup.iduser));
            }
            return ueasers;
        }
    }
}