using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace HRDB
{
    public class ExcelExport
    {
        public void Export(DataGridView dg)
        {
            if (dg == null || dg.Rows.Count <= 0)
            {
                string text = "Данные для экспорта не обнаружены (таблицы не существуют, или в ней "
+ "отсуствуют данные)!";
                string title = "Предупрждение";
                MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dg.ColumnCount; i++)
                {
                    excel.Cells[1, i] = dg.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dg.RowCount; i++)
                {
                    for (int j = 0; j < dg.ColumnCount; j++)
                    {
                        excel.Cells[i + 2, j + 1] = dg.Rows[i].Cells[j].Value.ToString();
                    }
                }
                excel.Columns.AutoFit();
                excel.Visible = true;
                excel = null;
            }
            catch(Exception ex)
            {
                string text = "В процессе экспорта произошла ошибка: " + ex.ToString() + "";
                string title = "Экспорт - ошибка";
                MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
