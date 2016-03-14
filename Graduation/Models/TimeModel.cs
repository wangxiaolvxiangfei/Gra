using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class TimeModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 时间类型（1研究生，2本科生，3院系管理员）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 时间类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}