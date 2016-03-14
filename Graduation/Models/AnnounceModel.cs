using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 后台发布通知模型
    /// </summary>
    public class AnnounceModel
    {
        /// <summary>
        /// 公告Id
        /// </summary>
        [Key]
        public int AnnounceId { get; set; }

        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string Message { get; set; }

    }
}