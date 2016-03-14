using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace Graduation.Models
{
    /// <summary>
    /// 用于本科生和研究生生源信息列表
    /// </summary>
    public class AdminGradViewModel
    {
        /// <summary>
        /// 用于搜索框的model
        /// </summary>
        public UploadModel upload { get; set; }

        /// <summary>
        /// 分页的信息
        /// </summary>
        public PagedList<UploadModel> uploadPagedList { get; set; }
        /// <summary>
        /// 基本信息的列表
        /// </summary>
        public List<UploadModel> uploadList { get; set; }
    }
}