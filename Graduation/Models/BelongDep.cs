using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class BelongDep
    {

        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 隶属部门
        /// </summary>
        public string ComBelongDep { get; set; }

        /// <summary>
        /// 隶属部门代码
        /// </summary>
        public string ComBelongDepCode { get; set; }
    }
}