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
        /// 签约率
        /// </summary>
        public double signP { get; set; }

        /// <summary>
        /// 合同就业人数
        /// </summary>
        public int hetongNumber { get; set; }

        /// <summary>
        /// 合同就业率
        /// </summary>
        public double hetongP { get; set; }

        /// <summary>
        /// 考研人数
        /// </summary>
        public int kaoyan { get; set; }

        /// <summary>
        /// 考研率
        /// </summary>
        public double kanyanP { get; set; }
        /// <summary>
        /// 保研人数
        /// </summary>
        public int baoyan { get; set; }

        /// <summary>
        /// 保研率
        /// </summary>
        public double baoyanP { get; set; }
        /// <summary>
        /// 升学人数
        /// </summary>
        public int upschoolNumber { get; set; }

        /// <summary>
        /// 升学率
        /// </summary>
        public double upschoolP { get; set; }

        /// <summary>
        /// 出国人数
        /// </summary>
        public int abroadNumber { get; set; }

        /// <summary>
        /// 出国率
        /// </summary>
        public double abroadP { get; set; }
        /// <summary>
        /// 自主创业人数
        /// </summary>
        public int zizhuNumber { get; set; }

        /// <summary>
        /// 自主创业率
        /// </summary>
        public double zizhuP { get; set; }

        /// <summary>
        /// 应征入伍人数
        /// </summary>
        public int ruwu { get; set; }

        /// <summary>
        /// 应征入伍率
        /// </summary>
        public double ruwuP { get; set; }

        /// <summary>
        /// 国家项目
        /// </summary>
        public int guojia { get; set; }

        /// <summary>
        /// 国家项目率
        /// </summary>
        public double guojiaP { get; set; }

        /// <summary>
        /// 科研助理
        /// </summary>
        public int keyanzhuli { get; set; }

        /// <summary>
        /// 科研助理率
        /// </summary>
        public double keyanzhuliP { get; set; }
        /// <summary>
        /// 地方基层项目
        /// </summary>
        public int difang { get; set; }

        /// <summary>
        /// 地方基层项目率
        /// </summary>
        public double difangP { get; set; }

        /// <summary>
        /// 灵活就业
        /// </summary>
        public int linghuo { get; set; }

        /// <summary>
        /// 灵活就业率
        /// </summary>
        public double linghuoP { get; set; }

        /// <summary>
        /// 继续考公务员人数
        /// </summary>
        public int gongwuyuan { get; set; }

        /// <summary>
        /// 继续考公务员率
        /// </summary>
        public double gongwuyuanP { get; set; }
        /// <summary>
        /// 就业困难人数
        /// </summary>
        public int jiuyekunnan { get; set; }

        /// <summary>
        /// 就业困难率
        /// </summary>
        public double jiuyekunnanP { get; set; }

        /// <summary>
        /// 其他暂未就业人数
        /// </summary>
        public int other { get; set; }

        /// <summary>
        /// 其他暂未就业人数率
        /// </summary>
        public double otherP { get; set; }

        /// <summary>
        /// 就业合计人数 
        /// </summary>
        public int jiuyeheji { get; set; }

        /// <summary>
        /// 就业合计率
        /// </summary>
        public double jiuyehejiP { get; set; }

        /// <summary>
        /// 未就业
        /// </summary>
        public int weijiuye { get; set; }

        /// <summary>
        /// 未就业率
        /// </summary>
        public double weijiuyyeP { get; set; }
    }
}