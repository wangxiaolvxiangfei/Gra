using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class LocationModel
    {
        /// <summary>
        /// 地区编码
        /// </summary>
        [Key]
        public string code { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string name { get; set; }
    }
}