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
                    return RedirectToAction("Logon");
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
                return Content("<script>alert('注册失败！');history.go(-1)</script>");
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
                    if (user.Ustate==0 || user.Ustate==null)
                    {
                        //将当前登录的学生信息存储到Session中
                        Session["user"] = user;
                        ViewBag.Erro = "";
                        return RedirectToAction("Main", "Frist");
                    }
                    else
                    {
                        return Content("<script>alert('抱歉，你的账号已被冻结！'); history.go(-1)</script>");
                    }
                    
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
            List<Appointment> app = db.Appointment.OrderByDescending(p => p.Aid)
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
        /// 取消预约
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteApp(int? id)
        {
            Appointment app = db.Appointment.Find(id);
            TimeSpan ts = Convert.ToDateTime(app.Atime) - Convert.ToDateTime(DateTime.Now);
            if (ts.Days<1)
            {
                return Content("<script>alert('预约时间即将到不能取消！');history.go(-1)</script>");
            }
            else
            {
                db.Appointment.Remove(app);
                db.SaveChanges();
                return RedirectToAction("MyOrder");
            }
        }

        /// <summary>
        /// 我的问诊
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        [Login]
        public ActionResult Question(int pageIndex = 1, int pageCount = 4)
        {
            User user = Session["user"] as User;
            //总行数
            int totalCount = db.Question.OrderBy(p => p.Qid).Where(p => p.Uid == user.Uid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Question> ques = db.Question.OrderByDescending(p => p.Qid)
                 .Where(p => p.Uid == user.Uid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.ques = ques;
            return View();
        }



        /// <summary>
        /// 删除问诊
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteQues(int? id)
        {
            Question que = db.Question.Find(id);
            db.Question.Remove(que);
            db.SaveChanges();
            return RedirectToAction("Question");
        }

        [Login]
        /// <summary>
        /// 我的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult MyMessage()
        {
            User user = Session["user"] as User;
            User Use = db.User.Find(user.Uid);
            ViewBag.Use = Use;
            return View();
        }


        /// <summary>
        /// 修改我的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMyMessage(User user)
        {
            User Use = db.User.Find(user.Uid);
            Use.Uloginname = user.Uloginname;
            Use.Uname = user.Uname;
            Use.Upwd = user.Upwd;
            Use.Gender = user.Gender;
            Use.Uidentity = user.Uidentity;
            Use.Mobile = user.Mobile;
            db.SaveChanges();
            return RedirectToAction("MyMessage", "Users");
        }
    }
}