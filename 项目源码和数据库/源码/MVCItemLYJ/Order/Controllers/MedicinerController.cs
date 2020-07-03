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


        public ActionResult Ords(int? id)
        {
            Mediciner med = db.Mediciner.Find(id);
            return View(med);
        }


        public ActionResult Ord(int? id)
        {
            Mediciner med = db.Mediciner.Find(id);
            return View(med);
        }
        [HttpPost]
        public ActionResult Ord(Mediciner med)
        {
            Mediciner med2 = db.Mediciner.Find(med.Mid);
             var time=DateTime.Now;
            if ((med.MtimeA<time) || (med.MtimeB<time) || (med.MtimeC<time))
            {
                return Content("<script>alert('时间选择有误！');history.go(-1)</script>");
            }
            else
            {
                if ((med.MtimeA==med.MtimeB) || (med.MtimeA==med.MtimeC) ||(med.MtimeB==med.MtimeC))
                {
                    return Content("<script>alert('时间选择有重复！');history.go(-1)</script>");
                }
                else
                {
                    med2.MtimeA = med.MtimeA;
                    med2.MtimeB = med.MtimeB;
                    med2.MtimeC = med.MtimeC;

                }
            }
            med2.Mcount = med.Mcount;
            db.SaveChanges();
            return RedirectToAction("Ords", "Mediciner", new { id = med2.Mid });
        }

        /// <summary>
        /// 医生：我的预约
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrd(int pageIndex = 1, int pageCount = 4)
        {
            Mediciner med = Session["med"] as Mediciner;
            //总行数
            int totalCount = db.Appointment.OrderBy(p => p.Aid).Where(p => p.Mid == med.Mid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Appointment> app = db.Appointment.OrderBy(p => p.Aid)
                 .Where(p => p.Mid == med.Mid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.app = app;
            return View();
        }


        /// <summary>
        /// 结束预约
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EndApp(int? id)
        {
            Mediciner med = Session["med"] as Mediciner;
            Appointment app = db.Appointment.Find(id);
            app.Astate = 1;
            Comment com = new Comment()
            {
                Mid = med.Mid,
                Uid = app.User.Uid,
                Aid = app.Aid,
                Cstate = 0,
            };
            db.Comment.Add(com);
            db.SaveChanges();
            return RedirectToAction("MyOrd", "Mediciner");
        }

        /// <summary>
        /// 医生：我的评论
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public ActionResult MedCom(int pageIndex = 1, int pageCount = 4)
        {
            Mediciner med = Session["med"] as Mediciner;
            //总行数
            int totalCount = db.Comment.OrderBy(p => p.Cid).Where(p => p.Mid == med.Mid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Comment> com = db.Comment.OrderBy(p => p.Cid)
                 .Where(p => p.Mid == med.Mid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.com = com;
            return View();
        }

        /// <summary>
        /// 医生：我的问诊
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public ActionResult Ques(int pageIndex = 1, int pageCount = 4)
        {
            Mediciner med = Session["med"] as Mediciner;
            //总行数
            int totalCount = db.Question.OrderBy(p => p.Qid).Where(p => p.Mid == med.Mid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Question> ques = db.Question.OrderBy(p => p.Qid)
                 .Where(p => p.Mid == med.Mid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.ques = ques;
            return View();
        }

        /// <summary>
        /// 问诊回复
        /// </summary>
        /// <param name="ques"></param>
        /// <returns></returns>
        public ActionResult QuesAnswer(Question ques)
        {
            Question ques1 = db.Question.Find(ques.Qid);
            ques1.Qanswer = ques.Qanswer;
            ques1.Qstate = 1;
            db.SaveChanges();
            return RedirectToAction("Ques", "Mediciner");
        }



       
    }
}