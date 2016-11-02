using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialSecurityCalculator.Context;
using FinancialSecurityCalculator.Services;
using System.Collections;

namespace FinancialSecurityCalculator
{
    public partial class Form1 : Form
    {
        List<TextBox> TextBoxes;
        List<EnterpriseIndicator> Indicators;
        List<TreeNode> Nodes;
        List<string> Branches;
        List<string> Regions;
        ArrayList EnterpriseData;
        SaveToDBModal dialogForm;
        FSCContext context;
        List<Enterprise> TotalList = new List<Enterprise>();
        List<Enterprise> chb1 = new List<Enterprise>();
        List<Enterprise> chb2 = new List<Enterprise>();
        List<Enterprise> chb3 = new List<Enterprise>();

        public Form1()
        {
            InitializeComponent();
            this.Init();
        }

        public void Init()
        {
            this.button4.BringToFront();
            EnterpriseData = new ArrayList();

            TextBoxes = new List<TextBox>(20);
            Indicators = new List<EnterpriseIndicator>(20);
            Nodes = new List<TreeNode>(20);

            //var GroupBoxes = tabControl1.TabPages[0].Controls.OfType<GroupBox>().ToList<GroupBox>();

            //   foreach (var groupBox in GroupBoxes)
            // { TextBoxes.Add(groupBox.Controls.OfType<TextBox>().Where(box => box.ReadOnly == false).ToList<TextBox>()[0]); }

            List<TreeNode> tempNodes = new List<TreeNode>();
            tempNodes.Add(treeView1.Nodes[0]);
            tempNodes.Add(treeView1.Nodes[1]);
            tempNodes.Add(treeView1.Nodes[2]);
            tempNodes.Add(treeView1.Nodes[3]);
            foreach (TreeNode parent in tempNodes)
            {
                foreach (TreeNode child in parent.Nodes)
                {
                    Nodes.Add(child);
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
                            TextBoxes.Add(item as TextBox);
                        }
                    }
                }
            }

            //TODO: add gradient at analysis left panel

            //enterpriseDataGridView.AutoResizeColumns();
            //enterpriseIndicatorsDataGridView.AutoResizeColumns();
            //enterpriseDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //enterpriseIndicatorsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            Branches = new List<string>() { "Виробнича галузь", "Торгівельна галузь", "Галузь фінансів" };
            Regions = new List<string>() { "Київська область", "Одеська область", "Дніпропетровська область",
                "Чернігівська область", "Харківська область", "Житомирська область", "Полтавська область", "Херсонська область",
                "Запорізька область", "Луганська область", "Донецька область", "Вінницька область", "Кiровоградська область",
                "Миколаївська область", "Сумська область", "Львівська область", "Черкаська область", "Хмельницька область",
                "Волинська область", "Рівненська область", "Івано-Франківська область", "Тернопільська область", "Закарпатська область",
                "Чернівецька область","Автономна Республіка Крим" };

