using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 数量模型
    /// </summary>
    public class NumberModel
    {
        /// <summary>
        /// id,所属专业ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 所属专业Id
        /// </summary>
        public int belongId { get; set; }
        /// <summary>
        /// 专业名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 专业人数
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// 签约人数
        /// </summary>
        public int signNumber { get; set; }

        /// <summary>
        /// 签约率
        /// </summary>
        public double signP { get; set; }

        /// <summary>
        /// 保研人数
        /// </summary>
        public int baoyan { get; set; }

        /// <summary>
        /// 保研率
        /// </summary>
        public double baoyanP { get; set; }

        /// <summary>
        /// 签约和保研人数
        /// </summary>
        public int signAbaoyan { get; set; }

        /// <summary>
        /// 签约和保研比例
        /// </summary>
        public double signAbaoyanP { get; set; }

        /// <summary>
        /// 考研上线人数
        /// </summary>
        public int kaoyan { get; set; }

        /// <summary>
        /// 考研上线比例
        /// </summary>
        public double kanyanP { get; set; }

        /// <summary>
        /// 意向人数
        /// </summary>
        public int yixiang { get; set; }

        /// <summary>
        /// 意向比例
        /// </summary>
        public double yixiangP { get; set; }

        /// <summary>
        /// 其他人数
        /// </summary>
        public int qita { get; set; }

        /// <summary>
        /// 其他比例
        /// </summary>
        public double qitaP { get; set; }

        /// <summary>
        /// 签约保研升学其他人数
        /// </summary>
        public int zonghe { get; set; }
        /// <summary>
        /// 综合比例
        /// </summary>
        public double zongheP { get; set; }

        /// <summary>
        /// 剩余人数
        /// </summary>
        public int shengyu { get; set; }

        /// <summary>
        /// 剩余比例
        /// </summary>
        public double shengyuP { get; set; }
    }
}