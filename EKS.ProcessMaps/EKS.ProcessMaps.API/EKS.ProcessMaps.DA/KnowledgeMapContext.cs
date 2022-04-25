namespace EKS.ProcessMaps.DA
{
    using EKS.ProcessMaps.Entities;
    using Microsoft.EntityFrameworkCore;

    public partial class KnowledgeMapContext : DbContext
    {
        public KnowledgeMapContext()
        {
        }

        public KnowledgeMapContext(DbContextOptions<KnowledgeMapContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityBlocks> Activities { get; set; }
        public virtual DbSet<ActivityConnections> ActivityConnections { get; set; }
        public virtual DbSet<SwimLanes> ActivityGroups { get; set; }
        public virtual DbSet<ActivityBlockTypes> ActivityTypes { get; set; }
        public virtual DbSet<ProcessMapMeta> ProcessMapMeta { get; set; }
        public virtual DbSet<ProcessMap> ProcessMaps { get; set; }

        public virtual DbSet<MasterDisciplineCode> MasterDisciplineCodes { get; set; }

        public virtual DbSet<UserPreferences> UserPreferences { get; set; }

        public virtual DbSet<Phases> Phases { get; set; }

        public virtual DbSet<ActivityPage> ActivityPage { get; set; }

        public virtual DbSet<ContentPhases> ContentPhases { get; set; }

        public virtual DbSet<ContentExportCompliances> ContentExportCompliances { get; set; }

        public virtual DbSet<ContentTags> ContentTags { get; set; }

        public virtual DbSet<ContentInformation> ContentInformation { get; set; }

        public virtual DbSet<ContentType> ContentType { get; set; }

        public virtual DbSet<PrivateAssets> PrivateAssets { get; set; }

        public virtual DbSet<KpacksMap> KpacksMap { get; set; }

        public virtual DbSet<KnowledgePack> KnowledgePack { get; set; }
        public virtual DbSet<KnowledgePackContent> KnowledgePackContent { get; set; }
        public virtual DbSet<KnowledgePackLessonLearned> KnowledgePackLessonLearned { get; set; }
        public virtual DbSet<KnowledgePackNatureOfChange> KnowledgePackNatureOfChange { get; set; }
        public virtual DbSet<KnowledgePackPhysics> KnowledgePackPhysics { get; set; }
        public virtual DbSet<KnowledgePackPurpose> KnowledgePackPurpose { get; set; }

        /// ActivityContainer
        public virtual DbSet<ActivityContainer> ActivityContainer { get; set; }
        public virtual DbSet<KnowledgeAsset> KnowledgeAssets { get; set; }
        public virtual DbSet<AssetTypes> AssetTypes { get; set; }
        public virtual DbSet<ConfidentialitiesRef> ConfidentialitiesRef { get; set; }
        public virtual DbSet<RevisionTypes> RevisionTypes { get; set; }
        public virtual DbSet<AssetControllingPrograms> AssetControllingPrograms { get; set; }
        public virtual DbSet<ControllingProgram> ControllingPrograms { get; set; }
        public virtual DbSet<AssetExportCompliances> AssetExportCompliances { get; set; }
        public virtual DbSet<AssetKeywords> AssetKeywords { get; set; }
        public virtual DbSet<AssetParts> AssetParts { get; set; }
        public virtual DbSet<AssetPhases> AssetPhases { get; set; }
        public virtual DbSet<AssetTags> AssetTags { get; set; }
        public virtual DbSet<AssetUsers> AssetUsers { get; set; }
        public virtual DbSet<ToDoTask> ToDoTask { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        /// AssetStatus
        public virtual DbSet<AssetStatus> AssetStatus { get; set; }

        /// MigratedContent
        public virtual DbSet<MigratedContent> MigratedContent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=pweswdb1.database.windows.net,1433;database=EKSMasterDB;User ID=pwesw1;password=pwesw@2020;Trusted_Connection=False;Encrypt=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetStatus>(entity =>
            {
                entity.HasKey(e => e.AssetStatusId)
                    .HasName("ConfidentialityId");

                entity.ToTable("AssetStatus");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Description).HasColumnName("Description");

                entity.Property(e => e.Version).HasColumnName("Version");

                entity.Property(e => e.EffectiveFrom).HasColumnName("EffectiveFrom");

                entity.Property(e => e.EffectiveTo).HasColumnName("EffectiveTo");

                entity.Property(e => e.CreatedDateTime).HasColumnName("CreatedDateTime");

                entity.Property(e => e.CreatedUser).HasColumnName("CreatedUser");

                entity.Property(e => e.LastUpdateDateTime).HasColumnName("LastUpdateDateTime");

                entity.Property(e => e.LastUpdateUser).HasColumnName("LastUpdateUser");
            });

            modelBuilder.Entity<ActivityBlocks>(entity =>
            {
                entity.ToTable("ActivityBlocks", "MAP");

                entity.Property(e => e.Id).HasColumnName("ActivityBlockId");

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .HasConstraintName("FK_ActivityBlock_Activity_Types");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ActivityBlocks_Phases");

                entity.HasOne(d => d.ProcessMap)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.ProcessMapId)
                    .HasConstraintName("FK_ActivityBlock_ProcessMap");

                entity.HasOne(d => d.SwimLane)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.SwimLaneId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ActivityBlock_SwimLane");
            });

            modelBuilder.Entity<ActivityConnections>(entity =>
            {
                entity.ToTable("ACTIVITY_CONNECTIONS", "MAP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BackgroundColor).HasMaxLength(256);

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.CaptionEnd).HasMaxLength(256);

                entity.Property(e => e.CaptionMiddle).HasMaxLength(256);

                entity.Property(e => e.CaptionStart).HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ActivityBlock)
                    .WithMany(p => p.ActivityConnections)
                    .HasForeignKey(d => d.ActivityBlockId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ACTIVITY_CONNECTIONS_ActivityBlock");
            });

            modelBuilder.Entity<SwimLanes>(entity =>
            {
                entity.ToTable("SwimLanes", "MAP");

                entity.Property(e => e.Id).HasColumnName("SwimLaneId");

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Size).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.SwimLanes)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_SwimLanes_Disciplines");

                entity.HasOne(d => d.ProcessMap)
                    .WithMany(p => p.SwimLanes)
                    .HasForeignKey(d => d.ProcessMapId)
                    .HasConstraintName("FK_SwimLanes_ProcessMap");
            });

            modelBuilder.Entity<ActivityBlockTypes>(entity =>
            {
                entity.ToTable("ActivityBlockTypes", "MAP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ProcessMapMeta>(entity =>
            {
                entity.ToTable("PROCESS_MAP_META", "MAP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedbyUserid)
                    .HasColumnName("createdby_userid")
                    .HasMaxLength(50);

                entity.Property(e => e.Createdon)
                    .HasColumnName("createdon")
                    .HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedbyUserid)
                    .HasColumnName("modifiedby_userid")
                    .HasMaxLength(50);

                entity.Property(e => e.Modifiedon)
                    .HasColumnName("modifiedon")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProcessMapId).HasColumnName("process_map_id");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(255);

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.ProcessMap)
                    .WithMany(p => p.ProcessMapMeta)
                    .HasForeignKey(d => d.ProcessMapId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PROCESS_MAP_META_ProcessMap");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("Disciplines", "ref");

                entity.HasKey(e => e.DisciplineId)
               .HasName("PK_Disciplines");

                entity.Property(e => e.DisciplineId).HasColumnName("DisciplineId");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
               .IsRequired()
               .HasMaxLength(128)
               .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Discipline1)
               .IsRequired()
               .HasMaxLength(128);

                entity.Property(e => e.Discipline2)
               .HasMaxLength(128);

                entity.Property(e => e.Discipline3).HasMaxLength(128);

                entity.Property(e => e.Discipline4).HasMaxLength(128);

                entity.Property(e => e.DisciplineCode)
               .HasMaxLength(5);

                entity.Property(e => e.EffectiveFrom)
               .HasColumnType("date")
               .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
               .HasColumnType("date")
               .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
               .IsRequired()
               .HasMaxLength(128)
               .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ProcessMap>(entity =>
            {
                entity.ToTable("ProcessMap", "MAP");

                entity.Property(e => e.Id).HasColumnName("ProcessMapId");

                entity.Property(e => e.AssetStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AssetTypeId).HasDefaultValueSql("((4))");

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.ClassificationDate).HasColumnType("date");
                
                entity.Property(e => e.ClockId).HasMaxLength(50);

                entity.Property(e => e.ContentId).HasMaxLength(255);

                entity.Property(e => e.Gen2ContentId).HasMaxLength(14);

                entity.Property(e => e.ContentOwnerId).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CustomId).HasMaxLength(255);

                entity.Property(e => e.DisciplineCode).HasMaxLength(5);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.ExportPdfurl)
                    .HasColumnName("ExportPDFUrl")
                    .HasMaxLength(250);

                entity.Property(e => e.Keywords).HasMaxLength(255);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ProcessInstId).HasMaxLength(255);

                entity.Property(e => e.SourceFileUrl).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Tpmdate)
                    .HasColumnName("TPMDate")
                    .HasColumnType("date");

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_ProcessMap_EKS_Discipline");

                entity.HasOne(d => d.ControllingProgram)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.ControllingProgramId)
                    .HasConstraintName("FK_ActivityPage_ControllingPrograms");

                /*
                entity.HasOne(d => d.ApprovalRequirement)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.ApprovalRequirementId)
                    .HasConstraintName("FK_ProcessMap_ApprovalRequirements");

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.AssetStatusId)
                    .HasConstraintName("FK_ProcessMap_AssetStatus");

                entity.HasOne(d => d.AssetType)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.AssetTypeId)
                    .HasConstraintName("FK_ProcessMap_EKS_ContentType");

                entity.HasOne(d => d.ConfidentialityNavigation)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.ConfidentialityId)
                    .HasConstraintName("FK_ProcessMap_Confidentialities");

                entity.HasOne(d => d.ExportAuthority)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.ExportAuthorityId)
                    .HasConstraintName("FK_ProcessMap_ExportAuthorities");

                entity.HasOne(d => d.RevisionType)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.RevisionTypeId)
                    .HasConstraintName("FK_ProcessMap_RevisionTypes");

                entity.HasOne(d => d.Usclassification)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.UsclassificationId)
                    .HasConstraintName("FK_ProcessMap_USClassifications");

                entity.HasOne(d => d.Usjurisdiction)
                    .WithMany(p => p.ProcessMap)
                    .HasForeignKey(d => d.UsjurisdictionId)
                    .HasConstraintName("FK_ProcessMap_USJurisdictions");*/
            });

            modelBuilder.Entity<MasterDisciplineCode>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ESW_Master_Discipline_Code");

                entity.ToTable("EKS_Master_Discipline_Code");

                entity.Property(e => e.Id).HasColumnName("PK_Master_Discipline_Code_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatorClockId)
                    .HasColumnName("CreatorClockID")
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("Discipline_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifierClockId)
                    .HasColumnName("ModifierClockID")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<UserPreferences>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("UserPreferencesId");
                entity.Property(e => e.Tiles).HasMaxLength(50);
            });

            modelBuilder.Entity<Phases>(entity =>
            {
                entity.ToTable("Phases", "MAP");

                entity.Property(e => e.Id).HasColumnName("PhaseId");

                entity.Property(e => e.Caption).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ProcessMap)
                    .WithMany(p => p.Phases)
                    .HasForeignKey(d => d.ProcessMapId)
                    .HasConstraintName("FK_Phases_ProcessMap");
            });

            modelBuilder.Entity<ActivityPage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ActivityPage");

                entity.ToTable("ActivityPage");

                entity.Property(e => e.Id).HasColumnName("ActivityPageId");

                entity.Property(e => e.AssetStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AssetTypeId).HasDefaultValueSql("((4))");

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.ClassificationDate).HasColumnType("date");

                entity.Property(e => e.ContentId).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CustomId).HasMaxLength(255);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.Keywords).HasMaxLength(255);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ProcessInstId).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.TPMDate)
                    .HasColumnType("date")
                    .HasColumnName("TPMDate");

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.AssetStatus)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.AssetStatusId)
                //    .HasConstraintName("FK_ActivityPage_AssetStatus");

                //entity.HasOne(d => d.AssetType)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.AssetTypeId)
                //    .HasConstraintName("FK_ActivityPage_EKS_ContentType");

                //entity.HasOne(d => d.ConfidentialityNavigation)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.ConfidentialityId)
                //    .HasConstraintName("FK_CriteriaGroup_Confidentialities");

                //entity.HasOne(d => d.DisciplineCode)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.DisciplineCodeId)
                //    .HasConstraintName("FK_ActivityPage_EKS_Discipline_Code");

                //entity.HasOne(d => d.Discipline)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.DisciplineId)
                //    .HasConstraintName("FK_ActivityPage_EKS_Discipline");

                //entity.HasOne(d => d.RevisionTypes)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.RevisionTypeId)
                //    .HasConstraintName("FK_ActivityPage_RevisionTypes");

                //entity.HasOne(d => d.SubDiscipline)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.SubDisciplineId)
                //    .HasConstraintName("FK_ActivityPage_EKS_Sub_Discipline");

                //entity.HasOne(d => d.SubSubDiscipline)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.SubSubDisciplineId)
                //    .HasConstraintName("FK_ActivityPage_EKS_Sub_Sub_Discipline");

                //entity.HasOne(d => d.SubSubSubDiscipline)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.SubSubSubDisciplineId)
                //    .HasConstraintName("FK_ActivityPage_EKS_Sub_Sub_Sub_Discipline");

                //entity.HasOne(d => d.Usclassification)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.UsclassificationId)
                //    .HasConstraintName("FK_ActivityPage_USClassifications");

                //entity.HasOne(d => d.Usjurisdiction)
                //    .WithMany(p => p.ActivityPage)
                //    .HasForeignKey(d => d.UsjurisdictionId)
                //    .HasConstraintName("FK_ActivityPage_USJurisdictions");
            });

            modelBuilder.Entity<ContentPhases>(entity =>
            {               
                entity.HasKey(e => e.ContentPhaseId)
                    .HasName("PK_ContentPhases");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.ContentPhases)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentPhases_Phases");
            });

            modelBuilder.Entity<ContentExportCompliances>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ContentExportComplianceId");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
                /*
                entity.HasOne(d => d.ExportCompliance)
                    .WithMany(p => p.ContentExportCompliances)
                    .HasForeignKey(d => d.ExportComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentExportCompliances_ExportCompliances");*/
            });

            modelBuilder.Entity<ContentTags>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ContentTagsId");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                /*
                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ContentTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentTags_Tags");*/
            });

            modelBuilder.Entity<ContentInformation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ContentInformationId");

                entity.Property(e => e.ContentId).HasMaxLength(256);

                entity.Property(e => e.ContentType).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.HasKey(e => e.ContentTypeId)
                   .HasName("PK_DocumentType");

                entity.ToTable("EKS_ContentType");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatorClockID)
                    .HasColumnName("CreatorClockID")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifierClockID)
                    .HasColumnName("ModifierClockID")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Phase>(entity =>
            {
                entity.ToTable("Phase");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Phase1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Phase");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<NatureOfChange>(entity =>
            {
                entity.ToTable("NatureOfChange");

                entity.Property(e => e.ContentId).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.NocdateTime).HasColumnName("NOCDateTime");
            });

            modelBuilder.Entity<PrivateAssets>(entity =>
            {
                entity.ToTable("PrivateAssets", "dbo");

                entity.HasKey(e => e.ContentAssetId);

                entity.Property(e => e.ContentAssetId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<KpacksMap>(entity =>  
            {
                entity.Property(e => e.KpacksMapId).HasColumnName("KPacksMapId");

                entity.Property(e => e.ContentAssetId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ParentContentAssetId).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<KnowledgePack>(entity =>
            {
                entity.ToTable("KnowledgePack", "ka");

                entity.Property(e => e.ApplicabilityCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AssetTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.ClassificationDate).HasColumnType("date");

                entity.Property(e => e.ContentId).HasMaxLength(256);

                entity.Property(e => e.ContentOwnerId).HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CustomId).HasMaxLength(256);

                entity.Property(e => e.DisciplineCode).HasMaxLength(5);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.Keywords).HasMaxLength(255);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.LevelCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProcessInstId).HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Tpmdate)
                    .HasColumnName("TPMDate")
                    .HasColumnType("date");

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.KnowledgePack)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePack_EKS_Discipline");

                entity.HasOne(d => d.AssetType)
                    .WithMany(p => p.KnowledgePack)
                    .HasForeignKey(d => d.AssetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePack_EKS_ContentType");
            });

            modelBuilder.Entity<KnowledgePackContent>(entity =>
            {
                entity.ToTable("KnowledgePackContent", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.LayoutType).HasMaxLength(100);

                entity.Property(e => e.TabCode).HasMaxLength(128);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgePack)
                    .WithMany(p => p.KnowledgePackContent)
                    .HasForeignKey(d => d.KnowledgePackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePackContent_KnowledgePack");
            });

            modelBuilder.Entity<KnowledgePackLessonLearned>(entity =>
            {
                entity.ToTable("KnowledgePackLessonLearned", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.FrameType)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Heading)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Layout)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Links)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgePack)
                    .WithMany(p => p.KnowledgePackLessonLearned)
                    .HasForeignKey(d => d.KnowledgePackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePackLessonLearned_KnowledgePack");
            });

            modelBuilder.Entity<KnowledgePackNatureOfChange>(entity =>
            {
                entity.ToTable("KnowledgePackNatureOfChange", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgePack)
                    .WithMany(p => p.KnowledgePackNatureOfChange)
                    .HasForeignKey(d => d.KnowledgePackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePackNatureOfChange_KnowledgePack");
            });

            modelBuilder.Entity<KnowledgePackPhysics>(entity =>
            {
                entity.ToTable("KnowledgePackPhysics", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.FrameType).HasMaxLength(64);

                entity.Property(e => e.Heading).HasMaxLength(128);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Layout).HasMaxLength(64);

                entity.Property(e => e.Links).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgePack)
                    .WithMany(p => p.KnowledgePackPhysics)
                    .HasForeignKey(d => d.KnowledgePackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePackPhysics_KnowledgePack");
            });

            modelBuilder.Entity<KnowledgePackPurpose>(entity =>
            {
                entity.ToTable("KnowledgePackPurpose", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("date");

                entity.Property(e => e.FrameType)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Heading)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Layout)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Links)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgePack)
                    .WithMany(p => p.KnowledgePackPurpose)
                    .HasForeignKey(d => d.KnowledgePackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePackPurpose_KnowledgePack");
            });

            // Mapping for Activity Container
            modelBuilder.Entity<ActivityContainer>(entity =>
            {
                entity.HasKey(e => e.ActivityContainerId)
                    .HasName("ActivityContainerId");

                entity.Property(e => e.ContentItemId).HasColumnName("ContentId");

            });

            modelBuilder.Entity<KnowledgeAsset>(entity =>
            {
                entity.HasKey(e => e.KnowledgeAssetId)
                    .HasName("PK_KnowledgeAsset");

                entity.ToTable("KnowledgeAssets", "ka");

                entity.HasIndex(e => new { e.DisciplineCode, e.AssetTypeCode, e.ContentNumber, e.EffectiveTo })
                    .HasName("UI_KnowledgeAssets_ContentId")
                    .IsUnique();

                entity.Property(e => e.AssetTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClassificationDate).HasColumnType("date");

                entity.Property(e => e.ContentId)
                    .HasMaxLength(12)
                    .HasComputedColumnSql("(CONVERT([nvarchar](12),((([DisciplineCode]+'-')+[AssetTypeCode])+'-')+format([ContentNumber],'d'+CONVERT([nchar](1),(5)+sign([ContentNumber]-(10000))))))");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DisciplineCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Tpmdate)
                    .HasColumnName("TPMDate")
                    .HasColumnType("date");

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
                
                entity.Property(e => e.ProgramControlled).HasColumnName("ProgramControlledInd").IsRequired();

                entity.Property(e => e.Outsourcable).HasColumnName("OutsourcableInd").IsRequired();
                
                entity.Property(e => e.ApprovalRequirementId).IsRequired(false);
                
                entity.Property(e => e.RevisionTypeId).IsRequired();

                entity.HasOne(d => d.AssetTypeCodeNavigation)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.AssetTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeAssets_AssetTypes");

                entity.HasOne(d => d.Confidentiality)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.ConfidentialityId)
                    .HasConstraintName("FK_KnowledgeAssets_Confidentialities");

                entity.HasOne(d => d.RevisionType)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.RevisionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeAssets_RevisionTypes");
            });

            modelBuilder.Entity<AssetTypes>(entity =>
            {
                entity.HasKey(e => e.AssetTypeCode);

                entity.ToTable("AssetTypes", "ref");

                entity.Property(e => e.AssetTypeCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ConfidentialitiesRef>(entity =>
            {
                entity.HasKey(e => e.ConfidentialityId);

                entity.ToTable("Confidentialities", "ref");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RevisionTypes>(entity =>
            {
                entity.HasKey(e => e.RevisionTypeId);

                entity.ToTable("RevisionTypes", "ref");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.RevisionTypeCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });
            modelBuilder.Entity<AssetControllingPrograms>(entity =>
            {
                entity.HasKey(e => e.AssetControllingProgramId);

                entity.ToTable("AssetControllingPrograms", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ControllingProgram)
                    .WithMany(p => p.AssetControllingPrograms)
                    .HasForeignKey(d => d.ControllingProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetControllingPrograms_ControllingPrograms");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetControllingPrograms)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetControllingPrograms_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetExportCompliances>(entity =>
            {
                entity.HasKey(e => e.AssetExportComplianceId);

                entity.ToTable("AssetExportCompliances", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetExportCompliances)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetExportCompliances_KnowledgeAssets");
            });
            modelBuilder.Entity<AssetKeywords>(entity =>
            {
                entity.HasKey(e => e.AssetKeywordId);

                entity.ToTable("AssetKeywords", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetKeywords)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetKeywords_KnowledgeAssets");
            });
            modelBuilder.Entity<AssetParts>(entity =>
            {
                entity.HasKey(e => e.AssetPartId);

                entity.ToTable("AssetParts", "ka");

                entity.Property(e => e.AssetPartTypeCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Link).HasMaxLength(1024);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetParts)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetParts_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetPhases>(entity =>
            {
                entity.HasKey(e => e.AssetPhaseId);

                entity.ToTable("AssetPhases", "ka");

                entity.Property(e => e.AssetPhaseId).ValueGeneratedOnAdd();

                entity.Property(e => e.AssetPhaseCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetPhases)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetPhases_KnowledgeAssets");
            });
            modelBuilder.Entity<AssetTags>(entity =>
            {
                entity.HasKey(e => e.AssetTagId);

                entity.ToTable("AssetTags", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetTags)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTags_KnowledgeAssets");
            });
            modelBuilder.Entity<AssetUsers>(entity =>
            {
                entity.ToTable("AssetUsers", "ka");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetUsers)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetUsers_KnowledgeAssets");
            });

            modelBuilder.Entity<ControllingProgram>(entity =>
            {
                //entity.HasKey(e => e.ControllingProgramId);

                entity.ToTable("ControllingPrograms", "ref");

                entity.Property(e => e.Id).HasColumnName("ControllingProgramId");

                entity.ToTable("ControllingPrograms", "ref");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Classifiers>(entity =>
            {
                entity.HasKey(e => e.ClassifiersId)
                    .HasName("ClassifiersId");

                entity.ToTable("Classifiers");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Description).HasColumnName("Description");

                entity.Property(e => e.Version).HasColumnName("Version");

                entity.Property(e => e.EffectiveFrom).HasColumnName("EffectiveFrom");

                entity.Property(e => e.EffectiveTo).HasColumnName("EffectiveTo");

                entity.Property(e => e.CreatedDateTime).HasColumnName("CreatedDateTime");

                entity.Property(e => e.CreatedUser).HasColumnName("CreatedUser");

                entity.Property(e => e.LastUpdateDateTime).HasColumnName("LastUpdateDateTime");

                entity.Property(e => e.LastUpdateUser).HasColumnName("LastUpdateUser");

                entity.Property(e => e.BusinessUnit).HasColumnName("BusinessUnit");

                entity.Property(e => e.FunctionalArea).HasColumnName("FunctionalArea");

                entity.Property(e => e.ClockId).HasMaxLength(50);
            });

            modelBuilder.Entity<ToDoTask>(entity =>
            {
                entity.HasKey(e => e.ToDoId)
                    .HasName("ToDoId");

                entity.ToTable("EKS_Task_ToDo");

                entity.Property(e => e.ToDoId).HasColumnName("ToDoId");

                entity.Property(e => e.ContentTypeId).HasColumnName("ContentTypeId");

                entity.Property(e => e.ContentId).HasColumnName("ContentId");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Comments).HasColumnName("Comments");

                entity.Property(e => e.IsDone).HasColumnName("IsDone");

                entity.Property(e => e.Url).HasColumnName("Url");

                entity.Property(e => e.ContentStatus).HasColumnName("ContentStatus");

                entity.Property(e => e.ActionId).HasColumnName("ActionId");

                entity.Property(e => e.DueDate).HasColumnName("DueDate");

                entity.Property(e => e.RequestedByUserId).HasColumnName("RequestedByUserId");

                entity.Property(e => e.TaskRequestedDate).HasColumnName("TaskRequestedDate");

                entity.Property(e => e.AssignedToUserID).HasColumnName("AssignedToUserID");

                entity.Property(e => e.TaskCompletedDate).HasColumnName("TaskCompletedDate");

                entity.Property(e => e.UserPermission).HasColumnName("UserPermission");

                entity.Property(e => e.ItemId).HasColumnName("ItemId");

                entity.Property(e => e.Version).HasColumnName("Version");
            });

            modelBuilder.Entity<MigratedContent>(entity =>
            {
                entity.ToTable("MigratedContent", "dbo");

                entity.HasKey(e => e.Id)
                    .HasName("PK_MigratedContent");

                entity.Property(e => e.Contentno)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                   .IsRequired();

                entity.Property(e => e.CurrentStatus)
                    .HasMaxLength(256)
                   .IsRequired();

                entity.Property(e => e.ContentType)
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(sysdatetime())");
            });

            this.OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
