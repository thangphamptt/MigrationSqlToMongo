﻿using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UrlDocument { get; set; }
        public string Content { get; set; }
    }
}
