using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Countries
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryLocalName { get; set; }
        public int? NumberCode { get; set; }
        public string PhoneNumberCode { get; set; }
        public string Iso3Code { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
