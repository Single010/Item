using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

namespace Order.Controllers
{
    public class UsersController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User use,string num)
        {
            User s = db.User.SingleOrDefault(p => p.Uloginname == use.Uloginname);
            if (ModelState.IsValid)
            {
                if (s == null)
                {
                    if (num=="2")
                    {
                    db.User.Add(use);
                    db.SaveChanges();
                    return Content("<script>alert('注册成功！');history.go(-1)</script>");
                    }
                    else
                    {
                        return Content("<script>alert('注册失败！');history.go(-1)</script>");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("flag", "改账号已存在");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(string Uloginname, string Upwd)
        {
            User user = db.User.Where(p => p.Uloginname == Uloginname && p.Upwd == Upwd).SingleOrDefault();
            if (user != null)
            {
                //将当前登录的学生信息存储到Session中
                Session["user"] = user;
                ViewBag.Erro = "";
                return RedirectToAction("Main","Frist");
            }
            else
            {
                ViewBag.Erro = "你输入的账号或密码错误!!";
            }
            return View();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["med"] = null;
            return RedirectToAction("Main","Frist");
        }



        /// <summary>
        /// 用户：我的预约
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder()
        {
            return View();
        }

    }
}