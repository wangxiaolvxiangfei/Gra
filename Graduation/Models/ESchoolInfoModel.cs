using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class ESchoolInfoModel
    {

        
        /// <summary>
        /// 学号（唯一标识）
        /// </summary>
        [Key, ForeignKey("uploadModel")]
        public string StudentNumber { get; set; }

        /// <summary>
        /// 协议书编号（唯一）
        /// </summary>
        public string AgreementID { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 单位所属集团或系统
        /// </summary>
        public string ComBelongGroup { get; set; }

        /// <summary>
        /// 单位组织机构代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 单位上级隶属部门
        /// </summary>
        public string ComBelongDep { get; set; }

        /// <summary>
        /// 单位隶属部门代码
        /// </summary>
        public string ComBelongDepCode { get; set; }

        /// <summary>
        /// 单位行业
        /// </summary>
        public string ComIndustry { get; set; }

        /// <summary>
        /// 单位行业代码
        /// </summary>
        public string ComIndustryCode { get; set; }

        /// <summary>
        /// 工作职务
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 工作职务代码
        /// </summary>
        public string JobTitleCode { get; set; }

        /// <summary>
        /// 《就业报到证》开具单位名称
        /// </summary>
        public string SCompanyName { get; set; }

        /// <summary>
        /// 单位所在地
        /// </summary>
        public string ComLocation { get; set; }

        /// <summary>
        /// 单位所在地省
        /// </summary>
        public string ComProvince { get; set; }

        /// <summary>
        /// 单位所在地市
        /// </summary>
        public string ComCity { get; set; }

        /// <summary>
        /// 单位所在地县
        /// </summary>
        public string ComCounty { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>
        public string ComType { get; set; }

        /// <summary>
        /// 单位性质代码
        /// </summary>
        public string ComTypeCode { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 联系人邮编
        /// </summary>
        public string ContactsCode { get; set; }

        /// <summary>
        /// 联系人通信地址
        /// </summary>
        public string ConCommAddress { get; set; }

        /// <summary>
        /// 联系人职务
        /// </summary>
        public string ConPost { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ConTel { get; set; }

        /// <summary>
        /// 户口迁移地址
        /// </summary>
        public string ResChangedAdd { get; set; }

        /// <summary>
        /// 档案转寄详细地址
        /// </summary>
        public string FileChangedAdd { get; set; }

        /// <summary>
        /// 邮政编码（档案转寄地）
        /// </summary>
        public string FileZipCode { get; set; }

        /// <summary>
        /// 是否专业对口
        /// </summary>
        public string IsMajorFit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 考研/保研
        /// </summary>
        public string UpType { get; set; }

        /// <summary>
        /// 升学院校名称
        /// </summary>
        public string UPSchool { get; set; }

        /// <summary>
        /// 升学专业
        /// </summary>
        public string UpMajor { get; set; }

        /// <summary>
        /// 留学院校名称
        /// </summary>
        public string OutSchool { get; set; }

        /// <summary>
        /// 留学专业
        /// </summary>
        public string OutMajor { get; set; }

        /// <summary>
        /// 出国国家
        /// </summary>
        public string OutCountry { get; set; }

        /// <summary>
        /// 就业形式
        /// </summary>
        public string Employment { get; set; }
        /// <summary>
        /// 就业形式代码
        /// </summary>
        public string EmploymentCode { get; set; }

        /// <summary>
        /// 就业困难下的就业形式
        /// </summary>
        public string JobDiff { get; set; }
        /// <summary>
        /// 就业困难下的就业形式代码
        /// </summary>

        public string JobDiffCode { get; set; }

        /// <summary>
        /// 就业困难原因
        /// </summary>
        public string JobDiffCause { get; set; }

        /// <summary>
        /// 学生编辑时间
        /// </summary>
        public DateTime? StudentEditTime { get; set; }

        /// <summary>
        /// 老师编辑时间
        /// </summary>
        public DateTime? TeacherEditTime { get; set; }

        /// <summary>
        /// 是否通过审核
        /// </summary>
        public string IsChecked { get; set; }

        /// <summary>
        /// 是否就业
        /// </summary>
        public string IsJiuYe { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public string IsClock { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
             

        /// <summary>
        /// 连接查询
        /// </summary>
        public virtual UploadModel uploadModel { get; set; }
    }
}