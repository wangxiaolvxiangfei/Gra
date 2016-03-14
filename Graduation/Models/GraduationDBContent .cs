using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class GraduationDBContent : DbContext
    {
        public DbSet<Students> StudentsTb
        { get; set; }

        public DbSet<UploadModel> UploadTb
        { get; set; }

        public DbSet<FillBaseInfoModel> BaseInfoTb
        { get; set; }

        public DbSet<ApplInfoModel> ApplInfoTb
        { get; set; }

        public DbSet<SignInfoModel> SingInfoTb
        { get; set; }

        public DbSet<ESchoolInfoModel> ESchoolInfoTb
        { get; set; }

        public DbSet<UserModel> UserTb
        { get; set; }

        public DbSet<AcademyModel> AcademyTb
        { get; set; }

        public DbSet<DepartmentModel> DepartmentTb
        { get; set; }

        public DbSet<MajorModel> MajorTb
        { get; set; }

        public DbSet<AnnounceModel> AnnounceTb
        { get; set; }

        public DbSet<TimeModel> TimeTb
        { get; set; }
    }
}