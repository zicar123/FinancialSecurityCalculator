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
        private List<int> listID;
        private string firstEnterprise;
        private string secondEnterprise;
        private string year;

        public Compare(IEnumerable<object> dataSourse, string firstEnterprise, string secondEnterprise, string year)
        {
            InitializeComponent(); 
            this.firstEnterprise = firstEnterprise;
            this.secondEnterprise = secondEnterprise;
            this.year = year;
            this.Text = "Порівняльна таблиця показників за " + year;
            this.Init(dataSourse);
        }

        private void Init(IEnumerable<object> dataSourse)
        {
            dataGridView1.DataSource = dataSourse;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView1.Columns[1].HeaderText = "Для: " + firstEnterprise;
            dataGridView1.Columns[2].HeaderText = "Для: " + secondEnterprise;
            dataGridView1.Columns[0].Width = 295;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Visible = false;

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
            listID = dataGridView1.Rows
                             .OfType<DataGridViewRow>()
                             .Select(r => int.Parse(r.Cells[3].Value.ToString()))
                             .ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ChartsCompares(listName, listFirstValue, listSecondValue, listID, firstEnterprise, secondEnterprise).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = new SLExcelData();

            data.Headers.Add(dataGridView1.Columns[0].HeaderText);
            data.Headers.Add(dataGridView1.Columns[1].HeaderText);
            data.Headers.Add(dataGridView1.Columns[2].HeaderText);

            data.SheetName = this.Text;

            for (int i = 0; i < listName.Count; i++)
                data.DataRows.Add(new List<string>() { listName[i], listFirstValue[i], listSecondValue[i] });

            byte[] binaryFile = (new SLExcelWriter()).GenerateExcel(data);
            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + year + " - " + firstEnterprise + ", " + secondEnterprise + ".xls", binaryFile);
            MessageBox.Show("Файл збережено у " + AppDomain.CurrentDomain.BaseDirectory);

        }
    }
}
