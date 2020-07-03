using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;
using Order.Filter;

namespace Order.Controllers
{
    public class CommentController : Controller
    {
        AppointmentsEntities db = new AppointmentsEntities();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [Login]
        public ActionResult UserComment(int pageIndex = 1, int pageCount = 4)
        {
            User user = Session["user"] as User;
            //总行数
            int totalCount = db.Comment.OrderBy(p => p.Cid).Where(p => p.Uid == user.Uid).Count();
            //总页数
            double totalPage = Math.Ceiling((double)totalCount / pageCount);
            //获得用户集合 , 分页查询Skip（）跳过指定数量的集合 Take() 从过滤后返回的集合中再从第一行取出指定的行数
            List<Comment> com = db.Comment.OrderBy(p => p.Cid)
                 .Where(p => p.Uid == user.Uid).ToList()
                 .Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            ViewBag.totalCount = totalCount;
            ViewBag.totalPage = totalPage;
            ViewBag.com = com;
            return View();
        }


        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public ActionResult AddCom(Comment com)
        {
            Comment com1 = db.Comment.Find(com.Cid);
            com1.Comments = com.Comments;
            com1.Ctime = DateTime.Now;
            com1.Cstate = 1;
            db.SaveChanges();
            return RedirectToAction("UserComment", "Comment");
        }


        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCom(int? id)
        {
            Comment com = db.Comment.Find(id);
            com.Comments = "";
            com.Cstate = 0;
            com.Ctime = null;
            db.SaveChanges();
            return RedirectToAction("UserComment", "Comment");
        }
    }
}