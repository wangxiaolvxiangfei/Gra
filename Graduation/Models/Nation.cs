using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class Nation
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string NationName { get; set; }


        /// <summary>
        /// 民族代码
        /// </summary>
        public string NationCode { get; set; }
    }
}