﻿using System;
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
        string DecisionMaking(EnterpriseIndicator entity, out int clusterId);
        void Calculate(TabControl tabControl);
        void ShowDetails(List<EnterpriseIndicator> indicators, string enterpriseName, string year);
        void Compare(List<Enterprise> enterpriseList);
    }
}
