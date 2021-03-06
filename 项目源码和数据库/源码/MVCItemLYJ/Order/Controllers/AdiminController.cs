﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;
using Order.Filter;

namespace Order.Controllers
{
    
    public class AdiminController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        // GET: Adimin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Aloginname, string Apwd)
        {

            Adimin ad = db.Adimin.Where(p => p.Aloginname == Aloginname && p.Apwd == Apwd).SingleOrDefault();
            if (ad != null)
            {
                //将当前登录的学生信息存储到Session中
                Session["admin"] = ad;
                ViewBag.Erro = "";
                return RedirectToAction("Main", "Adimin");
            }
            else
            {
                ViewBag.Erro = "你输入的账号或密码错误!!";
            }
            return View();
        }

        [Adimin]
        public ActionResult Main()
        {
            return View();
        }

        [Adimin]
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult One()
        {
            return View();
        }


        [Adimin]
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">页面显示行数</param>
        /// <returns></returns>
        public ActionResult UserH(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {
            //总行数
            int totalCount = db.User.OrderBy(p => p.Uid).Where(p => p.Uname.Contains(Nmae) || Nmae == "").Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<User> use = db.User.OrderByDescending(p => p.Uid)
                 .Where(p => p.Uname.Contains(Nmae) || Nmae == "").ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            return View(use);
        }


        [Adimin]
        /// <summary>
        /// 冻结账号
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            User sue = db.User.Find(id);
            sue.Ustate = 1;
            db.SaveChanges();
            return RedirectToAction("UserH");
        }

        [Adimin]
        /// <summary>
        /// 解冻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Frindom(int? id)
        {
            User sue = db.User.Find(id);
            sue.Ustate = 0;
            db.SaveChanges();
            return RedirectToAction("UserH");
        }


