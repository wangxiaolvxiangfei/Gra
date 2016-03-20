using Graduation.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
//添加文字

namespace Graduation.Controllers
{
    /// <summary>
    /// 管理员控制器
    /// </summary>
    public class AdminController : Controller
    {
        private GraduationDBContent db = new GraduationDBContent();
        #region 管理员登陆
        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            var temp = db.UserTb.Where(m => m.UserName == model.UserName);
            if (temp.FirstOrDefault() != null)
            {
                if (temp.FirstOrDefault().Pwd == model.Pwd)
                {
                    //保存登陆用户的ID
                    Session["Id"] = temp.FirstOrDefault().Id;
                    //保存登陆用户的类型(是院系还是就业中心)
                    Session["Type"] = temp.FirstOrDefault().TypeCode;

                    if (temp.FirstOrDefault().TypeCode == "1")//院系管理员登陆
                        return RedirectToAction("AdminGradList");
                    if (temp.FirstOrDefault().TypeCode == "2")//就业中心管理员登陆
                        return RedirectToAction("AdminAcademy");
                }
                else
                    return View("Login");
            }
            return View();
        }
        #endregion


        #region 重置密码
        //
        // GET: /Admin/
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ReSetPwd(string studentNumber = "0")
        {
            if (Session["Id"] != null)
            {
                ;

                var temp = db.UploadTb.Find(studentNumber);
                if (temp != null)
                {
                    try
                    {
                        temp.Pwd = temp.IDNumber.Substring(13);
                        db.Entry(temp).CurrentValues.SetValues(temp);
                        db.SaveChanges();
                        ViewBag.result = "修改成功，密码为身份证号后4位";
                    }
                    catch (Exception)
                    {
                        ViewBag.result = "修改密码失败";
                    }

                    return View(temp);
                }
                return View();
            }
            else
                return RedirectToAction("Login");

        }
        /// <summary>
        /// 重置密码搜索
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReSetPwd(UploadModel students)
        {
            if (students.StudentNumber != null)
            {
                var student = db.UploadTb.Find(students.StudentNumber);
                return View(student);
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region 系统时间设置
        public ActionResult AdminTime(int Id = 0, int type = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                ViewBag.NowTime = DateTime.Now;
                //删除
                if (type == 1)
                {
                    var time = db.TimeTb.Find(Id);
                    if (time == null)
                        return RedirectToAction("AdminTime");
                    db.TimeTb.Remove(time);
                    db.SaveChanges();
                }

                List<SelectListItem> TimeType = new List<SelectListItem> {
                    new SelectListItem{Text="研究生可登陆时间",Value="1",Selected=false},
                    new SelectListItem{Text="本科生可登录时间",Value="2",Selected=true},
                    new SelectListItem{Text="院系管理员可登陆时间",Value="3",Selected=false}
                };
                ViewBag.TimeType = TimeType;

                var temp = new TimeViewModel()
                {
                    TimeList = db.TimeTb.ToList()
                };
                if (Id != 0)
                {
                    temp.time = db.TimeTb.Find(Id);
                }

                return View(temp);
            }
            //为空则为未登录，跳入到登陆界面
            else
                return RedirectToAction("Login");

        }
        /// <summary>
        /// 登陆时间控制
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminTime(TimeViewModel model)
        {
            if (model.time.Type == "1")
                model.time.TypeName = "研究生使用时间";
            else if (model.time.Type == "2")
                model.time.TypeName = "本科生使用时间";
            else if (model.time.Type == "3")
                model.time.TypeName = "院系管理员使用时间";
            model.time.EndTime = model.time.EndTime.AddHours(23);
            model.time.EndTime = model.time.EndTime.AddMinutes(59);
            var temp = db.TimeTb.Find(model.time.Id);
            //为更新
            if (temp != null)
            {
                db.Entry(temp).CurrentValues.SetValues(model.time);
                db.SaveChanges();
            }
            //为空为添加
            else
            {
                db.TimeTb.Add(model.time);
                db.SaveChanges();
            }
            return RedirectToAction("AdminTime");
        }
        #endregion

        #region 实现院系专业联动的列表
        /// <summary>
        /// 返回学院列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAcalist()
        {
            List<AcademyModel> acaList = new List<AcademyModel>();
            var list = db.AcademyTb.ToList();
            acaList.Add(new AcademyModel() { Id = 0, Name = "===请选择===" });
            foreach (var item in list)
            {
                acaList.Add(new AcademyModel() { Id = item.Id, Name = item.Name });
            }
            return Json(acaList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回系列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDepartList(int pid)
        {
            List<DepartmentModel> departList = new List<DepartmentModel>();
            var list = db.DepartmentTb.Where(m => m.BelongId == pid).ToList();
            departList.Add(new DepartmentModel() { Id = 0, BelongId = 0, Name = "===请选择===" });
            foreach (var item in list)
                departList.Add(new DepartmentModel() { Id = item.Id, BelongId = item.BelongId, Name = item.Name });
            return Json(departList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回专业列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMajorList(int pid)
        {
            List<MajorModel> majorList = new List<MajorModel>();
            string stringId = pid.ToString();
            var list = db.MajorTb.Where(m => m.DepartId == stringId).ToList();
            majorList.Add(new MajorModel() { Id = 0, DepartId = "0", Name = "===请选择===" });
            foreach (var item in list)
                majorList.Add(new MajorModel() { Id = item.Id, DepartId = item.DepartId, Name = item.Name });
            return Json(majorList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回系列表的所有信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult Gettable(int pid)
        {
            List<DepartmentModel> departList = new List<DepartmentModel>();
            var list = db.DepartmentTb.Where(m => m.BelongId == pid).ToList();
            foreach (var item in list)
                departList.Add(new DepartmentModel() { Id = item.Id, BelongId = item.BelongId, Name = item.Name, BelongName = item.BelongName, Code = item.Code });
            return Json(departList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据院ID选出所有该院下面的所有专业
        /// </summary>
        /// <param name="pid">院ID</param>
        /// <returns></returns>
        public JsonResult GetMajorTbByAc(string pid)
        {
            List<MajorModel> majorList = new List<MajorModel>();
            var list = db.MajorTb.Where(m => m.AcademyId == pid).ToList();
            foreach (var item in list)
                majorList.Add(new MajorModel() { Id = item.Id, Name = item.Name, Code = item.Code, AcademyName = item.AcademyName, DepartmentName = item.DepartmentName, Edu = item.Edu });
            return Json(majorList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据系ID选出所有该系下面的所有专业
        /// </summary>
        /// <param name="pid">系ID</param>
        /// <returns></returns>
        public JsonResult GetMajorTbBydDe(string pid)
        {
            List<MajorModel> majorList = new List<MajorModel>();
            var list = db.MajorTb.Where(m => m.DepartId == pid).ToList();
            foreach (var item in list)
                majorList.Add(new MajorModel() { Id = item.Id, Name = item.Name, Code = item.Code, AcademyName = item.AcademyName, DepartmentName = item.DepartmentName, Edu = item.Edu });
            return Json(majorList, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 系统用户管理

        /// <summary>
        /// 系统用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminUser(int id = 0, int type = 0)
        {
            //不为空则为登陆
            if (Session["Id"] != null)
            {
                // 显示的设置
                ViewBag.type = Session["Type"];

                //类型不为0，则为删除
                if (type != 0)
                {
                    var user = db.UserTb.Find(id);
                    if (user == null)
                        return RedirectToAction("AdminUser");
                    db.UserTb.Remove(user);
                    db.SaveChanges();
                    ViewBag.message = "管理人员删除成功";
                }
                List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem{Text="院系管理员",Value="1",Selected=true},
                new SelectListItem{Text="就业中心管理员",Value="2",Selected=false}
            };
                ViewBag.list = list;

                List<SelectListItem> depList = new List<SelectListItem>();
                var dep = db.DepartmentTb.ToList();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    depList.Add(new SelectListItem { Text = "就业中心人员", Value = "0" });
                }
                ViewBag.dep = depList;

                var temp = new UserViewModel()
                {
                    user = new UserModel(),
                    userList = db.UserTb.ToList()
                };
                if (id != 0)
                {
                    temp.user = db.UserTb.Find(id);
                }
                return View(temp);
            }
            //为空则为未登录，跳入到登陆界面
            else
                return RedirectToAction("Login");

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminUser(UserViewModel admin)
        {

            var temp = db.UserTb.Where(model => model.UserName == admin.user.UserName).SingleOrDefault();
            if (admin.user.TypeCode == "1")
                admin.user.Type = "院系管理员";
            else if (admin.user.TypeCode == "2")
                admin.user.Type = "就业中心管理员";
            if (admin.user.DepartId != 0)
                admin.user.DepartName = db.DepartmentTb.Find(admin.user.DepartId).Name;
            //存在为更新
            if (temp != null)
            {
                db.Entry(temp).CurrentValues.SetValues(admin.user);
                db.SaveChanges();
                ViewBag.message = "管理员信息更新成功";
            }
            //不存在为添加
            else
            {
                db.UserTb.Add(admin.user);
                db.SaveChanges();
                ViewBag.message = "管理员信息添加成功";
            }
            return RedirectToAction("AdminUser");
        }

        #endregion

        #region 学院名称管理
        /// <summary>
        /// 学院管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminAcademy(int id = 0, int type = 0)
        {
            if (Session["Id"] != null)
            {
                // 显示的设置
                ViewBag.type = Session["Type"];
                //删除功能
                if (type == 1)
                {
                    var aca = db.AcademyTb.Find(id);
                    if (aca == null)
                        return RedirectToAction("AdminAcademy");
                    db.AcademyTb.Remove(aca);
                    //删除院下面的系和专业
                    var dep = db.DepartmentTb.Where(model => model.BelongId == aca.Id);
                    if (dep != null)
                    {
                        foreach (var item in dep)
                        {
                            db.DepartmentTb.Remove(item);
                        }
                    }
                    //删除院下面的专业
                    string acaId = id.ToString();
                    var major = db.MajorTb.Where(m => m.AcademyId == acaId);
                    if (major != null)
                    {
                        foreach (var item in major)
                            db.MajorTb.Remove(item);
                    }
                    db.SaveChanges();
                    ViewBag.message = "删除成功";
                }
                var temp = new AcademyViewModel();
                temp.Academy = new AcademyModel();
                temp.AcademyList = db.AcademyTb.ToList();
                if (id != 0)
                    temp.Academy = db.AcademyTb.Find(id);
                return View(temp);
            }
            else
            { return RedirectToAction("Login"); }

        }

        /// <summary>
        /// 学院管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAcademy(AcademyViewModel model)
        {
            var aca = db.AcademyTb.Find(model.Academy.Id);
            //更新
            if (aca != null)
            {
                db.Entry(aca).CurrentValues.SetValues(model.Academy);
                db.SaveChanges();
            }
            else
            {
                db.AcademyTb.Add(model.Academy);
                db.SaveChanges();
            }
            return RedirectToAction("AdminAcademy");
        }
        #endregion

        #region 系名称管理
        /// <summary>
        /// 系名称管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDepartmet(int id = 0, int type = 0)
        {
            if (Session["Id"] != null)
            {
                // 显示的设置
                ViewBag.type = Session["Type"];
                //删除功能
                if (type == 1)
                {
                    var temp = db.DepartmentTb.Find(id);
                    if (temp == null)
                        return RedirectToAction("AdminDepartmet");
                    db.DepartmentTb.Remove(temp);

                    ///删除系下面的专业
                    string acaId = id.ToString();
                    var major = db.MajorTb.Where(m => m.AcademyId == acaId);
                    if (major != null)
                    {
                        foreach (var item in major)
                            db.MajorTb.Remove(item);
                    }
                    db.SaveChanges();
                    ViewBag.OpenType = 1;
                }
                DepartmentViewModel dep = new DepartmentViewModel();
                //院系列表
                dep.departList = db.DepartmentTb.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                var aca = db.AcademyTb.ToList();
                if (aca != null)
                {
                    foreach (var item in aca)
                    {
                        list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.list = list;
                if (id != 0)
                    dep.depart = db.DepartmentTb.Find(id);
                return View(dep);
            }
            else
            { return RedirectToAction("Login"); }

        }
        /// <summary>
        /// 系名称管理
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminDepartmet(DepartmentViewModel dep)
        {
            var temp = db.DepartmentTb.Find(dep.depart.Id);
            //不为空则为更新
            if (temp != null)
            {
                //添加所属学院名字
                dep.depart.BelongName = (db.AcademyTb.Find(dep.depart.BelongId)).Name;
                db.Entry(temp).CurrentValues.SetValues(dep.depart);
                db.SaveChanges();
            }
            //为空为添加
            else
            {
                dep.depart.BelongName = (db.AcademyTb.Find(dep.depart.BelongId)).Name;
                db.DepartmentTb.Add(dep.depart);
                db.SaveChanges();
            }
            ViewBag.OpenType = 1;
            return RedirectToAction("AdminDepartmet");
        }
        #endregion

        #region 专业名称管理
        /// <summary>
        /// 专业名称管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminMajor(int id = 0, int type = 0)
        {
            if (Session["Id"] != null)
            {
                // 显示的设置
                ViewBag.type = Session["Type"];
                //删除功能
                if (type == 1)
                {
                    var temp = db.MajorTb.Find(id);
                    if (temp == null)
                        return RedirectToAction("AdminMajor");
                    db.MajorTb.Remove(temp);
                    db.SaveChanges();
                    ViewBag.OpenType = 1;
                }
                MajorViewModel major = new MajorViewModel();
                //学院列表
                List<SelectListItem> acaList = new List<SelectListItem>();
                var aca = db.AcademyTb.ToList();
                if (aca != null)
                {
                    foreach (var item in aca)
                    {
                        acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.aca = acaList;

                //系列表
                List<SelectListItem> depList = new List<SelectListItem>();
                var dep = db.DepartmentTb.ToList();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.dep = depList;

                if (id != 0)
                    major.Major = db.MajorTb.Find(id);
                return View(major);
            }
            else
            { return RedirectToAction("Login"); }

        }
        /// <summary>
        /// 专业名称管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminMajor(MajorViewModel model)
        {
            var temp = db.MajorTb.Find(model.Major.Id);
            //不为空则为更新
            if (temp != null)
            {
                int AcademyId = Convert.ToInt32(model.Major.AcademyId);
                int DepartId = Convert.ToInt32(model.Major.DepartId);
                //添加所属学院名字
                model.Major.AcademyName = (db.AcademyTb.Find(AcademyId)).Name;
                model.Major.DepartmentName = db.DepartmentTb.Find(DepartId).Name;
                db.Entry(temp).CurrentValues.SetValues(model.Major);
                db.SaveChanges();
            }
            //为空为添加
            else
            {
                int AcademyId = Convert.ToInt32(model.Major.AcademyId);
                int DepartId = Convert.ToInt32(model.Major.DepartId);
                model.Major.AcademyName = (db.AcademyTb.Find(AcademyId)).Name;
                model.Major.DepartmentName = db.DepartmentTb.Find(DepartId).Name;
                db.MajorTb.Add(model.Major);
                db.SaveChanges();
            }
            ViewBag.OpenType = 1;
            return RedirectToAction("AdminMajor");
        }
        #endregion

        #region 签约登记表
        /// <summary>
        /// 签约等级管理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Sign(int type = 0, int id = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                var user = db.UserTb.Find(Session["Id"]);
                var upload = db.UploadTb.Where(m => m.Department == user.DepartName);
                ViewBag.type = user.TypeCode;

                if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
                {
                    ViewBag.style = "1";
                    var userDep = db.DepartmentTb.Find(user.DepartId);
                    upload = db.UploadTb.Where(m => m.Department == user.DepartName);

                }
                else if (Session["Type"].ToString() == "2")//管理员登陆
                {
                    upload = db.UploadTb;
                    ViewBag.style = "2";
                }

                #region 下载excel表格
                if (type == 1)
                {
                    SignListViewModel sl = (SignListViewModel)Session["table"];
                    //string[] excelHead = { "学号", "姓名", "学历", "院", "系", "班级", "单位名称", "隶属部门", "隶属部门代码", "单位性质", "单位性质代码", "单位地址", "单位联系人", "单位电话", "个人联系方式", "签约方式", "协议书编号", "所属集团或系统", "所属集团代码", "登记日期" };
                    string[] excelHead = { "xh", "xm", "xl", "szxy", "szyx", "bj", "dwmc", "dwlsbm", "dwlsbmdm", "dwxz", "dwxzdm", "dwdz", "dwlxr", "lxrdh", "mobilephon", "签约方式", "xysbh", "dwzzjgdm", "所属集团代码", "登记日期" };
                    var workbook = new HSSFWorkbook();
                    //表格显示的名字
                    var sheet = workbook.CreateSheet("签约登记");
                    var col = sheet.CreateRow(0);
                    //遍历表头在exal表格中
                    for (int i = 0; i < excelHead.Length; i++)
                    {
                        //报表的头部
                        col.CreateCell(i).SetCellValue(excelHead[i]);
                    }
                    int a = 1;
                    //遍历表数据
                    foreach (var item in sl.uploadPagedList)
                    {
                        var row = sheet.CreateRow(a);
                        row.CreateCell(0).SetCellValue(item.StudentNumber);
                        row.CreateCell(1).SetCellValue(item.Name);
                        row.CreateCell(2).SetCellValue(item.Education);
                        row.CreateCell(3).SetCellValue(item.Academy);
                        row.CreateCell(4).SetCellValue(item.Department);
                        row.CreateCell(5).SetCellValue(item.Class);
                        if (item.signInfoModel != null)
                        {
                            row.CreateCell(6).SetCellValue(item.signInfoModel.CompanyName);
                            row.CreateCell(7).SetCellValue(item.signInfoModel.ComBelongDep);
                            row.CreateCell(8).SetCellValue(item.signInfoModel.ComBelongDepCode);
                            row.CreateCell(9).SetCellValue(item.signInfoModel.ComType);
                            row.CreateCell(10).SetCellValue(item.signInfoModel.ComTypeCode);
                            row.CreateCell(11).SetCellValue(item.signInfoModel.CompanyAddress);
                            row.CreateCell(12).SetCellValue(item.signInfoModel.CompanyConn);
                            row.CreateCell(13).SetCellValue(item.signInfoModel.CompanyTel);
                            row.CreateCell(14).SetCellValue(item.signInfoModel.PerTelType);
                            row.CreateCell(15).SetCellValue(item.signInfoModel.SignType);
                            row.CreateCell(16).SetCellValue(item.signInfoModel.AgreementID);
                            row.CreateCell(17).SetCellValue(item.signInfoModel.ComBelongDep);
                            row.CreateCell(18).SetCellValue(item.signInfoModel.ComBelongDepCode);
                            row.CreateCell(19).SetCellValue(item.signInfoModel.SignTime);
                        }
                        a++;
                    }
                    MemoryStream ms = new MemoryStream();
                    workbook.Write(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, "application/vnd.ms-excel", "签约登记表.xls");
                }
                #endregion

                var list = new SignListViewModel();
                list.Upload = new UploadModel();
                //list.uploadList = new List<FillBaseInfoViewModel>();

                foreach (var item in upload.ToList())
                {
                    SignInfoViewModel temp = new SignInfoViewModel();
                    temp.upload = new UploadModel();
                    temp.upload = item;
                    if (db.SingInfoTb.Find(item.StudentNumber) != null)
                        temp.signInfo = db.SingInfoTb.Find(item.StudentNumber);
                    //list.uploadList.Add(temp);
                }
                list.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
                Session["table"] = list;
                return View(list);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        /// <summary>
        /// 签约登记表格
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sign(SignListViewModel sl, int id = 0)
        {
            var UserId = Session["Id"];//用户Id
            var user = db.UserTb.Find(UserId);//查询出该用户
            ViewBag.type = user.TypeCode;
            if (Session["Type"].ToString() == "1")
            {
                ViewBag.type = "1";
            }
            else
            {
                ViewBag.type = "2";
            }

            #region 显示查询结果
            SignListViewModel display = new SignListViewModel();
            display.SignList = new List<SignInfoViewModel>();
            display.Upload = new UploadModel();
            display.Upload = sl.Upload;
            var upload = db.UploadTb.Include("signInfoModel").Where(m => m.Department == user.DepartName);
            if (Session["Type"].ToString() == "1")
            { upload = db.UploadTb.Include("signInfoModel").Where(m => m.Department == user.DepartName); }
            else
            { upload = db.UploadTb.Include("signInfoModel"); }
            if (sl.Upload.Name != null)//姓名
                upload = upload.Where(m => m.Name.Contains(sl.Upload.Name));
            if (sl.Upload.StudentNumber != null)//学号
                upload = upload.Where(m => m.StudentNumber.Contains(sl.Upload.StudentNumber));
            if (sl.Upload.StudentType != null)//学生类型
                upload = upload.Where(m => m.StudentType == sl.Upload.StudentType);
            foreach (var item in upload.ToList())
            {
                SignInfoViewModel temp = new SignInfoViewModel();
                temp.upload = new UploadModel();
                temp.upload = item;
                if (db.SingInfoTb.Find(item.StudentNumber) != null)
                    temp.signInfo = db.SingInfoTb.Find(item.StudentNumber);
                //disPlay.uploadList.Add(temp);
            }
            display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
            Session["table"] = display;
            return View(display);
            #endregion

        }
        #endregion

        #region 编辑签约登记表
        public ActionResult EditSign(string studentNumber)
        {

            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                #region 下拉栏初始化
                //签约方式
                List<SelectListItem> signType = new List<SelectListItem> {
                new SelectListItem{Text="校内",Value="校内",Selected=false},
                new SelectListItem{Text="校外",Value="校外",Selected=true}
            };
                ViewBag.signType = signType;

                //所属集团或系统
                List<SelectListItem> Group = new List<SelectListItem> {
                new SelectListItem{Text="国网",Value="国网",Selected=true},
                new SelectListItem{Text="南网",Value="南网",Selected=true},
                new SelectListItem{Text="大唐集团",Value="大唐集团",Selected=false},
                new SelectListItem{Text="华能集团",Value="华能集团",Selected=false},
                new SelectListItem{Text="国电集团",Value="国电集团",Selected=false},
                new SelectListItem{Text="华电集团",Value="华电集团",Selected=false},
                new SelectListItem{Text="中电投",Value="中电投",Selected=false},
                new SelectListItem{Text="中能建",Value="中能建",Selected=false},
                new SelectListItem{Text="中电建",Value="中电建",Selected=false},
                new SelectListItem{Text="国核集团",Value="国核集团",Selected=false},
                new SelectListItem{Text="中核集团",Value="中核集团",Selected=false},
                new SelectListItem{Text="中广核",Value="中广核",Selected=false},
                new SelectListItem{Text="华润集团",Value="华润集团",Selected=false},
                new SelectListItem{Text="神华集团",Value="神华集团",Selected=false},
                new SelectListItem{Text="京能集团",Value="京能集团",Selected=false},
                new SelectListItem{Text="浙能集团",Value="浙能集团",Selected=false},
                new SelectListItem{Text="深能集团",Value="深能集团",Selected=false},
                new SelectListItem{Text="粤电集团",Value="粤电集团",Selected=false},
                new SelectListItem{Text="国投电力",Value="国投电力",Selected=false},
                new SelectListItem{Text="其他地方能源集团",Value="其他地方能源集团",Selected=false},
                new SelectListItem{Text="金融类",Value="金融类",Selected=false},
                new SelectListItem{Text="IT类",Value="IT类",Selected=false},
                new SelectListItem{Text="设计类",Value="设计类",Selected=false},
                new SelectListItem{Text="地方企业",Value="地方企业",Selected=false},
                new SelectListItem{Text="教育行业",Value="教育行业",Selected=false},
                new SelectListItem{Text="银行",Value="银行",Selected=false},
                new SelectListItem{Text="通信行业",Value="通信行业",Selected=false},
                new SelectListItem{Text="交通运输业",Value="交通运输业",Selected=false},
                new SelectListItem{Text="机械机电",Value="机械机电",Selected=false},
                new SelectListItem{Text="其他",Value="其他",Selected=false},
            };
                ViewBag.Group = Group;

                var belong = db.belongDepTb.ToList();
                List<SelectListItem> belongDep = new List<SelectListItem> { };
                foreach (var item in belong)
                {
                    var belongDepList = new SelectListItem { Text = item.ComBelongDep, Value = item.ComBelongDepCode};
                    belongDep.Add(belongDepList);
                }
                ViewBag.belong = belongDep;

                #endregion

                SignInfoViewModel student = new SignInfoViewModel()
                {
                    signInfo = db.SingInfoTb.Find(studentNumber),
                    upload = db.UploadTb.Find(studentNumber)
                };
                if (student.signInfo == null)
                {
                    student.signInfo = new SignInfoModel();
                    student.signInfo.StudentNumber = studentNumber.ToString();
                }
             
                return View(student);
            }
            else
            { return RedirectToAction("Login"); }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSign(SignInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                string comType = students.signInfo.ComType;
                students.signInfo.ComType = comType.Substring(2);
                students.signInfo.ComTypeCode = comType.Substring(0, 2);
            
                var belongDep = db.belongDepTb.Where(m => m.ComBelongDepCode == students.signInfo.ComBelongDepCode).FirstOrDefault();
                students.signInfo.ComBelongDep = belongDep.ComBelongDep;
                //如果基本信息表中已经有基本，则为更新
                if (db.SingInfoTb.Find(students.signInfo.StudentNumber) != null)
                {
                    SignInfoModel temp = db.SingInfoTb.Find(students.signInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.signInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.SingInfoTb.Add(students.signInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EditSign", new { studentNumber = students.signInfo.StudentNumber });
            }
            return View();
        }
        #endregion

        #region 毕业生列表

        public ActionResult AdminGradList(string Idd = null, int type = 0, int id = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                var user = db.UserTb.Find(Session["Id"]);
                var upload = db.UploadTb.Where(m => m.Department == user.DepartName);
                ViewBag.type = user.TypeCode;
                #region 删除
                if (type == 2)
                {
                    var item = db.UploadTb.Find(Idd);
                    if (item == null)
                        RedirectToAction("AdminGradList");
                    db.UploadTb.Remove(item);
                    db.SaveChanges();
                }
                #endregion

                if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
                {
                    ViewBag.style = "1";
                    var userDep = db.DepartmentTb.Find(user.DepartId);
                    upload = db.UploadTb.Where(m => m.Department == user.DepartName);
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                    ViewBag.major = majorList;
                    #endregion
                }
                else if (Session["Type"].ToString() == "2")//管理员登陆
                {
                    upload = db.UploadTb;
                    ViewBag.style = "2";
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    List<SelectListItem> acaList = new List<SelectListItem>();
                    if (aca != null)
                    {
                        foreach (var item in aca)
                        { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                    }
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    List<SelectListItem> depList = new List<SelectListItem>();
                    if (dep != null)
                    {
                        foreach (var item in dep)
                        {
                            depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }

                #region 下载表格
                if (type == 3)
                {
                    AdminGradViewModel s1 = (AdminGradViewModel)Session["table"];
                    string[] excelHead = { "xh", "xm", "xb", "xbdm", "mz", "mzdm", "csrq", "sfzh", "原考生号", "ksh", "sfslbdm", "学籍变动代码", "yxmc", "yxdm", "院校隶属部门代码", "yxszsdm", "szxy", "szyx", "bj", "jxb", "专业名称", "zy", "zyfx", "zzmm", "zzmmdm", "健康状况", "是否曾转专业", "转系前专业", "宿舍楼号", "宿舍号", "bzr", "syszdgkkq", "生源所在地级市", "生源所在地级市的区或县级市或县", "syszd", "syszddm", "基本信息是否审核", "基本信息是否锁定", "就业信息是否审核", "就业信息是否锁定", "jthkszd", "家庭户口所在地代码", "jthklx", "dsznbz", "knslbdm", "jtknjb", "dasfzrxx", "rxqhkszdpcs", "fqxm", "mqxm", "mqgzdw", "fqgzdw", "jtdz", "jtyb", "jtdh", "mobilephon", "qq", "Email", "xl", "xldm", "xz", "pyfs", "pyfsdm", "dxhwpdw", "入学年份", "rxsj", "bysj", "在校任职情况代码", "综合排名", "排名方式", "英语通过级别", "四级成绩", "六级成绩", "计算机级别", "获奖1", "获奖2", "获奖3", "在校期间其他荣誉及奖励", "求职备注", "xysbh", "dwmc", "dwzzjgdm", "单位上级隶属部门", "dwhy", "工作职务", "dwmc", "dwxz", "dwlxr", "dwyb", "dwdz", "lxrzw", "lxrdh", "hkqzdz", "dazjdwdz", "dwyb", "是否专业对口", "升学院校", "升学专业", "出国国家", "zzjgdm", "byqxdm" };
                   // string[] excelHead = { "学号", "姓名", "性别", "性别代码", "民族", "民族代码", "出生日期", "身份证号", "原考生号", "考生号", "师范生类别代码", "学籍变动代码", "院校名称", "院校代码", "院校隶属部门代码", "院校所在地代码", "学院", "系", "班级", "教学班", "专业名称", "专业代码", "专业方向", "政治面貌", "政治面貌代码", "健康状况", "是否曾转专业", "转系前专业", "宿舍楼号", "宿舍号", "班主任", "生源所在地省", "生源所在地级市", "生源所在地级市的区或县级市或县", "生源所在地", "生源所在地代码", "基本信息是否审核", "基本信息是否锁定", "就业信息是否审核", "就业信息是否锁定", "家庭户口所在地", "家庭户口所在地代码", "家庭户口类型", "独生子女标志", "困难生类别代码", "家庭困难级别", "档案是否转入学校", "入学前户口所在地派出所", "父亲姓名", "母亲姓名", "父亲工作单位", "母亲工作单位", "家庭详细通信住址", "家庭所在地邮编", "家庭电话", "个人手机", "QQ号码", "Email", "学历", "学历代码", "学制", "培养方式", "培养方式代码", "定向或委培单位", "入学年份", "入学时间", "毕业时间", "在校任职情况代码", "综合排名", "排名方式", "英语通过级别", "四级成绩", "六级成绩", "计算机级别", "获奖1", "获奖2", "获奖3", "在校期间其他荣誉及奖励", "求职备注", "协议书编号", "单位名称", "单位所属集团或系统", "单位上级隶属部门", "单位行业", "工作职务", "开具单位名称", "单位性质", "联系人", "联系人邮编", "联系人通信地址", "联系人职务", "联系人电话", "户口迁移地址", "档案转寄详细地址", "邮政编码", "是否专业对口", "升学院校", "升学专业", "出国国家", "单位组织机构代码", "毕业去向" };
                    var workhood = new HSSFWorkbook();
                    var sheet = workhood.CreateSheet("基本信息表");
                    var col = sheet.CreateRow(0);
                    for (int i = 0; i < excelHead.Length; i++)
                    {
                        col.CreateCell(i).SetCellValue(excelHead[i]);
                    }
                    int a = 1;
                    foreach (var item in s1.uploadPagedList)
                    {
                        if (item.fillBaseInfoModel == null)
                            item.fillBaseInfoModel = new FillBaseInfoModel();
                        if (item.applInfoModel == null)
                            item.applInfoModel = new ApplInfoModel();
                        if (item.eSchoolInfoModel == null)
                            item.eSchoolInfoModel = new ESchoolInfoModel();
                        var row = sheet.CreateRow(a);
                        row.CreateCell(0).SetCellValue(item.StudentNumber);
                        row.CreateCell(1).SetCellValue(item.Name);
                        row.CreateCell(2).SetCellValue(item.Sex);
                        row.CreateCell(3).SetCellValue(item.SexCode);
                        row.CreateCell(4).SetCellValue(item.Nation);
                        row.CreateCell(5).SetCellValue(item.NationCode);
                        row.CreateCell(6).SetCellValue(item.BirthTime);
                        row.CreateCell(7).SetCellValue(item.IDNumber);
                        row.CreateCell(8).SetCellValue(item.YKSH);
                        row.CreateCell(9).SetCellValue(item.KSH);
                        row.CreateCell(10).SetCellValue(item.SFstudentCode);
                        row.CreateCell(11).SetCellValue(item.ChangeCode);
                        row.CreateCell(12).SetCellValue(item.School);
                        row.CreateCell(13).SetCellValue(item.SchoolCode);
                        row.CreateCell(14).SetCellValue(item.SchoolBeCode);
                        row.CreateCell(15).SetCellValue(item.SchoolAddCode);
                        row.CreateCell(16).SetCellValue(item.Academy);
                        row.CreateCell(17).SetCellValue(item.Department);
                        row.CreateCell(18).SetCellValue(item.Class);
                        row.CreateCell(19).SetCellValue(item.TeachingClass);
                        row.CreateCell(20).SetCellValue(item.Major);
                        row.CreateCell(21).SetCellValue(item.MajorCode);
                        row.CreateCell(22).SetCellValue(item.MajorDirection);
                        row.CreateCell(23).SetCellValue(item.fillBaseInfoModel.PoliticalStatus);
                        row.CreateCell(24).SetCellValue(item.fillBaseInfoModel.PoliticalStatusCode);
                        row.CreateCell(25).SetCellValue(item.fillBaseInfoModel.Health);
                        row.CreateCell(26).SetCellValue(item.fillBaseInfoModel.IsChangeMaj);
                        row.CreateCell(27).SetCellValue(item.fillBaseInfoModel.BeforeMajor);
                        row.CreateCell(28).SetCellValue(item.fillBaseInfoModel.Dormitory);
                        row.CreateCell(29).SetCellValue(item.fillBaseInfoModel.RoomNumber);
                        row.CreateCell(30).SetCellValue(item.fillBaseInfoModel.ClassTeacher);
                        row.CreateCell(31).SetCellValue(item.fillBaseInfoModel.OriginProvince);
                        row.CreateCell(32).SetCellValue(item.fillBaseInfoModel.OriginCity);
                        row.CreateCell(33).SetCellValue(item.fillBaseInfoModel.OriginCounty);
                        row.CreateCell(34).SetCellValue(item.fillBaseInfoModel.Origin);
                        row.CreateCell(35).SetCellValue(item.fillBaseInfoModel.OriginCode);
                        row.CreateCell(36).SetCellValue(item.fillBaseInfoModel.IsBaseChecked);
                         row.CreateCell(37).SetCellValue(item.fillBaseInfoModel.IsClocked);
                         row.CreateCell(38).SetCellValue(item.eSchoolInfoModel.IsChecked);
                         row.CreateCell(39).SetCellValue(item.eSchoolInfoModel.IsClock);
                        row.CreateCell(40).SetCellValue(item.fillBaseInfoModel.ResLocation);
                        row.CreateCell(41).SetCellValue(item.fillBaseInfoModel.ResLocationCode);
                        row.CreateCell(42).SetCellValue(item.fillBaseInfoModel.ResLocationType);
                        row.CreateCell(43).SetCellValue(item.fillBaseInfoModel.IsOnlyChild);
                        row.CreateCell(44).SetCellValue(item.fillBaseInfoModel.DiffLevelCode);
                        row.CreateCell(45).SetCellValue(item.fillBaseInfoModel.FDiffLevel);
                        row.CreateCell(46).SetCellValue(item.fillBaseInfoModel.IsArchivesToSch);
                        row.CreateCell(47).SetCellValue(item.fillBaseInfoModel.PoliceBefore);
                        row.CreateCell(48).SetCellValue(item.fillBaseInfoModel.FatherName);
                        row.CreateCell(49).SetCellValue(item.fillBaseInfoModel.MotherName);
                        row.CreateCell(50).SetCellValue(item.fillBaseInfoModel.FatherWorkUnit);
                        row.CreateCell(51).SetCellValue(item.fillBaseInfoModel.MotherWorkUnit);
                        row.CreateCell(52).SetCellValue(item.fillBaseInfoModel.FamilyLocation);
                        row.CreateCell(53).SetCellValue(item.fillBaseInfoModel.FamilyZipCode);
                        row.CreateCell(54).SetCellValue(item.fillBaseInfoModel.FamilyTel);
                        row.CreateCell(55).SetCellValue(item.fillBaseInfoModel.TelNumber);
                        row.CreateCell(56).SetCellValue(item.fillBaseInfoModel.QQ);
                        row.CreateCell(57).SetCellValue(item.fillBaseInfoModel.Email);
                        row.CreateCell(58).SetCellValue(item.Education);
                        row.CreateCell(59).SetCellValue(item.EducationCode);
                        row.CreateCell(60).SetCellValue(item.LengthOfSch);
                        row.CreateCell(61).SetCellValue(item.TrainType);
                        row.CreateCell(62).SetCellValue(item.TrainTypeCode);
                        row.CreateCell(63).SetCellValue(item.Weituo);
                        row.CreateCell(64).SetCellValue(item.EntranceYear);
                        row.CreateCell(65).SetCellValue(item.EntranceTime);
                        row.CreateCell(66).SetCellValue(item.GraduationTime);
                        row.CreateCell(67).SetCellValue(item.applInfoModel.PostAtSch);//在校任职情况代码
                        row.CreateCell(68).SetCellValue(item.applInfoModel.NM);
                        row.CreateCell(69).SetCellValue(item.applInfoModel.Rank);
                        //少一个英语语种
                        row.CreateCell(70).SetCellValue(item.applInfoModel.EnglishLevel);
                        row.CreateCell(71).SetCellValue(item.applInfoModel.LevelFour);
                        row.CreateCell(72).SetCellValue(item.applInfoModel.LevelSix);
                        row.CreateCell(73).SetCellValue(item.applInfoModel.ComputerLevel);
                        row.CreateCell(74).SetCellValue(item.applInfoModel.FristYearPrize);
                        row.CreateCell(75).SetCellValue(item.applInfoModel.SecondYearPrize);
                        row.CreateCell(76).SetCellValue(item.applInfoModel.ThirdYearPrize);
                        row.CreateCell(77).SetCellValue(item.applInfoModel.BriefPrize);
                        row.CreateCell(78).SetCellValue(item.applInfoModel.ApplNote);
                        row.CreateCell(79).SetCellValue(item.eSchoolInfoModel.AgreementID);
                        row.CreateCell(80).SetCellValue(item.eSchoolInfoModel.CompanyName);
                        row.CreateCell(81).SetCellValue(item.eSchoolInfoModel.ComBelongGroup);
                        row.CreateCell(82).SetCellValue(item.eSchoolInfoModel.ComBelongDep);
                        row.CreateCell(83).SetCellValue(item.eSchoolInfoModel.ComIndustry);
                        row.CreateCell(84).SetCellValue(item.eSchoolInfoModel.JobTitle);
                        row.CreateCell(85).SetCellValue(item.eSchoolInfoModel.SCompanyName);
                        row.CreateCell(86).SetCellValue(item.eSchoolInfoModel.ComType);
                        row.CreateCell(87).SetCellValue(item.eSchoolInfoModel.Contacts);
                        row.CreateCell(88).SetCellValue(item.eSchoolInfoModel.ContactsCode);
                        row.CreateCell(89).SetCellValue(item.eSchoolInfoModel.ConCommAddress);
                        row.CreateCell(90).SetCellValue(item.eSchoolInfoModel.ConPost);
                        row.CreateCell(91).SetCellValue(item.eSchoolInfoModel.ConTel);
                        row.CreateCell(92).SetCellValue(item.eSchoolInfoModel.ResChangedAdd);
                        row.CreateCell(93).SetCellValue(item.eSchoolInfoModel.FileChangedAdd);
                        row.CreateCell(94).SetCellValue(item.eSchoolInfoModel.FileZipCode);
                        row.CreateCell(95).SetCellValue(item.eSchoolInfoModel.IsMajorFit);
                        row.CreateCell(96).SetCellValue(item.eSchoolInfoModel.UPSchool);
                        row.CreateCell(97).SetCellValue(item.eSchoolInfoModel.UpMajor);
                        row.CreateCell(98).SetCellValue(item.eSchoolInfoModel.OutCountry);
                        row.CreateCell(99).SetCellValue(item.eSchoolInfoModel.CompanyCode);
                        row.CreateCell(100).SetCellValue(item.eSchoolInfoModel.Employment);
                        a++;

                    }
                    MemoryStream ms = new MemoryStream();
                    workhood.Write(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, "application/vnd.ms-excel", "毕业生信息.xls");
                }
                #endregion

                //table表格中显示的内容
                var list = new AdminGradViewModel();
                list.upload = new UploadModel();
             
                foreach (var item in upload.ToList())
                {
                    FillBaseInfoViewModel temp = new FillBaseInfoViewModel();
                    temp.upload = new UploadModel();
                    temp.upload = item;
                    if (db.BaseInfoTb.Find(item.StudentNumber) != null)
                        temp.baseInfo = db.BaseInfoTb.Find(item.StudentNumber);
                    //list.uploadList.Add(temp);
                }
                list.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
                Session["table"] = list;
                return View(list);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminGradList(AdminGradViewModel av, string action, int id = 0)
        {
            var user = db.UserTb.Find(Session["Id"]);
            ViewBag.type = user.TypeCode;
            if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
            {
                ViewBag.style = "1";
                #region 院系列表
                var userDep = db.DepartmentTb.Find(user.DepartId);
                //院列表
                var aca = db.AcademyTb.ToList();
                SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }
            else
            {
                ViewBag.style = "2";
                #region 院系列表
                //院列表
                var aca = db.AcademyTb.ToList();
                List<SelectListItem> acaList = new List<SelectListItem>();
                if (aca != null)
                {
                    foreach (var item in aca)
                    { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                }
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                List<SelectListItem> depList = new List<SelectListItem>();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }

            #region 查询
            if (action == "查询")
            {
                AdminGradViewModel disPlay = new AdminGradViewModel();
     
               disPlay.upload = new UploadModel();
               var upload = db.UploadTb.Include("fillBaseInfoModel").Include("eSchoolInfoModel").Include("applInfoModel").Include("signInfoModel").Where(m => m.Department == user.DepartName);
                if (Session["Type"].ToString() == "1")
                { upload = db.UploadTb.Include("fillBaseInfoModel").Include("eSchoolInfoModel").Include("applInfoModel").Include("signInfoModel").Where(m => m.Department == user.DepartName); }
                else
                { upload = db.UploadTb.Include("fillBaseInfoModel").Include("eSchoolInfoModel").Include("applInfoModel").Include("signInfoModel"); }
                if (av.upload.Name != null)//姓名
                    upload = upload.Where(m => m.Name.Contains(av.upload.Name));
                if (av.upload.StudentNumber != null)//学号
                    upload = upload.Where(m => m.StudentNumber.Contains(av.upload.StudentNumber));
                if (av.upload.EntranceYear != null)//入学时间
                    upload = upload.Where(m => m.EntranceYear == av.upload.EntranceYear);
                if (av.upload.GraduationTime != null)//毕业时间
                    upload = upload.Where(m => m.GraduationTime == av.upload.GraduationTime);
                if (av.upload.Academy != null && av.upload.Academy != "0")
                {
                    int Id = Convert.ToInt32(av.upload.Academy);
                    var acaTemp = db.AcademyTb.Find(Id);
                    upload = upload.Where(m => m.Academy == acaTemp.Name);
                    disPlay.upload.Academy = acaTemp.Id.ToString();
                }
                if (av.upload.Department != null && av.upload.Department != "0")
                {
                    int Id = Convert.ToInt32(av.upload.Department);
                    var depTemp = db.DepartmentTb.Find(Id);
                    upload = upload.Where(m => m.Department == depTemp.Name);
                    disPlay.upload.Department = depTemp.Id.ToString();
                }
                if (av.upload.Major != null && av.upload.Major != "0")//专业
                {
                    int Id = Convert.ToInt16(av.upload.Major);
                    var majorTemp = db.MajorTb.Find(Id);
                    upload = upload.Where(m => m.Major == majorTemp.Name);
                    disPlay.upload.Academy = majorTemp.AcademyId;
                    disPlay.upload.Department = majorTemp.DepartId;
                }
                if (av.upload.StudentType != null)
                    upload = upload.Where(m => m.StudentType == av.upload.StudentType);
                foreach (var item in upload.ToList())
                {
                    FillBaseInfoViewModel temp = new FillBaseInfoViewModel();
                    temp.upload = new UploadModel();
                    temp.upload = item;
                    if (db.BaseInfoTb.Find(item.StudentNumber) != null)
                        temp.baseInfo = db.BaseInfoTb.Find(item.StudentNumber);
                    //disPlay.uploadList.Add(temp);
                }
                disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);             
                Session["table"] = disPlay;
                return View(disPlay);
            }
            #endregion

            #region 添加毕业生信息
            if (action == "添加毕业生信息")
            {
                RedirectToAction("AddGradInfo");
            }
            #endregion
            return View();
        }
        #endregion

        #region 添加毕业生信息
        public ActionResult AddGradInfo()
        {
            #region 初始化数据

            List<SelectListItem> Political = new List<SelectListItem> {
                new SelectListItem{Text="共产党员",Value="01共产党员",Selected=false},
                new SelectListItem{Text="中共预备党员",Value="02中共预备党员",Selected=false},
                new SelectListItem{Text="共青团员",Value="03共青团员",Selected=false},
                new SelectListItem{Text="民革会员",Value="04民革会员",Selected=false},
                new SelectListItem{Text="民盟盟员",Value="05民盟盟员",Selected=false},
                new SelectListItem{Text="民建会员",Value="06民建会员",Selected=false},
                new SelectListItem{Text="民进会员",Value="07民进会员",Selected=false},
                new SelectListItem{Text="农工党党员",Value="08农工党党员",Selected=false},
                new SelectListItem{Text="致公党党员",Value="09致公党党员",Selected=false},
                new SelectListItem{Text="九三学社社员",Value="10九三学社社员",Selected=false},
                new SelectListItem{Text="台盟盟员",Value="11台盟盟员",Selected=false},
                new SelectListItem{Text="无党派民主人士",Value="12无党派民主人士",Selected=false},
                new SelectListItem{Text="群众",Value="13群众",Selected=true},
            };
            ViewBag.Political = Political;


            List<SelectListItem> dor = new List<SelectListItem> {
                new SelectListItem{Text="学1舍",Value="学1舍",Selected=false},
                new SelectListItem{Text="学2舍",Value="学2舍",Selected=false},
                new SelectListItem{Text="学3舍",Value="学3舍",Selected=false},
                new SelectListItem{Text="学4舍",Value="学4舍",Selected=false},
                new SelectListItem{Text="学5舍",Value="学5舍",Selected=false},
                new SelectListItem{Text="学6舍",Value="学6舍",Selected=false},
                new SelectListItem{Text="学7舍",Value="学7舍",Selected=false},
                new SelectListItem{Text="学8舍",Value="学8舍",Selected=false},
                new SelectListItem{Text="学9舍",Value="学9舍",Selected=false},
                new SelectListItem{Text="学10舍",Value="学10舍",Selected=false},
                new SelectListItem{Text="学11舍",Value="学11舍",Selected=false},
                new SelectListItem{Text="学12舍",Value="学12舍",Selected=false},
                new SelectListItem{Text="学13舍",Value="学13舍",Selected=false},
                new SelectListItem{Text="学14舍",Value="学14舍",Selected=false},
                new SelectListItem{Text="学15舍",Value="学15舍",Selected=false},
                new SelectListItem{Text="学16舍",Value="学16舍",Selected=false},
                new SelectListItem{Text="学17舍",Value="学17舍",Selected=false},
                new SelectListItem{Text="学18舍",Value="学18舍",Selected=false},
                new SelectListItem{Text="研究生公寓",Value="研究生公寓",Selected=true},
            };
            ViewBag.dor = dor;

            ///家庭户口类型
            List<SelectListItem> type = new List<SelectListItem> {
                new SelectListItem{Text="省会及直辖市",Value="1",Selected=false},
                new SelectListItem{Text="地级市",Value="2",Selected=false},
                new SelectListItem{Text="县或县级市",Value="3",Selected=false},
                new SelectListItem{Text="乡镇村",Value="4",Selected=false}
            };
            ViewBag.ftype = type;

            //民族
            var nation = db.nationTb.ToList();
            List<SelectListItem> nationlist = new List<SelectListItem> { };
            foreach (var item in nation)
            {
                var listItem = new SelectListItem { Text = item.NationName, Value = item.NationCode + item.NationName };
                nationlist.Add(listItem);
            }
            ViewBag.nation = nationlist;

            //生源所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var list = db.LocationTb.ToList();
            foreach (var item in list)
                cityList.Add(new SelectListItem { Text = item.name, Value = item.code });

            ViewBag.city = cityList;

            //家庭户口所在地
            List<SelectListItem> familyCityList = new List<SelectListItem>();

            foreach (var item in list)
                familyCityList.Add(new SelectListItem { Text = item.name, Value = item.code });

            ViewBag.familyCityList = familyCityList;

            #endregion

            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                return View();
            }
            else
            { return RedirectToAction("Login"); }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGradInfo(UploadModel upLoadModel)
        {
            if (ModelState.IsValid)
            {
                if (db.UploadTb.Find(upLoadModel.StudentNumber) == null)
                {
                    upLoadModel.School = "华北电力大学(保定)";
                    upLoadModel.SchoolCode = "10079";
                    upLoadModel.SchoolBeCode = "360";
                    upLoadModel.SchoolAddCode = "130600";
                    upLoadModel.fillBaseInfoModel.StudentNumber = upLoadModel.StudentNumber;
                    var nation = upLoadModel.Nation;
                    upLoadModel.Nation = nation.Substring(2);
                    upLoadModel.NationCode = upLoadModel.Nation.Substring(0, 2);
                    var orig = db.LocationTb.Find(upLoadModel.fillBaseInfoModel.OriginCode);//保存生源地
                    upLoadModel.fillBaseInfoModel.OriginCity = orig.name;
                    upLoadModel.fillBaseInfoModel.OriginProvince = db.BaseInfoTb.Find(upLoadModel.fillBaseInfoModel.StudentNumber).OriginProvince;
                    var familyLocation = db.LocationTb.Find(upLoadModel.fillBaseInfoModel.ResLocationCode);
                    upLoadModel.fillBaseInfoModel.ResLocation = familyLocation.name;
                    db.UploadTb.Add(upLoadModel);
                    db.SaveChanges();
                }
                else
                {
                    return Content("<script>alert('用户已存在');location.href='/Admin/AddGradInfo';</script>");
                }

            }
            return RedirectToAction("AddGradInfo");
        }
        #endregion

        #region 编辑毕业生信息
        public ActionResult AdminEditGradInfo(string studentNumber)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                #region 下拉框数据
                //政治面貌
                List<SelectListItem> Political = new List<SelectListItem> {
                new SelectListItem{Text="共产党员",Value="01共产党员",Selected=false},
                new SelectListItem{Text="中共预备党员",Value="02中共预备党员",Selected=false},
                new SelectListItem{Text="共青团员",Value="03共青团员",Selected=false},
                new SelectListItem{Text="民革会员",Value="04民革会员",Selected=false},
                new SelectListItem{Text="民盟盟员",Value="05民盟盟员",Selected=false},
                new SelectListItem{Text="民建会员",Value="06民建会员",Selected=false},
                new SelectListItem{Text="民进会员",Value="07民进会员",Selected=false},
                new SelectListItem{Text="农工党党员",Value="08农工党党员",Selected=false},
                new SelectListItem{Text="致公党党员",Value="09致公党党员",Selected=false},
                new SelectListItem{Text="九三学社社员",Value="10九三学社社员",Selected=false},
                new SelectListItem{Text="台盟盟员",Value="11台盟盟员",Selected=false},
                new SelectListItem{Text="无党派民主人士",Value="12无党派民主人士",Selected=false},
                new SelectListItem{Text="群众",Value="13群众",Selected=true},
            };
                ViewBag.Political = Political;

                //宿舍楼
                List<SelectListItem> dor = new List<SelectListItem> {
                new SelectListItem{Text="学1舍",Value="学1舍",Selected=false},
                new SelectListItem{Text="学2舍",Value="学2舍",Selected=false},
                new SelectListItem{Text="学3舍",Value="学3舍",Selected=false},
                new SelectListItem{Text="学4舍",Value="学4舍",Selected=false},
                new SelectListItem{Text="学5舍",Value="学5舍",Selected=false},
                new SelectListItem{Text="学6舍",Value="学6舍",Selected=false},
                new SelectListItem{Text="学7舍",Value="学7舍",Selected=false},
                new SelectListItem{Text="学8舍",Value="学8舍",Selected=false},
                new SelectListItem{Text="学9舍",Value="学9舍",Selected=false},
                new SelectListItem{Text="学10舍",Value="学10舍",Selected=false},
                new SelectListItem{Text="学11舍",Value="学11舍",Selected=false},
                new SelectListItem{Text="学12舍",Value="学12舍",Selected=false},
                new SelectListItem{Text="学13舍",Value="学13舍",Selected=false},
                new SelectListItem{Text="学14舍",Value="学14舍",Selected=false},
                new SelectListItem{Text="学15舍",Value="学15舍",Selected=false},
                new SelectListItem{Text="学16舍",Value="学16舍",Selected=false},
                new SelectListItem{Text="学17舍",Value="学17舍",Selected=false},
                new SelectListItem{Text="学18舍",Value="学18舍",Selected=false},
                new SelectListItem{Text="研究生公寓",Value="研究生公寓",Selected=true},
            };
                ViewBag.dor = dor;

                ///家庭户口类型
                List<SelectListItem> type = new List<SelectListItem> {
                new SelectListItem{Text="省会及直辖市",Value="1",Selected=false},
                new SelectListItem{Text="地级市",Value="2",Selected=false},
                new SelectListItem{Text="县或县级市",Value="3",Selected=false},
                new SelectListItem{Text="乡镇村",Value="4",Selected=false}
            };
                ViewBag.ftype = type;

                //生源所在地
                List<SelectListItem> cityList = new List<SelectListItem>();
                var st = db.BaseInfoTb.Find(studentNumber);
                if (st.OriginCode != null)
                    cityList.Add(new SelectListItem { Text = st.OriginCity, Value = st.OriginCode });
                else
                {
                    var list = db.LocationTb.Where(m => m.name.Contains(st.OriginProvince)).ToList();
                    foreach (var item in list)
                        cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
                }
                ViewBag.city = cityList;

                //家庭户口所在地
                List<SelectListItem> familyCityList = new List<SelectListItem>();
                var fcl = db.BaseInfoTb.Find(studentNumber);
                if (st.ResLocationCode != null)
                    familyCityList.Add(new SelectListItem { Text = st.ResLocation, Value = st.ResLocationCode });
                else
                {
                    var list = db.LocationTb.ToList();
                    foreach (var item in list)
                        familyCityList.Add(new SelectListItem { Text = item.name, Value = item.code });
                }
                ViewBag.familyCityList = familyCityList;

                //民族
                List<SelectListItem> nation = new List<SelectListItem>();
                var nationlist = db.nationTb.ToList();
                foreach (var item in nationlist)
                {
                    nation.Add(new SelectListItem { Text = item.NationName, Value = item.NationCode });
                }
                ViewBag.nation = nation;

                #endregion
                FillBaseInfoViewModel fillBaseInfoViewModel = new FillBaseInfoViewModel()
                {
                    baseInfo = db.BaseInfoTb.Find(studentNumber),
                    upload = db.UploadTb.Find(studentNumber)
                };
                if (fillBaseInfoViewModel.baseInfo == null)
                {
                    fillBaseInfoViewModel.baseInfo = new FillBaseInfoModel();
                    fillBaseInfoViewModel.baseInfo.StudentNumber = studentNumber;
                }
                fillBaseInfoViewModel.baseInfo.PoliticalStatus = fillBaseInfoViewModel.baseInfo.PoliticalStatusCode + fillBaseInfoViewModel.baseInfo.PoliticalStatus;

                fillBaseInfoViewModel.upload.Nation = fillBaseInfoViewModel.upload.NationCode + fillBaseInfoViewModel.upload.Nation;

                return View(fillBaseInfoViewModel);
            }
            else
            { return RedirectToAction("Login"); }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEditGradInfo(FillBaseInfoViewModel fillBaseInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                string PoliticalStatus = fillBaseInfoViewModel.baseInfo.PoliticalStatus;
                fillBaseInfoViewModel.baseInfo.PoliticalStatusCode = PoliticalStatus.Substring(0, 2);
                fillBaseInfoViewModel.baseInfo.PoliticalStatusCode = PoliticalStatus.Substring(2);

                //存入民族代码
                var nation = db.nationTb.Where(m=>m.NationCode==fillBaseInfoViewModel.upload.NationCode).FirstOrDefault();
                fillBaseInfoViewModel.upload.Nation = nation.NationName;

                var orig = db.LocationTb.Find(fillBaseInfoViewModel.baseInfo.OriginCode);//保存生源地
                fillBaseInfoViewModel.baseInfo.OriginCity = orig.name;
                fillBaseInfoViewModel.baseInfo.OriginProvince = db.BaseInfoTb.Find(fillBaseInfoViewModel.baseInfo.StudentNumber).OriginProvince;
                var familyLocation = db.LocationTb.Find(fillBaseInfoViewModel.baseInfo.ResLocationCode);
                fillBaseInfoViewModel.baseInfo.ResLocation = familyLocation.name;
                //如果有基本信息表中已经有基本，则为更新
                if (db.BaseInfoTb.Find(fillBaseInfoViewModel.baseInfo.StudentNumber) != null)
                {
                    FillBaseInfoModel temp = db.BaseInfoTb.Find(fillBaseInfoViewModel.baseInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(fillBaseInfoViewModel.baseInfo);
                    //UploadModel upload = db.UploadTb.Find(fillBaseInfoViewModel.baseInfo.StudentNumber);
                    //db.Entry(upload).CurrentValues.SetValues(fillBaseInfoViewModel.baseInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.UploadTb.Add(fillBaseInfoViewModel.upload);
                    db.BaseInfoTb.Add(fillBaseInfoViewModel.baseInfo);
                    db.SaveChanges();

                }
                return RedirectToAction("AdminEditGradInfo", new { studentNumber=fillBaseInfoViewModel.upload.StudentNumber});
            }

            return View(fillBaseInfoViewModel);
        }
        #endregion

        #region 求职信息审核

        public ActionResult ApplExam(int id = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                var user = db.UserTb.Find(Session["Id"]);
                var upload = db.UploadTb.Include("ApplInfoModel").Where(m => m.Department == user.DepartName);
                ViewBag.type = user.TypeCode;
                if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
                {
                    upload = db.UploadTb.Include("ApplInfoModel").Where(m => m.Department == user.DepartName);
                    ViewBag.style = "1";
                    var userDep = db.DepartmentTb.Find(user.DepartId);
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                else if (Session["Type"].ToString() == "2")//管理员登陆
                {
                    upload = db.UploadTb.Include("ApplInfoModel");
                    ViewBag.style = "2";
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    List<SelectListItem> acaList = new List<SelectListItem>();
                    if (aca != null)
                    {
                        foreach (var item in aca)
                        { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                    }
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    List<SelectListItem> depList = new List<SelectListItem>();
                    if (dep != null)
                    {
                        foreach (var item in dep)
                        {
                            depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                var list = new AppExamListViewModel();
                list.upload = new UploadModel();
                list.appInfoList = new List<ApplInfoViewModel>();

                foreach (var item in upload.ToList())
                {
                    ApplInfoViewModel temp = new ApplInfoViewModel();
                    temp.upload = new UploadModel();
                    temp.upload = item;
                    if (db.ApplInfoTb.Find(item.StudentNumber) != null)
                        temp.applInfo = db.ApplInfoTb.Find(item.StudentNumber);
                    //list.appInfoList.Add(temp);
                }
                list.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
                Session["table"] = list;

                return View(list);
            }
            else
                return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplExam(AppExamListViewModel av, int id = 0)
        {
            var user = db.UserTb.Find(Session["Id"]);
            ViewBag.type = user.TypeCode;
            if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
            {
                ViewBag.style = "1";
                #region 院系列表
                var userDep = db.DepartmentTb.Find(user.DepartId);
                //院列表
                var aca = db.AcademyTb.ToList();
                SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }
            else
            {
                ViewBag.style = "2";
                #region 院系列表
                //院列表
                var aca = db.AcademyTb.ToList();
                List<SelectListItem> acaList = new List<SelectListItem>();
                if (aca != null)
                {
                    foreach (var item in aca)
                    { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                }
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                List<SelectListItem> depList = new List<SelectListItem>();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }

            #region 查询
            AppExamListViewModel disPlay = new AppExamListViewModel();
            disPlay.appInfoList = new List<ApplInfoViewModel>();
            disPlay.upload = new UploadModel();
            disPlay.upload = av.upload;
            var upload = db.UploadTb.Include("applInfoModel").Where(m => m.Department == user.DepartName);
            if (Session["Type"].ToString() == "1")
            { upload = db.UploadTb.Include("applInfoModel").Where(m => m.Department == user.DepartName); }
            else
            { upload = db.UploadTb.Include("applInfoModel"); }

            if (av.upload.Name != null)//姓名
                upload = upload.Where(m => m.Name.Contains(av.upload.Name));
            if (av.upload.StudentNumber != null)//学号
                upload = upload.Where(m => m.StudentNumber.Contains(av.upload.StudentNumber));
            if (av.upload.EntranceYear != null)//入学时间
                upload = upload.Where(m => m.EntranceYear == av.upload.EntranceYear);
            if (av.upload.GraduationTime != null)//毕业时间
                upload = upload.Where(m => m.GraduationTime == av.upload.GraduationTime);
            if (av.upload.Academy != "0" && av.upload.Academy != null)
            {
                int Id = Convert.ToInt32(av.upload.Academy);
                var acaTemp = db.AcademyTb.Find(Id);
                upload = upload.Where(m => m.Academy == acaTemp.Name);
                disPlay.upload.Academy = acaTemp.Id.ToString();
            }
            if (av.upload.Department != "0" && av.upload.Department != null)
            {
                int Id = Convert.ToInt32(av.upload.Department);
                var depTemp = db.DepartmentTb.Find(Id);
                upload = upload.Where(m => m.Department == depTemp.Name);
                disPlay.upload.Department = depTemp.Id.ToString();
            }
            if (av.upload.Major != "0" && av.upload.Major != null)//专业
            {
                int Id = Convert.ToInt16(av.upload.Major);
                var majorTemp = db.MajorTb.Find(Id);
                upload = upload.Where(m => m.Major == majorTemp.Name);
                disPlay.upload.Academy = majorTemp.AcademyId;
                disPlay.upload.Department = majorTemp.DepartId;
            }
            if (av.upload.StudentType != null)//学生类型
                upload = upload.Where(m => m.StudentType == av.upload.StudentType);
            //从upload对应到求职信息
            foreach (var item in upload.ToList())
            {
                ApplInfoViewModel temp = new ApplInfoViewModel();
                temp.upload = new UploadModel();
                temp.upload = item;
                if (db.BaseInfoTb.Find(item.StudentNumber) != null)
                    temp.applInfo = db.ApplInfoTb.Find(item.StudentNumber);
                // disPlay.appInfoList.Add(temp);
            }
            //disPlay.uploadPagedList = db.UploadTb.Include("applInfoModel").OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
            disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);

            if (av.IsChecked != null)
            {
                if (av.IsChecked == "0")
                    //disPlay.appInfoList = disPlay.appInfoList.Where(m => m.applInfo.IsQiuChecked == av.IsChecked || m.applInfo.IsQiuChecked == null).ToList();

                    // disPlay.uploadPagedList= db.UploadTb.Include("applInfoModel").OrderBy(a => a.StudentNumber).Where(m => m.applInfoModel.IsQiuChecked == av.IsChecked || m.applInfoModel.IsQiuChecked == null).ToPagedList(id,10);
                    disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.applInfoModel.IsQiuChecked == av.IsChecked || m.applInfoModel.IsQiuChecked == null).ToPagedList(id, 10);
                else if (av.IsChecked == "1")
                    //disPlay.appInfoList = disPlay.appInfoList.Where(m => m.applInfo.IsQiuChecked == av.IsChecked).ToList();

                    disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.applInfoModel.IsQiuChecked == av.IsChecked).ToPagedList(id, 10);

            }
            if (av.IsFinish != null)
            {
                if (av.IsFinish == "0")
                    //disPlay.appInfoList = disPlay.appInfoList.Where(m => m.applInfo.IsFinish == av.IsFinish || m.applInfo.IsFinish == null).ToList();
                    disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.applInfoModel.IsFinish == av.IsFinish || m.applInfoModel.IsFinish == null).ToPagedList(id, 10);
                else if (av.IsFinish == "1")
                    // disPlay.appInfoList = disPlay.appInfoList.Where(m => m.applInfo.IsFinish == av.IsFinish).ToList();
                    disPlay.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.applInfoModel.IsFinish == av.IsFinish).ToPagedList(id, 10);
            }
            Session["table"] = disPlay;
            // disPlay.appInfoPageList = disPlay.appInfoList.OrderBy(a => a.upload.StudentNumber).ToPageList(Id, 10);
            #endregion
            return View(disPlay);
        }
        #endregion

        #region 编辑求职信息
        public ActionResult EditApplExam(string studentNumber)
        {
            #region 下拉栏的初始化
            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem{Text="学生干部",Value="1",Selected=false},
                new SelectListItem{Text="学生助理",Value="2",Selected=false},
                new SelectListItem{Text="社团干部",Value="3",Selected=false},
                new SelectListItem{Text="无",Value="0",Selected=true}
            };
            ViewBag.list = list;
            List<SelectListItem> english = new List<SelectListItem>{
                new SelectListItem{Text="四级",Value="四级",Selected=true},
                new SelectListItem{Text="六级",Value="六级",Selected=true},
            };
            ViewBag.english = english;
            #endregion

            var student = new ApplInfoViewModel()
            {
                applInfo = db.ApplInfoTb.Find(studentNumber),
                upload = db.UploadTb.Find(studentNumber)
            };
            ViewBag.studentType = student.upload.StudentType;
            ViewBag.numberCount = student.upload.StudentNumber.Length;
            if (student.applInfo == null)
            {
                student.applInfo = new ApplInfoModel();
                student.applInfo.StudentNumber = studentNumber;
            }
            var temp = db.BaseInfoTb.Find(studentNumber);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.H = temp.Health;//健康状况
                ViewBag.CC = temp.CommCode;//邮政编码
                ViewBag.CA = temp.CommAddress;//通信地址
                ViewBag.F = temp.FamilyLocation;//现家庭居住地
                ViewBag.E = temp.Email;//Email
                ViewBag.T = temp.TelNumber;//手机号
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditApplExam(ApplInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                //如果基本信息表中已经有基本，则为更新
                if (db.ApplInfoTb.Find(students.applInfo.StudentNumber) != null)
                {
                    ApplInfoModel temp = db.ApplInfoTb.Find(students.upload.StudentNumber);
                    //db.Entry(students.baseInfo).State = EntityState.Modified;
                    if (students.applInfo.RankType == "1")
                        students.applInfo.Rank = "班级排名";
                    else
                        students.applInfo.Rank = "专业排名";
                    students.applInfo.NM = students.applInfo.NumberN + "/" + students.applInfo.NumberM;
                    db.Entry(temp).CurrentValues.SetValues(students.applInfo);
                    db.SaveChanges();
                }
                else
                {
                    if (students.applInfo.RankType == "1")
                        students.applInfo.Rank = "班级排名";
                    else
                        students.applInfo.Rank = "专业排名";
                    students.applInfo.NM = students.applInfo.NumberN + "/" + students.applInfo.NumberM;
                    db.ApplInfoTb.Add(students.applInfo);
                    db.SaveChanges();
                }

                return RedirectToAction("FillApplInfo");
            }
            return View(students);
        }
        #endregion

        #region 就业信息审核
        /// <summary>
        /// 就业信息审核界面
        /// </summary>
        /// <returns></returns>
        public ActionResult EmplExam(int id = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                var user = db.UserTb.Find(Session["Id"]);
                var upload = db.UploadTb.Where(m => m.Department == user.DepartName);
                ViewBag.type = user.TypeCode;

                if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
                {
                    upload = db.UploadTb.Where(m => m.Department == user.DepartName);
                    ViewBag.style = "1";
                    var userDep = db.DepartmentTb.Find(user.DepartId);
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                else if (Session["Type"].ToString() == "2")//管理员登陆
                {
                    upload = db.UploadTb;
                    ViewBag.style = "2";
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    List<SelectListItem> acaList = new List<SelectListItem>();
                    if (aca != null)
                    {
                        foreach (var item in aca)
                        { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                    }
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    List<SelectListItem> depList = new List<SelectListItem>();
                    if (dep != null)
                    {
                        foreach (var item in dep)
                        {
                            depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                var list = new EmplExamListViewModel();
                list.upload = new UploadModel();
                list.InfoList = new List<ESchoolInfoViewModel>();
                foreach (var item in upload.ToList())
                {
                    ESchoolInfoViewModel temp = new ESchoolInfoViewModel();
                    temp.upload = new UploadModel();
                    temp.upload = item;
                    if (db.ESchoolInfoTb.Find(item.StudentNumber) != null)
                        temp.eSchoolInfo = db.ESchoolInfoTb.Find(item.StudentNumber);
                    // list.InfoList.Add(temp);
                }
                list.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
                Session["table"] = list;
                return View(list);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplExam(EmplExamListViewModel av, int id = 0)
        {
            var user = db.UserTb.Find(Session["Id"]);
            ViewBag.type = user.TypeCode;

            if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
            {
                ViewBag.style = "1";
                #region 院系列表
                var userDep = db.DepartmentTb.Find(user.DepartId);
                //院列表
                var aca = db.AcademyTb.ToList();
                SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }
            else
            {
                ViewBag.style = "2";
                #region 院系列表
                //院列表
                var aca = db.AcademyTb.ToList();
                List<SelectListItem> acaList = new List<SelectListItem>();
                if (aca != null)
                {
                    foreach (var item in aca)
                    { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                }
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                List<SelectListItem> depList = new List<SelectListItem>();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }

            #region 查询
            EmplExamListViewModel display = new EmplExamListViewModel();
            display.upload = new UploadModel();
            display.InfoList = new List<ESchoolInfoViewModel>();
            display.upload = av.upload;
            // var upload = db.UploadTb.Where(m => m.Department == user.DepartName);
            var upload = db.UploadTb.Include("eSchoolInfoModel").Where(m => m.Department == user.DepartName);
            if (Session["Type"].ToString() == "1")
            { upload = db.UploadTb.Include("eSchoolInfoModel").Where(m => m.Department == user.DepartName); }
            else
            { upload = db.UploadTb.Include("eSchoolInfoModel"); }

            if (av.upload.Name != null)//姓名
                upload = upload.Where(m => m.Name.Contains(av.upload.Name));
            if (av.upload.StudentNumber != null)//学号
                upload = upload.Where(m => m.StudentNumber.Contains(av.upload.StudentNumber));
            if (av.upload.EntranceYear != null)//入学时间
                upload = upload.Where(m => m.EntranceYear == av.upload.EntranceYear);
            if (av.upload.GraduationTime != null)//毕业时间
                upload = upload.Where(m => m.GraduationTime == av.upload.GraduationTime);
            if (av.upload.Academy != "0" && av.upload.Academy != null)
            {
                int Id = Convert.ToInt32(av.upload.Academy);
                var acaTemp = db.AcademyTb.Find(Id);
                upload = upload.Where(m => m.Academy == acaTemp.Name);
                display.upload.Academy = acaTemp.Id.ToString();
            }
            if (av.upload.Department != "0" && av.upload.Department != null)
            {
                int Id = Convert.ToInt32(av.upload.Department);
                var depTemp = db.DepartmentTb.Find(Id);
                upload = upload.Where(m => m.Department == depTemp.Name);
                display.upload.Department = depTemp.Id.ToString();
            }
            if (av.upload.Major != null & av.upload.Major != "0")//专业
            {
                int Id = Convert.ToInt16(av.upload.Major);
                var majorTemp = db.MajorTb.Find(Id);
                upload = upload.Where(m => m.Major == majorTemp.Name);
            }
            if (av.upload.StudentType != null)
                upload = upload.Where(m => m.StudentType == av.upload.StudentType);
            foreach (var item in upload.ToList())
            {
                ESchoolInfoViewModel temp = new ESchoolInfoViewModel();
                temp.upload = new UploadModel();
                temp.upload = item;
                if (db.BaseInfoTb.Find(item.StudentNumber) != null)
                    temp.eSchoolInfo = db.ESchoolInfoTb.Find(item.StudentNumber);
                //display.InfoList.Add(temp);
            }
            display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
            //是否通过审核
            if (av != null)
            {
                if (av.IsCheckEd == "0")
                    // display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsChecked == av.IsCheckEd || m.eSchoolInfo.IsChecked == null).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsChecked == av.IsCheckEd || m.applInfoModel.IsQiuChecked == null).ToPagedList(id, 10);
                else if (av.IsCheckEd == "1")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsChecked == av.IsCheckEd).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsChecked == av.IsCheckEd).ToPagedList(id, 10);
            }
            //是否锁定
            if (av.IsClocked != null)
            {
                if (av.IsClocked == "0")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsClock == av.IsClocked || m.eSchoolInfo.IsClock == null).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsClock == av.IsClocked || m.applInfoModel.IsClocked == null).ToPagedList(id, 10);
                else if (av.IsClocked == "1")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsClock == av.IsClocked).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsClock == av.IsClocked).ToPagedList(id, 10);
            }
            //是否就业
            if (av.IsJiuye != null)
            {
                if (av.IsJiuye == "0")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsJiuYe == av.IsJiuye || m.eSchoolInfo.IsClock == null).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsJiuYe == av.IsJiuye || m.eSchoolInfoModel.IsClock == null).ToPagedList(id, 10);
                else if (av.IsJiuye == "1")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.IsJiuYe == av.IsJiuye).ToList();
                    display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).Where(m => m.eSchoolInfoModel.IsJiuYe == av.IsJiuye).ToPagedList(id, 10);
            }
            //是否是第三方签学校
            if (av.IsESchool != null)
            {
                if (av.IsESchool == "0")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.EmploymentCode == null || m.eSchoolInfo.EmploymentCode != "10").ToList();
                    display.uploadPagedList = upload.OrderBy(m => m.StudentNumber).Where(m => m.eSchoolInfoModel.EmploymentCode == null || m.eSchoolInfoModel.EmploymentCode != "10").ToPagedList(id, 10);
                else if (av.IsESchool == "1")
                    //display.InfoList = display.InfoList.Where(m => m.eSchoolInfo.EmploymentCode == "10").ToList();
                    display.uploadPagedList = upload.OrderBy(m => m.StudentNumber).Where(m => m.eSchoolInfoModel.EmploymentCode == "10").ToPagedList(id, 10);
            }
            Session["table"] = display;
            return View(display);
            #endregion

        }

        #endregion

        #region 编辑就业信息

        #region 没有选择就业形势
        public ActionResult EditEmplExam(string studentNumber)
        {
            Session["adminStuNum"] = studentNumber;
            var student = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(studentNumber),
                upload = db.UploadTb.Find(studentNumber)
            };
            if (student.eSchoolInfo == null)
            {
                student.eSchoolInfo = new ESchoolInfoModel();
                student.eSchoolInfo.StudentNumber = studentNumber;
            }

            var temp = db.BaseInfoTb.Find(studentNumber);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            student.eSchoolInfo.JobTitle = student.eSchoolInfo.JobTitleCode + student.eSchoolInfo.JobTitle;//工作职位
            student.eSchoolInfo.ComIndustry = student.eSchoolInfo.ComIndustryCode + student.eSchoolInfo.ComIndustry;//单位行业
            student.eSchoolInfo.ComType = student.eSchoolInfo.ComTypeCode + student.eSchoolInfo.ComType;//单位性质
            student.eSchoolInfo.Employment = student.eSchoolInfo.EmploymentCode + student.eSchoolInfo.Employment;//就业形式
            return View(student);
        }

        #endregion

        #region 签学校三方协议就业
        public ActionResult EmplESchoolExam()
        {
            #region 下拉框初始化
            //单位所属集团或系统
            List<SelectListItem> Group = new List<SelectListItem> {
                new SelectListItem{Text="国网",Value="国网",Selected=true},
                new SelectListItem{Text="南网",Value="南网",Selected=true},
                new SelectListItem{Text="大唐集团",Value="大唐集团",Selected=false},
                new SelectListItem{Text="华能集团",Value="华能集团",Selected=false},
                new SelectListItem{Text="国电集团",Value="国电集团",Selected=false},
                new SelectListItem{Text="华电集团",Value="华电集团",Selected=false},
                new SelectListItem{Text="中电投",Value="中电投",Selected=false},
                new SelectListItem{Text="中能建",Value="中能建",Selected=false},
                new SelectListItem{Text="中电建",Value="中电建",Selected=false},
                new SelectListItem{Text="国核集团",Value="国核集团",Selected=false},
                new SelectListItem{Text="中核集团",Value="中核集团",Selected=false},
                new SelectListItem{Text="中广核",Value="中广核",Selected=false},
                new SelectListItem{Text="华润集团",Value="华润集团",Selected=false},
                new SelectListItem{Text="神华集团",Value="神华集团",Selected=false},
                new SelectListItem{Text="京能集团",Value="京能集团",Selected=false},
                new SelectListItem{Text="浙能集团",Value="浙能集团",Selected=false},
                new SelectListItem{Text="深能集团",Value="深能集团",Selected=false},
                new SelectListItem{Text="粤电集团",Value="粤电集团",Selected=false},
                new SelectListItem{Text="国投电力",Value="国投电力",Selected=false},
                new SelectListItem{Text="其他地方能源集团",Value="其他地方能源集团",Selected=false},
                new SelectListItem{Text="金融类",Value="金融类",Selected=false},
                new SelectListItem{Text="IT类",Value="IT类",Selected=false},
                new SelectListItem{Text="设计类",Value="设计类",Selected=false},
                new SelectListItem{Text="地方企业",Value="地方企业",Selected=false},
                new SelectListItem{Text="教育行业",Value="教育行业",Selected=false},
                new SelectListItem{Text="银行",Value="银行",Selected=false},
                new SelectListItem{Text="通信行业",Value="通信行业",Selected=false},
                new SelectListItem{Text="交通运输业",Value="交通运输业",Selected=false},
                new SelectListItem{Text="机械机电",Value="机械机电",Selected=false},
                new SelectListItem{Text="其他",Value="其他",Selected=false}
            };
            ViewBag.Group = Group;

            //单位行业
            List<SelectListItem> hangye = new List<SelectListItem> {
                new SelectListItem{Text="农、林、牧、渔业",Value="11农、林、牧、渔业",Selected=false},
                new SelectListItem{Text="采矿业",Value="21采矿业",Selected=false},
                new SelectListItem{Text="制造业",Value="22制造业",Selected=false},
                new SelectListItem{Text="电力、热力、燃气及水生产和供应业",Value="23电力、热力、燃气及水生产和供应业",Selected=false},
                new SelectListItem{Text="建筑业",Value="24建筑业",Selected=false},
                new SelectListItem{Text="批发和零售业",Value="31批发和零售业",Selected=false},
                new SelectListItem{Text="交通运输、仓储和邮政业",Value="32交通运输、仓储和邮政业",Selected=false},
                new SelectListItem{Text="住宿和餐饮业",Value="33住宿和餐饮业",Selected=false},
                new SelectListItem{Text="信息传输、软件和信息技术服务业",Value="34信息传输、软件和信息技术服务业",Selected=false},
                new SelectListItem{Text="金融业",Value="35金融业",Selected=false},
                new SelectListItem{Text="房地产业",Value="36房地产业",Selected=false},
                new SelectListItem{Text="租赁和商务服务业",Value="37租赁和商务服务业",Selected=false},
                new SelectListItem{Text="科学研究和技术服务业",Value="38科学研究和技术服务业",Selected=false},
                new SelectListItem{Text="水利、环境和公共设施管理业",Value="39水利、环境和公共设施管理业",Selected=false},
                new SelectListItem{Text="居民服务、修理和其他服务业",Value="41居民服务、修理和其他服务业",Selected=false},
                new SelectListItem{Text="教育",Value="42教育",Selected=false},
                new SelectListItem{Text="卫生和社会工作",Value="43卫生和社会工作",Selected=false},
                new SelectListItem{Text="文化、体育和娱乐业",Value="44文化、体育和娱乐业",Selected=false},
                new SelectListItem{Text="公共管理、社会保障和社会组织",Value="45公共管理、社会保障和社会组织",Selected=false},
                new SelectListItem{Text="国际组织",Value="46国际组织",Selected=false},
                new SelectListItem{Text="军队",Value="80军队",Selected=false}
            };
            ViewBag.hangye = hangye;

            //工作职务
            List<SelectListItem> zhiwei = new List<SelectListItem> {
                new SelectListItem{Text="公务员",Value="10公务员",Selected=false},
                new SelectListItem{Text="科学研究人员",Value="11科学研究人员",Selected=false},
                new SelectListItem{Text="工程技术人员",Value="13工程技术人员",Selected=false},
                new SelectListItem{Text="农林牧渔业技术人员",Value="17农林牧渔业技术人员",Selected=false},
                new SelectListItem{Text="卫生专业技术人员",Value="19卫生专业技术人员",Selected=false},
                new SelectListItem{Text="经济业务人员",Value="21经济业务人员",Selected=false},
                new SelectListItem{Text="金融业务人员",Value="22金融业务人员",Selected=false},
                new SelectListItem{Text="法律专业人员",Value="23法律专业人员",Selected=false},
                new SelectListItem{Text="教学人员",Value="24教学人员",Selected=false},
                new SelectListItem{Text="文学艺术工作人员",Value="25文学艺术工作人员",Selected=false},
                new SelectListItem{Text="体育工作人员",Value="26体育工作人员",Selected=false},
                new SelectListItem{Text="体育工作人员",Value="27体育工作人员",Selected=false},
                new SelectListItem{Text="其他专业技术人员",Value="29其他专业技术人员",Selected=false},
                new SelectListItem{Text="办事人员和有关人员",Value="30办事人员和有关人员",Selected=false},
                new SelectListItem{Text="商业和服务业人员",Value="40商业和服务业人员",Selected=false},
                new SelectListItem{Text="生产和运输设备操作人员",Value="60生产和运输设备操作人员",Selected=false},
                new SelectListItem{Text="军人",Value="80军人",Selected=false},
                new SelectListItem{Text="其他人员",Value="90其他人员",Selected=false},
            };
            ViewBag.zhiwei = zhiwei;

            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;



            //隶属部门
            var belong = db.belongDepTb.ToList();
            List<SelectListItem> belongDep = new List<SelectListItem> { };
            foreach (var item in belong)
            {
                var belongDepList = new SelectListItem { Text = item.ComBelongDep, Value =item.ComBelongDepCode };
                belongDep.Add(belongDepList);
            }
            ViewBag.belong = belongDep;

            #endregion
            if (Session["adminStuNum"] != null)
            {
                var student = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                    upload = db.UploadTb.Find(Session["adminStuNum"])
                };
                if (student.eSchoolInfo == null)
                {
                    student.eSchoolInfo = new ESchoolInfoModel();
                    student.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
                if (temp != null)
                {
                    ViewBag.P = temp.PoliticalStatus;//政治面貌
                    ViewBag.L = temp.Origin;//生源地
                    ViewBag.H = temp.Health;//健康状况
                    ViewBag.T = temp.TelNumber;//手机号
                    ViewBag.C = temp.FamilyZipCode;//邮政编码
                    ViewBag.ADDR = temp.CommAddress;//通信地址
                    ViewBag.FL = temp.FamilyLocation;//家庭地址
                }
                student.eSchoolInfo.JobTitle = student.eSchoolInfo.JobTitleCode + student.eSchoolInfo.JobTitle;//工作职位
                student.eSchoolInfo.ComIndustry = student.eSchoolInfo.ComIndustryCode + student.eSchoolInfo.ComIndustry;//单位行业
                student.eSchoolInfo.ComType = student.eSchoolInfo.ComTypeCode + student.eSchoolInfo.ComType;//单位性质          
                return View(student);
            }
            else
            {
                return Content("<script>alert('页面出错，请联系管理员');</script>");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplESchoolExam(ESchoolInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                //单位性质
                string comType = students.eSchoolInfo.ComType;
                students.eSchoolInfo.ComType = comType.Substring(2);
                students.eSchoolInfo.ComTypeCode = comType.Substring(0, 2);

                //单位行业
                string comIndustry = students.eSchoolInfo.ComIndustry;
                students.eSchoolInfo.ComIndustry = comIndustry.Substring(2);
                students.eSchoolInfo.ComIndustryCode = comIndustry.Substring(0, 2);

                //工作职位
                string jobTitle = students.eSchoolInfo.JobTitle;
                students.eSchoolInfo.JobTitle = jobTitle.Substring(2);
                students.eSchoolInfo.JobTitleCode = jobTitle.Substring(0, 2);

                //隶属部门
                var belong = db.belongDepTb.Where(m => m.ComBelongDepCode == students.eSchoolInfo.ComBelongDepCode).FirstOrDefault();
                students.eSchoolInfo.ComBelongDep = belong.ComBelongDep;

                //就业形式
                students.eSchoolInfo.Employment = "签学校三方协议就业";
                students.eSchoolInfo.EmploymentCode = "10";

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;


                //如果基本信息表中已经有基本，则为更新
                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplESchoolExam");
            }
            return View(students);
        }


        #endregion

        #region 签合同就业
        public ActionResult EmplESchoolContractExam()
        {
            #region 下拉框初始化
            //单位所属集团或系统
            List<SelectListItem> Group = new List<SelectListItem> {
                new SelectListItem{Text="国网",Value="国网",Selected=true},
                new SelectListItem{Text="南网",Value="南网",Selected=true},
                new SelectListItem{Text="大唐集团",Value="大唐集团",Selected=false},
                new SelectListItem{Text="华能集团",Value="华能集团",Selected=false},
                new SelectListItem{Text="国电集团",Value="国电集团",Selected=false},
                new SelectListItem{Text="华电集团",Value="华电集团",Selected=false},
                new SelectListItem{Text="中电投",Value="中电投",Selected=false},
                new SelectListItem{Text="中能建",Value="中能建",Selected=false},
                new SelectListItem{Text="中电建",Value="中电建",Selected=false},
                new SelectListItem{Text="国核集团",Value="国核集团",Selected=false},
                new SelectListItem{Text="中核集团",Value="中核集团",Selected=false},
                new SelectListItem{Text="中广核",Value="中广核",Selected=false},
                new SelectListItem{Text="华润集团",Value="华润集团",Selected=false},
                new SelectListItem{Text="神华集团",Value="神华集团",Selected=false},
                new SelectListItem{Text="京能集团",Value="京能集团",Selected=false},
                new SelectListItem{Text="浙能集团",Value="浙能集团",Selected=false},
                new SelectListItem{Text="深能集团",Value="深能集团",Selected=false},
                new SelectListItem{Text="粤电集团",Value="粤电集团",Selected=false},
                new SelectListItem{Text="国投电力",Value="国投电力",Selected=false},
                new SelectListItem{Text="其他地方能源集团",Value="其他地方能源集团",Selected=false},
                new SelectListItem{Text="金融类",Value="金融类",Selected=false},
                new SelectListItem{Text="IT类",Value="IT类",Selected=false},
                new SelectListItem{Text="设计类",Value="设计类",Selected=false},
                new SelectListItem{Text="地方企业",Value="地方企业",Selected=false},
                new SelectListItem{Text="教育行业",Value="教育行业",Selected=false},
                new SelectListItem{Text="银行",Value="银行",Selected=false},
                new SelectListItem{Text="通信行业",Value="通信行业",Selected=false},
                new SelectListItem{Text="交通运输业",Value="交通运输业",Selected=false},
                new SelectListItem{Text="机械机电",Value="机械机电",Selected=false},
                new SelectListItem{Text="其他",Value="其他",Selected=false}
            };
            ViewBag.Group = Group;

            //单位行业
            List<SelectListItem> hangye = new List<SelectListItem> {
                new SelectListItem{Text="农、林、牧、渔业",Value="11农、林、牧、渔业",Selected=false},
                new SelectListItem{Text="采矿业",Value="21采矿业",Selected=false},
                new SelectListItem{Text="制造业",Value="22制造业",Selected=false},
                new SelectListItem{Text="电力、热力、燃气及水生产和供应业",Value="23电力、热力、燃气及水生产和供应业",Selected=false},
                new SelectListItem{Text="建筑业",Value="24建筑业",Selected=false},
                new SelectListItem{Text="批发和零售业",Value="31批发和零售业",Selected=false},
                new SelectListItem{Text="交通运输、仓储和邮政业",Value="32交通运输、仓储和邮政业",Selected=false},
                new SelectListItem{Text="住宿和餐饮业",Value="33住宿和餐饮业",Selected=false},
                new SelectListItem{Text="信息传输、软件和信息技术服务业",Value="34信息传输、软件和信息技术服务业",Selected=false},
                new SelectListItem{Text="金融业",Value="35金融业",Selected=false},
                new SelectListItem{Text="房地产业",Value="36房地产业",Selected=false},
                new SelectListItem{Text="租赁和商务服务业",Value="37租赁和商务服务业",Selected=false},
                new SelectListItem{Text="科学研究和技术服务业",Value="38科学研究和技术服务业",Selected=false},
                new SelectListItem{Text="水利、环境和公共设施管理业",Value="39水利、环境和公共设施管理业",Selected=false},
                new SelectListItem{Text="居民服务、修理和其他服务业",Value="41居民服务、修理和其他服务业",Selected=false},
                new SelectListItem{Text="教育",Value="42教育",Selected=false},
                new SelectListItem{Text="卫生和社会工作",Value="43卫生和社会工作",Selected=false},
                new SelectListItem{Text="文化、体育和娱乐业",Value="44文化、体育和娱乐业",Selected=false},
                new SelectListItem{Text="公共管理、社会保障和社会组织",Value="45公共管理、社会保障和社会组织",Selected=false},
                new SelectListItem{Text="国际组织",Value="46国际组织",Selected=false},
                new SelectListItem{Text="军队",Value="80军队",Selected=false}
            };
            ViewBag.hangye = hangye;

            //工作职务
            List<SelectListItem> zhiwei = new List<SelectListItem> {
                new SelectListItem{Text="公务员",Value="10公务员",Selected=false},
                new SelectListItem{Text="科学研究人员",Value="11科学研究人员",Selected=false},
                new SelectListItem{Text="工程技术人员",Value="13工程技术人员",Selected=false},
                new SelectListItem{Text="农林牧渔业技术人员",Value="17农林牧渔业技术人员",Selected=false},
                new SelectListItem{Text="卫生专业技术人员",Value="19卫生专业技术人员",Selected=false},
                new SelectListItem{Text="经济业务人员",Value="21经济业务人员",Selected=false},
                new SelectListItem{Text="金融业务人员",Value="22金融业务人员",Selected=false},
                new SelectListItem{Text="法律专业人员",Value="23法律专业人员",Selected=false},
                new SelectListItem{Text="教学人员",Value="24教学人员",Selected=false},
                new SelectListItem{Text="文学艺术工作人员",Value="25文学艺术工作人员",Selected=false},
                new SelectListItem{Text="体育工作人员",Value="26体育工作人员",Selected=false},
                new SelectListItem{Text="体育工作人员",Value="27体育工作人员",Selected=false},
                new SelectListItem{Text="其他专业技术人员",Value="29其他专业技术人员",Selected=false},
                new SelectListItem{Text="办事人员和有关人员",Value="30办事人员和有关人员",Selected=false},
                new SelectListItem{Text="商业和服务业人员",Value="40商业和服务业人员",Selected=false},
                new SelectListItem{Text="生产和运输设备操作人员",Value="60生产和运输设备操作人员",Selected=false},
                new SelectListItem{Text="军人",Value="80军人",Selected=false},
                new SelectListItem{Text="其他人员",Value="90其他人员",Selected=false},
            };
            ViewBag.zhiwei = zhiwei;

            //隶属部门
            var belong = db.belongDepTb.ToList();
            List<SelectListItem> belongDep = new List<SelectListItem> { };
            foreach (var item in belong)
            {
                var belongDepList = new SelectListItem { Text = item.ComBelongDep, Value = item.ComBelongDepCode  };
                belongDep.Add(belongDepList);
            }
            ViewBag.belong = belongDep;

            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;

            #endregion
            if (Session["adminStuNum"] != null)
            {
                var student = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                    upload = db.UploadTb.Find(Session["adminStuNum"])
                };
                if (student.eSchoolInfo == null)
                {
                    student.eSchoolInfo = new ESchoolInfoModel();
                    student.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
                if (temp != null)
                {
                    ViewBag.P = temp.PoliticalStatus;//政治面貌
                    ViewBag.L = temp.Origin;//生源地
                    ViewBag.H = temp.Health;//健康状况
                    ViewBag.T = temp.TelNumber;//手机号
                    ViewBag.C = temp.FamilyZipCode;//邮政编码
                    ViewBag.ADDR = temp.CommAddress;//通信地址
                    ViewBag.FL = temp.FamilyLocation;//家庭地址
                }
                student.eSchoolInfo.JobTitle = student.eSchoolInfo.JobTitleCode + student.eSchoolInfo.JobTitle;//工作职位
                student.eSchoolInfo.ComIndustry = student.eSchoolInfo.ComIndustryCode + student.eSchoolInfo.ComIndustry;//单位行业
                student.eSchoolInfo.ComType = student.eSchoolInfo.ComTypeCode + student.eSchoolInfo.ComType;//单位性质             
                return View(student);
            }
            else
            {
                return Content("<script>alert('页面出错，请联系管理员');</script>");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplESchoolContractExam(ESchoolInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                //单位性质
                string comType = students.eSchoolInfo.ComType;
                students.eSchoolInfo.ComType = comType.Substring(2);
                students.eSchoolInfo.ComTypeCode = comType.Substring(0, 2);

                //单位行业
                string comIndustry = students.eSchoolInfo.ComIndustry;
                students.eSchoolInfo.ComIndustry = comIndustry.Substring(2);
                students.eSchoolInfo.ComIndustryCode = comIndustry.Substring(0, 2);

                //工作职位
                string jobTitle = students.eSchoolInfo.JobTitle;
                students.eSchoolInfo.JobTitle = jobTitle.Substring(2);
                students.eSchoolInfo.JobTitleCode = jobTitle.Substring(0, 2);

                //隶属部门
                var belong = db.belongDepTb.Where(m => m.ComBelongDepCode == students.eSchoolInfo.ComBelongDepCode).FirstOrDefault();
                students.eSchoolInfo.ComBelongDep = belong.ComBelongDep;

                //就业形式
                students.eSchoolInfo.Employment = "签合同就业";
                students.eSchoolInfo.EmploymentCode = "11";

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;


                //如果基本信息表中已经有基本，则为更新
                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplESchoolContractExam");
            }
            return View(students);
        }


        #endregion

        #region 其他形式就业


        #region 应征入伍


        public ActionResult EmplArmyExam()
        {
            if (Session["adminStuNum"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                    upload = db.UploadTb.Find(Session["adminStuNum"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
                if (temp != null)
                {
                    ViewBag.P = temp.PoliticalStatus;//政治面貌
                    ViewBag.L = temp.Origin;//生源地
                    ViewBag.H = temp.Health;//健康状况
                    ViewBag.T = temp.TelNumber;//手机号
                    ViewBag.C = temp.FamilyZipCode;//邮政编码
                    ViewBag.ADDR = temp.CommAddress;//通信地址
                    ViewBag.FL = temp.FamilyLocation;//家庭地址
                }
                return View(students);
            }
            else
            {
                return Content("<script>alert('页面出错，请联系管理员');</script>");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplArmyExam(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplArmyExam");
            }
            return View(students);
        }

        #endregion

        #region 自主创业
        /// <summary>
        /// 其他录用自主创业
        /// </summary>
        /// <returns></returns>
        public ActionResult EmplSelfExam()
        {
            #region 初始化下拉框
            List<SelectListItem> hangyefenlei = new List<SelectListItem>
            {
                new SelectListItem{Text="农、林、牧、渔业",Value="11农、林、牧、渔业"},
                new SelectListItem{Text="采矿业",Value="21采矿业"},
                new SelectListItem{Text="制造业",Value="22制造业"},
                new SelectListItem{Text="电力、热力、燃气及水生产和供应业",Value="23电力、热力、燃气及水生产和供应业"},
                new SelectListItem{Text="建筑业",Value="24建筑业"},
                new SelectListItem{Text="批发和零售业",Value="31批发和零售业"},
                new SelectListItem{Text="交通运输、仓储和邮政业",Value="32交通运输、仓储和邮政业"},
                new SelectListItem{Text="住宿和餐饮业",Value="33住宿和餐饮业"},
                new SelectListItem{Text="信息传输、软件和信息技术服务业",Value="34信息传输、软件和信息技术服务业"},
                new SelectListItem{Text="金融业",Value="35金融业"},
                new SelectListItem{Text="房地产业",Value="36房地产业"},
                new SelectListItem{Text="租赁和商务服务业",Value="37租赁和商务服务业"},
                new SelectListItem{Text="科学研究和技术服务业",Value="38科学研究和技术服务业"},
                new SelectListItem{Text="水利、环境和公共设施管理业",Value="39水利、环境和公共设施管理业"},
                new SelectListItem{Text="居民服务、修理和其他服务业",Value="41居民服务、修理和其他服务业"},
                new SelectListItem{Text="教育",Value="42教育"},
                new SelectListItem{Text="卫生和社会工作",Value="42卫生和社会工作"},
                new SelectListItem{Text="文化、体育和娱乐业",Value="44文化、体育和娱乐业"},
                new SelectListItem{Text="公共管理、社会保障和社会组织",Value="45公共管理、社会保障和社会组织"},
                new SelectListItem{Text="国际组织",Value="46国际组织"},
                new SelectListItem{Text="军队",Value="80军队"}                                   
            };
            ViewBag.hangyefenlei = hangyefenlei;

            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;


            #endregion

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            students.eSchoolInfo.ComIndustry = students.eSchoolInfo.ComIndustryCode + students.eSchoolInfo.ComIndustry;
            return View(students);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplSelfExam(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                students.eSchoolInfo.ComIndustry = students.eSchoolInfo.ComIndustry.Substring(2);
                students.eSchoolInfo.ComIndustryCode = students.eSchoolInfo.ComIndustry.Substring(0, 2);

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplSelfExam");
            }
            return View(students);
        }
        #endregion

        #region  灵活就业
        public ActionResult EmplFlexibleExam()
        {

            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(students);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplFlexibleExam(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplFlexibleExam");
            }
            return View(students);
        }

        #endregion

        #region 升学
        public ActionResult EmplUpSchExam()
        {

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址

            }
            return View(students);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplUpSchExam(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplUpSchExam");
            }
            return View(students);
        }

        #endregion

        #region 出国
        public ActionResult EmplAbroadExam()
        {
            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(students);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplAbroadExam(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplAbroadExam");
            }
            return View(students);
        }
        #endregion

        #region 国家基层项目
        public ActionResult EmplCountry()
        {
            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(students);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplCountry(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplCountry");
            }
            return View(students);
        }

        #endregion

        #region  科研助理
        public ActionResult EmplScience()
        {

            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(students);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplScience(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplScience");
            }
            return View(students);
        }

        #endregion

        #region 地方基层项目
        public ActionResult EmplPlace()
        {
            //单位所在地
            List<SelectListItem> cityList = new List<SelectListItem>();
            var st = db.ESchoolInfoTb.Find(Session["adminStuNum"]);
            if (st.ComLocation != null)
                cityList.Add(new SelectListItem { Text = st.ComLocation, Value = st.ComLocationCode });
            else
            {
                var list = db.LocationTb.ToList();
                foreach (var item in list)
                    cityList.Add(new SelectListItem { Text = item.name, Value = item.code });
            }
            ViewBag.city = cityList;

            var students = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (students.eSchoolInfo == null)
            {
                students.eSchoolInfo = new ESchoolInfoModel();
                students.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }

            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(students);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplPlace(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.EmploymentCode = employment.Substring(0, 2);
                students.eSchoolInfo.Employment = employment.Substring(2);

                //单位所在地
                var location = db.LocationTb.Find(students.eSchoolInfo.ComLocationCode);
                students.eSchoolInfo.ComLocation = location.name;


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplPlace");
            }
            return View(students);
        }

        #endregion

        #endregion

        #region 暂未就业
        public ActionResult EmplNotJobExam()
        {

            var student = new ESchoolInfoViewModel()
            {
                eSchoolInfo = db.ESchoolInfoTb.Find(Session["adminStuNum"]),
                upload = db.UploadTb.Find(Session["adminStuNum"])
            };
            if (student.eSchoolInfo == null)
            {
                student.eSchoolInfo = new ESchoolInfoModel();
                student.eSchoolInfo.StudentNumber = Session["adminStuNum"].ToString();
            }
            var temp = db.BaseInfoTb.Find(Session["adminStuNum"]);
            if (temp != null)
            {
                ViewBag.P = temp.PoliticalStatus;//政治面貌
                ViewBag.L = temp.Origin;//生源地
                ViewBag.H = temp.Health;//健康状况
                ViewBag.T = temp.TelNumber;//手机号
                ViewBag.C = temp.FamilyZipCode;//邮政编码
                ViewBag.ADDR = temp.CommAddress;//通信地址
                ViewBag.FL = temp.FamilyLocation;//家庭地址
            }
            return View(student);

        }
        /// <summary>
        /// 暂未就业
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplNotJobExam(ESchoolInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                //就业形式
                students.eSchoolInfo.Employment = "暂未就业";
                students.eSchoolInfo.EmploymentCode = "70";
                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber);
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("EmplNotJobExam");
            }

            return View(students);
        }
        #endregion



        #endregion

        #region 就业信息导出
        public ActionResult EmplExpot(int type = 0)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                var user = db.UserTb.Find(Session["Id"]);
                ViewBag.type = user.TypeCode;
                if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
                {
                    ViewBag.style = "1";
                    var userDep = db.DepartmentTb.Find(user.DepartId);
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                else if (Session["Type"].ToString() == "2")//管理员登陆
                {
                    ViewBag.style = "2";
                    #region 院系列表
                    //院列表
                    var aca = db.AcademyTb.ToList();
                    List<SelectListItem> acaList = new List<SelectListItem>();
                    if (aca != null)
                    {
                        foreach (var item in aca)
                        { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                    }
                    ViewBag.aca = acaList;

                    //系列表
                    var dep = db.DepartmentTb.ToList();
                    List<SelectListItem> depList = new List<SelectListItem>();
                    if (dep != null)
                    {
                        foreach (var item in dep)
                        {
                            depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }
                    ViewBag.dep = depList;

                    //专业列表
                    List<SelectListItem> majorList = new List<SelectListItem>();
                    var major = db.MajorTb.ToList();
                    if (major != null)
                    {
                        foreach (var item in major)
                        {
                            majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        }
                    }

                    ViewBag.major = majorList;
                    #endregion
                }
                #region 下载表格
                if (type == 1)
                {
                    EmplExamListViewModel s1 = (EmplExamListViewModel)Session["table"];
                    string[] excelHead = { "xysbh", "xh","", "dwmc", "dwlsbm", "dwszd", "dwszddm", "", "dwlxr", "lxrdh", "联系人邮编", "联系人通信地址", "dwxzdm", "dwxz", "dazjdwdz", "dazjdwyb", "hkqzdz", "", "专业是否对口", "备注", "dwhy", "zzjgdm", "毕业去向", "lxrzw", "dwzzjgdm", "bdzqwdwmc1", "升学专业", "升学院校", "出国国家", "工作职务", "工作职务代码", "xm", "xb", "szxy", "szyx", "zydm", "zy", "bj" };
                   // string[] excelHead = { "xysbh", "xh", "", "dwmc", "dwlsbm", "dwszd", "dwszddm", "", "dwlxr", "lxrdh", "联系人邮编", "联系人通信地址", "dwxzdm", "dwxz", "dazjdwdz", "档案转寄邮政", "户口迁移地址", "", "专业是否对口", "备注", "单位行业", "单位组织机构代码", "毕业去向", "联系人职务", "单位所属集团或系统", "《就业报到证》开具单位名称", "升学专业", "升学院校", "出国国家", "工作职务", "工作职务代码", "姓名", "性别", "院", "系", "专业代码", "专业", "班级" };
                    var workbook = new HSSFWorkbook();

                    var sheet = workbook.CreateSheet("签约登记");
                    var col = sheet.CreateRow(0);
                    //遍历表头在excel表格中
                    for (int i = 0; i < excelHead.Length; i++)
                    {
                        col.CreateCell(i).SetCellValue(excelHead[i]);
                    }
                    int a = 1;
                    //遍历表数据

                    foreach (var item in s1.uploadPagedList)
                    {

                        var row = sheet.CreateRow(a);
                        //从协议书标号开始
                        if (item.eSchoolInfoModel == null)
                            item.eSchoolInfoModel = new ESchoolInfoModel();
                        row.CreateCell(0).SetCellValue((item.eSchoolInfoModel.AgreementID));
                        row.CreateCell(1).SetCellValue(item.StudentNumber);
                        // row.CreateCell(2).SetCellValue();//是否签约
                        row.CreateCell(3).SetCellValue(item.eSchoolInfoModel.CompanyName);
                        row.CreateCell(4).SetCellValue(item.eSchoolInfoModel.ComBelongDep);
                        row.CreateCell(5).SetCellValue(item.eSchoolInfoModel.ComLocation);
                        row.CreateCell(6).SetCellValue(item.eSchoolInfoModel.ComLocationCode);
                        // row.CreateCell(7).SetCellValue(item.eSchoolInfoModel.);//单位所在地地区代
                        row.CreateCell(8).SetCellValue(item.eSchoolInfoModel.Contacts);
                        row.CreateCell(9).SetCellValue(item.eSchoolInfoModel.ConTel);
                        row.CreateCell(10).SetCellValue(item.eSchoolInfoModel.ContactsCode);
                        row.CreateCell(11).SetCellValue(item.eSchoolInfoModel.ConCommAddress);
                        row.CreateCell(12).SetCellValue(item.eSchoolInfoModel.ComTypeCode);
                        row.CreateCell(13).SetCellValue(item.eSchoolInfoModel.ComType);
                        row.CreateCell(14).SetCellValue(item.eSchoolInfoModel.FileChangedAdd);
                        row.CreateCell(15).SetCellValue(item.eSchoolInfoModel.FileZipCode);
                        row.CreateCell(16).SetCellValue(item.eSchoolInfoModel.ResChangedAdd);
                        // row.CreateCell(17).SetCellValue(item.eSchoolInfoModel.);//特殊地区
                        row.CreateCell(18).SetCellValue(item.eSchoolInfoModel.IsMajorFit);
                        row.CreateCell(19).SetCellValue(item.eSchoolInfoModel.Note);
                        row.CreateCell(20).SetCellValue(item.eSchoolInfoModel.ComIndustry);
                        row.CreateCell(21).SetCellValue(item.eSchoolInfoModel.CompanyCode);
                        row.CreateCell(22).SetCellValue(item.eSchoolInfoModel.Employment);
                        row.CreateCell(23).SetCellValue(item.eSchoolInfoModel.ConPost);
                        //专业是否对口重复
                        row.CreateCell(24).SetCellValue(item.eSchoolInfoModel.ComBelongGroup);
                        row.CreateCell(25).SetCellValue(item.eSchoolInfoModel.SCompanyName);
                        row.CreateCell(26).SetCellValue(item.eSchoolInfoModel.UpMajor);
                        row.CreateCell(27).SetCellValue(item.eSchoolInfoModel.UPSchool);
                        row.CreateCell(28).SetCellValue(item.eSchoolInfoModel.OutCountry);
                        row.CreateCell(29).SetCellValue(item.eSchoolInfoModel.JobTitle);
                        row.CreateCell(30).SetCellValue(item.eSchoolInfoModel.JobTitleCode);
                        row.CreateCell(31).SetCellValue(item.Name);
                        row.CreateCell(32).SetCellValue(item.Sex);
                        row.CreateCell(33).SetCellValue(item.Academy);
                        row.CreateCell(34).SetCellValue(item.Department);
                        row.CreateCell(35).SetCellValue(item.MajorCode);
                        row.CreateCell(36).SetCellValue(item.Major);
                        row.CreateCell(37).SetCellValue(item.Class);
                        a++;
                    }

                    MemoryStream ms = new MemoryStream();
                    workbook.Write(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, "application/vnd.ms-excel", "就业信息表.xls");
                }

                #endregion
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmplExpot(AdminGradViewModel students, int id = 0)
        {
            var user = db.UserTb.Find(Session["Id"]);
            ViewBag.type = user.TypeCode;
            if (Session["Type"].ToString() == "1")//如果为院系管理员登陆，则绑定好
            {
                ViewBag.style = "1";
                var userDep = db.DepartmentTb.Find(user.DepartId);
                #region 院系列表
                //院列表
                var aca = db.AcademyTb.ToList();
                SelectList acaList = new SelectList(aca, "Id", "Name", userDep.BelongId);
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                SelectList depList = new SelectList(dep, "Id", "Name", userDep.Id);
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }
            else if (Session["Type"].ToString() == "2")//管理员登陆
            {
                ViewBag.style = "2";
                #region 院系列表
                //院列表
                var aca = db.AcademyTb.ToList();
                List<SelectListItem> acaList = new List<SelectListItem>();
                if (aca != null)
                {
                    foreach (var item in aca)
                    { acaList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); }
                }
                ViewBag.aca = acaList;

                //系列表
                var dep = db.DepartmentTb.ToList();
                List<SelectListItem> depList = new List<SelectListItem>();
                if (dep != null)
                {
                    foreach (var item in dep)
                    {
                        depList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                ViewBag.dep = depList;

                //专业列表
                List<SelectListItem> majorList = new List<SelectListItem>();
                var major = db.MajorTb.ToList();
                if (major != null)
                {
                    foreach (var item in major)
                    {
                        majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                ViewBag.major = majorList;
                #endregion
            }

            #region 查询
            EmplExamListViewModel display = new EmplExamListViewModel();
            display.upload = new UploadModel();
            display.InfoList = new List<ESchoolInfoViewModel>();
            display.upload = students.upload;
            // var upload = db.UploadTb.Where(m => m.Department == user.DepartName);
            var upload = db.UploadTb.Include("eSchoolInfoModel").Where(m => m.Department == user.DepartName);
            if (Session["Type"].ToString() == "1")
            { upload = db.UploadTb.Include("eSchoolInfoModel").Where(m => m.Department == user.DepartName); }
            else
            { upload = db.UploadTb.Include("eSchoolInfoModel"); }



            if (students.upload.Name != null)//姓名
                upload = upload.Where(m => m.Name.Contains(students.upload.Name));
            if (students.upload.StudentNumber != null)//学号
                upload = upload.Where(m => m.StudentNumber.Contains(students.upload.StudentNumber));
            if (students.upload.GraduationTime != null)//毕业时间
                upload = upload.Where(m => m.GraduationTime == students.upload.GraduationTime);
            if (students.upload.eSchoolInfoModel.CheckTime != null)//审核时间
                upload = upload.Where(m => m.eSchoolInfoModel.CheckTime == students.upload.eSchoolInfoModel.CheckTime);

            if (students.upload.Academy != "0" && students.upload.Academy != null)
            {
                int Id = Convert.ToInt32(students.upload.Academy);
                var acaTemp = db.AcademyTb.Find(Id);
                upload = upload.Where(m => m.Academy == acaTemp.Name);
                display.upload.Academy = acaTemp.Id.ToString();
            }
            if (students.upload.Department != "0" && students.upload.Department != null)
            {
                int Id = Convert.ToInt32(students.upload.Department);
                var depTemp = db.DepartmentTb.Find(Id);
                upload = upload.Where(m => m.Department == depTemp.Name);
                display.upload.Department = depTemp.Id.ToString();
            }
            if (students.upload.Major != "0" && students.upload.Major != null)//专业
            {
                int Id = Convert.ToInt16(students.upload.Major);
                var majorTemp = db.MajorTb.Find(Id);
                upload = upload.Where(m => m.Major == majorTemp.Name);
                display.upload.Academy = majorTemp.AcademyId;
                display.upload.Department = majorTemp.DepartId;
            }
            //从upload对应到求职信息
            foreach (var item in upload.ToList())
            {
                ESchoolInfoViewModel temp = new ESchoolInfoViewModel();
                temp.upload = new UploadModel();
                temp.upload = item;
                if (db.BaseInfoTb.Find(item.StudentNumber) != null)
                    temp.eSchoolInfo = db.ESchoolInfoTb.Find(item.StudentNumber);
            }
            display.uploadPagedList = upload.OrderBy(a => a.StudentNumber).ToPagedList(id, 10);
            Session["table"] = display;
            return View(display);
            #endregion
        }
        #endregion

        #region 发布公告

        /// <summary>
        /// 显示公告列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Announce(int id=0,string type=null)
        {
            var title = db.AnnounceTb.ToList();
            if (type == "1")
            {
                AnnounceModel announce = db.AnnounceTb.Find(id);
                db.AnnounceTb.Remove(announce);
                db.SaveChanges();
                return RedirectToAction("Announce");      
            }
            return View(title);
        }

       

        /// <summary>
        /// 显示公告详细信息
        /// </summary>
        /// <param name="AnnounceId"></param>
        /// <returns></returns>
        public ActionResult AnnounceDetails(int AnnounceId)
        {
            var detail = db.AnnounceTb.Find(AnnounceId);
            return View(detail);
        }
        /// <summary>
        /// 发布公告
        /// </summary>
        /// <returns></returns>
        public ActionResult AnnouncePost()
        {
            AnnounceModel am = new AnnounceModel();
            return View(am);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnnouncePost(FormCollection form)
        {
            HttpPostedFileBase file = Request.Files["file"];
            if (file != null)
            {
                string filepath = Path.Combine(HttpContext.Server.MapPath("../Uploads"), Path.GetFileName(file.FileName));
                file.SaveAs(filepath);



                //获取文档内容
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                object fileobj = filepath;
                object nullobj = System.Reflection.Missing.Value;
                //打开指定文件（不同版本的COM参数个数有差异，一般而言除第一个外都用nullobj就行了）
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref fileobj, ref nullobj, ref nullobj,
        ref nullobj, ref nullobj, ref nullobj,
        ref nullobj, ref nullobj, ref nullobj,
        ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj
        );
                //取得doc文件中的文本
                string outText = doc.Content.Text;
                
                //关闭文件
                doc.Close(ref nullobj, ref nullobj, ref nullobj);
                //关闭COM
                wordApp.Quit(ref nullobj, ref nullobj, ref nullobj);
                //返回
                AnnounceModel uploadfile = new AnnounceModel();
                uploadfile.Message = outText;
                uploadfile.Title = Request.Form["message"];
                db.AnnounceTb.Add(uploadfile);
                db.SaveChanges();
                return View();
            }
            else { return View(); }
        }



        #endregion

        #region 就业统计
        public ActionResult jiuyetongji()
        {
            List<jieyeListViewModel> display = new List<jieyeListViewModel>();
            var acaList = db.AcademyTb.ToList();
            foreach (var itemaca in acaList)
            {
                foreach (var itemdep in db.DepartmentTb.Where(m => m.BelongId == itemaca.Id))
                {
                    string depId = itemdep.Id.ToString();//系的Id
                    foreach (var itemMajor in db.MajorTb.Where(m => m.DepartId == depId))
                    {
                        int acanum = db.UploadTb.Where(m => m.Major == itemMajor.Name).Count();//这个专业的总人数
                        //  int sign = db.ESchoolInfoTb.Where()
                        jieyeListViewModel model = new jieyeListViewModel
                        {
                            aca = itemaca.Name,
                            dep = itemdep.Name,
                            major = itemMajor.Name

                        };
                    }
                }
            }
            return View();
        }

        public ActionResult jiyeYX()
        {
            return View();
        }
        #endregion

        #region 上传文件

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload(string type = null, string studentTyppe = null, string path = null)
        {
            if (Session["Id"] != null)
            {
                ViewBag.type = Session["Type"];
                if (type == "1")
                {
                    if (Session["FilePath"] == null)
                    {
                        return View();
                    }
                    //往数据库添加数据
                    string filePath = Session["FilePath"].ToString();//文件的路径
                    ReadExcel(filePath);
                }
                if (path != null)
                {
                    UploadViewModel model = new UploadViewModel();
                    ViewBag.messagge = "文件上传成功";
                    return View(model);
                }
                return View();
            }
            else
            { return RedirectToAction("Login"); }

        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null)
            {
                if (uploadFile.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"), Path.GetFileName(uploadFile.FileName));
                    uploadFile.SaveAs(filePath);
                    UploadViewModel model = new UploadViewModel();
                    model.FilePath = filePath;
                    Session["FilePath"] = filePath;
                    return RedirectToAction("Upload", new { type = "2", studentTyppe = "2", path = filePath });
                }
            }
            return View();
        }

        public static DataTable ReadExcel(string fileName)
        {
            string basePath = ConfigurationManager.AppSettings["FilePath"];
            DataTable dt = new DataTable();
            if (fileName != null)
            {
                ImportExcelFile(fileName);
            }
            //文件是否存在  
            if (System.IO.File.Exists(fileName))
            {

            }
            return dt;
        }

        public static void ImportExcelFile(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            #region 读取信息并保存
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            rows.MoveNext();//移动到第一行，数据为表头的代码
            HSSFRow row = (HSSFRow)rows.Current;//获取第一行的值
            DataTable dt = new DataTable();
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(row.GetCell(j).ToString());//保存表头
            }
            DataRow header = dt.NewRow();//保存表头数据
            DataRow dr = dt.NewRow();//保存数据
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                header[j] = row.GetCell(j).ToString();//保存表头
            }
            rows.MoveNext();//再移动一行，为实际数据
            while (rows.MoveNext())
            {
                row = (HSSFRow)rows.Current;
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                GraduationDBContent db1 = new GraduationDBContent();
                UploadModel model = new UploadModel();
                ESchoolInfoModel eschool = new ESchoolInfoModel();
                ESchoolInfoModel eschool1 = new ESchoolInfoModel();
                FillBaseInfoModel baseInfo = new FillBaseInfoModel();
                UploadModel student = new UploadModel();
                FillBaseInfoModel baseInfo1 = new FillBaseInfoModel();
                string isNull = "";//上传信息表中有没有
                string isNull1 = "";//基本信息表中有没有
                string isNull2 = "";//就业信息表中有没有
                for (int i = 0; i < header.ItemArray.Length; i++)
                {
                    string number = "";
                    if (header.ItemArray[i].ToString() == "xh")
                    {
                        number = dr.ItemArray[i].ToString();
                        student = db1.UploadTb.Find(number);
                        baseInfo1 = db1.BaseInfoTb.Find(number);
                        eschool1 = db1.ESchoolInfoTb.Find(number);
                        #region 判断是否在表中已经存在
                        if (student == null)
                        {
                            model = new UploadModel();
                            isNull = "null";
                        }
                        else
                        {
                            model = student;
                            isNull = "NotNull";
                        }
                        if (baseInfo1 == null)
                        {
                            baseInfo = new FillBaseInfoModel();
                            isNull1 = "null";
                        }
                        else
                        {
                            baseInfo = baseInfo1;
                            isNull1 = "NotNull";
                        }
                        if (eschool1 == null)
                        {
                            eschool = new ESchoolInfoModel();
                            isNull2 = "null";
                        }
                        else
                        {
                            eschool = eschool1;
                            isNull2 = "NotNull";
                        }
                        #endregion
                    }

                }
                #region 将每一行的数据进行保存

                for (int i = 0; i < header.ItemArray.Length; i++)
                {
                    string head = header.ItemArray[i].ToString();
                    string data = dr.ItemArray[i].ToString();
                    #region 上传数据的判断
                    switch (head)
                    {
                        case "ksh"://考生号
                            model.KSH = data;
                            break;
                        case "szxy"://学院
                            model.Academy = data;
                            break;
                        case "szyx"://系
                            model.Department = data;
                            break;
                        case "bj"://班级
                            model.Class = data;
                            break;
                        case "jxb"://教学班
                            model.TeachingClass = data;
                            break;
                        case "xh"://学号
                            model.StudentNumber = data;
                            eschool.StudentNumber = data;
                            baseInfo.StudentNumber = data;
                            break;
                        case "xm"://姓名
                            model.Name = data;
                            break;
                        case "xb"://性别
                            model.Sex = data;
                            if (data == "男")
                                model.SexCode = "1";
                            else if (data == "女")
                                model.SexCode = "2";
                            break;
                        case "mz"://民族
                            model.Nation = data;
                            model.NationCode = NationCode(data);
                            break;
                        case "csrq"://出生年月
                            model.BirthTime = data;
                            break;
                        case "sfzh"://身份证号
                            model.IDNumber = data;
                            break;
                        case "zy"://专业名称
                            model.Major = data;
                            GraduationDBContent db = new GraduationDBContent();
                            //var maj = db.MajorTb.Where(m => m.Name == data).FirstOrDefault();
                            //model.MajorCode = maj.Code;
                            break;
                        case "zyfx"://专业方向
                            model.MajorDirection = data;
                            break;
                        case "sfzyxw"://是否专业学位
                            model.Sfzyxw = data;
                            break;
                        case "syszdgkkq"://生源地所在地（高考生源地）只有省份
                            baseInfo.OriginProvince = data;//在基本信息表中
                            break;
                        case "xl"://学历
                            model.Education = data;
                            if (data == "本科生" || data == "本科")
                            {
                                model.StudentType = "0";
                                model.EducationCode = "31";
                            }
                            else if (data == "研究生")
                            {
                                model.EducationCode = "11";
                                model.StudentType = "1";
                            }
                            break;
                        case "xz"://学制
                            model.LengthOfSch = data;
                            break;
                        case "pyfs"://培养方式
                            model.TrainType = data;
                            if (data == "非定向")
                                model.TrainTypeCode = "1";
                            else if (data == "定向")
                                model.TrainTypeCode = "2";
                            else if (data == "在职")
                                model.TrainTypeCode = "3";
                            else if (data == "委培")
                                model.TrainTypeCode = "4";
                            else if (data == "自筹")
                                model.TrainTypeCode = "5";
                            break;
                        case "dxhwpdw"://定向委托单位
                            model.Weituo = data;
                            break;
                        case "rxsj"://入学时间
                            model.EntranceTime = data;
                            model.EntranceYear = data.Substring(0, 4);
                            break;
                        case "bysj"://毕业时间
                            model.GraduationTime = data;
                            break;
                        case "xysbh"://协议书编号
                            eschool.AgreementID = data;
                            break;
                    }
                    #endregion
                }
                model.Pwd = "123";
                model.School = "华北电力大学";
                model.SchoolCode = "10079";
                model.SchoolBeCode = "360";
                model.SchoolAddCode = "130600";
                model.SFstudentCode = "2";

                #region 用于判断为更新还是为上传，上传的是哪个表
                if (isNull == "null")//为空则为添加,修改的为上传表
                {
                    db1.UploadTb.Add(model);
                    db1.SaveChanges();
                }
                else
                {
                    db1.Entry(student).CurrentValues.SetValues(model);
                    db1.SaveChanges();
                }

                if (baseInfo.OriginProvince != null)
                {
                    if (isNull1 == "null")//为空则为添加,修改的为基本信息表
                    {
                        db1.BaseInfoTb.Add(baseInfo);
                        db1.SaveChanges();
                    }
                    else
                    {
                        db1.Entry(baseInfo1).CurrentValues.SetValues(baseInfo);
                        db1.SaveChanges();
                    }
                }
                if (eschool.AgreementID != null)
                {
                    if (isNull2 == "null")//为空则为添加,修改的为就业信息表
                    {
                        db1.ESchoolInfoTb.Add(eschool);
                        db1.SaveChanges();
                    }
                    else
                    {
                        db1.Entry(eschool1).CurrentValues.SetValues(eschool);
                        db1.SaveChanges();
                    }
                }
                #endregion

                #endregion
            }
            #endregion

        }
        #endregion

        #region 对应的民族代码
        public static string NationCode(string nation)
        {
            string code = "";
            switch (nation)
            {
                case "汉族":
                    code = "01";
                    break;
                case "蒙古族":
                    code = "02";
                    break;
                case "回族":
                    code = "03";
                    break;
                case "藏族":
                    code = "04";
                    break;
                case "维吾尔族":
                    code = "05";
                    break;
                case "苗族":
                    code = "06";
                    break;
                case "彝族":
                    code = "07";
                    break;
                case "壮族":
                    code = "08";
                    break;
                case "布依族":
                    code = "09";
                    break;
                case "朝鲜族":
                    code = "10";
                    break;
                case "满族":
                    code = "11";
                    break;
                case "侗族":
                    code = "12";
                    break;
                case "瑶族":
                    code = "13";
                    break;
                case "白族":
                    code = "14";
                    break;
                case "土家族":
                    code = "15";
                    break;
                case "哈尼族":
                    code = "16";
                    break;
                case "哈萨克族":
                    code = "17";
                    break;
                case "傣族":
                    code = "18";
                    break;
                case "黎族":
                    code = "19";
                    break;
                case "傈僳族":
                    code = "20";
                    break;
                case "佤族":
                    code = "21";
                    break;
                case "畲族":
                    code = "22";
                    break;
                case "高山族":
                    code = "23";
                    break;
                case "拉祜族":
                    code = "24";
                    break;
                case "水族":
                    code = "25";
                    break;
                case "东乡族":
                    code = "26";
                    break;
                case "纳西族":
                    code = "27";
                    break;
                case "景颇族":
                    code = "28";
                    break;
                case "柯尔克孜族":
                    code = "29";
                    break;
                case "土族":
                    code = "30";
                    break;
                case "达斡尔族":
                    code = "31";
                    break;
                case "仫佬族":
                    code = "32";
                    break;
                case "羌族":
                    code = "33";
                    break;
                case "布朗族":
                    code = "34";
                    break;
                case "撒拉族":
                    code = "35";
                    break;
                case "毛难族":
                    code = "36";
                    break;
                case "仡佬族":
                    code = "37";
                    break;
                case "锡伯族":
                    code = "38";
                    break;
                case "阿昌族":
                    code = "39";
                    break;
                case "普米族":
                    code = "40";
                    break;
                case "塔吉克族":
                    code = "41";
                    break;
                case "怒族":
                    code = "42";
                    break;
                case "乌孜别克族":
                    code = "43";
                    break;
                case "俄罗斯族":
                    code = "44";
                    break;
                case "鄂温克族":
                    code = "45";
                    break;
                case "崩龙族":
                    code = "46";
                    break;
                case "保安族":
                    code = "47";
                    break;
                case "裕固族":
                    code = "48";
                    break;
                case "京族":
                    code = "49";
                    break;
                case "塔塔尔族":
                    code = "50";
                    break;
                case "独龙族":
                    code = "51";
                    break;
                case "鄂伦春族":
                    code = "52";
                    break;
                case "赫哲族":
                    code = "53";
                    break;
                case "门巴族":
                    code = "54";
                    break;
                case "珞巴族":
                    code = "55";
                    break;
                case "基诺族":
                    code = "56";
                    break;
                case "其他":
                    code = "97";
                    break;
                case "外国血统中国籍人士":
                    code = "98";
                    break;
            }

            return code;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
