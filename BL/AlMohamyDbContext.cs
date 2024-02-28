using System;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BL
{
    public partial class AlMohamyDbContext : IdentityDbContext<ApplicationUser>
    {
        public AlMohamyDbContext()
        {
        }

        public AlMohamyDbContext(DbContextOptions<AlMohamyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAboutApp> TbAboutApps { get; set; }
        public virtual DbSet<TbApprovedOffice> TbApprovedOffices { get; set; }
        public virtual DbSet<TbCharge> TbCharges { get; set; }
        public virtual DbSet<TbComplainsAndSuggestion> TbComplainsAndSuggestions { get; set; }
        public virtual DbSet<TbConsultingEstablish> TbConsultingEstablishes { get; set; }
        public virtual DbSet<TbConsultingType> TbConsultingTypes { get; set; }
        public virtual DbSet<TbDocumentationOfContract> TbDocumentationOfContracts { get; set; }
        public virtual DbSet<TbEvaluation> TbEvaluations { get; set; }
        public virtual DbSet<TbMainConsulting> TbMainConsultings { get; set; }
        public virtual DbSet<TbNotification> TbNotifications { get; set; }
        public virtual DbSet<TbOffer> TbOffers { get; set; }
        public virtual DbSet<TbPoliciesAndPrivacy> TbPoliciesAndPrivacies { get; set; }
        public virtual DbSet<TbPromocode> TbPromocodes { get; set; }
        public virtual DbSet<TbSetting> TbSettings { get; set; }
        public virtual DbSet<TbSubMainConsulting> TbSubMainConsultings { get; set; }
        public virtual DbSet<TbTermAndCondition> TbTermAndConditions { get; set; }
        public virtual DbSet<TbRealTimeNotifcation> TbRealTimeNotifcations { get; set; }

        public virtual DbSet<TbLawyerPeriodCostConsult> TbLawyerPeriodCostConsults { get; set; }

        public virtual DbSet<TbLogHistory> TbLogHistories { get; set; }

        public virtual DbSet<TbPaymentGates> TbPaymentGatess { get; set; }

        public virtual DbSet<TbChat> TbChats { get; set; }

        public virtual DbSet<TbLawyerAppintments> TbLawyerAppintmentss { get; set; }

        public virtual DbSet<TbEvaluationApprovedOffice> TbEvaluationApprovedOffices { get; set; }

        public virtual DbSet<TbCity> TbCities { get; set; }

        public virtual DbSet<TbArea> TbAreas { get; set; }

        public virtual DbSet<TbServices> TbServicess { get; set; }

        public virtual DbSet<TbLawyersMainConsultings> TbLawyersMainConsultingss { get; set; }
        public virtual DbSet<VwChargeDeduct> VwChargeDeducts { get; set; }

        public virtual DbSet<TbActivityLog> TbActivityLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbAboutApp>(entity =>
            {
                entity.HasKey(e => e.AboutAppId);

                entity.ToTable("TbAboutApp");

                entity.Property(e => e.AboutAppId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AboutAppDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.AboutAppForWhom).HasMaxLength(200);

                entity.Property(e => e.AboutAppTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime");
                    

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TbActivityLog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TbActivityLog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Action).HasColumnType("nvarchar(MAX)");

              

              

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

               
            });

            modelBuilder.Entity<VwChargeDeduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwChargeDeduct");


            });

            modelBuilder.Entity<TbServices>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ServiceName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

              
            });

            modelBuilder.Entity<TbArea>(entity =>
            {
                entity.HasKey(e => e.AreaId);

                entity.Property(e => e.AreaId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AreaName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TbAreas)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TbAreas_TbCities");
            });

            modelBuilder.Entity<TbCity>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CityName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbRealTimeNotifcation>(entity =>
            {
                entity.HasKey(e => e.RealTimeNotifcationId);

                entity.Property(e => e.RealTimeNotifcationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NotificationType).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.NotificationSyntax).HasColumnType("nvarchar(MAX)");



                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");


            });

            modelBuilder.Entity<TbApprovedOffice>(entity =>
            {
                entity.HasKey(e => e.ApprovedOfficeId);

                entity.ToTable("TbApprovedOffice");

                entity.Property(e => e.ApprovedOfficeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApprovalStatus).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeLicenseDoc).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeLogo).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.AreaName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CityName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluationNoOfStatrs).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluationNumerical).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeShortDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbCharge>(entity =>
            {
                entity.HasKey(e => e.ChargeId);

                entity.ToTable("TbCharge");

                entity.Property(e => e.ChargeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChargeTypeSender).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ChargeValue).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.IdSender).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbComplainsAndSuggestion>(entity =>
            {
                entity.HasKey(e => e.ComplaintsAndSuggestionsId);

                entity.ToTable("TbComplainsAndSuggestion");

                entity.Property(e => e.ComplaintsAndSuggestionsId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ComplaintsAndSuggestionsText).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Email).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Idd).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Name).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbConsultingEstablish>(entity =>
            {
                entity.HasKey(e => e.ConsultingId);

                entity.ToTable("TbConsultingEstablish");

                entity.Property(e => e.ConsultingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AreaName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CityName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingDateAndTime).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingPeriod).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingPeriodCost).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingType).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingTypeId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingVatvalue)
                    .HasMaxLength(200)
                    .HasColumnName("ConsultingVATValue");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.DelegationRejectionCause).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DelegationReplyBack).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DelegationValueApproved).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DelegationValueSentFromLawyer).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DelegationValueSentFromUser).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.IsDelegationAsked).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.MainConsultingId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.MainConsultingName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.NoOfOffers).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PromocodeDiscountValue).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestAudio).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestDocument).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestNo).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestStatus).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestText).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RequestType).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SubConsultingId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SubConsultingName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TheConsultingPaidValue).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TheTotal).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TimeRemainingForConsultingToStart).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TransactionType).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UserFamilyName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UserFirstName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UserId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UserImage).HasColumnType("nvarchar(MAX)");
              
                  
            });

            modelBuilder.Entity<TbLogHistory>(entity =>
            {
                entity.HasKey(e => e.LogHistoryId);

                entity.ToTable("TbLogHistory");

                entity.Property(e => e.LogHistoryId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.LoggedUserName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TbLawyerPeriodCostConsult>(entity =>
            {
                entity.HasKey(e => e.LawyerPeriodCostConsultId);

                entity.ToTable("TbLawyerPeriodCostConsult");

                entity.Property(e => e.LawyerPeriodCostConsultId).HasDefaultValueSql("(newid())");

             

                entity.Property(e => e.ConsultingPeriod).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingPeriodCost).HasColumnType("nvarchar(MAX)");



                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

             

                entity.Property(e => e.LawyerId).HasColumnType("nvarchar(MAX)");



                entity.Property(e => e.LawyerName).HasColumnType("nvarchar(MAX)");


                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");



                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

             
            });

            modelBuilder.Entity<TbConsultingType>(entity =>
            {
                entity.HasKey(e => e.ConsultingTypeId);

                entity.ToTable("TbConsultingType");

                entity.Property(e => e.ConsultingTypeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConsultingTypeDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ConsultingTypeTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbDocumentationOfContract>(entity =>
            {
                entity.HasKey(e => e.DocumentationOfContractId);

                entity.ToTable("TbDocumentationOfContract");

                entity.Property(e => e.DocumentationOfContractId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.DocumentationOfContractCost).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DocumentationOfContractDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DocumentationOfContractImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.DocumentationOfContractTilte).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbEvaluation>(entity =>
            {
                entity.HasKey(e => e.EvaluationId);

                entity.ToTable("TbEvaluation");

                entity.Property(e => e.EvaluationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConsultationServiceId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.EvaluaterId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluaterImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluationText).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.StartsNo).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ToBeEvaluatedId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ToBeEvaluatedImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });



            modelBuilder.Entity<TbEvaluationApprovedOffice>(entity =>
            {
                entity.HasKey(e => e.EvaluationApprovedOfficeId);

                entity.ToTable("TbEvaluationApprovedOffice");

                entity.Property(e => e.EvaluationApprovedOfficeId).HasDefaultValueSql("(newid())");

               

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.EvaluaterId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluaterName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluaterImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.EvaluationApprovedOfficeText).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.StartsNo).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ApprovedOfficeLogo).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbMainConsulting>(entity =>
            {
                entity.HasKey(e => e.MainConsultingId);

                entity.ToTable("TbMainConsulting");

                entity.Property(e => e.MainConsultingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Consulting30MinutesCost).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Consulting60MinutesCost).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Consulting90MinutesCost).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.MainConsultingImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.MainConsultingTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TbPaymentGates>(entity =>
            {
                entity.HasKey(e => e.PaymentGatesId);

                entity.ToTable("TbPaymentGates");

                entity.Property(e => e.PaymentGatesId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.PaymentGateTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PaymentGateImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ActivationStatus).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

            

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });



            modelBuilder.Entity<TbChat>(entity =>
            {
                entity.HasKey(e => e.ChatId);

                entity.ToTable("TbChat");

                entity.Property(e => e.ChatId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConsultingId).HasColumnType("uniqueidentifier");

                entity.Property(e => e.SenderId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SenderFirstName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SenderEmail).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SenderText).HasColumnType("nvarchar(max)");

                entity.Property(e => e.SenderAudio).HasColumnType("nvarchar(max)");

                entity.Property(e => e.SenderDocument).HasColumnType("nvarchar(max)");

                entity.Property(e => e.SenderUserType).HasColumnType("nvarchar(MAX)");


                entity.Property(e => e.RecieverId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.RecieverFirstName).HasColumnType("nvarchar(MAX)");


                entity.Property(e => e.RecieverEmail).HasColumnType("nvarchar(MAX)");


                entity.Property(e => e.RecieverUserType).HasColumnType("nvarchar(MAX)");


                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");



                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.ToTable("TbNotification");

                entity.Property(e => e.NotificationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.NotificationType).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SenderId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SenderName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Text).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ToWhomId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ToWhomName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOffer>(entity =>
            {
                entity.HasKey(e => e.OfferId);

                entity.ToTable("TbOffer");

                entity.Property(e => e.OfferId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(getdate())")
                    .IsFixedLength(true);

                entity.Property(e => e.CurrentState)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((1))")
                    .IsFixedLength(true);

                entity.Property(e => e.LawyerEvalNoStarts).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerName).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerShortDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyersEvalNumerical).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyersExperinceYears).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Notes)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.OfferEndDate).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.OfferStatus).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UserName).HasColumnType("nvarchar(MAX)");
            });

            modelBuilder.Entity<TbPoliciesAndPrivacy>(entity =>
            {
                entity.HasKey(e => e.PoliciesAndPrivacyId);

                entity.ToTable("TbPoliciesAndPrivacy");

                entity.Property(e => e.PoliciesAndPrivacyId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PoliciesAndPrivacyDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PoliciesAndPrivacyForWhom).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PoliciesAndPrivacyTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbPromocode>(entity =>
            {
                entity.HasKey(e => e.PromocodeId);

                entity.ToTable("TbPromocode");

                entity.Property(e => e.PromocodeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PromocodeDiscountPercent).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.PromocodeTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });



            modelBuilder.Entity<TbLawyerAppintments>(entity =>
            {
                entity.HasKey(e => e.LawyerAppointmentId);

                entity.ToTable("TbLawyerAppintments");

                entity.Property(e => e.LawyerAppointmentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.LawyerId).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.WeekDay).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.FromHour).HasColumnType("float");

                entity.Property(e => e.MorEveFrst).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.ToHour).HasColumnType("float");

                entity.Property(e => e.MorEveScond).HasColumnType("nvarchar(MAX)");



                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });




            modelBuilder.Entity<TbSetting>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("TbSetting");

                entity.Property(e => e.SettingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AppProfitPercent).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.OffersValidityDays).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValueAddedTax).HasColumnType("nvarchar(MAX)");
            });

            modelBuilder.Entity<TbSubMainConsulting>(entity =>
            {
                entity.HasKey(e => e.SubMainConsultingId);

                entity.ToTable("TbSubMainConsulting");

                entity.Property(e => e.SubMainConsultingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SubMainConsulting)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SubMainConsultingDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SubMainConsultingImage).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.SubMainConsultingTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbTermAndCondition>(entity =>
            {
                entity.HasKey(e => e.TermsAndConditionsId);

                entity.ToTable("TbTermAndCondition");

                entity.Property(e => e.TermsAndConditionsId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TermsAndConditionsDescription).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TermsAndConditionsForWhom).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.TermsAndConditionsTitle).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedBy).HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
