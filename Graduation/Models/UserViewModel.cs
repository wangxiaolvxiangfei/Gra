using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// 添加用户信息
        /// </summary>
        public UserModel user { get; set; }

        /// <summary>
        /// 用户信息列表
        /// </summary>
        public List<UserModel> userList { get; set; }
    }
}