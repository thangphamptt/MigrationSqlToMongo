using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class MailboxLoaded
    {
        public string Mailbox { get; set; }
        public int SequenceId { get; set; }
        public string UniqueIdPop { get; set; }
        public int? UniqueIdImap { get; set; }
        public string Id { get; set; }
    }
}
