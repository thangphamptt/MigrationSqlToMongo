using Microsoft.EntityFrameworkCore;

namespace SqlDatabase.Model
{
    public partial class HrToolDbContext : DbContext
    {
        public HrToolDbContext()
        {
        }

        public HrToolDbContext(DbContextOptions<HrToolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<AdminAccount> AdminAccount { get; set; }
        public virtual DbSet<Allowances> Allowances { get; set; }
        public virtual DbSet<Bonus> Bonus { get; set; }
        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<CandidateJob> CandidateJob { get; set; }
        public virtual DbSet<CandidateNote> CandidateNote { get; set; }
        public virtual DbSet<CandidateRole> CandidateRole { get; set; }
        public virtual DbSet<CandidateSignProcess> CandidateSignProcess { get; set; }
        public virtual DbSet<CandidateTag> CandidateTag { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractAllowances> ContractAllowances { get; set; }
        public virtual DbSet<ContractBonus> ContractBonus { get; set; }
        public virtual DbSet<ContractCode> ContractCode { get; set; }
        public virtual DbSet<ContractType> ContractType { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Cvsource> Cvsource { get; set; }
        public virtual DbSet<DayOff> DayOff { get; set; }
        public virtual DbSet<DayOffType> DayOffType { get; set; }
        public virtual DbSet<Dependents> Dependents { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<EmailTracking> EmailTracking { get; set; }
        public virtual DbSet<EmailTrackingAttachment> EmailTrackingAttachment { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeDependents> EmployeeDependents { get; set; }
        public virtual DbSet<EmployeeEmail> EmployeeEmail { get; set; }
        public virtual DbSet<EmployeeHistoryAction> EmployeeHistoryAction { get; set; }
        public virtual DbSet<EmployeeInsurance> EmployeeInsurance { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }
        public virtual DbSet<EmployeeTax> EmployeeTax { get; set; }
        public virtual DbSet<EmployeeTaxDependent> EmployeeTaxDependent { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistory { get; set; }
        public virtual DbSet<EtagHandler> EtagHandler { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<Forms> Forms { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<HistoryActionCandidate> HistoryActionCandidate { get; set; }
        public virtual DbSet<IncomeTaxRateTable> IncomeTaxRateTable { get; set; }
        public virtual DbSet<IncomeTaxRateTableDetail> IncomeTaxRateTableDetail { get; set; }
        public virtual DbSet<Insurance> Insurance { get; set; }
        public virtual DbSet<InsuranceType> InsuranceType { get; set; }
        public virtual DbSet<Interview> Interview { get; set; }
        public virtual DbSet<InterviewComment> InterviewComment { get; set; }
        public virtual DbSet<InterviewRound> InterviewRound { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedule { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobApplication> JobApplication { get; set; }
        public virtual DbSet<JobApplicationAttachment> JobApplicationAttachment { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<LetterTemplate> LetterTemplate { get; set; }
        public virtual DbSet<LetterTemplateType> LetterTemplateType { get; set; }
        public virtual DbSet<MailboxLoaded> MailboxLoaded { get; set; }
        public virtual DbSet<MailHistory> MailHistory { get; set; }
        public virtual DbSet<MarkOnlineTest> MarkOnlineTest { get; set; }
        public virtual DbSet<MarkOnlineTestBackup> MarkOnlineTestBackup { get; set; }
        public virtual DbSet<Natioinality> Natioinality { get; set; }
        public virtual DbSet<OutstandingProject> OutstandingProject { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PersonalIncomeTax> PersonalIncomeTax { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionTemplate> PositionTemplate { get; set; }
        public virtual DbSet<ProbationAppraisal> ProbationAppraisal { get; set; }
        public virtual DbSet<RecruitmentTemplate> RecruitmentTemplate { get; set; }
        public virtual DbSet<ReductFaminlyCir> ReductFaminlyCir { get; set; }
        public virtual DbSet<RemindUpdateCvschedule> RemindUpdateCvschedule { get; set; }
        public virtual DbSet<ResultOnlineTest> ResultOnlineTest { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<ScreeningCvhistory> ScreeningCvhistory { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<TagInfo> TagInfo { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TimeSheet> TimeSheet { get; set; }
        public virtual DbSet<TimeSheetReport> TimeSheetReport { get; set; }
        public virtual DbSet<WorkExperience> WorkExperience { get; set; }
        public virtual DbSet<WorkingGroup> WorkingGroup { get; set; }
        public virtual DbSet<WorkingGroupEmployee> WorkingGroupEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=thangpt-pc;Database=HrToolv1;user id=sa;password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actions>(entity =>
            {
                entity.Property(e => e.ActionApi).HasMaxLength(200);

                entity.Property(e => e.ActionName).HasMaxLength(200);

                entity.Property(e => e.MethodName).HasMaxLength(200);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actions_Permissions");
            });

            modelBuilder.Entity<AdminAccount>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Updatedate).HasColumnType("datetime");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Allowances>(entity =>
            {
                entity.HasKey(e => e.AlwId);

                entity.Property(e => e.AlwId).HasColumnName("AlwID");

                entity.Property(e => e.AlwName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Bonus>(entity =>
            {
                entity.HasKey(e => e.BnsId);

                entity.Property(e => e.BnsId).HasColumnName("BnsID");

                entity.Property(e => e.BnsName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Money).HasColumnType("money");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.ConfirmDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Domain).HasMaxLength(250);

                entity.Property(e => e.EducationSchool).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.EmailOther).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GraduationYear).HasMaxLength(4);

                entity.Property(e => e.IdNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFirstLogin).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsUpdated).HasDefaultValueSql("((0))");

                entity.Property(e => e.IssuingDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Profession).HasMaxLength(50);

                entity.Property(e => e.SendingDate).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasMaxLength(64);

                entity.Property(e => e.SigningDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.WorkingStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.ResponsiveEmp)
                    .WithMany(p => p.Candidate)
                    .HasForeignKey(d => d.ResponsiveEmpId)
                    .HasConstraintName("FK_Candidate_Employee");
            });

            modelBuilder.Entity<CandidateJob>(entity =>
            {
                entity.Property(e => e.StartDateSuggest).HasColumnType("date");
            });

            modelBuilder.Entity<CandidateNote>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateNote)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateNote_Candidate");

                entity.HasOne(d => d.CreatedUserNavigation)
                    .WithMany(p => p.CandidateNote)
                    .HasForeignKey(d => d.CreatedUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateNote_Employee");
            });

            modelBuilder.Entity<CandidateRole>(entity =>
            {
                entity.HasKey(e => new { e.CandidateId, e.RoleId });

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateRole)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateRole_Candidate");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CandidateRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateRole_Role");
            });

            modelBuilder.Entity<CandidateSignProcess>(entity =>
            {
                entity.HasKey(e => e.CanHisId);

                entity.Property(e => e.CanHisId).HasColumnName("CanHisID");

                entity.Property(e => e.CanHisCanDtlId).HasColumnName("CanHisCanDtlID");

                entity.Property(e => e.CanHisCanId).HasColumnName("CanHisCanID");

                entity.Property(e => e.CanHisCcdId).HasColumnName("CanHisCcdID");

                entity.Property(e => e.CanHisRcmPstId).HasColumnName("CanHisRcmPstID");

                entity.Property(e => e.DateConfirm).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.CanHisCanDtl)
                    .WithMany(p => p.CandidateSignProcess)
                    .HasForeignKey(d => d.CanHisCanDtlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSignProcess_CandidateDetail");

                entity.HasOne(d => d.CanHisCan)
                    .WithMany(p => p.CandidateSignProcess)
                    .HasForeignKey(d => d.CanHisCanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSignProcess_Candidate");

                entity.HasOne(d => d.CanHisCcd)
                    .WithMany(p => p.CandidateSignProcess)
                    .HasForeignKey(d => d.CanHisCcdId)
                    .HasConstraintName("FK_CandidateSignProcess_ContractCode");

                entity.HasOne(d => d.CanHisRcmPst)
                    .WithMany(p => p.CandidateSignProcess)
                    .HasForeignKey(d => d.CanHisRcmPstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSignProcess_RecruitmentPosition");
            });

            modelBuilder.Entity<CandidateTag>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.CandidateId });

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateTag)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateTag_Candidate");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.CandidateTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateTag_TagInfo");

                entity.HasOne(d => d.CandidateTagNavigation)
                    .WithOne(p => p.InverseCandidateTagNavigation)
                    .HasForeignKey<CandidateTag>(d => new { d.TagId, d.CandidateId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateTag_CandidateTag");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(255);

                entity.Property(e => e.AgreementTemplatePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ContractTemplatePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EmailAllEmpl).HasMaxLength(255);

                entity.Property(e => e.EmailNameForSend).HasMaxLength(100);

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HrDepartmentDisplayName).HasMaxLength(100);

                entity.Property(e => e.HrDepartmentEmail).HasMaxLength(100);

                entity.Property(e => e.Hrmail)
                    .IsRequired()
                    .HasColumnName("HRMail")
                    .HasMaxLength(50);

                entity.Property(e => e.Itmail)
                    .IsRequired()
                    .HasColumnName("ITMail")
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OffterTemplatePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PassEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassHrmail)
                    .HasColumnName("PassHRMail")
                    .HasMaxLength(50);

                entity.Property(e => e.PassItmail)
                    .HasColumnName("PassITMail")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.ProbationTemplatePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(255);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(250);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Code1)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.InsuranceSalary).HasMaxLength(200);

                entity.Property(e => e.NetSalary)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProbationSalary).HasMaxLength(200);

                entity.Property(e => e.RefCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReportTo).HasMaxLength(100);

                entity.Property(e => e.SalaryIncrease).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UrlContract).HasMaxLength(255);

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.ContractTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_ContractType");

                entity.HasOne(d => d.LabourEmployee)
                    .WithMany(p => p.ContractLabourEmployee)
                    .HasForeignKey(d => d.LabourEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RepresentativeEmployee)
                    .WithMany(p => p.ContractRepresentativeEmployee)
                    .HasForeignKey(d => d.RepresentativeEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ContractAllowances>(entity =>
            {
                entity.HasKey(e => new { e.CtrAlwCtrId, e.CtrAlwAlwId });

                entity.Property(e => e.CtrAlwCtrId).HasColumnName("CtrAlwCtrID");

                entity.Property(e => e.CtrAlwAlwId).HasColumnName("CtrAlwAlwID");

                entity.HasOne(d => d.CtrAlwAlw)
                    .WithMany(p => p.ContractAllowances)
                    .HasForeignKey(d => d.CtrAlwAlwId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractAllowances_Allowances");

                entity.HasOne(d => d.CtrAlwCtr)
                    .WithMany(p => p.ContractAllowances)
                    .HasForeignKey(d => d.CtrAlwCtrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractAllowances_Contract");
            });

            modelBuilder.Entity<ContractBonus>(entity =>
            {
                entity.HasKey(e => new { e.CtrBnsCtrId, e.CtrBnsBnsId });

                entity.Property(e => e.CtrBnsCtrId).HasColumnName("CtrBnsCtrID");

                entity.Property(e => e.CtrBnsBnsId).HasColumnName("CtrBnsBnsID");

                entity.HasOne(d => d.CtrBnsBns)
                    .WithMany(p => p.ContractBonus)
                    .HasForeignKey(d => d.CtrBnsBnsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractBonus_Bonus");

                entity.HasOne(d => d.CtrBnsCtr)
                    .WithMany(p => p.ContractBonus)
                    .HasForeignKey(d => d.CtrBnsCtrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractBonus_Contract");
            });

            modelBuilder.Entity<ContractCode>(entity =>
            {
                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.Allowance).HasMaxLength(200);

                entity.Property(e => e.AnotherAgree).HasMaxLength(500);

                entity.Property(e => e.BasicSalaryOffer).HasMaxLength(200);

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.ConfirmDate).HasColumnType("datetime");

                entity.Property(e => e.IssuingDate).HasColumnType("datetime");

                entity.Property(e => e.NoteIssuing).HasMaxLength(200);

                entity.Property(e => e.NoteOffer).HasMaxLength(500);

                entity.Property(e => e.NoteSending).HasMaxLength(20);

                entity.Property(e => e.NoteWorkingStartDate).HasMaxLength(200);

                entity.Property(e => e.OfferLetterFile).HasMaxLength(200);

                entity.Property(e => e.ReportTo).HasMaxLength(200);

                entity.Property(e => e.SalaryIncrease).HasMaxLength(200);

                entity.Property(e => e.SalaryOffer)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SendingDate).HasColumnType("datetime");

                entity.Property(e => e.SigningDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.WorkingStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ContractCode)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_ContractCode_Employee");

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.ContractCode)
                    .HasForeignKey(d => d.JobApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractCode_JobApplication");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.ContractCode)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractCode_RecruimentPosition");
            });

            modelBuilder.Entity<ContractType>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CodeView).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NameView).HasMaxLength(255);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.CountryLocalName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CountryNameEn)
                    .IsRequired()
                    .HasColumnName("CountryNameEN")
                    .HasMaxLength(100);

                entity.Property(e => e.Iso3Code).HasMaxLength(3);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneNumberCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Cvsource>(entity =>
            {
                entity.ToTable("CVSource");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<DayOff>(entity =>
            {
                entity.HasKey(e => e.DofId);

                entity.Property(e => e.DofId).HasColumnName("DofID");

                entity.Property(e => e.ApplyDate).HasColumnType("datetime");

                entity.Property(e => e.DofDtpId).HasColumnName("DofDtpID");

                entity.Property(e => e.DofEmplIdapproved).HasColumnName("DofEmplIDApproved");

                entity.Property(e => e.DofEmplIdrequested).HasColumnName("DofEmplIDRequested");

                entity.Property(e => e.DofEmplIdverified).HasColumnName("DofEmplIDVerified");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.DofDtp)
                    .WithMany(p => p.DayOff)
                    .HasForeignKey(d => d.DofDtpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayOff_DayOffType");

                entity.HasOne(d => d.DofEmplIdapprovedNavigation)
                    .WithMany(p => p.DayOffDofEmplIdapprovedNavigation)
                    .HasForeignKey(d => d.DofEmplIdapproved)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayOff_Employee2");

                entity.HasOne(d => d.DofEmplIdrequestedNavigation)
                    .WithMany(p => p.DayOffDofEmplIdrequestedNavigation)
                    .HasForeignKey(d => d.DofEmplIdrequested)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayOff_Employee");

                entity.HasOne(d => d.DofEmplIdverifiedNavigation)
                    .WithMany(p => p.DayOffDofEmplIdverifiedNavigation)
                    .HasForeignKey(d => d.DofEmplIdverified)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DayOff_Employee1");
            });

            modelBuilder.Entity<DayOffType>(entity =>
            {
                entity.HasKey(e => e.DtpId);

                entity.Property(e => e.DtpId).HasColumnName("DtpID");

                entity.Property(e => e.DotName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Dependents>(entity =>
            {
                entity.HasKey(e => e.DepId);

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.DepEmplId).HasColumnName("DepEmplID");

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DepEmpl)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.DepEmplId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependents_Employee");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(250);

                entity.Property(e => e.UrlDocument).HasMaxLength(500);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Field).HasMaxLength(250);

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SchoolLevel).HasMaxLength(30);

                entity.Property(e => e.To).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailTracking>(entity =>
            {
                entity.Property(e => e.From).HasMaxLength(200);

                entity.Property(e => e.SendingTime).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(400);

                entity.Property(e => e.To).HasMaxLength(200);

                entity.Property(e => e.TypeOfEmail).HasMaxLength(250);

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.EmailTracking)
                    .HasForeignKey(d => d.JobApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailTracking_JobApplication");
            });

            modelBuilder.Entity<EmailTrackingAttachment>(entity =>
            {
                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.EmailTracking)
                    .WithMany(p => p.EmailTrackingAttachment)
                    .HasForeignKey(d => d.EmailTrackingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailTrackingAttachment_EmailTracking");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.AgreementAdd)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AnnualLeave).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.AnnualLeaveRequest).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.BalanceNote).HasMaxLength(1000);

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BirthPlace).HasMaxLength(100);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Branch).HasMaxLength(150);

                entity.Property(e => e.CompanyEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Domain).HasMaxLength(250);

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.EmergencyContact).HasMaxLength(200);

                entity.Property(e => e.EmergencyName).HasMaxLength(50);

                entity.Property(e => e.EmergencyPhone).HasMaxLength(50);

                entity.Property(e => e.EmplNatId).HasColumnName("EmplNatID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstNameEn)
                    .HasColumnName("FirstNameEN")
                    .HasMaxLength(50);

                entity.Property(e => e.FooterEmail).HasMaxLength(4000);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.IdCard).HasMaxLength(20);

                entity.Property(e => e.IdProvidedDate).HasColumnType("datetime");

                entity.Property(e => e.IdProvidedPlace).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastNameEn)
                    .HasColumnName("LastNameEN")
                    .HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.OfferLetterAddSigned).HasMaxLength(255);

                entity.Property(e => e.Origin).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PermanentAddress).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Photograph).HasMaxLength(255);

