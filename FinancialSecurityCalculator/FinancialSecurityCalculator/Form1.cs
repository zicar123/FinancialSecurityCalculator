using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FinancialSecurityCalculator.Context;
using FinancialSecurityCalculator.Services;
using FinancialSecurityCalculator.Model;
using FinancialSecurityCalculator.Entities;
using System.Collections;

namespace FinancialSecurityCalculator
{
    public partial class Form1 : Form
    {
        Services.Services services;
        DataModel dataModel;
        FSCContext context;// = new FSCContext(); //TODO: get rid of this

        public Form1()
        {
            InitializeComponent();
            dataModel = new DataModel();
            services = new Services.Services();
            this.Init();
        }

        private void Init()
        {
            //var GroupBoxes = tabControl1.TabPages[0].Controls.OfType<GroupBox>().ToList<GroupBox>();
            //   foreach (var groupBox in GroupBoxes)
            // { TextBoxes.Add(groupBox.Controls.OfType<TextBox>().Where(box => box.ReadOnly == false).ToList<TextBox>()[0]); }
            label22.BringToFront();
            this.button4.BringToFront();
            List<TreeNode> tempNodes = new List<TreeNode>();
            tempNodes.Add(treeView1.Nodes[0]);
            tempNodes.Add(treeView1.Nodes[1]);
            tempNodes.Add(treeView1.Nodes[2]);
            tempNodes.Add(treeView1.Nodes[3]);
            foreach (TreeNode parent in tempNodes)
            {
                foreach (TreeNode child in parent.Nodes)
                {
                    dataModel.Nodes.Add(child);
                }
            }

            foreach (TabPage page in tabControl2.Controls)
            {
                foreach (var item in page.Controls)
                {
                    if (item is TextBox)
                    {
                        if ((item as TextBox).ReadOnly == true)
                        {
                            dataModel.TextBoxes.Add(item as TextBox);
                        }
                    }
                }
            }
            //TODO: add gradient at analysis left panel

            //enterpriseDataGridView.AutoResizeColumns();
            //enterpriseIndicatorsDataGridView.AutoResizeColumns();
            //enterpriseDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //enterpriseIndicatorsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            comboBox1.Items.Add("Всі");
            comboBox1.Items.AddRange(dataModel.Regions.ToArray());
            comboBox1.SelectedIndex = 0;
        }
        #region Events
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int index = 0; index < 20; index++)
                this.services.TreeToTabConformity(this.treeView1, this.tabControl2, index); //TODO: recursion, performance
        }

        private void button3_Click(object sender, EventArgs e)
        {
            services.SaveToDB(dataModel);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            services.Calculate(tabControl2);
        }
        #region It works. I dont know why or how. Dont touch this.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.context == null) context = new FSCContext();

            if (checkBox1.Checked)
            {
                var querry = from c in context.Enterprise
                             where c.Branch == "Виробнича галузь"
                             select c;
                dataModel.chb1 = querry.ToList();
                dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            else
            {
                dataModel.chb1.Clear(); dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            recordsBindingSource.ResetBindings(false);
            enterpriseIndicatorsBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = dataModel.TotalList;
            else enterpriseBindingSource.DataSource = dataModel.TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.context == null) context = new FSCContext();

            if (checkBox2.Checked)
            {
                var querry = from c in context.Enterprise
                             where c.Branch == "Торгівельна галузь"
                             select c;
                dataModel.chb2 = querry.ToList();
                dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            else
            {
                dataModel.chb2.Clear();
                dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            recordsBindingSource.ResetBindings(false);
            enterpriseIndicatorsBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = dataModel.TotalList;
            else enterpriseBindingSource.DataSource = dataModel.TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
            //TODO: refresh datagridviews after add new record
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.context == null) context = new FSCContext();
            //TODO: API for CRUD methods
            if (checkBox3.Checked)
            {
                var querry = from c in context.Enterprise
                             where c.Branch == "Галузь фінансів"
                             select c;
                dataModel.chb3 = querry.ToList();
                dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            else
            {
                dataModel.chb3.Clear(); dataModel.TotalList.Clear();
                dataModel.TotalList.AddRange(dataModel.chb1);
                dataModel.TotalList.AddRange(dataModel.chb2);
                dataModel.TotalList.AddRange(dataModel.chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            recordsBindingSource.ResetBindings(false);
            enterpriseIndicatorsBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = dataModel.TotalList;
            else enterpriseBindingSource.DataSource = dataModel.TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
        }
        #endregion
        private void newEnterpriseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.services.ResetTextBoxes(this.Controls);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.context == null) context = new FSCContext();

            if (comboBox1.SelectedItem.ToString() == "Всі")
            {
                enterpriseBindingSource.ResetBindings(false);
                recordsBindingSource.ResetBindings(false);
                enterpriseIndicatorsBindingSource.ResetBindings(false);
                enterpriseBindingSource.Filter = comboBox1.SelectedItem.ToString();
                enterpriseBindingSource.DataSource = dataModel.TotalList;
            }
            else
            {
                enterpriseBindingSource.ResetBindings(false);
                recordsBindingSource.ResetBindings(false);
                enterpriseIndicatorsBindingSource.ResetBindings(false);// somewhy it didnt help. MastedDetail - detail table dont refresh
                enterpriseBindingSource.Filter = comboBox1.SelectedItem.ToString();//dont know whether i need this line
                enterpriseBindingSource.DataSource = dataModel.TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
            }
            // enterpriseBindingSource.ResumeBinding(); // this one works just fine(Not tested at this line!)

            //context?.Dispose();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            using (var context = new FSCContext())
            {
                if (enterpriseIndicatorsDataGridView.SelectedCells.Count > 0)
                    services.ShowDetails(context.Enterprise.ToList().FirstOrDefault(en => en.EnterpriseId == Convert.ToInt32(enterpriseDataGridView.CurrentRow.Cells[0].Value)).Records.ToList().FirstOrDefault(r => r.Year == Convert.ToInt32(recordsDataGridView.CurrentCell.Value)).EnterpriseIndicators.ToList());
                //services.ShowDetails(enterpriseIndicatorsDataGridView.DataSource as List<EnterpriseIndicator>);
            }
        }
        //TODO: add info from which document(balance list or another one) find Arguments for calculating
        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            if (this.context == null) context = new FSCContext();

            enterpriseBindingSource.ResetBindings(false);
            recordsBindingSource.ResetBindings(false);
            enterpriseIndicatorsBindingSource.ResetBindings(false);// somewhy it didnt help. MastedDetail - detail table dont refresh
            if (!string.IsNullOrEmpty(textBox42.Text))
                enterpriseBindingSource.DataSource = dataModel.TotalList?.Where(n => n.EnterpriseName.ToLower().Contains(textBox42.Text.ToLower()));
            else if (string.IsNullOrEmpty(textBox42.Text))//TODO: fix(looks like fixed)
            { enterpriseBindingSource.DataSource = dataModel.TotalList; }

            enterpriseBindingSource.ResumeBinding(); // this one works just fine
            recordsBindingSource.ResumeBinding();
        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {
            if (this.context == null) context = new FSCContext();

            enterpriseBindingSource.ResetBindings(false);
            recordsBindingSource.ResetBindings(false);
            enterpriseIndicatorsBindingSource.ResetBindings(false);// somewhy it didnt help. MastedDetail - detail table dont refresh. Use ResumeBindings Instead
            if (!string.IsNullOrEmpty(textBox45.Text))
                enterpriseBindingSource.DataSource = dataModel.TotalList?.Where(n => n.EnterpriseId.ToString().Contains(textBox45.Text));
            else
            { enterpriseBindingSource.DataSource = dataModel.TotalList; }

            enterpriseBindingSource.ResumeBinding(); // this one works just fine
            recordsBindingSource.ResumeBinding();
        }

        private void зареєструватиНовеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterEnterprise dialogFormRegister = new RegisterEnterprise(dataModel.Regions, dataModel.Branches);
            dialogFormRegister.ShowDialog();
            if (dialogFormRegister.DialogResult == DialogResult.OK)
            {
                dataModel.EnterpriseData = dialogFormRegister.EnterpriseData;
                label159.Text = dataModel.EnterpriseData["EnterpriseName"].ToString();
                toolTip1.SetToolTip(this.label159, dataModel.EnterpriseData["EnterpriseName"].ToString());

                comboBox2.SelectedItem = default(ComboBox);
                comboBox2.Text = "Оберіть рік";
            }
        }

        private void обратиІснуючеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectEnterprise dialogFormSelect;
            using (var context = new FSCContext())
            {
                dialogFormSelect = new SelectEnterprise(context.Enterprise.ToList());
            }
            dialogFormSelect.ShowDialog();
            if (dialogFormSelect.DialogResult == DialogResult.OK)
            {
                dataModel.EnterpriseData = dialogFormSelect.EnterpriseData;
                label159.Text = dataModel.EnterpriseData["EnterpriseName"].ToString();
                toolTip1.SetToolTip(this.label159, dataModel.EnterpriseData["EnterpriseName"].ToString());

                comboBox2.SelectedItem = default(ComboBox);
                comboBox2.Text = "Оберіть рік";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            object temp;
            if (dataModel.EnterpriseData.TryGetValue("Year", out temp))
            {
                dataModel.EnterpriseData["Year"] = comboBox2.SelectedItem; //TODO: fix comboBox exception when write year, not select
            }
            else
            {
                dataModel.EnterpriseData.Add("Year", comboBox2.SelectedItem);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new FSCContext())
            {
                services.Compare();
            }
        }
    }
}
