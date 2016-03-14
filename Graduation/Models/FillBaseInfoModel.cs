using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class FillBaseInfoModel
    {

        /// <summary>
        /// 学号(主键)
        /// </summary>
        [Key, ForeignKey("uploadModel")]
        public string StudentNumber { get; set; }

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
        /// 研究生导师
        /// </summary>
        public string Tutor { get; set; }

        /// <summary>
        /// 是否专业学位
        /// </summary>
        public string IsMajorDegrees { get; set; }

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
        /// 是否锁定
        /// </summary>
        public string IsClocked { get; set; }

        /// <summary>
        /// 审核的时间
        /// </summary>
        public DateTime? CheckTime { get; set; }



        /// <summary>
        /// 连接上传信息
        /// </summary>
        public virtual UploadModel uploadModel { get; set; }
    }
}