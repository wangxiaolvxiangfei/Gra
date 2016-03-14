using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class Students
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 密码（使用MD5）
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 学生类型 0本科生/1研究生
        /// </summary>
        public string StudentType { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 性别编号
        /// </summary>
        public string SexCode { get; set; }

        /// <summary>
        /// 出生年月日
        /// </summary>
        public string BirthTime { get; set; }

        /// <summary>
        /// 师范生类别代码
        /// </summary>
        public string SFstudentCode { get; set; }

        /// <summary>
        /// 原来考生号
        /// </summary>
        public string YKSH { get; set; }

        /// <summary>
        /// 考生号
        /// </summary>
        public string KSH { get; set; }

        /// <summary>
        /// 学籍变动代码
        /// </summary>
        public string ChangeCode { get; set; }

        /// <summary>
        /// 院校名称
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 院校代码
        /// </summary>
        public string SchoolCode { get; set; }

        /// <summary>
        /// 院校隶属部门代码
        /// </summary>
        public string SchoolBeCode { get; set; }

        /// <summary>
        /// 院校所在地代码
        /// </summary>
        public string SchoolAddCode { get; set; }

        /// <summary>
        /// 所在学院
        /// </summary>
        public string Academy { get; set; }

        /// <summary>
        /// 所在系
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 专业代码
        /// </summary>
        public string MajorCode { get; set; }

        /// <summary>
        /// 专业方向
        /// </summary>
        public string MajorDirection { get; set; }

        /// <summary>
        /// 所在班级
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 所在教学班
        /// </summary>
        public string TeachingClass { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 民族代码
        /// </summary>
        public string NationCode { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 健康状况
        /// </summary>
        public string Health { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
         
        public string PoliticalStatus { get; set; }

        /// <summary>
        /// 政治面貌代码
        /// </summary>
        public string PoliticalStatusCode { get; set; }

        /// <summary>
        /// 班主任
        /// </summary>
         
        public string ClassTeacher { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
         
        public string TelNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
         
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
         
        public string QQ { get; set; }

        /// <summary>
        /// 宿舍
        /// </summary>
         
        public string Dormitory { get; set; }

        /// <summary>
        /// 宿舍号
        /// </summary>
         
        public string RoomNumber { get; set; }

        /// <summary>
        /// 户口是否转入学校(1是/0否)
        /// </summary>
         
        public string IsResidenceToSch { get; set; }

        /// <summary>
        /// 婚否
        /// </summary>
         
        public string IsMargery { get; set; }

        /// <summary>
        /// 档案是否转入学校
        /// </summary>
         
        public string IsArchivesToSch { get; set; }

        /// <summary>
        /// 入学前所在地派出所
        /// </summary>
         
        public string PoliceBefore { get; set; }

        /// <summary>
        /// 是否曾转专业
        /// </summary>
         
        public string IsChangeMaj { get; set; }

        /// <summary>
        /// 转系前的专业
        /// </summary>
        public string BeforeMajor { get; set; }

        /// <summary>
        /// 独生子女标识
        /// </summary>
         
        public string IsOnlyChild { get; set; }

        /// <summary>
        /// 困难生类别
        /// </summary>
         
        public string DiffLevel { get; set; }

        /// <summary>
        /// 困难生类别代码
        /// </summary>
        public string DiffLevelCode { get; set; }

        /// <summary>
        /// 家庭困难级别
        /// </summary>
         
        public string FDiffLevel { get; set; }

        /// <summary>
        /// 生源地是否发生过变动
        /// </summary>
         
        public string IsChangeOrigin { get; set; }

        /// <summary>
        /// 生源所在地 
        /// </summary>
         
        public string Origin { get; set; }

        /// <summary>
        /// 生源地所在地省
        /// </summary>
         
        public string OriginProvince { get; set; }

        /// <summary>
        /// 生源地所在地市
        /// </summary>
         
        public string OriginCity { get; set; }

        /// <summary>
        /// 生源地所在地县
        /// </summary>
         
        public string OriginCounty { get; set; }

        /// <summary>
        /// 生源地所在代码
        /// </summary>
        public string OriginCode { get; set; }

        /// <summary>
        /// 家庭户口所在地类型
        /// </summary>
         
        public string ResLocationType { get; set; }

        /// <summary>
        /// 家庭户口所在地
        /// </summary>
         
        public string ResLocation { get; set; }

        /// <summary>
        /// 家庭户口所在地省
        /// </summary>
         
        public string ResProvince { get; set; }

        /// <summary>
        /// 家庭户口所在地市
        /// </summary>
         
        public string ResCity { get; set; }

        /// <summary>
        /// 家庭户口所在地县
        /// </summary>
         
        public string ResCounty { get; set; }

        /// <summary>
        /// 家庭户口所在地代码
        /// </summary>
        public string ResLocationCode { get; set; }

        /// <summary>
        /// 家庭户口类型
        /// </summary>
         
        public string ResType { get; set; }

        /// <summary>
        /// 现任职务
        /// </summary>
         
        public string Post { get; set; }

        /// <summary>
        /// 家庭电话
        /// </summary>
         
        public string FamilyTel { get; set; }

        /// <summary>
        /// 其他联系方式
        /// </summary>
         
        public string OtherTel { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
         
        public string CommCode { get; set; }

        /// <summary>
        /// 通信地址
        /// </summary>
         
        public string CommAddress { get; set; }


        /// <summary>
        /// 家庭邮编
        /// </summary>

         
        public string FamilyZipCode { get; set; }

        /// <summary>
        /// 家庭详细地址
        /// </summary>

         
        public string FamilyLocation { get; set; }

        /// <summary>
        /// 父亲姓名
        /// </summary>
         
        public string FatherName { get; set; }

        /// <summary>
        /// 父亲工作单位
        /// </summary>
         
        public string FatherWorkUnit { get; set; }

        /// <summary>
        /// 母亲姓名
        /// </summary>
         
        public string MotherName { get; set; }

        /// <summary>
        /// 母亲工作单位
        /// </summary>
         
        public string MotherWorkUnit { get; set; }

        /// <summary>
        /// 基本信息备注
        /// </summary>
         
        public string BaseNote { get; set; }

        /// <summary>
        /// 基本信息是否审核
        /// </summary>
        public string IsBaseChecked { get; set; }

        /// <summary>
        /// 学制(填写的为数字，4或者2.5)
        /// </summary>
        public string LengthOfSch { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 学历代码
        /// </summary>
        public string EducationCode { get; set; }

        /// <summary>
        /// 培养方式
        /// </summary>
        public string TrainType { get; set; }

        /// <summary>
        /// 培养方式代码
        /// </summary>
        public string TrainTypeCode { get; set; }

        /// <summary>
        /// 定向或委培单位
        /// </summary>
        public string Weituo { get; set; }

        /// <summary>
        /// 入学年份
        /// </summary>
        public string EntranceYear { get; set; }

        /// <summary>
        /// 入学时间
        /// </summary>
        public string EntranceTime { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public string GraduationTime { get; set; }

        /// <summary>
        /// 综合排名
        /// </summary>
        public string NM { get; set; }

        /// <summary>
        /// 综合排名n
        /// </summary>
        public string NumberN { get; set; }

        /// <summary>
        /// 综合排名m
        /// </summary>
        public string NumberM { get; set; }

        /// <summary>
        /// 排名方式(1班级排名/2专业排名)
        /// </summary>
        public string RankType { get; set; }

        /// <summary>
        /// 在校任职情况(1学生干部/2学生助理/3社团干部/0无)
        /// </summary>
        public string PostAtSch { get; set; }

        /// <summary>
        /// 第一年度获奖
        /// </summary>
        public string FristYearPrize { get; set; }

        /// <summary>
        /// 第二年度获奖
        /// </summary>
        public string SecondYearPrize { get; set; }

        /// <summary>
        /// 第三年度获奖
        /// </summary>
        public string ThirdYearPrize { get; set; }

        /// <summary>
        /// 在校期间其他荣誉及奖励（简要）
        /// </summary>
        public string BriefPrize { get; set; }

        /// <summary>
        /// 英语通过级别
        /// </summary>
        public string EnglishLevel { get; set; }

        /// <summary>
        /// 四级成绩
        /// </summary>
        public string LevelFour { get; set; }

        /// <summary>
        /// 六级成绩
        /// </summary>
        public string LevelSix { get; set; }

        /// <summary>
        /// 计算机级别
        /// </summary>
        public string ComputerLevel { get; set; }

        /// <summary>
        /// 求职备注
        /// </summary>
        public string ApplNote { get; set; }

        /// <summary>
        /// 业务素质
        /// </summary>
        public string StudyTest { get; set; }

        /// <summary>
        /// 思想道德素质表现
        /// </summary>
        public string MoralTest { get; set; }

        /// <summary>
        /// 科技创新能力
        /// </summary>
        public string ScienceTest { get; set; }

        /// <summary>
        /// 社会活动能力
        /// </summary>
        public string ActTest { get; set; }

        /// <summary>
        /// 文化素质
        /// </summary>
        public string CultureTest { get; set; }

        /// <summary>
        /// 身心素质
        /// </summary>
        public string HealthTest { get; set; }

        /// <summary>
        /// 综合测评结果
        /// </summary>
        public string TotalTest { get; set; }

        /// <summary>
        /// 求职信息是否审核
        /// </summary>
        public string IsQiuChecked { get; set; }

        /// <summary>
        /// 毕业去向代码
        /// </summary>
        public string EmploymentType { get; set; }

        /// <summary>
        /// 协议书编号（唯一）
        /// </summary>
        public string AgreementID { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 单位所属集团或系统
        /// </summary>
        public string ComBelongGroup { get; set; }

        /// <summary>
        /// 单位组织机构代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 单位上级隶属部门
        /// </summary>
        public string ComBelongDep { get; set; }

        /// <summary>
        /// 单位隶属部门代码
        /// </summary>
        public string ComBelongDepCode { get; set; }

        /// <summary>
        /// 单位行业
        /// </summary>
        public string ComIndustry { get; set; }

        /// <summary>
        /// 单位行业代码
        /// </summary>
        public string ComIndustryCode { get; set; }

        /// <summary>
        /// 工作职务
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 工作职务代码
        /// </summary>
        public string JobTitleCode { get; set; }

        /// <summary>
        /// 《就业报到证》开具单位名称
        /// </summary>
        public string SCompanyName { get; set; }

        /// <summary>
        /// 单位所在地
        /// </summary>
        public string ComLocation { get; set; }

        /// <summary>
        /// 单位所在地省
        /// </summary>
        public string ComProvince { get; set; }

        /// <summary>
        /// 单位所在地市
        /// </summary>
        public string ComCity { get; set; }

        /// <summary>
        /// 单位所在地县
        /// </summary>
        public string ComCounty { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>
        public string ComType { get; set; }

        /// <summary>
        /// 单位性质代码
        /// </summary>
        public string ComTypeCode { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 联系人邮编
        /// </summary>
        public string ContactsCode { get; set; }

        /// <summary>
        /// 联系人通信地址
        /// </summary>
        public string ConCommAddress { get; set; }

        /// <summary>
        /// 联系人职务
        /// </summary>
        public string ConPost { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ConTel { get; set; }

        /// <summary>
        /// 户口迁移地址
        /// </summary>
        public string ResChangedAdd { get; set; }

        /// <summary>
        /// 档案转寄详细地址
        /// </summary>
        public string FileChangedAdd { get; set; }

        /// <summary>
        /// 邮政编码（档案转寄地）
        /// </summary>
        public string FileZipCode { get; set; }

        /// <summary>
        /// 是否专业对口
        /// </summary>
        public string IsMajorFit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 考研/保研
        /// </summary>
        public string UpType { get; set; }

        /// <summary>
        /// 升学院校名称
        /// </summary>
        public string UPSchool { get; set; }

        /// <summary>
        /// 升学专业
        /// </summary>
        public string UpMajor { get; set; }

        /// <summary>
        /// 留学院校名称
        /// </summary>
        public string OutSchool { get; set; }

        /// <summary>
        /// 留学专业
        /// </summary>
        public string OutMajor { get; set; }

        /// <summary>
        /// 出国国家
        /// </summary>
        public string OutCountry { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public string SignTimeQ { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyNameQ { get; set; }

        /// <summary>
        /// 所属集团或系统
        /// </summary>
        public string ComBelongGroupQ { get; set; }

        /// <summary>
        /// 隶属部门
        /// </summary>
        public string ComBelongDepQ { get; set; }

        /// <summary>
        /// 隶属部门代码
        /// </summary>
        public string ComBelongDepCodeQ { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>
        public string ComTypeQ { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string CompanyAddressQ { get; set; }

        /// <summary>
        /// 单位联系人
        /// </summary>
        public string CompanyConnQ { get; set; }

        /// <summary>
        /// 单位电话
        /// </summary>
        public string CompanyTelQ { get; set; }

        /// <summary>
        /// 个人联系方式
        /// </summary>
        public string PerTelTypeQ { get; set; }

        /// <summary>
        /// 签约方式
        /// </summary>
        public string SignTypeQ { get; set; }

        /// <summary>
        /// 协议书编号
        /// </summary>
        public string AgreementIDQ { get; set; }
    }
}