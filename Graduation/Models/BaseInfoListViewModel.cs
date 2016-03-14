using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class BaseInfoListViewModel
    {
        /// <summary>
        /// 用于搜索框的model
        /// </summary>
        public UploadModel upload { get; set; }
        /// <summary>
        /// 基本信息的列表
        /// </summary>
        public List<FillBaseInfoViewModel> uploadList { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public string isChecked { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public string isClocked { get; set; }
    }
}