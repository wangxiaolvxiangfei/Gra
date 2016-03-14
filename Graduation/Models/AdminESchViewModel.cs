using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 用于就业信息管理中就业信息导出中的查询
    /// </summary>
    public class AdminESchViewModel
    {

        public UploadModel upload { get; set; }

        public List<UploadModel> uploadList { get; set; }

        public ESchoolInfoModel eSchoolInfo { get; set; }

        public List<ESchoolInfoModel> eSchoolInfoList { get; set; }
    }
}