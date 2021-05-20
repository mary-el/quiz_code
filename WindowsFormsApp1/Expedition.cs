using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Expedition
    {
        public string Name { get; set; }
        public string HelpType { get; set; }
        public List<Question> Questions { get; set; }

        public Expedition() { }
    }
}
