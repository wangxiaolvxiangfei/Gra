using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    /// <summary>
    /// 签约登记管理模型
    /// </summary>
    public class SignInfoModel
    {
        /// <summary>
        /// 学号，唯一标识
        /// </summary>
        [Key]
        public string StudentNumber { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public string SignTime { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 所属集团或系统
        /// </summary>
        public string ComBelongGroup { get; set; }

        /// <summary>
        /// 隶属部门
        /// </summary>
        public string ComBelongDep { get; set; }

        /// <summary>
        /// 隶属部门代码
        /// </summary>
        public string ComBelongDepCode { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>
        public string ComType { get; set; }

        /// <summary>
        /// 单位性质代码
        /// </summary>
        public string ComTypeCode { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 单位联系人
        /// </summary>
        public string CompanyConn { get; set; }

        /// <summary>
        /// 单位电话
        /// </summary>
        public string CompanyTel { get; set; }

        /// <summary>
        /// 个人联系方式
        /// </summary>
        public string PerTelType { get; set; }

        /// <summary>
        /// 签约方式
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 协议书编号
        /// </summary>
        public string AgreementID { get; set; }
    }
}