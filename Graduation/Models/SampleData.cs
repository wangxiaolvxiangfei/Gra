﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Graduation.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<GraduationDBContent>
    {
        protected override void Seed(GraduationDBContent context)
        {
            #region 隶属部门
            new List<BelongDep>{
                new BelongDep { ComBelongDep = "空值", ComBelongDepCode = "000" },
                new BelongDep { ComBelongDep = "录取研究生", ComBelongDepCode = "010" },
                new BelongDep { ComBelongDep = "出国、退学及其它", ComBelongDepCode = "011" },
                new BelongDep { ComBelongDep = "人大常委会办公厅", ComBelongDepCode = "101" },
                new BelongDep { ComBelongDep = "全国人大民族委员会", ComBelongDepCode = "103" },
                new BelongDep { ComBelongDep = "人大法制委员会", ComBelongDepCode = "107" },
                new BelongDep { ComBelongDep = "全国人大内务司法委员会", ComBelongDepCode = "109" },
                new BelongDep { ComBelongDep = "全国人大财政经济委员会", ComBelongDepCode = "111" },
                new BelongDep { ComBelongDep = "全国人大教科文卫委员会", ComBelongDepCode = "113" },
                new BelongDep { ComBelongDep = "全国人大外事委员会", ComBelongDepCode = "115" },
                new BelongDep { ComBelongDep = "全国人大华侨委员会", ComBelongDepCode = "117" },
                new BelongDep { ComBelongDep = "全国政协", ComBelongDepCode = "131" },
                new BelongDep { ComBelongDep = "最高检察院", ComBelongDepCode = "151" },
                new BelongDep { ComBelongDep = "最高法院", ComBelongDepCode = "161" },
                new BelongDep { ComBelongDep = "中央办公厅", ComBelongDepCode = "201" },
                new BelongDep { ComBelongDep = "中央组织部", ComBelongDepCode = "203" },
                new BelongDep { ComBelongDep = "中央宣传部", ComBelongDepCode = "211" },
                new BelongDep { ComBelongDep = "中央统战部", ComBelongDepCode = "213" },
                new BelongDep { ComBelongDep = "中央对外联络部", ComBelongDepCode = "215" },
                new BelongDep { ComBelongDep = "中央政治体制改革研究室", ComBelongDepCode = "217" },
                new BelongDep { ComBelongDep = "中央农村政策研究室", ComBelongDepCode = "219" },
                new BelongDep { ComBelongDep = "中央顾问委员会机关", ComBelongDepCode = "221" },
                new BelongDep { ComBelongDep = "中央纪律检查委员会", ComBelongDepCode = "222" },
                new BelongDep { ComBelongDep = "中央对台工作领导小组", ComBelongDepCode = "231" },
                new BelongDep { ComBelongDep = "中央直属机关工作委员会", ComBelongDepCode = "233" },
                new BelongDep { ComBelongDep = "中央国家机关工作委员会", ComBelongDepCode = "235" },
                new BelongDep { ComBelongDep = "中央党校", ComBelongDepCode = "281" },
                new BelongDep { ComBelongDep = "人民日报社", ComBelongDepCode = "282" },
                new BelongDep { ComBelongDep = "中央党史研究室", ComBelongDepCode = "283" },
                new BelongDep { ComBelongDep = "中央文献研究室", ComBelongDepCode = "284" },
                new BelongDep { ComBelongDep = "中央编译局", ComBelongDepCode = "285" },
                new BelongDep { ComBelongDep = "外交部", ComBelongDepCode = "301" },
                new BelongDep { ComBelongDep = "国防科学技术工业委员会", ComBelongDepCode = "302" },
                new BelongDep { ComBelongDep = "国家发展和改革委员会", ComBelongDepCode = "303" },
                new BelongDep { ComBelongDep = "国家经济体制改革委员会", ComBelongDepCode = "305" },
                new BelongDep { ComBelongDep = "国家科学技术委员会", ComBelongDepCode = "306" },
                new BelongDep { ComBelongDep = "总装备部", ComBelongDepCode = "307" },
                new BelongDep { ComBelongDep = "国家民族事务委员会", ComBelongDepCode = "308" },
                new BelongDep { ComBelongDep = "国家监察部", ComBelongDepCode = "311" },
                new BelongDep { ComBelongDep = "公安部", ComBelongDepCode = "312" },
                new BelongDep { ComBelongDep = "国家安全部", ComBelongDepCode = "313" },
                new BelongDep { ComBelongDep = "民政部", ComBelongDepCode = "314" },
                new BelongDep { ComBelongDep = "司法部", ComBelongDepCode = "315" },
                new BelongDep { ComBelongDep = "人事部", ComBelongDepCode = "316" },
                new BelongDep { ComBelongDep = "劳动和社会保障部", ComBelongDepCode = "317" },
                new BelongDep { ComBelongDep = "财政部", ComBelongDepCode = "318" },
                new BelongDep { ComBelongDep = "审计署", ComBelongDepCode = "319" },
                new BelongDep { ComBelongDep = "中国人民银行", ComBelongDepCode = "320" },
                new BelongDep { ComBelongDep = "商务部", ComBelongDepCode = "322" },
                new BelongDep { ComBelongDep = "中华供销合作总社", ComBelongDepCode = "323" },
                new BelongDep { ComBelongDep = "农业部", ComBelongDepCode = "326" },
                new BelongDep { ComBelongDep = "国家林业局", ComBelongDepCode = "327" },
                new BelongDep { ComBelongDep = "中国航空工业第一集团公司", ComBelongDepCode = "329" },
                new BelongDep { ComBelongDep = "中国航天科技集团公司", ComBelongDepCode = "330" },
                new BelongDep { ComBelongDep = "国家电力公司", ComBelongDepCode = "331" },
                new BelongDep { ComBelongDep = "水利部", ComBelongDepCode = "332" },
                new BelongDep { ComBelongDep = "建设部", ComBelongDepCode = "333" },
                new BelongDep { ComBelongDep = "国土资源部", ComBelongDepCode = "334" },
                new BelongDep { ComBelongDep = "铁道部", ComBelongDepCode = "347" },
                new BelongDep { ComBelongDep = "交通部", ComBelongDepCode = "348" },
                new BelongDep { ComBelongDep = "信息产业部", ComBelongDepCode = "349" },
                new BelongDep { ComBelongDep = "文化部", ComBelongDepCode = "357" },
                new BelongDep { ComBelongDep = "广播电影电视局", ComBelongDepCode = "359" },
                new BelongDep { ComBelongDep = "教育部", ComBelongDepCode = "360" },
                new BelongDep { ComBelongDep = "卫生部", ComBelongDepCode = "361" },
                new BelongDep { ComBelongDep = "国家体育总局", ComBelongDepCode = "362" },
                new BelongDep { ComBelongDep = "国家人口和计划生育委员会", ComBelongDepCode = "363" },
                new BelongDep { ComBelongDep = "中央大型企业工作委员会", ComBelongDepCode = "405" },
                new BelongDep { ComBelongDep = "国务院国有资产监督管理委员会", ComBelongDepCode = "406" },
                new BelongDep { ComBelongDep = "中国银行业监督管理委员会", ComBelongDepCode = "407" },
                new BelongDep { ComBelongDep = "国家统计局", ComBelongDepCode = "410" },
                new BelongDep { ComBelongDep = "国家物价局", ComBelongDepCode = "411" },
                new BelongDep { ComBelongDep = "国家工商行政管理局", ComBelongDepCode = "414" },
                new BelongDep { ComBelongDep = "海关总署", ComBelongDepCode = "415" },
                new BelongDep { ComBelongDep = "中国气象局", ComBelongDepCode = "416" },
                new BelongDep { ComBelongDep = "中国民用航空局", ComBelongDepCode = "417" },
                new BelongDep { ComBelongDep = "国家海洋局", ComBelongDepCode = "418" },
                new BelongDep { ComBelongDep = "国家地震局", ComBelongDepCode = "419" },
                new BelongDep { ComBelongDep = "国家旅游局", ComBelongDepCode = "420" },
                new BelongDep { ComBelongDep = "国家新闻出版署", ComBelongDepCode = "421" },
                new BelongDep { ComBelongDep = "国家技术监督局", ComBelongDepCode = "422" },
                new BelongDep { ComBelongDep = "国家土地管理局", ComBelongDepCode = "423" },
                new BelongDep { ComBelongDep = "国务院宗教事务局", ComBelongDepCode = "427" },
                new BelongDep { ComBelongDep = "国家档案局", ComBelongDepCode = "428" },
                new BelongDep { ComBelongDep = "国务院参事室", ComBelongDepCode = "429" },
                new BelongDep { ComBelongDep = "国务院机关事务管理局", ComBelongDepCode = "430" },
                new BelongDep { ComBelongDep = "国务院研究室", ComBelongDepCode = "431" },
                new BelongDep { ComBelongDep = "国务院法制局", ComBelongDepCode = "433" },
                new BelongDep { ComBelongDep = "国务院办公厅", ComBelongDepCode = "434" },
                new BelongDep { ComBelongDep = "国务院侨务办公室", ComBelongDepCode = "435" },
                new BelongDep { ComBelongDep = "国务院港澳办公室", ComBelongDepCode = "436" },
                new BelongDep { ComBelongDep = "国务院特区办公室", ComBelongDepCode = "437" },
                new BelongDep { ComBelongDep = "国务院外事办公室", ComBelongDepCode = "438" },
                new BelongDep { ComBelongDep = "国务院台湾事务办公室", ComBelongDepCode = "439" },
                new BelongDep { ComBelongDep = "国家外国专家局", ComBelongDepCode = "440" },
                new BelongDep { ComBelongDep = "国家税务总局", ComBelongDepCode = "444" },
                new BelongDep { ComBelongDep = "国家外汇管理局", ComBelongDepCode = "445" },
                new BelongDep { ComBelongDep = "中国黄金管理局", ComBelongDepCode = "446" },
                new BelongDep { ComBelongDep = "国家矿产储量管理局", ComBelongDepCode = "447" },
                new BelongDep { ComBelongDep = "国家国有资产管理局", ComBelongDepCode = "448" },
                new BelongDep { ComBelongDep = "国家保密局", ComBelongDepCode = "452" },
                new BelongDep { ComBelongDep = "国家文物局", ComBelongDepCode = "453" },
                new BelongDep { ComBelongDep = "国家版权局", ComBelongDepCode = "454" },
                new BelongDep { ComBelongDep = "外文出版局", ComBelongDepCode = "455" },
                new BelongDep { ComBelongDep = "国家核安全局", ComBelongDepCode = "458" },
                new BelongDep { ComBelongDep = "国家物资储备局", ComBelongDepCode = "462" },
                new BelongDep { ComBelongDep = "中国专利局", ComBelongDepCode = "463" },
                new BelongDep { ComBelongDep = "国家食品药品监督管理局", ComBelongDepCode = "464" },
                new BelongDep { ComBelongDep = "国家进出口商品捡验局", ComBelongDepCode = "465" },
                new BelongDep { ComBelongDep = "国家测绘局", ComBelongDepCode = "466" },
                new BelongDep { ComBelongDep = "国家环境保护局", ComBelongDepCode = "467" },
                new BelongDep { ComBelongDep = "国家中医药管理局", ComBelongDepCode = "468" },
                new BelongDep { ComBelongDep = "国际问题研究中心", ComBelongDepCode = "476" },
                new BelongDep { ComBelongDep = "国家自然科学基金委员会", ComBelongDepCode = "480" },
                new BelongDep { ComBelongDep = "国家信息中心", ComBelongDepCode = "481" },
                new BelongDep { ComBelongDep = "新华通讯社", ComBelongDepCode = "490" },
                new BelongDep { ComBelongDep = "中国科学院", ComBelongDepCode = "491" },
                new BelongDep { ComBelongDep = "中国社会科学院", ComBelongDepCode = "492" },
                new BelongDep { ComBelongDep = "中国工程物理研究院", ComBelongDepCode = "493" },
                new BelongDep { ComBelongDep = "国务院经济技术社会研究中心", ComBelongDepCode = "494" },
                new BelongDep { ComBelongDep = "国务院农村发展研究中心", ComBelongDepCode = "495" },
                new BelongDep { ComBelongDep = "中国工商银行", ComBelongDepCode = "501" },
                new BelongDep { ComBelongDep = "中国农业银行", ComBelongDepCode = "502" },
                new BelongDep { ComBelongDep = "中国银行", ComBelongDepCode = "503" },
                new BelongDep { ComBelongDep = "中国人民建设银行", ComBelongDepCode = "504" },
                new BelongDep { ComBelongDep = "中国人民保险公司", ComBelongDepCode = "505" },
                new BelongDep { ComBelongDep = "中国交通银行", ComBelongDepCode = "507" },
                new BelongDep { ComBelongDep = "中国投资银行", ComBelongDepCode = "508" },
                new BelongDep { ComBelongDep = "国家开发银行", ComBelongDepCode = "509" },
                new BelongDep { ComBelongDep = "光大实业银行", ComBelongDepCode = "510" },
                new BelongDep { ComBelongDep = "中国船舶工业集团公司", ComBelongDepCode = "511" },
                new BelongDep { ComBelongDep = "中国国际信托投资公司", ComBelongDepCode = "512" },
                new BelongDep { ComBelongDep = "中国石油化工集团公司", ComBelongDepCode = "513" },
                new BelongDep { ComBelongDep = "中国石油天然气集团公司", ComBelongDepCode = "515" },
                new BelongDep { ComBelongDep = "国家安全生产监督管理局", ComBelongDepCode = "516" },
                new BelongDep { ComBelongDep = "中国核工业集团公司", ComBelongDepCode = "517" },
                new BelongDep { ComBelongDep = "中国兵器工业集团公司", ComBelongDepCode = "518" },
                new BelongDep { ComBelongDep = "信息产业部", ComBelongDepCode = "519" },
                new BelongDep { ComBelongDep = "中国证监会", ComBelongDepCode = "521" },
                new BelongDep { ComBelongDep = "中国保监会", ComBelongDepCode = "522" },
                new BelongDep { ComBelongDep = "农村合作金融协会", ComBelongDepCode = "523" },
                new BelongDep { ComBelongDep = "中国农业发展银行", ComBelongDepCode = "524" },
                new BelongDep { ComBelongDep = "中国进出口银行", ComBelongDepCode = "525" },
                new BelongDep { ComBelongDep = "中信实业银行", ComBelongDepCode = "526" },
                new BelongDep { ComBelongDep = "招商银行", ComBelongDepCode = "527" },
                new BelongDep { ComBelongDep = "中国人民保险公司", ComBelongDepCode = "528" },
                new BelongDep { ComBelongDep = "中国人寿保险公司", ComBelongDepCode = "529" },
                new BelongDep { ComBelongDep = "平安保险公司", ComBelongDepCode = "530" },
                new BelongDep { ComBelongDep = "太平洋保险公司", ComBelongDepCode = "531" },
                new BelongDep { ComBelongDep = "中国华融资产管理公司", ComBelongDepCode = "532" },
                new BelongDep { ComBelongDep = "中国东方资产管理公司", ComBelongDepCode = "533" },
                new BelongDep { ComBelongDep = "中国信达资产管理公司", ComBelongDepCode = "534" },
                new BelongDep { ComBelongDep = "中国核工业建设集团公司", ComBelongDepCode = "535" },
                new BelongDep { ComBelongDep = "中国航天机电集团公司", ComBelongDepCode = "536" },
                new BelongDep { ComBelongDep = "中国航空工业第二集团公司", ComBelongDepCode = "537" },
                new BelongDep { ComBelongDep = "中国船舶重工集团公司", ComBelongDepCode = "538" },
                new BelongDep { ComBelongDep = "中国兵器装备集团公司", ComBelongDepCode = "539" },
                new BelongDep { ComBelongDep = "中国包装总公司", ComBelongDepCode = "542" },
                new BelongDep { ComBelongDep = "中国烟草总公司", ComBelongDepCode = "543" },
                new BelongDep { ComBelongDep = "中国建筑工程总公司", ComBelongDepCode = "544" },
                new BelongDep { ComBelongDep = "中国汽车工业总公司", ComBelongDepCode = "545" },
                new BelongDep { ComBelongDep = "中国海洋石油总公司", ComBelongDepCode = "546" },
                new BelongDep { ComBelongDep = "中国国际工程咨询公司", ComBelongDepCode = "548" },
                new BelongDep { ComBelongDep = "中国高新技术投资集团公司", ComBelongDepCode = "549" },
                new BelongDep { ComBelongDep = "中国能源投资公司", ComBelongDepCode = "550" },
                new BelongDep { ComBelongDep = "中国农业投资公司", ComBelongDepCode = "551" },
                new BelongDep { ComBelongDep = "中国林业投资公司", ComBelongDepCode = "552" },
                new BelongDep { ComBelongDep = "中国交通投资公司", ComBelongDepCode = "553" },
                new BelongDep { ComBelongDep = "中国非金属矿业总公司", ComBelongDepCode = "560" },
                new BelongDep { ComBelongDep = "中国非金属材料总公司", ComBelongDepCode = "561" },
                new BelongDep { ComBelongDep = "中国新型建材总公司", ComBelongDepCode = "562" },
                new BelongDep { ComBelongDep = "中国北方机车车辆工业集团公司", ComBelongDepCode = "563" },
                new BelongDep { ComBelongDep = "东风汽车工业联营公司", ComBelongDepCode = "601" },
                new BelongDep { ComBelongDep = "解放汽车工业联营公司", ComBelongDepCode = "602" },
                new BelongDep { ComBelongDep = "中国重型汽车集团公司", ComBelongDepCode = "603" },
                new BelongDep { ComBelongDep = "第一拖拉机联营公司", ComBelongDepCode = "604" },
                new BelongDep { ComBelongDep = "西安电力机械制造公司", ComBelongDepCode = "605" },
                new BelongDep { ComBelongDep = "中国长城计算机集团公司", ComBelongDepCode = "606" },
                new BelongDep { ComBelongDep = "广东核电联营公司", ComBelongDepCode = "608" },
                new BelongDep { ComBelongDep = "中国东方电气(集团)公司", ComBelongDepCode = "609" },
                new BelongDep { ComBelongDep = "哈尔滨电站成套设备公司", ComBelongDepCode = "610" },
                new BelongDep { ComBelongDep = "东北输变电设备公司", ComBelongDepCode = "611" },
                new BelongDep { ComBelongDep = "长江计算机集团公司", ComBelongDepCode = "613" },
                new BelongDep { ComBelongDep = "上海电气联合公司", ComBelongDepCode = "614" },
                new BelongDep { ComBelongDep = "中国国际企业合作公司", ComBelongDepCode = "615" },
                new BelongDep { ComBelongDep = "华能集团公司", ComBelongDepCode = "616" },
                new BelongDep { ComBelongDep = "新疆生产建设兵团", ComBelongDepCode = "617" },
                new BelongDep { ComBelongDep = "中国长江三峡开发总公司", ComBelongDepCode = "618" },
                new BelongDep { ComBelongDep = "神华集团公司", ComBelongDepCode = "619" },
                new BelongDep { ComBelongDep = "中国联通公司", ComBelongDepCode = "620" },
                new BelongDep { ComBelongDep = "中国电信集团", ComBelongDepCode = "621" },
                new BelongDep { ComBelongDep = "中国移动", ComBelongDepCode = "622" },
                new BelongDep { ComBelongDep = "中国网通", ComBelongDepCode = "623" },
                new BelongDep { ComBelongDep = "国家邮政局", ComBelongDepCode = "624" },
                new BelongDep { ComBelongDep = "光明日报社", ComBelongDepCode = "631" },
                new BelongDep { ComBelongDep = "中国日报社", ComBelongDepCode = "632" },
                new BelongDep { ComBelongDep = "经济日报", ComBelongDepCode = "633" },
                new BelongDep { ComBelongDep = "《求是》杂志社", ComBelongDepCode = "634" },
                new BelongDep { ComBelongDep = "工人日报", ComBelongDepCode = "635" },
                new BelongDep { ComBelongDep = "中国青年报", ComBelongDepCode = "636" },
                new BelongDep { ComBelongDep = "科技日报", ComBelongDepCode = "637" },
                new BelongDep { ComBelongDep = "解放军总政治部", ComBelongDepCode = "651" },
                new BelongDep { ComBelongDep = "解放军总后工厂部", ComBelongDepCode = "652" },
                new BelongDep { ComBelongDep = "武警部队", ComBelongDepCode = "653" },
                new BelongDep { ComBelongDep = "全国总工会", ComBelongDepCode = "711" },
                new BelongDep { ComBelongDep = "共青团中央", ComBelongDepCode = "712" },
                new BelongDep { ComBelongDep = "全国妇联", ComBelongDepCode = "713" },
                new BelongDep { ComBelongDep = "全国工商联", ComBelongDepCode = "714" },
                new BelongDep { ComBelongDep = "中华教职社", ComBelongDepCode = "715" },
                new BelongDep { ComBelongDep = "中国文联", ComBelongDepCode = "721" },
                new BelongDep { ComBelongDep = "中国作家协会", ComBelongDepCode = "723" },
                new BelongDep { ComBelongDep = "中国科学技术协会", ComBelongDepCode = "731" },
                new BelongDep { ComBelongDep = "中国佛教协会", ComBelongDepCode = "732" },
                new BelongDep { ComBelongDep = "全国新闻工作者协会", ComBelongDepCode = "734" },
                new BelongDep { ComBelongDep = "中国国际贸易促进会", ComBelongDepCode = "741" },
                new BelongDep { ComBelongDep = "中国人民对外友好协会", ComBelongDepCode = "751" },
                new BelongDep { ComBelongDep = "中国人民外交协会", ComBelongDepCode = "752" },
                new BelongDep { ComBelongDep = "中国红十字会总会", ComBelongDepCode = "761" },
                new BelongDep { ComBelongDep = "中国残疾人福利基金会", ComBelongDepCode = "762" },
                new BelongDep { ComBelongDep = "中华全国归国华侨联合会", ComBelongDepCode = "771" },
                new BelongDep { ComBelongDep = "中华全国台湾同胞联谊会", ComBelongDepCode = "772" },
                new BelongDep { ComBelongDep = "欧美同学会", ComBelongDepCode = "773" },
                new BelongDep { ComBelongDep = "黄埔同学会", ComBelongDepCode = "774" },
                new BelongDep { ComBelongDep = "宋庆龄基金会", ComBelongDepCode = "781" },
                new BelongDep { ComBelongDep = "中国致公党中央", ComBelongDepCode = "782" },
                new BelongDep { ComBelongDep = "民革中央", ComBelongDepCode = "783" },
                new BelongDep { ComBelongDep = "民盟中央", ComBelongDepCode = "784" },
                new BelongDep { ComBelongDep = "民建中央", ComBelongDepCode = "785" },
                new BelongDep { ComBelongDep = "北京市", ComBelongDepCode = "911" },
                new BelongDep { ComBelongDep = "天津市", ComBelongDepCode = "912" },
                new BelongDep { ComBelongDep = "河北省", ComBelongDepCode = "913" },
                new BelongDep { ComBelongDep = "山西省", ComBelongDepCode = "914" },
                new BelongDep { ComBelongDep = "内蒙古自治区", ComBelongDepCode = "915" },
                new BelongDep { ComBelongDep = "辽宁省", ComBelongDepCode = "921" },
                new BelongDep { ComBelongDep = "吉林省", ComBelongDepCode = "922" },
                new BelongDep { ComBelongDep = "黑龙江省", ComBelongDepCode = "923" },
                new BelongDep { ComBelongDep = "上海市", ComBelongDepCode = "931" },
                new BelongDep { ComBelongDep = "江苏省", ComBelongDepCode = "932" },
                new BelongDep { ComBelongDep = "浙江省", ComBelongDepCode = "933" },
                new BelongDep { ComBelongDep = "安徽省", ComBelongDepCode = "934" },
                new BelongDep { ComBelongDep = "福建省", ComBelongDepCode = "935" },
                new BelongDep { ComBelongDep = "江西省", ComBelongDepCode = "936" },
                new BelongDep { ComBelongDep = "山东省", ComBelongDepCode = "937" },
                new BelongDep { ComBelongDep = "河南省", ComBelongDepCode = "941" },
                new BelongDep { ComBelongDep = "湖北省", ComBelongDepCode = "942" },
                new BelongDep { ComBelongDep = "湖南省", ComBelongDepCode = "943" },
                new BelongDep { ComBelongDep = "广东省", ComBelongDepCode = "944" },
                new BelongDep { ComBelongDep = "广西壮族自治区", ComBelongDepCode = "945" },
                new BelongDep { ComBelongDep = "海南省", ComBelongDepCode = "946" },
                new BelongDep { ComBelongDep = "重庆市", ComBelongDepCode = "950" },
                new BelongDep { ComBelongDep = "四川省", ComBelongDepCode = "951" },
                new BelongDep { ComBelongDep = "贵州省", ComBelongDepCode = "952" },
                new BelongDep { ComBelongDep = "云南省", ComBelongDepCode = "953" },
                new BelongDep { ComBelongDep = "西藏自治区", ComBelongDepCode = "954" },
                new BelongDep { ComBelongDep = "陕西省", ComBelongDepCode = "961" },
                new BelongDep { ComBelongDep = "甘肃省", ComBelongDepCode = "962" },
                new BelongDep { ComBelongDep = "青海省", ComBelongDepCode = "963" },
                new BelongDep { ComBelongDep = "宁夏回族自治区", ComBelongDepCode = "964" },
                new BelongDep { ComBelongDep = "新疆维吾尔自治区", ComBelongDepCode = "965" },
                new BelongDep { ComBelongDep = "台湾省", ComBelongDepCode = "971" },
                new BelongDep { ComBelongDep = "香港", ComBelongDepCode = "972" },
                new BelongDep { ComBelongDep = "澳门", ComBelongDepCode = "973" },
                new BelongDep { ComBelongDep = "大连市", ComBelongDepCode = "981" },
                new BelongDep { ComBelongDep = "宁波市", ComBelongDepCode = "982" },
                new BelongDep { ComBelongDep = "厦门市", ComBelongDepCode = "983" },
                new BelongDep { ComBelongDep = "青岛市", ComBelongDepCode = "984" },
                new BelongDep { ComBelongDep = "深圳市", ComBelongDepCode = "985" },
           
            }.ForEach(a => context.belongDepTb.Add(a));
            #endregion


            #region 民族
            new List<Nation>{
                new Nation{NationName="汉族",NationCode="01"},
                new Nation{NationName="蒙古族",NationCode="02"},
                new Nation{NationName="回族",NationCode="03"},
                new Nation{NationName="藏族",NationCode="04"},
                new Nation{NationName="维吾尔族",NationCode="05"},
                new Nation{NationName="苗族",NationCode="06"},
                new Nation{NationName="彝族",NationCode="07"},
                new Nation{NationName="壮族",NationCode="08"},
                new Nation{NationName="布依族",NationCode="09"},
                new Nation{NationName="朝鲜族",NationCode="10"},
                new Nation{NationName="满族",NationCode="11"},
                new Nation{NationName="侗族",NationCode="12"},
                new Nation{NationName="瑶族",NationCode="13"},
                new Nation{NationName="白族",NationCode="14"},
                new Nation{NationName="土家族",NationCode="15"},
                new Nation{NationName="哈尼族",NationCode="16"},
                new Nation{NationName="哈萨克族",NationCode="17"},
                new Nation{NationName="傣族",NationCode="18"},
                new Nation{NationName="黎族",NationCode="19"},
                new Nation{NationName="傈僳族",NationCode="20"},
                new Nation{NationName="佤族",NationCode="21"},
                new Nation{NationName="畲族",NationCode="22"},
                new Nation{NationName="高山族",NationCode="23"},
                new Nation{NationName="拉祜族",NationCode="24"},
                new Nation{NationName="水族",NationCode="25"},
                new Nation{NationName="东乡族",NationCode="26"},
                new Nation{NationName="纳西族",NationCode="27"},
                new Nation{NationName="景颇族",NationCode="28"},
                new Nation{NationName="柯尔克孜族",NationCode="29"},
                new Nation{NationName="土族",NationCode="30"},
                new Nation{NationName="达斡尔族",NationCode="31"},
                new Nation{NationName="仫佬族",NationCode="32"},
                new Nation{NationName="羌族",NationCode="33"},
                new Nation{NationName="布朗族",NationCode="34"},
                new Nation{NationName="撒拉族",NationCode="35"},
                new Nation{NationName="毛难族",NationCode="36"},
                new Nation{NationName="仡佬族",NationCode="37"},
                new Nation{NationName="锡伯族",NationCode="38"},
                new Nation{NationName="阿昌族",NationCode="39"},
                new Nation{NationName="普米族",NationCode="40"},
                new Nation{NationName="塔吉克族",NationCode="41"},
                new Nation{NationName="怒族",NationCode="42"},
                new Nation{NationName="乌孜别克族",NationCode="43"},
                new Nation{NationName="俄罗斯族",NationCode="44"},
                new Nation{NationName="鄂温克族",NationCode="45"},
                new Nation{NationName="崩龙族",NationCode="46"},
                new Nation{NationName="保安族",NationCode="47"},
                new Nation{NationName="裕固族",NationCode="48"},
                new Nation{NationName="京族",NationCode="49"},
                new Nation{NationName="塔塔尔族",NationCode="50"},
                new Nation{NationName="独龙族",NationCode="51"},
                new Nation{NationName="鄂伦春族",NationCode="52"},
                new Nation{NationName="赫哲族",NationCode="53"},
                new Nation{NationName="门巴族",NationCode="54"},
                new Nation{NationName="珞巴族",NationCode="55"},
                new Nation{NationName="基诺族",NationCode="56"},
                new Nation{NationName="其他",NationCode="97"},
                new Nation{NationName="外国血统中国籍人士",NationCode="98"},
            }.ForEach(a => context.nationTb.Add(a));


            #endregion


            base.Seed(context);
        }
    }
}