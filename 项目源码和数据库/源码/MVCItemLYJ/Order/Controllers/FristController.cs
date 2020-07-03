using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;
using Order.Filter;

namespace Order.Controllers
{
    public class FristController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        // GET: Frist
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            List<Dept> dept = db.Dept.ToList();
            List<Hospital> hos = db.Hospital.ToList();
            ViewBag.dept = dept;
            ViewBag.hos = hos;
            return View();
        }

        public ActionResult FindMed(int? id)
        {
            List<Mediciner> findmed = db.Mediciner.Where(p => p.Did == id).Take(5).ToList();
            return PartialView(findmed);
        }

        public ActionResult FindHos(string name)
        {
            List<Hospital> findhos = db.Hospital.Where(p => p.Address == name).Take(3).ToList();
            return PartialView(findhos);
        }


        [Login]
        public ActionResult FindID(int? id)
        {
            Session["id"] = id;
            //总行数
            int totalCount = db.Mediciner.OrderBy(p => p.Mid).Where(p => p.Did == id).Count();
            if (totalCount == 0)
            {
                return Content("<script>alert('没有找到此科室相关的医生');history.go(-1)</script>");
            }
            else
            {
                return RedirectToAction("FindMeds");
            }
            
        }

        [Login]
        /// <summary>
        /// 首页点击科室查询相关医生
        /// </summary>
        /// <returns></returns>
        public ActionResult FindMeds(int pageIndex = 1, int pageCount = 4)
        {
           
            int ID = Convert.ToInt32(Session["id"].ToString().Trim());
            //总行数
            int totalCount = db.Mediciner.OrderBy(p => p.Mid).Where(p=>p.Did== ID).Count();
                //总页数
                double totalPage = Math.Ceiling((double)totalCount / pageCount);
                //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
                List<Mediciner> med = db.Mediciner.OrderBy(p => p.Mid).Where(p => p.Did == ID)
                     .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                Dept dept = db.Dept.Find(ID);
                ViewBag.med = med;
                ViewBag.dept = dept;
                ViewBag.pageIndex = pageIndex;
                ViewBag.pageCount = pageCount;
                ViewBag.totalCount = totalCount;
                ViewBag.totalPage = totalPage;
                return View();
       
        }

        [Login]
        public ActionResult More()
        {
            List<Dept> dept = db.Dept.ToList();
            ViewBag.dept = dept;
            return View();
        }
    }
}