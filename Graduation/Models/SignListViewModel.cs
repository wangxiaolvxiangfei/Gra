using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class SignListViewModel
    {
        /// <summary>
        /// 学生基本信息
        /// </summary>
        public UploadModel Upload { get; set; }

        /// <summary>
        /// 签约等级列表
        /// </summary>
        public List<SignInfoViewModel> SignList { get; set; }
    }
}