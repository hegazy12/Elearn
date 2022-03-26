﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elearn.Models;
using Elearn.Users;
using Elearn.dbCon;

namespace Elearn.Controllers
{
    public class HomeController : Controller
    {

        DbCon dbCon = new DbCon();
        Sinup sinup = new Sinup();
        valdation valdation = new valdation();
        public ActionResult Index()
        {
            Session["UserName"] = "Normal";
            Session["Role"] = "Normal";
            Session["id"] = "0";
            Session["Token"] = "";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Login()
        {
            Session["UserName"] = "Normal";
            Session["Role"] = "Normal";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string username = form["uname"];
            string pass = form["pass"];
            Ueaser x=dbCon.Con.Ueasers.Where(m => m.UserName==username && m.Pass==pass && m.actv==1).FirstOrDefault();
            if (x==null)
            {
                ViewBag.m = "password false";
                return View();
            }
            Session["UserName"] = x.UserName;
            Session["id"] = x.id.ToString();
            Session["Token"] = valdation.RandomString();
            
            Login login = new Login();
            login.iduser = x.id;
            login.token =Convert.ToString(Session["Token"]);
            login.state = 1;
            login.role =x.role;
            login.username = x.UserName;

            dbCon.Con.Logins.Add(login);
            dbCon.Con.SaveChanges();

            if (x.role==1)
            {
                Session["Role"] = "Student";
                return RedirectToAction("Index", "StuGroup");
            }else if(x.role==2){
                Session["Role"]="Employee";
                return RedirectToAction("Index", "Groups");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            int id = Convert.ToInt32(Session["id"]);
            string token = Convert.ToString(Session["Token"]);
            List<Login> login = dbCon.Con.Logins.Where(m=>m.iduser == id).ToList();
            dbCon.Con.Logins.RemoveRange(login);
            dbCon.Con.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SignUp()
        {
            Session["UserName"] = "Normal";
            Session["Role"] = "Normal";
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Ueaser ueaser ,string Repass)
        {
            if (sinup.emptyfield(ueaser.Fname) == false)
            {
                ViewBag.m = "First name is empty";
                return View();
            }
            else if(sinup.emptyfield(ueaser.Lname) == false)
            {
                ViewBag.m = "Last name is empty";
                return View();
            }
            else if(sinup.emptyfield(ueaser.phone) == false)
            {
                ViewBag.m = "Phone is empty";
                return View();
            }
            else if(sinup.emptyfield(ueaser.Pass) == false)
            {
                ViewBag.m = "Password is empty";
                return View();
            }
            else if(sinup.emptyfield(ueaser.UserName) == false)
            {
                ViewBag.m = "UserName is empty";
                return View();
            }
            else if(ueaser.Pass != Repass)
            {
                ViewBag.m = "pass not equal repass";
                return View();
            }
            else if(sinup.usernamevaled(ueaser.UserName))
            {
                ViewBag.m = "Username is Repetition";
                return View();
            }
            ueaser.role = 1;
            ueaser.actv = 1;
            dbCon.Con.Ueasers.Add(ueaser);
            dbCon.Con.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}