using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class TimeViewModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public TimeModel time { get; set; }
        /// <summary>
        /// 时间列表
        /// </summary>
        public List<TimeModel> TimeList { get; set; }
    }
}