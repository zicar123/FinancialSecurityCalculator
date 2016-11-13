using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
using FinancialSecurityCalculator.Model;
using FinancialSecurityCalculator.Context;
using FinancialSecurityCalculator.Entities;

namespace FinancialSecurityCalculator.Services
{
    interface IServices
    {
        void ResetTextBoxes(ControlCollection controls);
        void TreeToTabConformity(TreeView treeView, TabControl tabControl, int index);
        void SaveToDB(DataModel dataModel);
        string DecisionMaking(EnterpriseIndicator entity);
        void Calculate(TabControl tabControl);
        void ShowDetails();
    }
}
