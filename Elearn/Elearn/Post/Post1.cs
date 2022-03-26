using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elearn.Models;
using Elearn.dbCon;
namespace Elearn.Post
{
    public class Postone
    {
        DbCon dbCon = new DbCon();
        post posts = new post();
        List<comment> comments = new List<comment>();
        public Postone(int idpost)
        {
            posts = dbCon.Con.posts.Find(idpost);
            comments = dbCon.Con.comments.Where(m => m.idpost == idpost).ToList();
        }
        public post GetPost()
        {
            return posts;
        }
        public List<comment> GetComments()
        {
            return comments;
        }
    }
    public class posts
    {
        DbCon dbCon = new DbCon();
        List<Postone> postones = new List<Postone>();
        Postone Postone;
        List<post> postss = new List<post>();
        public posts(int idexam)
        {            
            postss = dbCon.Con.posts.Where(m => m.idexame == idexam).ToList();
            foreach(var item in postss){
                Postone = new Postone(item.id);
                postones.Add(Postone);
            }
        }
        public List<Postone> GetPostones()
        {
            return postones;
        }
    }
}