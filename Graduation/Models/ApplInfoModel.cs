using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Graduation.Models
{
    public class ApplInfoModel
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Key, ForeignKey("uploadModel")]
        public string StudentNumber { get; set; }

        /// <summary>
        /// 综合排名
        /// </summary>
        public string NM { get; set; }

        /// <summary>
        /// 综合排名n
        /// </summary>
        public string NumberN { get; set; }

        /// <summary>
        /// 综合排名m
        /// </summary>
        public string NumberM { get; set; }

        /// <summary>
        /// 排名方式(1班级排名/2专业排名)
        /// </summary>
        public string RankType { get; set; }

        /// <summary>
        /// 排名方式（用于下载） 
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// 在校任职情况(1学生干部/2学生助理/3社团干部/0无)
        /// </summary>
        public string PostAtSch { get; set; }

        /// <summary>
        /// 第一年度获奖
        /// </summary>
        public string FristYearPrize { get; set; }

        /// <summary>
        /// 第二年度获奖
        /// </summary>
        public string SecondYearPrize { get; set; }

        /// <summary>
        /// 第三年度获奖
        /// </summary>
        public string ThirdYearPrize { get; set; }

        /// <summary>
        /// 在校期间其他荣誉及奖励（简要）
        /// </summary>
        public string BriefPrize { get; set; }

        /// <summary>
        /// 竞赛获奖
        /// </summary>
        public string CompetitionPrize { get; set; }
        /// <summary>
        /// 其他获奖情况
        /// </summary>
        public string OtherPrize { get; set; }
        /// <summary>
        /// 发表论文和学术成果
        /// </summary>
        public string PublishPaper { get; set; }

        /// <summary>
        /// 英语通过级别
        /// </summary>
        public string EnglishLevel { get; set; }

        /// <summary>
        /// 四级成绩
        /// </summary>
        public string LevelFour { get; set; }

        /// <summary>
        /// 六级成绩
        /// </summary>
        public string LevelSix { get; set; }

        /// <summary>
        /// 计算机级别或水平
        /// </summary>
        public string ComputerLevel { get; set; }

        /// <summary>
        /// 求职备注
        /// </summary>
        public string ApplNote { get; set; }

        /// <summary>
        /// 语言考试分数
        /// </summary>
        public string LanguageScore { get; set; }

        /// <summary>
        /// 特长能力
        /// </summary>
        public string SpecialtyAbilty { get; set; }

        /// <summary>
        /// 社会工作
        /// </summary>
        public string SocietyJob { get; set; }

        /// <summary>
        /// 实践活动
        /// </summary>
        public string PracticeActivity { get; set; }

        /// <summary>
        /// 院系意见
        /// </summary>
        public string AcademicOpinion { get; set; }

        /// <summary>
        /// 导师意见
        /// </summary>
        public string TutorOpinion { get; set; }

        /// <summary>
        /// 业务素质
        /// </summary>
        public string StudyTest { get; set; }

        /// <summary>
        /// 思想道德素质表现
        /// </summary>
        public string MoralTest { get; set; }

        /// <summary>
        /// 科技创新能力
        /// </summary>
        public string ScienceTest { get; set; }

        /// <summary>
        /// 社会活动能力
        /// </summary>
        public string ActTest { get; set; }

        /// <summary>
        /// 文化素质
        /// </summary>
        public string CultureTest { get; set; }

        /// <summary>
        /// 身心素质
        /// </summary>
        public string HealthTest { get; set; }

        /// <summary>
        /// 综合测评结果
        /// </summary>
        public string TotalTest { get; set; }

        /// <summary>
        /// 求职信息是否审核
        /// </summary>
        public string IsQiuChecked { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public string IsClocked { get; set; }

        /// <summary>
        /// 是否完成综合测评
        /// </summary>
        public string IsFinish { get; set; }

        /// <summary>
        /// 学生编辑的时间
        /// </summary>
        public DateTime StudentEditTime { get; set; }

        /// <summary>
        /// 老师编辑的时间
        /// </summary>
        public DateTime TeacherEditTime { get; set; }

        public virtual UploadModel uploadModel { get; set; }
    }
}