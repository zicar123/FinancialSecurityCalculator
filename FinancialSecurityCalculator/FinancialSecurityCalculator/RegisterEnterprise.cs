using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace FinancialSecurityCalculator
{
    public partial class RegisterEnterprise : Form
    {
        public RegisterEnterprise(List<string> regions, List<string> branches)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(regions.ToArray());
            comboBox2.Items.AddRange(branches.ToArray());
        }

        public void ClearFields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox1.Text = null;
            comboBox2.Text = null;
        }

        public Dictionary<string, object> EnterpriseData
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { "EnterpriseID", textBox2.Text },
                    { "EnterpriseName", textBox1.Text },
                    { "Region", comboBox1.SelectedItem },
                    { "Branch", comboBox2.SelectedItem },
                };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (comboBox1.SelectedItem != null)
                {
                    if (comboBox2.SelectedItem != null)
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Помилка. Невірний формат у полі 'ID'.");
                            textBox2.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Помилка. Оберіть галузь підприємства.");
                        comboBox2.Text = null;
                    }
                }
                else
                {
                    MessageBox.Show("Помилка. Оберіть область.");
                    comboBox1.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Помилка. Введіть назву підприємства");
                textBox1.Text = string.Empty;
            }
        }
    }
}
