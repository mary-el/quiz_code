﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Tour
    {
        public List<Expedition> Expeditions { get; set; }
        public Tour() { }
    }

}