                entity.Property(e => e.PrivateEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Profession).HasMaxLength(250);

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.SickLeave).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.SickLeaveRequest).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Skype)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SocialInsuranceCode).HasMaxLength(20);

                entity.Property(e => e.SocialInsuranceIssue).HasColumnType("datetime");

                entity.Property(e => e.SocialInsurancePlace).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TaxCode).HasMaxLength(50);

                entity.Property(e => e.TemporaryAddress).HasMaxLength(150);

                entity.Property(e => e.TimeSheetCode).HasMaxLength(10);

                entity.Property(e => e.UnpaidLeave).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.UnpaidLeaveRequest).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WorkEndDate).HasColumnType("datetime");

                entity.Property(e => e.WorkStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Employee_Company");

                entity.HasOne(d => d.ContractCodeNavigation)
                    .WithMany(p => p.EmployeeNavigation)
                    .HasForeignKey(d => d.ContractCodeId)
                    .HasConstraintName("FK_Employee_ContractCode");

                entity.HasOne(d => d.CreatedUserNavigation)
                    .WithMany(p => p.InverseCreatedUserNavigation)
                    .HasForeignKey(d => d.CreatedUser)
                    .HasConstraintName("FK_Employee_Employee");

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.JobApplicationId)
                    .HasConstraintName("FK_Employee_JobApplication");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Position");
            });

            modelBuilder.Entity<EmployeeDependents>(entity =>
            {
                entity.HasKey(e => e.EdpId);

                entity.Property(e => e.EdpId).HasColumnName("EdpID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EdpEmplId).HasColumnName("EdpEmplID");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.EdpEmpl)
                    .WithMany(p => p.EmployeeDependents)
                    .HasForeignKey(d => d.EdpEmplId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDependents_Employee");
            });

            modelBuilder.Entity<EmployeeEmail>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.EmployeeEmail)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEmail_Company");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEmail)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEmail_Employee");
            });

            modelBuilder.Entity<EmployeeHistoryAction>(entity =>
            {
                entity.Property(e => e.ActionDate).HasColumnType("date");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ErrorDate).HasColumnType("datetime");

                entity.Property(e => e.ErrorStatus).HasMaxLength(50);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeHistoryAction)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeHistoryAction_Employee");
            });

            modelBuilder.Entity<EmployeeInsurance>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MoneyCompanyPay)
                    .HasColumnType("numeric(25, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MoneyEmployeePay)
                    .HasColumnType("numeric(25, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PercentCompanyPay)
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PercentEmployeePay)
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalMoney)
                    .HasColumnType("numeric(25, 0)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeInsurance)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeInsurance_Employee");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.EmployeeInsurance)
                    .HasForeignKey(d => d.InsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeInsurance_Insurance");
            });

            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.RoleId });

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRole)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRole_Employee");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EmployeeRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRole_Role");
            });

            modelBuilder.Entity<EmployeeTax>(entity =>
            {
                entity.HasKey(e => e.EmplTaxId);

                entity.Property(e => e.EmplTaxId)
                    .HasColumnName("EmplTaxID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmplTaxEmplId).HasColumnName("EmplTaxEmplID");

                entity.Property(e => e.EmplTaxPerTaxId).HasColumnName("EmplTaxPerTaxID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.MoneyDependentReduct).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.MoneyReductFamilyCir).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.MoneyTax).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TotalMoney).HasColumnType("numeric(25, 5)");

                entity.HasOne(d => d.EmplTaxEmpl)
                    .WithMany(p => p.EmployeeTax)
                    .HasForeignKey(d => d.EmplTaxEmplId)
                    .HasConstraintName("FK_EmployeeTax_Employee");

                entity.HasOne(d => d.EmplTaxPerTax)
                    .WithMany(p => p.EmployeeTax)
                    .HasForeignKey(d => d.EmplTaxPerTaxId)
                    .HasConstraintName("FK_EmployeeTax_PersonalIncomeTax1");
            });

            modelBuilder.Entity<EmployeeTaxDependent>(entity =>
            {
                entity.HasKey(e => new { e.EmplTaxId, e.DepId });

                entity.Property(e => e.EmplTaxId).HasColumnName("EmplTaxID");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.EmployeeTaxDependent)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTaxDependent_Dependents");

                entity.HasOne(d => d.EmplTax)
                    .WithMany(p => p.EmployeeTaxDependent)
                    .HasForeignKey(d => d.EmplTaxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTaxDependent_EmployeeTax");
            });

            modelBuilder.Entity<EmploymentHistory>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ReasonForLeaving).HasMaxLength(500);

                entity.Property(e => e.Responsibilities).HasMaxLength(500);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Supervisor).HasMaxLength(255);

                entity.Property(e => e.SupervisorEmail).HasMaxLength(255);

                entity.Property(e => e.SupervisorPhoneNumber)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<EtagHandler>(entity =>
            {
                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Etag)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.UrlEncryption).HasMaxLength(100);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.EtagHandler)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_EtagHandler_Company");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActionName).HasMaxLength(100);

                entity.Property(e => e.ControllerName).HasMaxLength(100);

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Forms>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(200);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.Property(e => e.ActionId).HasMaxLength(50);

                entity.Property(e => e.DateAction)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ipaction)
                    .HasColumnName("IPAction")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_Employee");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_Form");
            });

            modelBuilder.Entity<HistoryActionCandidate>(entity =>
            {
                entity.HasKey(e => e.CanHisId);

                entity.Property(e => e.CanHisId)
                    .HasColumnName("CanHisID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CanHisCanId).HasColumnName("CanHisCanID");

                entity.Property(e => e.CanHisCandtlId).HasColumnName("CanHisCandtlID");

                entity.Property(e => e.CanHisRcmId).HasColumnName("CanHisRcmID");

                entity.Property(e => e.DateConfirm).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<IncomeTaxRateTable>(entity =>
            {
                entity.HasKey(e => e.IncId);

                entity.Property(e => e.IncId).HasColumnName("IncID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IncomeTaxRateTableDetail>(entity =>
            {
                entity.HasKey(e => e.IncDtlId);

                entity.Property(e => e.IncDtlId).HasColumnName("IncDtlID");

                entity.Property(e => e.FromIncome).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.IncDtlIncId).HasColumnName("IncDtlIncID");

                entity.Property(e => e.PercentTax).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ToIncome).HasColumnType("numeric(25, 5)");

                entity.HasOne(d => d.IncDtlInc)
                    .WithMany(p => p.IncomeTaxRateTableDetail)
                    .HasForeignKey(d => d.IncDtlIncId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncomeTaxRateTableDetail_IncomeTaxRateTable");
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PercentCompanyPay)
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PercentEmployeePay)
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.InsuranceType)
                    .WithMany(p => p.Insurance)
                    .HasForeignKey(d => d.InsuranceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insurance_InsuranceType");
            });

            modelBuilder.Entity<InsuranceType>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UC_InsuranceType")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.NameVn)
                    .IsRequired()
                    .HasColumnName("NameVN")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.DateFeedbackEmail).HasColumnType("datetime");

                entity.Property(e => e.DateFeedbackPhone).HasColumnType("datetime");

                entity.Property(e => e.DateInterview).HasColumnType("datetime");

                entity.Property(e => e.ExpectedSalary).HasMaxLength(50);

                entity.Property(e => e.RecordingPath).HasMaxLength(255);

                entity.Property(e => e.SuggestedSalary).HasMaxLength(50);

                entity.Property(e => e.Summary).HasMaxLength(1000);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.InterviewRound)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.InterviewRoundId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_InterviewRound");

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.JobApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_JobApplication");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_Job");
            });

            modelBuilder.Entity<InterviewComment>(entity =>
            {
                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.InterviewerName).HasMaxLength(255);

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.InterviewComment)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interview__Comme__49AEE81E");
            });

            modelBuilder.Entity<InterviewRound>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<InterviewSchedule>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FromBookRoomDate).HasColumnType("datetime");

                entity.Property(e => e.FromTechnicalDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.ToBookRoomDate).HasColumnType("datetime");

                entity.Property(e => e.ToTechnicalDate).HasColumnType("datetime");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.InterviewSchedule)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewSchedule_Candidate");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.InterviewSchedule)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_InterviewSchedule_Employee");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.InterviewSchedule)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewSchedule_Interview");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.InterviewSchedule)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_InterviewSchedule_Room");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.JobCode).HasMaxLength(100);

                entity.Property(e => e.JobTitle).HasMaxLength(400);

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Company");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Position");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentSalary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CvsourceId).HasColumnName("CVSourceId");

                entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExperienceYear).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.GroupIq).HasColumnName("GroupIQ");

                entity.Property(e => e.IsCandidateUpdated).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OfferPositionName).HasMaxLength(100);

                entity.Property(e => e.RatingUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.SalaryOffer).HasColumnType("numeric(25, 0)");

                entity.Property(e => e.StartDateSuggest).HasColumnType("date");

                entity.Property(e => e.SuggestedSalary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UrlDocument).HasMaxLength(255);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.JobApplication)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplication_Candidate");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.JobApplication)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplication_Company");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplication)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_JobApplication_Job");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.JobApplication)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_JobApplication_Postion");
            });

            modelBuilder.Entity<JobApplicationAttachment>(entity =>
            {
                entity.Property(e => e.FileType).HasMaxLength(50);

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.JobApplicationAttachment)
                    .HasForeignKey(d => d.JobApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplicationAttachment_JobApplication");
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobStatus)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobStatus_Job");
            });

            modelBuilder.Entity<LetterTemplate>(entity =>
            {
                entity.Property(e => e.BccEmail).HasMaxLength(255);

                entity.Property(e => e.CcEmail).HasMaxLength(255);

                entity.Property(e => e.FromEmail).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Subject).HasMaxLength(255);

                entity.Property(e => e.ToEmail).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.LetterTemplate)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_LetterTemplate_Company");
            });

            modelBuilder.Entity<LetterTemplateType>(entity =>
            {
                entity.HasKey(e => e.LetTempTypId);

                entity.Property(e => e.LetTempTypId)
                    .HasColumnName("LetTempTypID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LetTempTypName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MailboxLoaded>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.Mailbox)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UniqueIdImap).HasColumnName("UniqueIdIMAP");

                entity.Property(e => e.UniqueIdPop)
                    .HasColumnName("UniqueIdPOP")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MailHistory>(entity =>
            {
                entity.HasKey(e => e.MailId);

                entity.Property(e => e.MailId).HasColumnName("MailID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileAttachs).HasMaxLength(255);

                entity.Property(e => e.MailId2ref).HasColumnName("MailID2Ref");

                entity.Property(e => e.MailId3ref).HasColumnName("MailID3Ref");

                entity.Property(e => e.MailIdref).HasColumnName("MailIDRef");

                entity.Property(e => e.Subject).HasMaxLength(255);

                entity.Property(e => e.TypeMail).HasMaxLength(255);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<MarkOnlineTest>(entity =>
            {
                entity.HasKey(e => e.JobApplicationId);

                entity.Property(e => e.JobApplicationId).ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("smalldatetime");

                entity.Property(e => e.Iqmark)
                    .HasColumnName("IQMark")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("smalldatetime");

                entity.Property(e => e.TechnicalMark)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MarkOnlineTestBackup>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Iqmark)
                    .HasColumnName("IQMark")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TechnicalMark)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Natioinality>(entity =>
            {
                entity.HasKey(e => e.NatId);

                entity.HasIndex(e => e.NatCode)
                    .HasName("UQ__Natioina__9118D91D5DCAEF64")
                    .IsUnique();

                entity.HasIndex(e => e.NatName)
                    .HasName("UQ__Natioina__0877A6F760A75C0F")
                    .IsUnique();

                entity.Property(e => e.NatId).HasColumnName("NatID");

                entity.Property(e => e.NatCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NatName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OutstandingProject>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.GeneralInformation).HasMaxLength(500);

                entity.Property(e => e.InterfaceSystem).HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TechnologyUsed).HasMaxLength(255);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UC_Permissions")
                    .IsUnique();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permissions_Forms");
            });

            modelBuilder.Entity<PersonalIncomeTax>(entity =>
            {
                entity.HasKey(e => e.PerTaxId);

                entity.Property(e => e.PerTaxId)
                    .HasColumnName("PerTaxID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DependentReduct).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.PercentReductFarmilyCir).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.PercentTax).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.CareerNameVn)
                    .HasColumnName("CareerNameVN")
                    .HasMaxLength(255);

                entity.Property(e => e.Ceogroup).HasColumnName("CEOGroup");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PositionNameVn)
                    .HasColumnName("PositionNameVN")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Position)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Position_Company");
            });

            modelBuilder.Entity<PositionTemplate>(entity =>
            {
                entity.HasKey(e => new { e.PositionId, e.CompanyId });

                entity.Property(e => e.JobDescription).IsRequired();

                entity.Property(e => e.JobRequirement).IsRequired();

                entity.Property(e => e.JobTitle).HasMaxLength(255);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PositionTemplate)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PositionTemplate_Company");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PositionTemplate)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PositionTemplate_Position");
            });

            modelBuilder.Entity<ProbationAppraisal>(entity =>
            {
                entity.Property(e => e.AppraisalDate).HasColumnType("date");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ProbationPeriod).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProbationAppraisal)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProbationAppraisal_Employee");
            });

            modelBuilder.Entity<RecruitmentTemplate>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CompanySize).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Cvlanguage)
                    .HasColumnName("CVLanguage")
                    .HasMaxLength(50);

                entity.Property(e => e.JobCategory).HasMaxLength(255);

                entity.Property(e => e.JobLevel).HasMaxLength(50);

                entity.Property(e => e.JobTitle).HasMaxLength(255);

                entity.Property(e => e.JobType).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.LogoUrl).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Salary).HasMaxLength(50);

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.Website).HasMaxLength(255);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.RecruitmentTemplate)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecruitmentTemplate_Job");
            });

            modelBuilder.Entity<ReductFaminlyCir>(entity =>
            {
                entity.HasKey(e => e.RfaId);

                entity.Property(e => e.RfaId).HasColumnName("RfaID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Money).HasColumnType("numeric(25, 5)");

                entity.Property(e => e.Percent).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RemindUpdateCvschedule>(entity =>
            {
                entity.ToTable("RemindUpdateCVSchedule");

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ResultOnlineTest>(entity =>
            {
                entity.Property(e => e.Answer).HasMaxLength(1000);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName)
                    .HasName("UC_RoleName")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RoleType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Role_Company");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_Role");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoomCode).HasMaxLength(255);

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ScreeningCvhistory>(entity =>
            {
                entity.ToTable("ScreeningCVHistory");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.ScreeningCvhistory)
                    .HasForeignKey(d => d.JobApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScreeningCVHistory_JobApplication");

                entity.HasOne(d => d.ModifiedUserNavigation)
                    .WithMany(p => p.ScreeningCvhistory)
                    .HasForeignKey(d => d.ModifiedUser)
                    .HasConstraintName("FK_ScreeningCVHistory_Employee");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.SkillName).HasMaxLength(100);
            });

            modelBuilder.Entity<TagInfo>(entity =>
            {
                entity.HasKey(e => e.TagId);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TagName).IsRequired();
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Template_Company");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.HasKey(e => e.TmsId);

                entity.Property(e => e.TmsId).HasColumnName("TmsID");

                entity.Property(e => e.TmsCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TmsDate).HasColumnType("date");

                entity.Property(e => e.TmsMachineCode)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TimeSheetReport>(entity =>
            {
                entity.HasKey(e => e.TmsRptId);

                entity.Property(e => e.TmsRptId).HasColumnName("TmsRptID");

                entity.Property(e => e.DateOut).HasColumnType("date");

                entity.Property(e => e.TmsRptCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TmsRptDate).HasColumnType("date");

                entity.Property(e => e.TmsRptMachineCode).HasMaxLength(10);
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.HasKey(e => e.WkeId);

                entity.Property(e => e.WkeId).HasColumnName("WkeID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.WkeEmplId).HasColumnName("WkeEmplID");

                entity.Property(e => e.WkePstId).HasColumnName("WkePstID");

                entity.HasOne(d => d.WkeEmpl)
                    .WithMany(p => p.WorkExperience)
                    .HasForeignKey(d => d.WkeEmplId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkExperience_Employee");

                entity.HasOne(d => d.WkePst)
                    .WithMany(p => p.WorkExperience)
                    .HasForeignKey(d => d.WkePstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkExperience_Position");
            });

            modelBuilder.Entity<WorkingGroup>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupEndDate).HasColumnType("datetime");

                entity.Property(e => e.GroupStartDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasColumnType("datetime");
            });

            modelBuilder.Entity<WorkingGroupEmployee>(entity =>
            {
                entity.HasKey(e => new { e.WorkingGroupId, e.EmployeeId });

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkingGroupEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingGroupEmployee_Employee");

                entity.HasOne(d => d.WorkingGroup)
                    .WithMany(p => p.WorkingGroupEmployee)
                    .HasForeignKey(d => d.WorkingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingGroupEmployee_WorkingGroup");
            });
        }
    }
}
