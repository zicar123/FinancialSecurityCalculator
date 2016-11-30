using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialSecurityCalculator.Entities;
using FinancialSecurityCalculator.SLExcelUtility;

namespace FinancialSecurityCalculator
{
    public partial class Compare : Form
    {
        private List<string> listName;
        private List<string> listFirstValue;
        private List<string> listSecondValue;

        public Compare(IEnumerable<object> dataSourse, string firstEnterprise, string secondEnterprise)
        {
            InitializeComponent();
            this.Init(dataSourse, firstEnterprise, secondEnterprise);
        }

        private void Init(IEnumerable<object> dataSourse, string firstEnterprise, string secondEnterprise)
        {
            dataGridView1.DataSource = dataSourse;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView1.Columns[1].HeaderText = "Для: " + firstEnterprise;
            dataGridView1.Columns[2].HeaderText = "Для: " + secondEnterprise;
            dataGridView1.Columns[0].Width = 295;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            listName = dataGridView1.Rows
                             .OfType<DataGridViewRow>()
                             .Select(r => r.Cells[0].Value.ToString())
                             .ToList();
            listFirstValue = dataGridView1.Rows
                             .OfType<DataGridViewRow>()
                             .Select(r => r.Cells[1].Value.ToString())
                             .ToList();
            listSecondValue = dataGridView1.Rows
                             .OfType<DataGridViewRow>()
                             .Select(r => r.Cells[2].Value.ToString())
                             .ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ChartsCompares(listName, listFirstValue, listSecondValue).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = new SLExcelData();

            data.Headers.Add(dataGridView1.Columns[0].HeaderText);
            data.Headers.Add(dataGridView1.Columns[1].HeaderText);
            data.Headers.Add(dataGridView1.Columns[2].HeaderText);

            for (int i = 0; i < listName.Count; i++)
                data.DataRows.Add(new List<string>() { listName[i], listFirstValue[i], listSecondValue[i] });

            byte[] binaryFile = (new SLExcelWriter()).GenerateExcel(data);
            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "Порівняння" + ".xls", binaryFile);

        }
    }
}
