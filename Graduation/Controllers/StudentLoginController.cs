using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graduation.Controllers
{
    public class StudentLoginController : Controller
    {
        //
        // GET: /StudentLogin/
        private GraduationDBContent db = new GraduationDBContent();

        /// <summary>
        /// 学生登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 学生登陆
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(StudentsLoginModel student)
        {

            if (ModelState.IsValid)
            {
                UploadModel temp = db.UploadTb.Find(student.StudentNumber);
                if ((temp!=null)&&temp.Pwd==student.Pwd)
                {
                    //0为本科生
                    if (temp.StudentType == "0")
                    {
                        //2为本科生登陆时间
                        var time = db.TimeTb.Where(m => m.Type == "2").FirstOrDefault();
                        if (DateTime.Now > time.BeginTime && DateTime.Now < time.EndTime)
                        {
                            Session["number"] = student.StudentNumber;
                            return RedirectToAction("Index", "FillBaseInfo");
                        }
                        else
                        {
                            ViewBag.message = "不在规定的登陆时间内";
                        }
                    }
                    if (temp.StudentType == "1")
                    {
                        //1为研究生登陆时间
                        var time = db.TimeTb.Where(m => m.Type == "1").FirstOrDefault();
                        if (DateTime.Now > time.BeginTime && DateTime.Now < time.EndTime)
                        {
                            Session["number"] = student.StudentNumber;
                            return RedirectToAction("Index", "FillBaseInfo");
                        }
                        else
                        {
                            ViewBag.message = "不在规定的登陆时间内";
                        }
                    }
                }
                return View("Login");
            }
            return View("Login");
        }

    }
}
