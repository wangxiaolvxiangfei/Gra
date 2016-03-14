using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 上传文件的viewmodel
    /// </summary>
    public class UploadViewModel
    {
        /// <summary>
        /// 上传学生类型
        /// </summary>
        public string StudentType { get; set; }

        /// <summary>
        /// 上传文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
}