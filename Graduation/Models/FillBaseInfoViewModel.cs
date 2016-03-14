using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class FillBaseInfoViewModel
    {
        /// <summary>
        /// 填写的基本信息
        /// </summary>
        public FillBaseInfoModel baseInfo { get; set; }
        /// <summary>
        /// 老师上传的基本信息
        /// </summary>
        public UploadModel upload { get; set; }
    }
}