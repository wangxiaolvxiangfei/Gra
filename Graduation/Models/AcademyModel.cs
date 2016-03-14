using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 学院模型
    /// </summary>
    public class AcademyModel
    {
        /// <summary>
        /// 学院ID
        /// </summary>
        [Key]
        public int Id
        { get; set; }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string Name
        { get; set; }
    }
}