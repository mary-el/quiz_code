using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Question
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public string PictureQ { get; set; }
        public string PictureA { get; set; }
        public string AudioQ { get; set; }
        public string AudioA { get; set; }
        public string Video { get; set; }
        public string Prompt { get; set; }
        public string Help { get; set; }
        public string Hint { get; set; }
        public string HintForm { get; set; }
        public Question() { }
        public Question(string text_, string Prompt_, string Help_, string Hint_, string HintForm_)
            {
            this.Text = text_;
            this.Prompt = Prompt_;
            this.Help = Help_;
            this.Hint = Hint_;
            this.HintForm = HintForm_;

        }
    }
}
