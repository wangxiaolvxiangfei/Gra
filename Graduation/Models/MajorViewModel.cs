using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class MajorViewModel
    {
        /// <summary>
        /// 专业
        /// </summary>
        public MajorModel Major { get; set; }
        /// <summary>
        /// 专业列表
        /// </summary>
        public List<MajorModel> MajorList { get; set; }
    }
}