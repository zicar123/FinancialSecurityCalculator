﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialSecurityCalculator.Entities;

namespace FinancialSecurityCalculator
{
    public partial class CompareModal : Form
    {
        public CompareModal(List<Enterprise> dataSourse)
        {
            InitializeComponent();
            var chk = new DataGridViewCheckBoxColumn();
            chk.TrueValue = "1";
            chk.FalseValue = "0";
            dataGridView1.Columns.Insert(0, chk);
            dataGridView1.DataSource = dataSourse;
            //dataGridView1.ReadOnly = true;


            //dataGridView1.Columns[2].ReadOnly = false;
        }

        public List<List<EnterpriseIndicator>> GetSelectedEnterpriseData { get; private set; } = new List<List<EnterpriseIndicator>>();

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cell = (DataGridViewCheckBoxCell)row.Cells[0];

                if (cell.Value == cell.TrueValue)
                {
                    dataGridView1.CurrentCell = row.Cells[0];
                    //if (context.Record.ToList().FirstOrDefault(y => y.Year == Convert.ToInt32(dataModel.EnterpriseData["Year"]) && y.Enterprise.EnterpriseId == (int)dataModel.EnterpriseData["EnterpriseID"]) != null) //if record with this year already exists
                    this.GetSelectedEnterpriseData.Add((dataGridView1.DataSource as List<Enterprise>).FirstOrDefault(x => x.EnterpriseId == (int)dataGridView1.CurrentRow.Cells[2].Value).Records?.FirstOrDefault(y => y.Year == Convert.ToInt32(comboBox2.SelectedItem)).EnterpriseIndicators);
                }
            }//TODO: catch exceptions, add check if year not exists in second enterprise

            if (GetSelectedEnterpriseData.Any()) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
                dataGridView1.DataSource = (dataGridView1.DataSource as List<Enterprise>)?.Where(n => n.EnterpriseId.ToString().Contains(textBox1.Text));
        }
    }
}