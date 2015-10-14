using System;
using System.Collections.Generic;
using System.Text;
using Excel;
using System.Data;
using System.Windows.Forms;

namespace OrderManage.Util
{
    class DataSetToExcel
    {
        public DataSetToExcel()
        {
        }
        /// <summary>
        /// DataSet导出成Excel，调用函数或实例化后一定要写垃圾回收，否则会造成内存泄漏，GC.Collect();
        /// </summary>
        /// <param name="Excel_DS">要导出的DataSet</param>
        public bool ToExcel(DataSet Excel_DS, string strTitle)
        {
            try
            {
                // 判断是否有数据表
                if (Excel_DS.Tables.Count < 1)
                {
                    MessageBox.Show("没有数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {
                    Excel.Application xlApp = new Excel.ApplicationClass();
                    if (xlApp == null)
                    {
                        MessageBox.Show("Excel无法启动，可能是没有安装或已损坏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    // 新建一个保存窗口，并设置默认属性
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.DefaultExt = ".xls";                                     // 默认扩展名
                    saveFileDialog.AddExtension = true;                                     // 允许自动添加设置的默认扩展名
                    saveFileDialog.RestoreDirectory = true;                                 // 对话框关闭后恢复默认目录
                    saveFileDialog.Filter = "Microsoft Excel 工作薄 (*.xls)|*.xls";
                    saveFileDialog.Title = "导出到 Microsoft Excel 工作薄";
                    DialogResult result = saveFileDialog.ShowDialog();                      // 显示保存对话框

                    if (result == DialogResult.OK)
                    {
                        // 创建Excel工作薄
                        Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                        Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets[1];

                        // 循环DataSet中的表
                        for (int i = 0; i < Excel_DS.Tables.Count; i++)
                        {
                            // 列索引，行索引，总列数，总行数
                            int ColIndex = 0;
                            int RowIndex = 0;
                            int ColCount = Excel_DS.Tables[i].Columns.Count;
                            int RowCount = Excel_DS.Tables[i].Rows.Count;

                            // 创建缓存数据
                            object[,] objData = new object[RowCount + 1, ColCount];

                            xlSheet.Name = Excel_DS.Tables[i].TableName;                    // 表单名

                            // 获取列标题
                            for (int k = 0; k < ColCount; k++)
                            {
                                objData[RowIndex, ColIndex++] = Excel_DS.Tables[i].Columns[k].Caption;
                            }

                            // 获取数据
                            for (RowIndex = 1; RowIndex <= RowCount; RowIndex++)
                            {
                                for (ColIndex = 0; ColIndex < ColCount; ColIndex++)
                                {
                                    objData[RowIndex, ColIndex] = Excel_DS.Tables[i].Rows[RowIndex - 1][ColIndex];
                                }
                                System.Windows.Forms.Application.DoEvents();
                            }

                            // 写入Excel的样式,设置标题,列名等
                            Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, ColCount]);
                            //xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, ColCount]).MergeCells = true;           // 合并指定单元格
                            //xlApp.ActiveCell.FormulaR1C1 = strTitle;                                                    // 设置标题
                            //xlApp.ActiveCell.Font.Size = 15;                                                            // 标题字体大小
                            //xlApp.ActiveCell.Font.Bold = 1;                                                             // 标题字体加粗
                            //xlApp.ActiveCell.RowHeight = 50;                                                            // 标题栏行高
                            //xlApp.ActiveCell.Interior.ColorIndex = 42;                                                  // 标题栏背景色
                            //xlApp.ActiveCell.HorizontalAlignment = Excel.Constants.xlLeft;                              // 标题栏对齐方式

                            //xlSheet.Cells.Font.Name = "Arial";                                                          // 全表单中字体
                            //xlSheet.Cells.Columns.WrapText = true;                                                      // 全表单的单元格自动换行

                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, 1]).ColumnWidth = 20;                   // 第一列的列宽
                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, ColCount]).Interior.ColorIndex = 28;    // 列名栏背景色
                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, ColCount]).Font.Size = 10;              // 列名栏字体大小
                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, ColCount]).Font.Bold = 1;               // 列名栏字体加粗
                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, ColCount]).HorizontalAlignment = Excel.Constants.xlCenter;      // 列名栏对齐方式，水平居中
                            //xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, ColCount]).Borders.LineStyle = XlLineStyle.xlContinuous;        // 列名栏边框样式

                            //xlSheet.Cells[RowCount + 3, 1] = "End of the Report!";
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 3, 1], xlApp.Cells[RowCount + 3, ColCount]).MergeCells = true;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 3, 1], xlApp.Cells[RowCount + 3, ColCount]).Interior.ColorIndex = 28;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 3, 1], xlApp.Cells[RowCount + 3, ColCount]).Font.Size = 10;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 3, 1], xlApp.Cells[RowCount + 3, ColCount]).Font.Bold = 1;

                            //xlSheet.Cells[RowCount + 4, 1] = "* If the number of organization is less than 3, the city index information will be unavaliable.";
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 4, 1], xlApp.Cells[RowCount + 4, ColCount]).MergeCells = true;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 4, 1], xlApp.Cells[RowCount + 4, ColCount]).Font.Size = 10;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 4, 1], xlApp.Cells[RowCount + 4, ColCount]).Font.Bold = 1;

                            //xlSheet.Cells[RowCount + 5, 1] = "* TM: Top Management; M: Management; P: Professional; S: Staff; B: Blue Collar";
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 5, 1], xlApp.Cells[RowCount + 5, ColCount]).MergeCells = true;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 5, 1], xlApp.Cells[RowCount + 5, ColCount]).Font.Size = 10;
                            //xlSheet.get_Range(xlApp.Cells[RowCount + 5, 1], xlApp.Cells[RowCount + 5, ColCount]).Font.Bold = 1;

                            // 设置表单内数据的字体大小
                            for (int n = 0; n < RowCount; n++)
                            {
                                xlSheet.get_Range(xlApp.Cells[n + 2, 1], xlApp.Cells[n + 2, ColCount]).Font.Size = 9;
                            }

                            // 数据写入Excel
                            range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[RowCount + 1, ColCount]);
                            range.Value2 = objData;
                        }
                        object oMissiong = System.Reflection.Missing.Value;         //通过反射产生一个函数的默认参数
                        xlSheet.Application.DisplayAlerts = false;                  //禁止保存提示
                        xlSheet.SaveAs(saveFileDialog.FileName, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);         //Microsoft Excel 对象类库11.0以上版本时有10个参数，11以下有9个参数，如果少参数则用oMissiong填入
                        xlApp.Quit();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

