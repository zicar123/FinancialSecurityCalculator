using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
using FinancialSecurityCalculator.Model;
using FinancialSecurityCalculator.Context;
using FinancialSecurityCalculator.Entities;
using System.Linq;
using System.Collections;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FinancialSecurityCalculator.SLExcelUtility;

namespace FinancialSecurityCalculator.Services
{
    public class Services : IServices
    {
        private readonly List<LimitValue> decisionData = new List<LimitValue>()
        {
                new LimitValue() { Value = 0.5, Below = true },
                new LimitValue() { Value = 0.8, Below = true },
                new LimitValue() { FirstValue = 0.75, SecondValue = 0.9 },
                new LimitValue() { FirstValue = 0.3, SecondValue = 0.5 },
                new LimitValue() { Value = 0.1, Below = true },
                new LimitValue() { FirstValue = 0.2, SecondValue = 0.35 },
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { FirstValue = 0.7, SecondValue = 1.0 },
                new LimitValue() { Value = 1.0, Below = false },
                new LimitValue() { Value = 0.1, Below = true },
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
                new LimitValue() { Title = "Збільшення"},
        };

        public void ResetTextBoxes(ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control c in controls)
            {
                var tb = c as TextBox;
                if (tb != null)
                {
                    tb.Text = string.Empty;
                }
                ResetTextBoxes(c.Controls);
            }
        }

        public void TreeToTabConformity(TreeView treeView, TabControl tabControl, int index)
        {
            switch (treeView.SelectedNode.Parent == null ? 666 : treeView.SelectedNode.Parent.Index)
            {
                case 0:
                    if (treeView.SelectedNode.Level == 1 && treeView.SelectedNode.Index == index)
                    {
                        tabControl.SelectTab(index);
                    }
                    break;
                case 1:
                    if (treeView.SelectedNode.Level == 1 && treeView.SelectedNode.Index == index - 5)
                    {
                        tabControl.SelectTab(index);
                    }
                    break;
                case 2:
                    if (treeView.SelectedNode.Level == 1 && treeView.SelectedNode.Index == index - 10)
                    {
                        tabControl.SelectTab(index);
                    }
                    break;
                case 3:
                    if (treeView.SelectedNode.Level == 1 && treeView.SelectedNode.Index == index - 16)
                    {
                        tabControl.SelectTab(index);
                    }
                    break;
                default: break;
            }
        }

