using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 学院管理显示模型
    /// </summary>
    public class AcademyViewModel
    {
        public AcademyModel Academy { get; set; }

        public List<AcademyModel> AcademyList { get; set; }
    }
}