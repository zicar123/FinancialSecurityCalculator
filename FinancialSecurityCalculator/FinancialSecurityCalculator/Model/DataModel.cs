using System.Collections.Generic;
using System.Windows.Forms;
using FinancialSecurityCalculator.Entities;
using System.Collections;

namespace FinancialSecurityCalculator.Model
{
    public class DataModel
    {
        #region Global Properties
        public List<TextBox> TextBoxes { get; set; } = new List<TextBox>(20); //list of Results
        public List<EnterpriseIndicator> Indicators { get; set; } = new List<EnterpriseIndicator>(20);
        public List<TreeNode> Nodes { get; set; } = new List<TreeNode>(20);
        public Dictionary<string, object> EnterpriseData { get; set; } = new Dictionary<string, object>(); //Modal window data(SaveToDBModal)
        public List<Enterprise> TotalList { get; set; } = new List<Enterprise>(); //only for checkboxes
        public List<Enterprise> chb1 { get; set; } = new List<Enterprise>();       //
        public List<Enterprise> chb2 { get; set; } = new List<Enterprise>();      //
        public List<Enterprise> chb3 { get; set; } = new List<Enterprise>();      //
        public List<string> Branches { get; set; } = new List<string>() { "Виробнича галузь", "Торгівельна галузь", "Галузь фінансів" };
        public List<string> Regions { get; set; } = new List<string>()
        {
                "Київська область", "Одеська область", "Дніпропетровська область",
                "Чернігівська область", "Харківська область", "Житомирська область", "Полтавська область", "Херсонська область",
                "Запорізька область", "Луганська область", "Донецька область", "Вінницька область", "Кiровоградська область",
                "Миколаївська область", "Сумська область", "Львівська область", "Черкаська область", "Хмельницька область",
                "Волинська область", "Рівненська область", "Івано-Франківська область", "Тернопільська область", "Закарпатська область",
                "Чернівецька область","Автономна Республіка Крим"
        };
        #endregion
    }
}
