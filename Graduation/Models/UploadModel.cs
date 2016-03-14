using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class UploadModel
    {
        /// <summary>
        /// 学号(主键)
        /// </summary>
        [Key]
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
        /// 是否专业学位
        /// </summary>
        public string Sfzyxw { get; set; }
        /// <summary>
        /// 连接就业信息表
        /// </summary>
        public virtual ESchoolInfoModel eSchoolInfoModel { get; set; }

        /// <summary>
        /// 连接求职信息表
        /// </summary>
        public virtual ApplInfoModel applInfoModel { get; set; }


        /// <summary>
        /// 连接基本信息表
        /// </summary>
        public virtual FillBaseInfoModel fillBaseInfoModel { get; set; }




    }
}