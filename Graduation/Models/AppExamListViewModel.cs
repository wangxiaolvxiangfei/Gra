using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace Graduation.Models
{
    public class AppExamListViewModel
    {
        /// <summary>
        /// 老师上传信息
        /// </summary>
        public UploadModel upload { get; set; }


        public PagedList<UploadModel> uploadPagedList { get; set; }


       


        /// <summary>
        /// table中要显示的list
        /// </summary>
        public List<ApplInfoViewModel> appInfoList { get; set; }


        /// <summary>
        /// table中要显示的list（含分页）
        /// </summary>
        public PagedList<ApplInfoViewModel> applInfoPageList { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public string IsChecked { get; set; }

        /// <summary>
        /// 是否完成综合测评
        /// </summary>
        public string IsFinish { get; set; }
    }
}