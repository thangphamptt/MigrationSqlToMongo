using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class MailHistory
    {
        public int MailId { get; set; }
        public string ToMail { get; set; }
        public string FromMail { get; set; }
        public string CcMail { get; set; }
        public string BccMail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string FileAttachs { get; set; }
        public string TypeMail { get; set; }
        public DateTime CreateDate { get; set; }
        public int? MailIdref { get; set; }
        public int? MailId2ref { get; set; }
        public int? MailId3ref { get; set; }
        public bool IsError { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
