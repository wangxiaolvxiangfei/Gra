using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class ESchoolInfoViewModel
    {
        /// <summary>
        /// 签学校第三方协议就业
        /// </summary>
        public ESchoolInfoModel eSchoolInfo { get; set; }

        /// <summary>
        /// 上传数据信息
        /// </summary>
        public UploadModel upload { get; set; }
    }
}