﻿using System;

namespace PunData
{
    [Serializable]
    public class Pun
    {
        public int PunID { get; set; }
        public string Title { get; set; }
        public string Joke { get; set; }
    }
}