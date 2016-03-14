using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class EmplExamListViewModel
    {
        /// <summary>
        /// 上传的数据
        /// </summary>
        public UploadModel upload { get; set; }

        /// <summary>
        /// table中显示的数据
        /// </summary>
        public List<ESchoolInfoViewModel> InfoList { get; set; }

        /// <summary>
        /// 是否通过审核
        /// </summary>
        public string IsCheckEd { get; set; }

        /// <summary>
        /// 是否签学校第三方
        /// </summary>
        public string IsESchool { get; set; }

        /// <summary>
        /// 是否就业
        /// </summary>
        public string IsJiuye { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public string IsClocked { get; set; }
    }
}