            comboBox1.Items.Add("Всі");
            comboBox1.Items.AddRange(Regions.ToArray());
            comboBox1.SelectedIndex = 0;
            dialogForm = new SaveToDBModal(Regions, Branches);
        }

        public void ShowHide(int index)
        {
            if (treeView1.SelectedNode.Index == index && treeView1.SelectedNode.Level == 1 && treeView1.Nodes[0].Name == "ParentNode0")//TODO: fix
            {
                this.tabControl2.SelectTab(index);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int index = 0; index < 20; index++)
            { ShowHide(index); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EnterpriseData.Count == 0)
            {
                MessageBox.Show("Помилка. Для збереження в БД спочатку створіть запис про підприємтсво (Файл - Новий запис.)");
            }
            else
            {
                for (int i = 0; i < TextBoxes.Count; ++i)
                {
                    try
                    {
                        Indicators.Add(new EnterpriseIndicator() { IndicatorName = Nodes[i].Text, IndicatorValue = double.Parse(TextBoxes[i].Text) });
                    }
                    catch (Exception) { }
                }

                using (var context = new FSCContext())
                {
                    var enterprise = new Enterprise()
                    {
                        EnterpriseName = EnterpriseData[0].ToString(),
                        Region = EnterpriseData[1].ToString(),
                        Branch = EnterpriseData[2].ToString(),
                        Date = DateTime.Now.Date.ToString(),
                        EnterpriseIndicators = Indicators
                    };

                    context.Enterprise.Add(enterprise);
                    context.SaveChanges();
                    Indicators.Clear();
                    MessageBox.Show("Дані успішно збережені у БД.");
                }
            }
        }
        //TODO: add possibility to open datagridviews in Analysis on seperate windows
        private void button4_Click(object sender, EventArgs e)
        {
            Formulas formulas = new Formulas();
            List<TextBox> arguments = new List<TextBox>();
            foreach (var elem in tabControl2.SelectedTab.Controls)
            {
                if (elem is TextBox)
                {
                    if ((elem as TextBox).ReadOnly == false)
                    {
                        arguments.Add(elem as TextBox);
                    }
                }
            }
            foreach (var box in tabControl2.SelectedTab.Controls)
            {
                if (box is TextBox)
                {
                    if ((box as TextBox).ReadOnly == true)
                    {

                        switch (tabControl2.SelectedTab.Name)
                        {
                            case "x2":
                                (box as TextBox).Text = formulas.CalcSumDiv(arguments[0], arguments[1], arguments[2]);
                                break;
                            case "x3":
                                (box as TextBox).Text = formulas.CalcSubDiv(arguments[0], arguments[1], arguments[0]);
                                break;
                            case "x4":
                                (box as TextBox).Text = formulas.CalcSubDiv(arguments[0], arguments[1], arguments[2]);
                                break;
                            case "x8":
                                (box as TextBox).Text = formulas.CalcDivSum(arguments[0], arguments[1], arguments[2]);
                                break;
                            default:
                                (box as TextBox).Text = formulas.CalcDiv(arguments[0], arguments[1]);
                                break;
                        }
                    }
                }
            }
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.context == null) context = new FSCContext();

            if (checkBox1.Checked)
            {

                var querry = from c in context.Enterprise
                             where c.Branch == "Виробнича галузь"
                             select c;
                chb1 = querry.ToList();
                TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            else
            {
                chb1.Clear(); TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = TotalList;
            else enterpriseBindingSource.DataSource = TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.context == null) context = new FSCContext();

            if (checkBox2.Checked)
            {
                var querry = from c in context.Enterprise
                             where c.Branch == "Торгівельна галузь"
                             select c;
                chb2 = querry.ToList();
                TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            else
            {
                chb2.Clear();
                TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = TotalList;
            else enterpriseBindingSource.DataSource = TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.context == null) context = new FSCContext();

            if (checkBox3.Checked)
            {
                var querry = from c in context.Enterprise
                             where c.Branch == "Галузь фінансів"
                             select c;
                chb3 = querry.ToList();
                TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            else
            {
                chb3.Clear(); TotalList.Clear();
                TotalList.AddRange(chb1);
                TotalList.AddRange(chb2);
                TotalList.AddRange(chb3);
            }
            enterpriseBindingSource.ResetBindings(false);
            if (comboBox1.SelectedItem.ToString() == "Всі") enterpriseBindingSource.DataSource = TotalList;
            else enterpriseBindingSource.DataSource = TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
        }

        private void newEnterpriseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogForm.ShowDialog();
            if (dialogForm.DialogResult == DialogResult.Cancel)
            {
                dialogForm.ClearFields();
            }
            else
            {
                EnterpriseData = dialogForm.EnterpriseData;
                dialogForm.ClearFields();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Services.Services().ResetTextBoxes(this.Controls);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.context == null) context = new FSCContext();

            if (comboBox1.SelectedItem.ToString() == "Всі")
            {
                enterpriseBindingSource.ResetBindings(false);
                enterpriseBindingSource.Filter = comboBox1.SelectedItem.ToString();
                enterpriseBindingSource.DataSource = TotalList;
            }
            else
            {
                enterpriseBindingSource.ResetBindings(false);
                enterpriseBindingSource.Filter = comboBox1.SelectedItem.ToString();
                enterpriseBindingSource.DataSource = TotalList.Where(elem => elem.Region == comboBox1.SelectedItem.ToString());
            }
            //context?.Dispose();
        }
    }
}
