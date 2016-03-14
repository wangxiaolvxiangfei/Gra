using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 专业模型
    /// </summary>
    public class MajorModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 专业名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 专业代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string AcademyName { get; set; }

        /// <summary>
        /// 系名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 系代码
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 学院ID
        /// </summary>
        public string AcademyId { get; set; }

        /// <summary>
        /// 系ID
        /// </summary>
        public string DepartId { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Edu { get; set; }
    }
}