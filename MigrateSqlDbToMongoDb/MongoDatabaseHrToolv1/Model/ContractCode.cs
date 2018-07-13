using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
    public class ContractCode
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public int JobApplicationId { get; set; }
        public int JobId { get; set; }       
        public object Number { get; set; }        
        public object Code1 { get; set; }       
        public object Code2 { get; set; }     
        public object Type { get; set; }        
        public object EmployeeId { get; set; }  
        public object IssuingDate { get; set; }  
        public object NoteIssuing { get; set; }    
        public object SendingDate { get; set; }  
        public object NoteSending { get; set; }       
        public object ConfirmDate { get; set; } 
        public object SigningDate { get; set; }   
        public object WorkingStartDate { get; set; }       
        public object NoteWorkingStartDate { get; set; }       
        public object ViewScreenCandidate { get; set; }
        public string SalaryOffer { get; set; }     
        public object BasicSalaryOffer { get; set; }     
        public object PercentageSalaryOffer { get; set; } 
        public object NoteOffer { get; set; }
        public object AnotherAgree { get; set; }
        public string ReportTo { get; set; }  
        public object IsAcceptConfirm { get; set; }   
        public object NoteConfirm { get; set; }    
        public object IsAcceptSigning { get; set; }    
        public object NoteSigning { get; set; }   
        public object ValidTo { get; set; }  
        public object IsFreshGraduated { get; set; }     
        public object MonthTrail { get; set; }
        public object SalaryIncrease { get; set; }   
        public object Allowance { get; set; }     
        public object MonthProbation { get; set; }
        public object AcceptDate { get; set; }   
        public object NoteAccept { get; set; }      
        public string OfferLetterFile { get; set; }
        public long RowID { get; set; }
    }
}
