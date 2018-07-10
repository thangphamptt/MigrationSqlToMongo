using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Candidate
    {
        public Candidate()
        {
            CandidateNote = new HashSet<CandidateNote>();
            CandidateRole = new HashSet<CandidateRole>();
            CandidateSignProcess = new HashSet<CandidateSignProcess>();
            CandidateTag = new HashSet<CandidateTag>();
            InterviewSchedule = new HashSet<InterviewSchedule>();
            JobApplication = new HashSet<JobApplication>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string EmailOther { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string EducationSchool { get; set; }
        public string GraduationYear { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? IsAccepted { get; set; }
        public DateTime? IssuingDate { get; set; }
        public DateTime? SendingDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public DateTime? WorkingStartDate { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }
        public int? SalaryOffer { get; set; }
        public int? BasicSalaryOffer { get; set; }
        public int? PercentageSalaryOffer { get; set; }
        public string NoteOffer { get; set; }
        public bool? IsUpdated { get; set; }
        public string SessionId { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public string IdNo { get; set; }
        public string Domain { get; set; }
        public bool? IsDelete { get; set; }
        public string Profession { get; set; }
        public string Token { get; set; }
        public int? ResponsiveEmpId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string City { get; set; }
        public bool? IsFirstLogin { get; set; }

        public Employee ResponsiveEmp { get; set; }
        public ICollection<CandidateNote> CandidateNote { get; set; }
        public ICollection<CandidateRole> CandidateRole { get; set; }
        public ICollection<CandidateSignProcess> CandidateSignProcess { get; set; }
        public ICollection<CandidateTag> CandidateTag { get; set; }
        public ICollection<InterviewSchedule> InterviewSchedule { get; set; }
        public ICollection<JobApplication> JobApplication { get; set; }
    }
}