        public void SaveToDB(DataModel dataModel)
        {
            using (var context = new FSCContext())
            {
                if (dataModel.EnterpriseData.Count == 0 || dataModel.EnterpriseData.Count == 1)
                {
                    MessageBox.Show("Помилка. Для збереження в БД спочатку створіть запис про підприємтсво (Файл - Зареєструвати підприємство), або виберіть існуюче.");
                    return;
                }

                object temp;
                if (!dataModel.EnterpriseData.TryGetValue("Year", out temp))
                //if (dataModel.EnterpriseData.FirstOrDefault(x => x.Key == "Year").Value.ToString() == null)
                {
                    MessageBox.Show("Помилка. Перед збереженням оберіть рік.");
                    return;
                }
                //TODO: check if cant connect
                if (context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseId.Equals(dataModel.EnterpriseData["EnterpriseID"].ToString())) != null) //if enterprise with this id already exists
                {
                    if (MessageBox.Show("Дані будуть збережені в існуюче підприємство.", "Увага!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;

                    if (context.Record.ToList().FirstOrDefault(y => y.Year == Convert.ToInt32(dataModel.EnterpriseData["Year"]) && y.Enterprise.EnterpriseId.Equals(dataModel.EnterpriseData["EnterpriseID"].ToString())) != null) //if record with this year already exists
                    {
                        for (int i = 0; i < dataModel.TextBoxes.Count; ++i)
                        {
                            try
                            {
                                dataModel.Indicators.Add(new EnterpriseIndicator() { IndicatorID = i, IndicatorName = dataModel.Nodes[i].Text, IndicatorValue = double.Parse(dataModel.TextBoxes[i].Text) });
                            }
                            catch (Exception) { }
                        }
                        if (MessageBox.Show("Дані про даний рік будуть перезаписані", "Увага!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;

                        int tt = Convert.ToInt32(dataModel.EnterpriseData["Year"]);
                        string t = dataModel.EnterpriseData["EnterpriseID"].ToString();
                        Record data = context.Record.FirstOrDefault(y => y.Year == tt && y.Enterprise.EnterpriseId.Equals(t));
                        data.EnterpriseIndicators.Clear();
                        data.EnterpriseIndicators = dataModel.Indicators;

                        context.SaveChanges();
                        dataModel.Indicators.Clear();
                        MessageBox.Show("Дані успішно збережені.");
                    }
                    else
                    {
                        for (int i = 0; i < dataModel.TextBoxes.Count; ++i)
                        {
                            try
                            {
                                dataModel.Indicators.Add(new EnterpriseIndicator() { IndicatorID = i, IndicatorName = dataModel.Nodes[i].Text, IndicatorValue = double.Parse(dataModel.TextBoxes[i].Text) });
                            }
                            catch (Exception) { }
                        }
                        context.Record.Add(new Record()
                        {
                            Enterprise = context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseId.Equals(dataModel.EnterpriseData["EnterpriseID"].ToString())),
                            Year = Convert.ToInt32(dataModel.EnterpriseData["Year"]),
                            EnterpriseIndicators = dataModel.Indicators
                        });
                        context.SaveChanges();
                        dataModel.Indicators.Clear();
                        MessageBox.Show("Дані успішно збережені.");
                    }
                    //TODO make datagridview sortings 

                }
                else
                {
                    for (int i = 0; i < dataModel.TextBoxes.Count; ++i)
                    {
                        try
                        {
                            dataModel.Indicators.Add(new EnterpriseIndicator() { IndicatorID = i, IndicatorName = dataModel.Nodes[i].Text, IndicatorValue = double.Parse(dataModel.TextBoxes[i].Text) });
                        }
                        catch (Exception) { }
                    }
                    //TODO: fix crutch with exceptions (but fill database with zeros is not correct)  //TODOTODO: measure speed of this one and for-if-continue variant (performance)

                    context.Enterprise.Add(new Enterprise()
                    {
                        EnterpriseId = dataModel.EnterpriseData["EnterpriseID"].ToString(),
                        EnterpriseName = dataModel.EnterpriseData["EnterpriseName"].ToString(),
                        Region = dataModel.EnterpriseData["Region"].ToString(),
                        Branch = dataModel.EnterpriseData["Branch"].ToString(),
                        Records = new List<Record>() { new Record() { Year = Convert.ToInt32(dataModel.EnterpriseData["Year"]), EnterpriseIndicators = dataModel.Indicators } }
                    });
                    context.SaveChanges();
                    dataModel.Indicators.Clear();
                    MessageBox.Show("Дані успішно збережені у БД.");
                }
            }
        }

        public void Calculate(TabControl tabControl)
        {
            var formulas = new Formulas();
            var arguments = new List<TextBox>();
            foreach (var elem in tabControl.SelectedTab.Controls)
            {
                if (elem is TextBox)
                {
                    if ((elem as TextBox).ReadOnly == false)
                    {
                        arguments.Add(elem as TextBox);
                    }
                }
            }
            foreach (var box in tabControl.SelectedTab.Controls)
            {
                if (box is TextBox)
                {
                    if ((box as TextBox).ReadOnly == true)
                    {

                        switch (tabControl.SelectedTab.Name)
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

        public void Compare(List<Enterprise> enterpriseList)
        {
            var modalCompare = new CompareModal(enterpriseList);
            modalCompare.ShowDialog();

            if (modalCompare.DialogResult == DialogResult.OK)
            {
                new Compare((from first in modalCompare.GetSelectedEnterpriseData[0]
                             from second in modalCompare.GetSelectedEnterpriseData[1]
                             where first.IndicatorID == second.IndicatorID
                             select new
                             {
                                 IndicatorName = first.IndicatorName,
                                 FirstEnterprise = first.IndicatorValue,
                                 SecondEnterprise = second.IndicatorValue,
                                 IndicatorID = first.IndicatorID
                             }).ToList(), modalCompare.GetSelectedEnterpriseData[0][0]?.Record?.Enterprise?.EnterpriseName, modalCompare.GetSelectedEnterpriseData[1][0]?.Record?.Enterprise?.EnterpriseName, modalCompare.GetSelectedEnterpriseData[0][0].Record.Year.ToString()).Show();
            }
        }

        public void ShowDetails(List<EnterpriseIndicator> indicators, string enterpriseName, string year)
        {
            int clusterId = default(int);
            new Details((from item in indicators                            //will not recognize this.DecisionMaking if make querry directly from context
                         select new
                         {
                             NameOfIndicator = item.IndicatorName,
                             CurrentValue = item.IndicatorValue,
                             Conclusion = this.DecisionMaking(item, out clusterId),
                             ClusterID = clusterId
                         }).ToList(), enterpriseName, year).Show();

            //dataGridView1.DataSource = querry.Select(x=> new { d = x.CurrentValue}).ToList();// рабочий вариант вывода одного столбца
        }

        public string DecisionMaking(EnterpriseIndicator entity, out int clusterId)
        {
            // var properties = typeof(DecisionData).GetProperties();

            PropertyInfo[] props = decisionData[entity.IndicatorID].GetType().GetProperties();

            if (props[0].GetValue(decisionData[entity.IndicatorID]) != null && props[4].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                if ((bool) props[0].GetValue(decisionData[entity.IndicatorID]))
                {
                    if (entity.IndicatorValue > (double?) props[4].GetValue(decisionData[entity.IndicatorID]))
                    {
                        clusterId = 1;
                        return "Допустиме значення";
                    }
                    else if (entity.IndicatorValue <= (double?) props[4].GetValue(decisionData[entity.IndicatorID]) && entity.IndicatorValue > ((double?) props[4].GetValue(decisionData[entity.IndicatorID]) - ((double?) props[4].GetValue(decisionData[entity.IndicatorID]) * 0.2)))
                    {
                        clusterId = 2;
                        return "Передкризовий стан";
                    }
                    else
                    {
                        clusterId = 3;
                        return "Кризисний стан";
                    }
                }
                else
                {
                    if (entity.IndicatorValue < (double?) props[4].GetValue(decisionData[entity.IndicatorID]))
                    {
                        clusterId = 1;
                        return "Допустиме значення";
                    }
                    else if (entity.IndicatorValue >= (double?) props[4].GetValue(decisionData[entity.IndicatorID]) && entity.IndicatorValue < ((double?) props[4].GetValue(decisionData[entity.IndicatorID]) + ((double?) props[4].GetValue(decisionData[entity.IndicatorID]) * 0.2)))
                    {
                        clusterId = 2;
                        return "Передкризовий стан";
                    }
                    else
                    {
                        clusterId = 3;
                        return "Кризисний стан";
                    }
                }
            }
            else if (props[1].GetValue(decisionData[entity.IndicatorID]) != null && props[2].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                if (entity.IndicatorValue > (double?) props[1].GetValue(decisionData[entity.IndicatorID]) && entity.IndicatorValue < (double?) props[2].GetValue(decisionData[entity.IndicatorID]))
                {
                    clusterId = 1;
                    return "Допустиме значення діапазону";
                }
                else if ((entity.IndicatorValue > ((double?) props[1].GetValue(decisionData[entity.IndicatorID]) - (double?) props[1].GetValue(decisionData[entity.IndicatorID]) * 0.2) && entity.IndicatorValue <= ((double?) props[1].GetValue(decisionData[entity.IndicatorID])) || (entity.IndicatorValue < ((double?) props[1].GetValue(decisionData[entity.IndicatorID]) + (double?) props[1].GetValue(decisionData[entity.IndicatorID]) * 0.2) && entity.IndicatorValue >= ((double?) props[1].GetValue(decisionData[entity.IndicatorID])))))
                {
                    clusterId = 2;
                    return "Передкризовий стан";
                }
                else
                {
                    clusterId = 3;
                    return "Кризовий стан";
                }
            }
            else if (props[3].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                using (var context = new FSCContext())
                {
                    //List<Record> list = context.Record.OrderBy(x => x.Year).ToList();
                    List<Record> list = entity.Record.Enterprise.Records.OrderBy(x => x.Year).ToList();
                    Record prev = list.FirstOrDefault(x => x.RecordId == entity.Record.RecordId);
                    int r = list.IndexOf(prev);
                    EnterpriseIndicator previousValue;
                    if (r == 0)
                    {
                        clusterId = 0;
                        return "Немає попередніх даних";
                    }
                    else
                    {
                        previousValue = list[r - 1].EnterpriseIndicators?.FirstOrDefault(x => x.IndicatorID == entity.IndicatorID);

                        if (previousValue == null)
                        {
                            clusterId = 0;
                            return "Немає попередніх даних";
                        }
                    }

                    if (previousValue.IndicatorValue <= entity.IndicatorValue)
                    {
                        clusterId = 1;
                        return "Збільшення";
                    }
                    else
                    {
                        clusterId = 2;
                        return "Зменшення";
                    }
                }
            }
            clusterId = 666;
            return null;
        }

        private class LimitValue
        {
            public bool? Below { get; set; }
            public double? FirstValue { get; set; }
            public double? SecondValue { get; set; }
            public string Title { get; set; }
            public double? Value { get; set; }
        }
    }
}
