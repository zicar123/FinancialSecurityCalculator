using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace FinancialSecurityCalculator.Services
{
    public class Services
    {
        public void ResetTextBoxes(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                TextBox tb = c as TextBox;
                if (tb != null)
                {
                    tb.Text = string.Empty;
                }
                ResetTextBoxes(c.Controls);
            }
        }
    }
}
