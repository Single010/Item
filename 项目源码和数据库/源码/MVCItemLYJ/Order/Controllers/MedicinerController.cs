using System;
using System.Collections.Generic;
using System.IO;
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


        /// <summary>
        /// 我的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MedMessageEdit(int? id)
        {
            Mediciner med = db.Mediciner.Find(id);
            List<Hospital> hos = db.Hospital.ToList();
            List<Dept> dep = db.Dept.ToList();
            ViewBag.hos = hos;
            ViewBag.dep = dep;
            return View(med);
        }
        /// <summary>
        /// 修改我的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult MedMessage(int? id)
        {
            Mediciner med = db.Mediciner.Find(id);
            List<Hospital> hos = db.Hospital.ToList();
            List<Dept> dep = db.Dept.ToList();
            ViewBag.hos = hos;
            ViewBag.dep = dep;
            return View(med);
        }
        
        [HttpPost]
        public ActionResult MedMessage(Mediciner med,HttpPostedFileBase file, string photo)
        {

            if (file != null)
            {                //获取文件名
                string fileName = Path.GetFileName(file.FileName);
                //保存
                file.SaveAs(Server.MapPath("~/Content/HosImage/" + file.FileName));
                //将文件名赋值给实体类照片属性
                med.MPic = fileName;
            }
            else
            {
                //保留原图片
                med.MPic = photo;
            }
            Mediciner med2 = db.Mediciner.Find(med.Mid);
            med2.Mloginname = med.Mloginname;
            med2.Mpwd = med.Mpwd;
            med2.Mname = med.Mname;
            med2.Gender = med.Gender;
            med2.Titles = med.Titles;
            med2.Mspec = med.Mspec;
            med2.Hid = med.Hid;
            med2.Did = med.Did;
            med2.MPic = med.MPic;
            db.SaveChanges();
            List<Hospital> hos = db.Hospital.ToList();
            List<Dept> dep = db.Dept.ToList();
            ViewBag.hos = hos;
            ViewBag.dep = dep;
            return RedirectToAction("MedMessageEdit","Mediciner",new { id=med2.Mid});
        }
    }
}