        [Adimin]
        /// <summary>
        /// 医生管理
        /// </summary>
        /// <returns></returns>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">页面显示行数</param>
        public ActionResult MedcH(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {

            //总行数
            int totalCount = db.Mediciner.OrderBy(p => p.Mid).Where(p => p.Mname.Contains(Nmae) || Nmae == "").Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得医生集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Mediciner> medc = db.Mediciner.OrderByDescending(p => p.Mid)
                 .Where(p => p.Mname.Contains(Nmae) || Nmae == "").ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            return View(medc);
        }

        [Adimin]
        /// <summary>
        /// 冻结医生账号
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteMedc(int id)
        {
            Mediciner med = db.Mediciner.Find(id);
            med.Mstate = 1;
            db.SaveChanges();
            return RedirectToAction("MedcH", "Adimin");
        }

        /// <summary>
        /// 医生账号解冻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FrindomMedc(int id)
        {
            Mediciner med = db.Mediciner.Find(id);
            med.Mstate = 0;
            db.SaveChanges();
            return RedirectToAction("MedcH", "Adimin");
        }


        [Adimin]
        /// <summary>
        /// 管理员
        /// </summary>
        /// <returns></returns>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">页面显示行数</param>
        public ActionResult AdimH(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {
            //总行数
            int totalCount = db.Adimin.OrderBy(p => p.Aid).Where(p => p.ANmae.Contains(Nmae) || Nmae == "").Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得医生集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Adimin> adi = db.Adimin.OrderByDescending(p => p.Aid)
                 .Where(p => p.ANmae.Contains(Nmae) || Nmae == "").ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            return View(adi);
        }

        [Adimin]
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="adim"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAdim(Adimin adim)
        {
            if (adim.ANmae==null || adim.Aloginname==null || adim.Apwd==null)
            {
                return Content("<script>alert('输入不能为空');history.go(-1)</script>");
            }
            else
            {
                db.Adimin.Add(adim);
                db.SaveChanges();
                return RedirectToAction("AdimH","Adimin");
            }
            
        }

        [Adimin]
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteAdim(int id)
        {
            Adimin adi = db.Adimin.Find(id);
            db.Adimin.Remove(adi);
            db.SaveChanges();
            return RedirectToAction("AdimH", "Adimin");
        }

        [Adimin]
        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAdim(int? id)
        {
            Adimin adi = db.Adimin.Find(id);
            return View(adi);
        }


        [Adimin]
        /// <summary>
        /// 医院管理
        /// </summary>
        /// <returns></returns>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">页面显示行数</param>
        public ActionResult HospitalH(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {

            //总行数
            int totalCount = db.Hospital.OrderBy(p => p.Hid).Where(p => p.Hname.Contains(Nmae) || Nmae == "").Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得医院集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Hospital> hos = db.Hospital.OrderByDescending(p => p.Hid)
                 .Where(p => p.Hname.Contains(Nmae) || Nmae == "").ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.hos = hos;
            return View(hos);
        }


        [Adimin]
        /// <summary>
        /// 添加医院
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddHos(Hospital hos,HttpPostedFileBase file)
        {
            if (file != null)
            {
                //获取文件大小
                if (file.ContentLength > 0)
                {
                    //获取文件名称
                    string fileName = Path.GetFileName(file.FileName);
                    //获取后缀名
                    string name = fileName.Substring(fileName.LastIndexOf(".") + 1);
                    //fileName.EndsWith("png"),看文件末尾有没有png
                    if (name == "png" || name == "jpg" || name == "gif")
                    {
                        file.SaveAs(Server.MapPath("~/Content/HosImage/" + file.FileName));
                        ViewBag.Path = "~/Content/HosImage/" + fileName;
                        hos.HImage = fileName;
                    }
                    else
                    {
                        ViewBag.Message = "上传图片格式不正确！";
                    }
                }
                else
                {
                    ViewBag.Message = "文件大小为0！";
                }
                db.Hospital.Add(hos);
                db.SaveChanges();
                return RedirectToAction("HospitalH", "Adimin");
            }
            else
            {
                //提示
                return Content("<script>alert('请上传图片文件');history.go(-1)</script>");
            }
           
        }


        [Adimin]
        /// <summary>
        /// 修改医院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditHos(int? id)
        {
            Hospital hos = db.Hospital.Find(id);
            ViewBag.hos = hos;
            return View(hos);
        }


        [Adimin]
        [HttpPost]
        /// <summary>
        /// 修改医院信息
        /// </summary>
        /// <param name="hos"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult EditHos(Hospital hos, HttpPostedFileBase file, string photo)
        {
            if (file != null)
            {                //获取文件名
                string fileName = Path.GetFileName(file.FileName);
                //保存
                file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
                //将文件名赋值给实体类照片属性
                hos.HImage = fileName;
            }
            else
            {
                //保留原图片
                hos.HImage = photo;
            }
            db.Entry(hos).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("HospitalH", "Adimin");
        }

        [Adimin]
        /// <summary>
        /// 删除医院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteHos(int id)
        {
            Hospital hos = db.Hospital.Find(id);
            List<Mediciner> dept = db.Mediciner.Where(p => p.Hid == id).ToList();
            if (dept.Count()==0)
            {
                db.Hospital.Remove(hos);
                db.SaveChanges();
                return RedirectToAction("HospitalH", "Adimin");
            }
            else
            {
                return Content("<script>alert('此医院内还有在岗医生，不能删除！'); history.go(-1)</script>");
            }
           
        }


        [Adimin]
        /// <summary>
        /// 一级科室
        /// </summary>
        /// <returns></returns>
        public ActionResult FristDept(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {
            //总行数
            int totalCount = db.Dept.OrderBy(p => p.Did).Where(p => p.Dname.Contains(Nmae) || Nmae == "").Where(p => p.Pdid == null).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得医院集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Dept> dept = db.Dept.OrderByDescending(p => p.Did)
                 .Where(p => (p.Dname.Contains(Nmae) || Nmae == "")&&(p.Pdid == null)).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.hos = dept;
            return View(dept);
        }


        [Adimin]
        /// <summary>
        /// 添加一级科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFristDept(Dept dept)
        {
            if (dept.Dname==null || dept.Dspec==null)
            {
                return Content("<script>alert('输入不能为空');history.go(-1)</script>");
            }
            else
            {
                db.Dept.Add(dept);
                db.SaveChanges();
                return RedirectToAction("FristDept", "Adimin");
            }
        }

        [Adimin]
        /// <summary>
        /// 修改一级科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditFristDept(int? id)
        {
            Dept dept = db.Dept.Find(id);
            return View(dept);
        }


        [Adimin]
        [HttpPost]
        public ActionResult EditFristDept(Dept dept)
        {
            db.Entry(dept).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FristDept", "Adimin");

        }



        [Adimin]
        /// <summary>
        /// 删除一级科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteFristDept(int? id)
        {
            Dept dept = db.Dept.Find(id);
            var num = dept.Dept1.Where(p => p.Pdid == dept.Did).Count();
            if (num>0)
            {
                return Content("<script>alert('此一级科室内还包含二级科室，不能删除！'); history.go(-1)</script>");
            }
            else
            {
                db.Dept.Remove(dept);
                db.SaveChanges();
                return RedirectToAction("FristDept", "Adimin");
            }
        }




        [Adimin]
        /// <summary>
        /// 二级科室
        /// </summary>
        /// <param name="Nmae"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public ActionResult TwoDept(string Nmae = "", int pageIndex = 1, int pageCount = 4)
        {
            //总行数
            int totalCount = db.Dept.OrderBy(p => p.Did).Where(p => p.Dname.Contains(Nmae) || Nmae == "").Where(p => p.Pdid != null).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得医院集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Dept> dept = db.Dept.OrderByDescending(p => p.Did)
                 .Where(p => (p.Dname.Contains(Nmae) || Nmae == "") && (p.Pdid != null)).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            List<Dept> dept2 = db.Dept.ToList();
            ViewBag.dept2 = dept2;
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.hos = dept;
            return View(dept);
        }

        [Adimin]
        /// <summary>
        /// 添加二级科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public ActionResult AddTwoDept(Dept dept)
        {
            if (dept.Dname==null || dept.Dspec==null)
            {
                return Content("<script>alert('输入不能为空');history.go(-1)</script>");
            }
            else
            {
                db.Dept.Add(dept);
                db.SaveChanges();
                return RedirectToAction("TwoDept", "Adimin");
            }
           
        }


        [Adimin]
        /// <summary>
        /// 删除二级科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTwoDept(int? id)
        {
            Dept dept = db.Dept.Find(id);
            var num = db.Mediciner.Where(p => p.Did == dept.Did).Count();
            if (num>0)
            {
                return Content("<script>alert('此二级科室内有医生在岗，不能删除！'); history.go(-1)</script>");
            }
            else
            {
                db.Dept.Remove(dept);
                db.SaveChanges();
                return RedirectToAction("FristDept", "Adimin");
            }
        }


        [Adimin]
        /// <summary>
        /// 修改二级科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditTwoDept(int? id)
        {
            Dept dept = db.Dept.Find(id);
            List<Dept> dept2 = db.Dept.ToList();
            ViewBag.dept2 = dept2;
            return View(dept);
        }


        [Adimin]
        [HttpPost]
        public ActionResult EditTwoDept(Dept dept)
        {
            db.Entry(dept).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TwoDept","Adimin");
        }


        [Adimin]
        /// <summary>
        /// 医院地区分部
        /// </summary>
        /// <returns></returns>
        public ActionResult TC()
        {
            List< Hospital > hos = db.Hospital.ToList();
            ViewBag.hos = hos;
            return View();
        }


        [Adimin]
        /// <summary>
        /// 医生等级分部
        /// </summary>
        /// <returns></returns>
        public ActionResult TCMed()
        {
            List<Mediciner> med = db.Mediciner.ToList();
            ViewBag.med = med;
            return View();
        }

        [Adimin]
        /// <summary>
        ///用户统计
        /// </summary>
        /// <returns></returns>
        public ActionResult TCUser()
        {
            List<User> user = db.User.ToList();
            ViewBag.user = user;
            return View();
        }


        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Main", "Frist");
        }
    }
}