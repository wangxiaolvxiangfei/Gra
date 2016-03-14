using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class StudentsLoginModel
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "学号不能为空")]
        public string StudentNumber { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Pwd { get; set; }

        /// <summary>
        /// 学生类型，0为研究生，1为本科生
        /// </summary>
        public string Type { get; set; }
    }
}