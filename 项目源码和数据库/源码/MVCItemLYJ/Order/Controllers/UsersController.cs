using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;
using Order.Filter;

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



        [Login]
        /// <summary>
        /// 用户：我的预约
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder(int pageIndex = 1, int pageCount = 4)
        {
            User user = Session["user"] as User;
            //总行数
            int totalCount = db.Appointment.OrderBy(p =>p.Aid).Where(p=>p.Uid==user.Uid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Appointment> app = db.Appointment.OrderBy(p => p.Aid)
                 .Where(p => p.Uid == user.Uid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.app = app;
            return View();
        }

        /// <summary>
        /// 删除预约
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteApp(int? id)
        {
            Appointment app = db.Appointment.Find(id);
            if (app.Astate==0)
            {
                return Content("<script>alert('预约还没结束不能删除！');history.go(-1)</script>");
            }
            else
            {
                db.Appointment.Remove(app);
                db.SaveChanges();
                return RedirectToAction("MyOrder");
            }
        }

        [Login]
        public ActionResult Question(int pageIndex = 1, int pageCount = 4)
        {
            User user = Session["user"] as User;
            //总行数
            int totalCount = db.Question.OrderBy(p => p.Qid).Where(p => p.Uid == user.Uid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Question> ques = db.Question.OrderBy(p => p.Qid)
                 .Where(p => p.Uid == user.Uid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.ques = ques;
            return View();
        }
    }
}