using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
using FinancialSecurityCalculator.Model;
using FinancialSecurityCalculator.Context;
using FinancialSecurityCalculator.Entities;
using System.Linq;
using System.Collections;

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
                if (dataModel.EnterpriseData.Count == 0)
                {
                    MessageBox.Show("Помилка. Для збереження в БД спочатку створіть запис про підприємтсво (Файл - Зареєструвати підприємство.)");
                }
                else
                {
                    if (context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseName == dataModel.EnterpriseData["EnterpriseName"].ToString()) != null) //if enterprise with this name already exists
                    {
                        for (int i = 0; i < dataModel.TextBoxes.Count; ++i)
                        {
                            try
                            {
                                dataModel.Indicators.Add(new EnterpriseIndicator() { IndicatorName = dataModel.Nodes[i].Text, IndicatorValue = double.Parse(dataModel.TextBoxes[i].Text) });
                            }
                            catch (Exception) { }
                        }
                        context.Record.Add(new Record()
                        {
                            Enterprise = context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseName == dataModel.EnterpriseData["EnterpriseName"].ToString()),
                            Year = Convert.ToInt32(dataModel.EnterpriseData["Year"]),
                            EnterpriseIndicators = dataModel.Indicators
                        });
                        context.SaveChanges();
                        dataModel.Indicators.Clear();
                        MessageBox.Show("Дані збережено в уже існуюче підприємство."); //TODO: modify messageBox. Add OK cancel
                    }

                    else
                    {
                        for (int i = 0; i < dataModel.TextBoxes.Count; ++i)
                        {
                            try
                            {
                                dataModel.Indicators.Add(new EnterpriseIndicator() { IndicatorName = dataModel.Nodes[i].Text, IndicatorValue = double.Parse(dataModel.TextBoxes[i].Text) });
                            }
                            catch (Exception) { }
                        }
                        //TODO: fix crutch with exceptions (but fill database with zeros is not correct)  //TODOTODO: measure speed of this one and for-if-continue variant (performance)

                        context.Enterprise.Add(new Enterprise()
                        {
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

        }

        public string DecisionMaking(EnterpriseIndicator entity)
        {
            List<Types> limitValues = new List<Types>()
            {
                new TypeA() { Value = 0.5, Below = false },
                new TypeA() { Value = 0.8, Below = false },
                new TypeB() { FirstValue = 0.75, SecondValue = 0.9 },
                new TypeB() { FirstValue = 0.3, SecondValue = 0.5 },
                new TypeA() { Value = 0.1, Below = false },
                new TypeB() { FirstValue = 0.2, SecondValue = 0.35 },
                new TypeC() { Title = "Збільшення"},
                new TypeB() { FirstValue = 0.7, SecondValue = 1.0 },
                new TypeA() { Value = 1.0, Below = true },
                new TypeA() { Value = 0.1, Below = false },
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
                new TypeC() { Title = "Збільшення"},
            };


            //switch (limitValues[entity.EnterpriseIndicatorId].GetType().ToString())
            //{
            //    default:
            //        break;
            //}

            if (limitValues[entity.EnterpriseIndicatorId].GetType() == typeof(TypeA))
            {
                if ((limitValues[entity.EnterpriseIndicatorId] as TypeA).Below == false)
                {
                    if (entity.IndicatorValue > (limitValues[entity.EnterpriseIndicatorId] as TypeA).Value)
                    {
                        return "Everything is Ok!";
                    }
                    else return "Everything is BAD!";
                }
                else
                {
                    if (entity.IndicatorValue > (limitValues[entity.EnterpriseIndicatorId] as TypeA).Value)
                    {
                        return "Everything is BAD!";
                    }
                    else return "Everything is Ok!";
                }
                
            }
            else if (limitValues[entity.EnterpriseIndicatorId].GetType() == typeof(TypeB))
            {
                if (entity.IndicatorValue > (limitValues[entity.EnterpriseIndicatorId] as TypeB).FirstValue && entity.IndicatorValue < (limitValues[entity.EnterpriseIndicatorId] as TypeB).SecondValue)
                {
                    return "Everything is Ok!";
                }
                else return "Everything is BAD!";
            }
            else if (limitValues[entity.EnterpriseIndicatorId].GetType() == typeof(TypeC))
            {
                //if (entity.IndicatorValue > с предідущего года)
                //{
                //    return "Everything is Ok!";
                //}
                //else return "Everything is BAD!";
            }
            return "lol";
        }

        private abstract class Types
        {

        }

        private class TypeA : Types
        {
            public double Value { get; set; }
            public bool Below { get; set; }
        }

        private class TypeB : Types
        {
            public double FirstValue { get; set; }
            public double SecondValue { get; set; }
        }

        private class TypeC : Types
        {
            public string Title { get; set; }
        }

        public void Calculate(TabControl tabControl)
        {
            Formulas formulas = new Formulas();
            List<TextBox> arguments = new List<TextBox>();
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
    }
}
