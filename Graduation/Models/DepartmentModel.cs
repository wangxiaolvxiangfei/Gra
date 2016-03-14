using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 系模型
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 外键，所属学院ID
        /// </summary>
        public int BelongId { get; set; }

        /// <summary>
        /// 所属院名字
        /// </summary>
        public string BelongName { get; set; }
        /// <summary>
        /// 系名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系代码
        /// </summary>
        public string Code { get; set; }
    }
}