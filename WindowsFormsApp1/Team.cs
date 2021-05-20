using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Team
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int Group { get; set; }
        public bool Plays1 { get; set; }
        public bool Plays2 { get; set; }
        public string Short { get; set; }
        override public string  ToString()
        {
            string text = $"{Name} ({Short}) №{Number} Группа {Group} ";
            if (this.Plays1)
                text += "1 тур ";
            if (this.Plays2)
                text += "2 тур ";
            text += "\n";
            return text;
        }

    }
}
