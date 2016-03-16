using Graduation.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graduation.Controllers
{
    public class wxController : Controller
    {
        private GraduationDBContent db = new GraduationDBContent();
        public ActionResult Index()
        {
            HSSFWorkbook hssfworkbook;
            #region//初始化信息
            try
            {
                string filePath = "C:\\Users\\ThinkPad_wx\\Desktop\\地区.xls";
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            #region 读取信息并保存
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            rows.MoveNext();//移动到第一行，数据为表头的代码
            HSSFRow row = (HSSFRow)rows.Current;//获取第一行的值
            DataTable dt = new DataTable();
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(row.GetCell(j).ToString());//保存表头
            }
            DataRow header = dt.NewRow();//保存表头数据
            DataRow dr = dt.NewRow();//保存数据
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                header[j] = row.GetCell(j).ToString();//保存表头
            }
            while (rows.MoveNext())
            {
                row = (HSSFRow)rows.Current;
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                GraduationDBContent db1 = new GraduationDBContent();
                LocationModel model = new LocationModel();
                #region 将每一行的数据进行保存

                for (int i = 0; i < header.ItemArray.Length; i++)
                {
                    string head = header.ItemArray[i].ToString();
                    string data = dr.ItemArray[i].ToString();
                    #region 上传数据的判断
                    switch (head)
                    {
                        case "地区代码":
                            model.code = data;
                            break;
                        case "显示名称":
                            model.name = data;
                            break;
                    }
                    #endregion
                }
                db.LocationTb.Add(model);
                db.SaveChanges();
                #endregion
            }
            #endregion

            return View();
        }

    }
}
