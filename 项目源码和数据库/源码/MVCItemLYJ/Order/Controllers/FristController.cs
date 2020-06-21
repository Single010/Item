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
            ViewBag.dept = dept;
            return View();
        }

        public ActionResult FindMed(int? id)
        {
            List<Mediciner> findmed = db.Mediciner.Where(p => p.Did == id).ToList();
            return PartialView(findmed);
        }

    }
}