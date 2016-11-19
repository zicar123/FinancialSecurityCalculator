using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialSecurityCalculator.Services
{
    public class Formulas
    {
        public string CalcDiv(TextBox tb1, TextBox tb2)
        {
            try
            {
                return tb1.Text.Equals("0")
                    ? "0"
                    : string.Format("{0:f3}", (double.Parse(tb2.Text.Replace('.', ','))
                    / double.Parse(tb1.Text.Replace('.', ','))));
            }
            catch (FormatException)
            {
                MessageBox.Show("Невірний формат вводу.");
                return null;
            }
        }

        public string CalcSubDiv(TextBox tb1, TextBox tb2, TextBox tb3)
        {
            try
            {
                return tb3.Text.Equals("0")
                    ? "0"
                    : string.Format("{0:f3}", (((double.Parse(tb1.Text.Replace('.', ','))
                    - double.Parse(tb2.Text.Replace('.', ',')))
                    / double.Parse(tb3.Text.Replace('.', ',')))));
            }
            catch (Exception)
            {
                MessageBox.Show("Невірний формат вводу.");
                return null;
            }
        }

        public string CalcSumDiv(TextBox tb1, TextBox tb2, TextBox tb3)
        {
            try
            {
                return tb3.Text.Equals("0")
                    ? "0" 
                    : string.Format("{0:f3}", (((double.Parse(tb1.Text.Replace('.', ',')) 
                    + double.Parse(tb2.Text.Replace('.', ',')))
                    / double.Parse(tb3.Text.Replace('.', ',')))));
            }
            catch (Exception)
            {
                MessageBox.Show("Невірний формат вводу.");
                return null;
            }
        }

        public string CalcDivSum(TextBox tb1, TextBox tb2, TextBox tb3)
        {
            try
            {
                return double.Parse(tb2.Text)+ double.Parse(tb3.Text) == 0 
                    ? "0" 
                    : string.Format("{0:f3}", (double.Parse(tb1.Text.Replace('.', ','))
                    / (double.Parse(tb2.Text.Replace('.', ','))
                    + double.Parse(tb3.Text.Replace('.', ',')))));
            }
            catch (Exception)
            {
                MessageBox.Show("Невірний формат вводу.");
                return null;
            }
        }
    }
}
