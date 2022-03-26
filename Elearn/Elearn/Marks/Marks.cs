using System;
using System.Collections.Generic;
using System.Linq;
using Elearn.Models;
using Elearn.dbCon;

namespace Elearn.Marks
{
    public class Marks
    {
        DbCon dbCon = new DbCon();
        public markexame m = new markexame();
        public Ueaser u = new Ueaser();
        public Marks(int idexam,int idstu)
        {
            m = dbCon.Con.markexames.Where(m => m.idexame == idexam && m.idstu == idstu).FirstOrDefault();
            u = dbCon.Con.Ueasers.Find(idstu);
        }
        
    }
    public class Markinexam
    {
        DbCon dbCon = new DbCon();
        public List<Marks> marks = new List<Marks>();
        public List<Ueaser> ueasers = new List<Ueaser>();
        Marks M;

        public Markinexam(int idexam){
            List<markexame> markexames = dbCon.Con.markexames.Where(m => m.idexame == idexam).ToList();
            foreach (markexame item in markexames)
            {
                M = new Marks(Convert.ToInt16(item.idexame),Convert.ToInt16(item.idstu));
                marks.Add(M);
            }
        }
        public List<Ueaser> NotSolve(int idexam)
        {
            exame exame = dbCon.Con.exames.Find(idexam);
            List<UserInGroup> userInGroup = dbCon.Con.UserInGroups.Where(m => m.idgroup == exame.idgroup).ToList();
            markexame markexame;
            foreach (UserInGroup item in userInGroup){
                markexame = new markexame();
                markexame = dbCon.Con.markexames.Where(m => m.idstu == item.iduser).FirstOrDefault();
                if (markexame==null){
                    ueasers.Add(item.Ueaser);
                }
            }
            return ueasers;
        }
    }
}