using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class SignInfoViewModel
    {
        /// <summary>
        /// 签约信息
        /// </summary>
        public SignInfoModel signInfo { get; set; }
        /// <summary>
        /// 上传信息
        /// </summary>
        public UploadModel upload { get; set; }
    }
}