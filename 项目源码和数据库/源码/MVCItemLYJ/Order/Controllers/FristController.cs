using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

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


    }
}