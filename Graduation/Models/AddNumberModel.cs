using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 包含了院列表、系列表、专业列表
    /// </summary>
    public class AddNumberModel
    {
        /// <summary>
        /// 院列表
        /// </summary>
        public List<AcademyModel> acaList { get; set; }
        /// <summary>
        /// 系列表
        /// </summary>
        public List<DepartmentModel> depList { get; set; }
        /// <summary>
        /// 专业列表(本科专业)
        /// </summary>
        public List<MajorModel> BmajorList { get; set; }

        /// <summary>
        /// 专业列表(研究生)
        /// </summary>
        public List<MajorModel> YmajorList { get; set; }
        /// <summary>
        /// 专业人数(本科生人数)
        /// </summary>
        public List<NumberModel> BnumberList { get; set; }

        /// <summary>
        /// 专业人数(研究生)
        /// </summary>
        public List<NumberModel> YnumberList { get; set; }
    }
}