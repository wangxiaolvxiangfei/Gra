using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class jieyeListViewModel
    {
        /// <summary>
        /// 学院
        /// </summary>
        public string aca { get; set; }

        /// <summary>
        /// 系
        /// </summary>
        public string dep { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string major { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public int totalNumber { get; set; }

        /// <summary>
        /// 签约人数（有协议书就业）
        /// </summary>
        public int signNumber { get; set; }

        /// <summary>
        /// 合同就业人数
        /// </summary>
        public int hetongNumber { get; set; }

        /// <summary>
        /// 升学人数
        /// </summary>
        public int upschoolNumber { get; set; }

        /// <summary>
        /// 出国人数
        /// </summary>
        public int abroadNumber { get; set; }
        /// <summary>
        /// 自主创业人数
        /// </summary>
        public int zizhuNumber { get; set; }

        /// <summary>
        /// 应征入伍人数
        /// </summary>
        public int ruwu { get; set; }

        /// <summary>
        /// 国家项目
        /// </summary>
        public int guojia { get; set; }

        /// <summary>
        /// 科研助理
        /// </summary>
        public int keyanzhuli { get; set; }

        /// <summary>
        /// 地方基层项目
        /// </summary>
        public int difang { get; set; }

        /// <summary>
        /// 灵活就业
        /// </summary>
        public int linghuo { get; set; }

        /// <summary>
        /// 继续考公务员人数
        /// </summary>
        public int gongwuyuan { get; set; }

        /// <summary>
        /// 就业困难人数
        /// </summary>
        public int jiuyekunnan { get; set; }

        public int jiuyeheji { get; set; }

        public int weijiuye { get; set; }
    }
}