﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class cella
    {
        public int belso { get; set; }
        public char kiir { get; set; }
        public bool nyitott { get; set; }

        public cella()
        {
            belso = 0;
            kiir = '\0';
            nyitott = false;
        }
    }
}
