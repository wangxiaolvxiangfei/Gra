using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graduation.Models;

namespace Graduation.Controllers
{
    public class FillBaseInfoController : Controller
        /// jukjkjl
    {
        private GraduationDBContent db = new GraduationDBContent();

        #region 通知界面
        /// <summary>
        /// 通知页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["number"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "StudentLogin");
        }
        #endregion

        #region 基本信息管理界面

        /// <summary>
        /// 填写基本信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult FillBaseInfo()
        {
            #region 下拉栏的初始化
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
            ViewBag.type = type;
            #endregion

            if (Session["number"] != null)
            {
                var student = new FillBaseInfoViewModel()
                {
                    baseInfo = db.BaseInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (student.baseInfo == null)
                {
                    student.baseInfo = new FillBaseInfoModel();
                    student.baseInfo.StudentNumber = Session["number"].ToString();
                }
                student.baseInfo.PoliticalStatus = student.baseInfo.PoliticalStatusCode + student.baseInfo.PoliticalStatus;
                return View(student);
            }
            else 
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        /// <summary>
        /// 填写基本信息
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillBaseInfo(FillBaseInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                string PoliticalStatus = students.baseInfo.PoliticalStatus;
                students.baseInfo.PoliticalStatus = PoliticalStatus.Substring(2);
                students.baseInfo.PoliticalStatusCode = PoliticalStatus.Substring(0, 2);
                //如果基本信息表中已经有基本，则为更新
                if (db.BaseInfoTb.Find(students.baseInfo.StudentNumber) != null)
                {
                    FillBaseInfoModel temp = db.BaseInfoTb.Find(Session["number"].ToString());
                    //db.Entry(students.baseInfo).State = EntityState.Modified;
                    db.Entry(temp).CurrentValues.SetValues(students.baseInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.BaseInfoTb.Add(students.baseInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillBaseInfo");
            }
            return View(students);
        }

        #endregion

        #region 求职信息管理界面
        /// <summary>
        /// 求职信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FillApplInfo()
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
            if (Session["number"] != null)
            {
                var student = new ApplInfoViewModel()
                {
                    applInfo = db.ApplInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (student.applInfo == null)
                {
                    student.applInfo = new ApplInfoModel();
                    student.applInfo.StudentNumber = Session["number"].ToString();
                }
                return View(student);
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }

        /// <summary>
        /// 填写求职信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillApplInfo(ApplInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                //如果基本信息表中已经有基本，则为更新
                if (db.ApplInfoTb.Find(students.applInfo.StudentNumber) != null)
                {
                    ApplInfoModel temp = db.ApplInfoTb.Find(Session["number"].ToString());
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

        #region 签约登记管理界面
        public ActionResult FillSignInfo()
        {

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
            #endregion
            if (Session["number"] != null)
            {
                var student = new SignInfoViewModel()
                {
                    signInfo = db.SingInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (student.signInfo == null)
                {
                    student.signInfo = new SignInfoModel();
                    student.signInfo.StudentNumber = Session["number"].ToString();
                }
                student.signInfo.ComType = student.signInfo.ComTypeCode + student.signInfo.ComType;
                return View(student);
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillSignInfo(SignInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                string comType = students.signInfo.ComType;
                students.signInfo.ComType = comType.Substring(2);
                students.signInfo.ComTypeCode = comType.Substring(0, 2);
                //如果基本信息表中已经有基本，则为更新
                if (db.SingInfoTb.Find(students.signInfo.StudentNumber) != null)
                {
                    SignInfoModel temp = db.SingInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.signInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.SingInfoTb.Add(students.signInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillSignInfo");
            }
            return View(students);
        }
        #endregion

        #region 就业信息管理

        /// <summary>
        /// 就业信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployInfo()
        {
            if (Session["number"] != null)
            {
                ViewBag.number = Session["number"];
                return View();
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }

        #region 签学校三方协议就业
        /// <summary>
        /// 签学校三方协议就业
        /// </summary>
        /// <returns></returns>
        public ActionResult FillESchoolInfo()
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

            #endregion
            if (Session["number"] != null)
            {
                var student = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (student.eSchoolInfo == null)
                {
                    student.eSchoolInfo = new ESchoolInfoModel();
                    student.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                return RedirectToAction("Login", "StudentLogin");
            }
        }

        /// <summary>
        /// 签学校第三方协议就业
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillESchoolInfo(ESchoolInfoViewModel students)
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


                //如果基本信息表中已经有基本，则为更新
                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillESchoolInfo");
            }
            return View(students);
        }

        #endregion

        #region 其他录用应征入伍形式就业

        /// <summary>
        /// 其他录用应征入伍形式就业
        /// </summary>
        /// <returns></returns>
        public ActionResult FillEOtherArmyInfo()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherArmyInfo(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherArmyInfo");
            }
            return View(students);
        }

#endregion

        #region 其他录用自主创业
        /// <summary>
        /// 其他录用自主创业
        /// </summary>
        /// <returns></returns>
        public ActionResult FillEOtherSelfInfo()
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

            #endregion
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherSelfInfo(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);

                students.eSchoolInfo.ComIndustry = students.eSchoolInfo.ComIndustry.Substring(2);
                students.eSchoolInfo.ComIndustryCode = students.eSchoolInfo.ComIndustry.Substring(0, 2);





                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherSelfInfo");
            }
            return View(students);
        }


        #endregion 

        #region 其他录用灵活就业
        /// <summary>
        /// 其他录用灵活就业
        /// </summary>
        /// <returns></returns>
        public ActionResult FillEOtherFlexibleInfo()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherFlexibleInfo(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherFlexibleInfo");
            }
            return View(students);
        }


        #endregion

        #region 其他录用升学

        /// <summary>
        /// 其他录用升学
        /// </summary>
        /// <returns></returns>
        public ActionResult FillEOtherSchoolInfo()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherSchoolInfo(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherSchoolInfo");
            }
            return View(students);
        }


        #endregion

        #region 其他录用出国
        /// <summary>
        /// 其他录用出国
        /// </summary>
        /// <returns></returns>
        public ActionResult FillEOtherAbroadInfo()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherAbroadInfo(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);

                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherAbroadInfo");
            }
            return View(students);
        }

        #endregion


        #region 国家基层项目

        public ActionResult FillEOtherCountry()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                //ViewBag.IsLocked = students.eSchoolInfo.IsLocked;
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherCountry(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherCountry");
            }
            return View(students);
        }
        #endregion

        #region 科研助理
        public ActionResult FillEOtherSciene()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
               // ViewBag.IsLocked = students.eSchoolInfo.IsLocked;
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherSciene(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherSciene");
            }
            return View(students);
        }
        #endregion

