using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

namespace Order.Controllers
{
    public class MedicinerController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        // GET: Mediciner
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            List<Dept> dept = db.Dept.Where(p=>p.Pdid.ToString() !="").ToList();
            List<Hospital> hos = db.Hospital.ToList();
            ViewBag.hos=hos;
            return View(dept);
        }

        [HttpPost]
        public ActionResult Create(Mediciner med)
        {
            Mediciner m = db.Mediciner.SingleOrDefault(p => p.Mloginname==med.Mloginname);
            if (ModelState.IsValid)
            {
                if (m == null)
                {
                        db.Mediciner.Add(med);
                        db.SaveChanges();
                        return Content("<script>alert('注册成功！');history.go(-1)</script>");
                }
                else
                {
                    return Content("<script>alert('账号已存在！');history.go(-1)</script>");
                }
            }
            else
            {
                List<Dept> dept = db.Dept.Where(p => p.Pdid.ToString() != "").ToList();
                List<Hospital> hos = db.Hospital.ToList();
                ViewBag.hos = hos;
                return View(dept);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult  Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(string Mloginname, string Mpwd)
        {
            Mediciner med = db.Mediciner.Where(p => p.Mloginname == Mloginname && p.Mpwd == Mpwd).SingleOrDefault();
            if (med != null)
            {
                //将当前登录的医生信息存储到Session中
                Session["med"] = med;
                ViewBag.Erro = "";
                return RedirectToAction("MedIndex", "Mediciner");
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
            return RedirectToAction("Main", "Frist");
        }

        /// <summary>
        /// 医生的数据主页
        /// </summary>
        /// <returns></returns>
        public ActionResult MedIndex()
        {
            return View();
        }
    }
}