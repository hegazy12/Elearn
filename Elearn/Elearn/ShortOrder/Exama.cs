using Elearn.Models;
using System.Collections.Generic;
using System.Linq;
using Elearn.dbCon;
using Elearn.Exama;

namespace Elearn.ShortOrder
{
    public class Examago
    {
        DbCon dbCon = new DbCon(); 
        public List<qu> GetQus(int id)
        {
            return dbCon.Con.qus.Where(m => m.idexame == id).ToList();
        }

        public int putmark(int mark ,int idstu, int idexam){

            markexame mrk = new markexame();
            mrk.idstu = idstu;
            mrk.idexame = idexam;
            mrk.fullmark = GetQus(idexam).Count;
            mrk.mark =mark;
            try
            {
                dbCon.Con.markexames.Add(mrk);
                dbCon.Con.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        
        public int is_mark_in_table(int idstu,int idexame)
        {
            markexame mark =dbCon.Con.markexames.Where(m => m.idstu == idstu && m.idexame == idexame).FirstOrDefault();
            if(mark == null){
                return 0;
            }
            return 1;
        }
        public int putmastkqu(List<mastikqu> mastikqus,int idexam ,int idstu)
        {
            mistak mistak;
            foreach (mastikqu mq in mastikqus)
            {
                try
                {
                    mistak = new mistak();
                    mistak.idexam = idexam;
                    mistak.idqu = mq.GetQu().id;
                    mistak.idstu = idstu;
                    mistak.Nummistak = mq.Getmastik();
                    mistak.CorectAn = mq.GetQu().QreAn;
                    dbCon.Con.mistaks.Add(mistak);
                    dbCon.Con.SaveChanges();
                }
                catch
                {
                    return 0;
                }
            }
            return 1;
        }
        

    }
}