        #region 地方基层项目


        public ActionResult FillEOtherPlace()
        {
            if (Session["number"] != null)
            {
                var students = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (students.eSchoolInfo == null)
                {
                    students.eSchoolInfo = new ESchoolInfoModel();
                    students.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }

                var temp = db.BaseInfoTb.Find(Session["number"]);
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
                //ViewBag.IsLocked = students.eSchoolInfo.IsLocked;
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillEOtherPlace(ESchoolInfoViewModel students)
        {

            if (ModelState.IsValid)
            {
                //就业形式
                string employment = students.eSchoolInfo.Employment;
                students.eSchoolInfo.Employment = employment.Substring(0, 2);
                students.eSchoolInfo.EmploymentCode = employment.Substring(2);


                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillEOtherPlace");
            }
            return View(students);
        }



        #endregion

        #region 暂未就业

        /// <summary>
        /// 暂未就业
        /// </summary>
        /// <returns></returns>
        public ActionResult FillNotJobInfo()
        {
            if (Session["number"] != null)
            {
                var student = new ESchoolInfoViewModel()
                {
                    eSchoolInfo = db.ESchoolInfoTb.Find(Session["number"]),
                    upload = db.UploadTb.Find(Session["number"])
                };
                if (student.eSchoolInfo == null)
                {
                    student.eSchoolInfo = new ESchoolInfoModel();
                    student.eSchoolInfo.StudentNumber = Session["number"].ToString();
                }
                var temp = db.BaseInfoTb.Find(Session["number"]);
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
            else
            {
                return RedirectToAction("Login", "StudentLogin");
            }
        }


        /// <summary>
        /// 暂未就业
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillNotJobInfo(ESchoolInfoViewModel students)
        {
            if (ModelState.IsValid)
            {
                if (db.ESchoolInfoTb.Find(students.eSchoolInfo.StudentNumber) != null)
                {
                    ESchoolInfoModel temp = db.ESchoolInfoTb.Find(Session["number"].ToString());
                    db.Entry(temp).CurrentValues.SetValues(students.eSchoolInfo);
                    db.SaveChanges();
                }
                else
                {
                    db.ESchoolInfoTb.Add(students.eSchoolInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("FillNotJobInfo");
            }

            return View(students);
        }
        #endregion


        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}