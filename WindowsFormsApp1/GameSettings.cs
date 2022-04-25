using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class GameSettings
    {
        public int TourN { get; set; }
        public int Length { get; set; }
        public int StalkersMethod { get; set; }
        public int[] Cost { get; set; }
        public int[] CostsSt { get; set; }
        public bool Individual { get; set; }
        public GameSettings() { }
        public static string[] methods = {"Чёрный ящик", "Сначала аутсайдер", "Сначала лидер", "По группам"};
            
        override public string ToString()
        {
            string text = $"{TourN} тур: Стоимость вопросов: ";
            foreach (int i in Cost)
                text += $"{i} ";
            text += $"Метод определения очерёдности сталкеров: {methods[StalkersMethod]} ";
            text += $"Длина: {Length} ";
            text += "Стоимость для первого сталкера: ";
            foreach (int i in CostsSt)
                text += $"{i} ";
            if (Individual)
                text += "Индивидуальный";
            else
                text += "Коллективный";
            text += "\n";
            return text;

        }
    }
}
