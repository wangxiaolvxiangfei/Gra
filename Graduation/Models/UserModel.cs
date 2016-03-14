using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class UserModel
    {
        /// <summary>
        /// ID主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 用户类型，权限
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 用户类型编码
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 所属系ID
        /// </summary>
        public int DepartId { get; set; }

        /// <summary>
        /// 所属系的名称 
        /// </summary>
        public string DepartName { get; set; }
    }
}