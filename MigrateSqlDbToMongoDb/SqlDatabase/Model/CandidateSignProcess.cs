using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class CandidateSignProcess
    {
        public int CanHisId { get; set; }
        public string Type { get; set; }
        public DateTime DateConfirm { get; set; }
        public DateTime? ValidTo { get; set; }
        public int CanHisCanId { get; set; }
        public int CanHisCanDtlId { get; set; }
        public int CanHisRcmPstId { get; set; }
        public bool IsAccept { get; set; }
        public string Note { get; set; }
        public int? CanHisCcdId { get; set; }

        public Candidate CanHisCan { get; set; }
        public JobApplication CanHisCanDtl { get; set; }
        public ContractCode CanHisCcd { get; set; }
        public Job CanHisRcmPst { get; set; }
    }
}
