using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;
using Order.Filter;

namespace Order.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Find(int? id)
        //{
        //    Mediciner med = db.Mediciner.Find(id);
        //    ViewBag.med = med;
        //    return RedirectToAction("FindMed");
        //}

        [Login]
        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public ActionResult Add(int? id,DateTime time)
        {

            //根据对应时间查询预约表的记录
            List<Appointment> App = db.Appointment.Where(p =>p.Ttime==time).ToList();
            int num = App.Count();

            User user = Session["user"] as User;
            Mediciner med = db.Mediciner.Find(id);

            //查询对应时间、医生、用户对应的预约条数
            List<Appointment> App1 = db.Appointment.Where(p => (p.Mid == id) && (p.Uid == user.Uid) && (p.Ttime == time)).ToList();
            int num1 = App1.Count();
            //如果此时间段的预约条数小于医生设置的次数
            if (med.Mcount>num)
            {
                //判断用户是否重复预约
                if (num1==0)
                {
                    Appointment app = new Appointment()
                    {
                        Mid = med.Mid,
                        Uid = user.Uid,
                        Atime = DateTime.Now,
                        Ttime = time,
                        Astate = 0,
                        Anun = med.Mid,
                    };
                    db.Appointment.Add(app);
                    db.SaveChanges();
                    return Content("<script>alert('预约成功!');history.go(-1)</script>");

                }
                else
                {
                    return Content("<script>alert('此时间段您已经预约!');history.go(-1)</script>");
                }

            }
            else
            {
                return Content("<script>alert('此时间段已没有预约号!');history.go(-1)</script>");
            }

        }


        [Login]
        ///问诊
        public ActionResult Ques(string Mid,string Qcontent)
        {
            List<Question> que = db.Question.ToList();
            int id = Convert.ToInt32(Mid);
            if (Qcontent!="")
            {
                User user = Session["user"] as User;
                Mediciner med = db.Mediciner.Find(id);
                Question ques = new Question()
                {
                    Mid = med.Mid,
                    Uid = user.Uid,
                    Qtime = DateTime.Now,
                    Qcontent = Qcontent,
                    Qstate = 0,
                };
                db.Question.Add(ques);
                db.SaveChanges();
                return Content("<script>alert('提交成功!');history.go(-1)</script>");
            }
            else
            {
                return Content("<script>alert('请输入问题!');history.go(-1)</script>");
            }
        }

    }
}