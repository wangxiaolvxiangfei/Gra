using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 求职信息显示模型
    /// </summary>
    public class ApplInfoViewModel
    {
        /// <summary>
        /// 求职信息模型
        /// </summary>
        public ApplInfoModel applInfo { get; set; }
        /// <summary>
        /// 老师上传数据模型
        /// </summary>
        public UploadModel upload { get; set; }
    }
}