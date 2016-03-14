using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class DepartmentViewModel
    {
        public DepartmentModel depart { get; set; }
        public List<DepartmentModel> departList { get; set; }
    }
}