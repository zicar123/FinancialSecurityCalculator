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

namespace FinancialSecurityCalculator.Services
{
    public class Services : IServices
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
                    MessageBox.Show("Помилка. Для збереження в БД спочатку створіть запис про підприємтсво (Файл - Зареєструвати підприємство)");
                    return;
                }

                object temp;
                if (!dataModel.EnterpriseData.TryGetValue("Year", out temp))
                //if (dataModel.EnterpriseData.FirstOrDefault(x => x.Key == "Year").Value.ToString() == null)
                {
                    MessageBox.Show("Помилка. Перед збереженням виберіть рік.");
                    return;
                }

                if (context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseId == (int)dataModel.EnterpriseData["EnterpriseID"]) != null) //if enterprise with this id already exists
                {
                    if (MessageBox.Show("Дані будуть збережені в існуюче підприємство.", "Увага!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;

                    if (context.Record.ToList().FirstOrDefault(y => y.Year == Convert.ToInt32(dataModel.EnterpriseData["Year"])) != null) //if record with this year already exists
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

                        var tt = Convert.ToInt32(dataModel.EnterpriseData["Year"]);
                        var data = context.Record.FirstOrDefault(y => y.Year == tt);
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
                            Enterprise = context.Enterprise.ToList().FirstOrDefault(x => x.EnterpriseId == (int)dataModel.EnterpriseData["EnterpriseID"]),
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
                        EnterpriseId = (int)dataModel.EnterpriseData["EnterpriseID"],
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

        public string DecisionMaking(EnterpriseIndicator entity)
        {
            //switch (limitValues[entity.EnterpriseIndicatorId].GetType().ToString())
            //{
            //    default:
            //        break;
            //}

            if (limitValues[entity.IndicatorID].GetType() == typeof(TypeA))//TODO: fix (throws exception)
            {
                if ((limitValues[entity.IndicatorID] as TypeA).Below == false)
                {
                    if (entity.IndicatorValue > (limitValues[entity.IndicatorID] as TypeA).Value)
                    {
                        return "Everything is Ok!";
                    }
                    else return "Everything is BAD!";
                }
                else
                {
                    if (entity.IndicatorValue > (limitValues[entity.IndicatorID] as TypeA).Value)
                    {
                        return "Everything is BAD!";
                    }
                    else return "Everything is Ok!";
                }

            }
            else if (limitValues[entity.IndicatorID].GetType() == typeof(TypeB))
            {
                if (entity.IndicatorValue > (limitValues[entity.IndicatorID] as TypeB).FirstValue && entity.IndicatorValue < (limitValues[entity.IndicatorID] as TypeB).SecondValue)
                {
                    return "Everything is Ok!";
                }
                else return "Everything is BAD!";
            }
            else if (limitValues[entity.IndicatorID].GetType() == typeof(TypeC))
            {
                //if (entity.IndicatorValue > с предідущего года)
                //{
                //    return "Everything is Ok!";
                //}
                //else return "Everything is BAD!";
                return "maybe better";
            }
            return "lolThiswould never happen";
        }


        public void ShowDetails(List<EnterpriseIndicator> indicators)
        {
            new Details((from item in indicators                            //will not recognize this.DecisionMaking if make querry directly from context
                         select new
                         {
                             indicatorID = item.EnterpriseIndicatorId,
                             NameOfIndicator = item.IndicatorName,
                             CurrentValue = item.IndicatorValue,
                             Conclusion = this.DecisionMaking(item)
                         }).ToList()).Show();

            //var listAnons = (new[] { new
            //{
            //    indicatorID = default(int),
            //    NameOfIndicator = default(string),
            //    CurrentValue = default(double),
            //    Conclusion = default(string)
            //} }).ToList();

            //listAnons.Clear();



            //List < EnterpriseConclusion > conclusionsList = new List<EnterpriseConclusion>(); //old-fashioned way
            //foreach (var item in indicators)
            //{
            //    conclusionsList.Add(new EnterpriseConclusion()
            //    {
            //        indicatorID = item.EnterpriseIndicatorId,
            //        NameOfIndicator = item.IndicatorName,
            //        CurrentValue = item.IndicatorValue,
            //        Conclusion = this.DecisionMaking(item)
            //    });
            //}

            //TODO: this doesnt work right

            //dataGridView1.DataSource = querry.Select(x=> new { d = x.CurrentValue}).ToList();// рабочий вариант вывода одного столбца
            //dataGridView1.DataSource = querry.ToList();


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

        private abstract class Types
        {

        } //TODO: join everything in a single Class

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

        //public class EnterpriseConclusion
        //{
        //    public int indicatorID { get; set; }
        //    public string NameOfIndicator { get; set; }
        //    public double CurrentValue { get; set; }
        //    public string Conclusion { get; set; }
        //}


        private List<DecisionData> decisionData = new List<DecisionData>()
        {
                new DecisionData() { Value = 0.5, Below = false },
                new DecisionData() { Value = 0.8, Below = false },
                new DecisionData() { FirstValue = 0.75, SecondValue = 0.9 },
                new DecisionData() { FirstValue = 0.3, SecondValue = 0.5 },
                new DecisionData() { Value = 0.1, Below = false },
                new DecisionData() { FirstValue = 0.2, SecondValue = 0.35 },
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { FirstValue = 0.7, SecondValue = 1.0 },
                new DecisionData() { Value = 1.0, Below = true },
                new DecisionData() { Value = 0.1, Below = false },
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
                new DecisionData() { Title = "Збільшення"},
        };

        private class DecisionData
        {
            public double? Value { get; set; }
            public bool? Below { get; set; }
            public double? FirstValue { get; set; }
            public double? SecondValue { get; set; }
            public string Title { get; set; }
        }

        public void DecisionMakingReflection(EnterpriseIndicator entity)
        {
            // var properties = typeof(DecisionData).GetProperties();
            
            foreach (var obj in decisionData)
            {
                var properties = (obj.GetType()).GetProperties();

                foreach (var prop in properties)
                {
                    var a = obj.GetType().GetProperty(prop.Name).GetValue(obj, null);

                    if (true)
                    {

                    }
                }
            }



            var props = decisionData[entity.IndicatorID].GetType().GetProperties();

            if (props[0].GetValue(decisionData[entity.IndicatorID]) != null && props[1].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                //TypeA actions
            }
            else if (props[2].GetValue(decisionData[entity.IndicatorID]) != null && props[3].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                //TypeB actions
            }
            else if (props[4].GetValue(decisionData[entity.IndicatorID]) != null)
            {
                //TypeC actions
            }


            //var propValue = decisionData[entity.IndicatorID].GetType().GetProperty("Value");
            //var propBelow = decisionData[entity.IndicatorID].GetType().GetProperty("Below");
            //var propFirstValue = decisionData[entity.IndicatorID].GetType().GetProperty("Value");
            //var propSecondValue = decisionData[entity.IndicatorID].GetType().GetProperty("Below");
            //var propTitle = decisionData[entity.IndicatorID].GetType().GetProperty("Below");



            //if (propValue.GetValue(decisionData[entity.IndicatorID], null) != null && propBelow.GetValue(decisionData[entity.IndicatorID], null) != null)
            //{
            //    //TypeA actions
            //}
        }
    }






            

    
}
