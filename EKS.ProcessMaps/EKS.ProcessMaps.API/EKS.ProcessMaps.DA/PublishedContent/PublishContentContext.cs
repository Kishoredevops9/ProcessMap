namespace EKS.ProcessMaps.DA
{
    using EKS.ProcessMaps.Entities.PublishedContent;
    using Microsoft.EntityFrameworkCore;

    public partial class PublishContentContext : DbContext
    {
        public PublishContentContext()
        {
        }

        public PublishContentContext(DbContextOptions<PublishContentContext> options)
            : base(options)
        {
        }


        public virtual DbSet<ActivityBlockTypes> ActivityBlockTypes { get; set; }
        public virtual DbSet<ActivityBlocks> ActivityBlocks { get; set; }
        public virtual DbSet<ActivityStates> ActivityStates { get; set; }
        public virtual DbSet<AssetControllingPrograms> AssetControllingPrograms { get; set; }
        public virtual DbSet<AssetExportCompliances> AssetExportCompliances { get; set; }
        public virtual DbSet<AssetKeywords> AssetKeywords { get; set; }
        public virtual DbSet<AssetPartTypes> AssetPartTypes { get; set; }
        public virtual DbSet<AssetParts> AssetParts { get; set; }
        public virtual DbSet<AssetPhases> AssetPhases { get; set; }
        public virtual DbSet<AssetReferences> AssetReferences { get; set; }
        public virtual DbSet<AssetStatementTypes> AssetStatementTypes { get; set; }
        public virtual DbSet<AssetStatements> AssetStatements { get; set; }
        public virtual DbSet<AssetStatuses> AssetStatuses { get; set; }
        public virtual DbSet<AssetTags> AssetTags { get; set; }
        public virtual DbSet<AssetTypes> AssetTypes { get; set; }
        public virtual DbSet<AssetUserRoles> AssetUserRoles { get; set; }
        public virtual DbSet<AssetUsers> AssetUsers { get; set; }
        public virtual DbSet<AuthorizationLog> AuthorizationLog { get; set; }
        public virtual DbSet<Confidentialities> Confidentialities { get; set; }
        public virtual DbSet<Connectors> Connectors { get; set; }
        public virtual DbSet<ContainerItem> ContainerItem { get; set; }
        public virtual DbSet<ContainerItems> ContainerItems { get; set; }
        public virtual DbSet<ControllingPrograms> ControllingPrograms { get; set; }
        public virtual DbSet<Disciplines> Disciplines { get; set; }
        public virtual DbSet<ExcludedContainerItems> ExcludedContainerItems { get; set; }
        public virtual DbSet<ExecutionStates> ExecutionStates { get; set; }
        public virtual DbSet<ExportCompliances> ExportCompliances { get; set; }
        public virtual DbSet<Keywords> Keywords { get; set; }
        public virtual DbSet<KnowledgeAssets> KnowledgeAssets { get; set; }
        public virtual DbSet<Kpacks> Kpacks { get; set; }
        public virtual DbSet<PhasesMap> PhasesMap { get; set; }
        public virtual DbSet<PhasesRef> PhasesRef { get; set; }
        public virtual DbSet<PrivateAssets> PrivateAssets { get; set; }
        public virtual DbSet<RelatedContentCategories> RelatedContentCategories { get; set; }
        public virtual DbSet<RevisionTypes> RevisionTypes { get; set; }
        public virtual DbSet<StatementPhases> StatementPhases { get; set; }
        public virtual DbSet<StatementTags> StatementTags { get; set; }
        public virtual DbSet<SwimLanes> SwimLanes { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Usclassifications> Usclassifications { get; set; }
        public virtual DbSet<UsersCache> UsersCache { get; set; }
        public virtual DbSet<Usjurisdictions> Usjurisdictions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=pweswdb1.database.windows.net,1433;database=EKS_PublishedContent;User ID=pwesw1;password=pwesw@2020;Trusted_Connection=False;Encrypt=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityBlockTypes>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_ActivityBlocksTypes");

                entity.ToTable("ActivityBlockTypes", "ref");
                
                entity.Property(e => e.Id).HasColumnName("ActivityBlockTypeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

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
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ActivityBlocks>(entity =>
            {
                // entity.HasKey(e => e.ActivityBlockId)
                  //  .IsClustered(false);

                entity.ToTable("ActivityBlocks", "maps");

                entity.Property(e => e.Id).HasColumnName("ActivityBlockId");

                entity.Property(e => e.AssetContentId).HasMaxLength(14);

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

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

                entity.Property(e => e.RequiredInd).HasDefaultValueSql("((0))");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ActivityBlockType)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.ActivityBlockTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityBlocks_ActivityBlockTypes");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .HasConstraintName("FK_ActivityBlocks_KnowledgeAssets");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.PhaseId)
                    .HasConstraintName("FK_ActivityBlocks_Phases");

                entity.HasOne(d => d.SwimLane)
                    .WithMany(p => p.ActivityBlocks)
                    .HasForeignKey(d => d.SwimLaneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityBlocks_SwimLanes");
            });

            modelBuilder.Entity<ActivityStates>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ActivityStates", "task");

                entity.Property(e => e.Id).HasColumnName("ActivityStateId");

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

                entity.Property(e => e.Performer).HasMaxLength(128);

                entity.Property(e => e.Reviewer).HasMaxLength(128);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ExecutionState)
                    .WithMany(p => p.ActivityStates)
                    .HasForeignKey(d => d.ExecutionStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityStates_ExecutionStates");

                entity.HasOne(d => d.Shape)
                    .WithMany(p => p.ActivityStates)
                    .HasForeignKey(d => d.ShapeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityStates_Shapes");
            });

            modelBuilder.Entity<AssetControllingPrograms>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetControllingPrograms", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetControllingProgramId");

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
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetExportCompliances", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetExportComplianceId");

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

                entity.HasOne(d => d.ExportCompliance)
                    .WithMany(p => p.AssetExportCompliances)
                    .HasForeignKey(d => d.ExportComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetExportCompliances_ExportCompliances");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetExportCompliances)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetExportCompliances_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetKeywords>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetKeywords", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetKeywordId"); 

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

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.AssetKeywords)
                    .HasForeignKey(d => d.KeywordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetKeywords_Keywords");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetKeywords)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetKeywords_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetPartTypes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetPartTypes", "ref");

                entity.Property(e => e.Id).HasColumnName("AssetPartTypeId");

                entity.HasIndex(e => e.AssetPartTypeCode)
                    .HasName("UI_AssetPartTypes")
                    .IsUnique();

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AssetParts>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetParts", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetPartId");

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

                entity.HasOne(d => d.AssetPartType)
                    .WithMany(p => p.AssetParts)
                    .HasForeignKey(d => d.AssetPartTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetParts_AssetPartTypes");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetParts)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetParts_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetPhases>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetPhases", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetPhaseId");

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

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.AssetPhases)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetPhases_Phases");
            });

            modelBuilder.Entity<AssetReferences>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetReferences", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetReferenceId");

                entity.HasIndex(e => new { e.ReferencingKnowledgeAssetId, e.ReferencedContentId })
                    .HasName("UI_AssetReferences")
                    .IsUnique();

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

                entity.Property(e => e.ReferencedContentId)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ReferencingKnowledgeAsset)
                    .WithMany(p => p.AssetReferences)
                    .HasForeignKey(d => d.ReferencingKnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetReferences_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetStatementTypes>(entity =>
            {
                entity.HasKey(e => e.AssetStatementTypeCode);

                entity.ToTable("AssetStatementTypes", "ref");

                entity.Property(e => e.AssetStatementTypeCode)
                    .HasMaxLength(1)
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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AssetStatements>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetStatements", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetStatementId");

                entity.Property(e => e.AssetStatementTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
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

                entity.Property(e => e.Link).HasMaxLength(1024);

                entity.Property(e => e.Rationale).HasMaxLength(4000);

                entity.Property(e => e.Statement).IsRequired();

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetStatements)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetStatements_KnowledgeAssets");
            });

            modelBuilder.Entity<AssetStatuses>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetStatuses", "ref");

                entity.Property(e => e.Id).HasColumnName("AssetStatusId");

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AssetTags>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetTags", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetTagId");

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

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.AssetTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTags_Tags");
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

            modelBuilder.Entity<AssetUserRoles>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AssetUserRoles", "ref");

                entity.Property(e => e.Id).HasColumnName("AssetUserRoleId");

                entity.HasIndex(e => e.AssetUserRoleCode)
                    .HasName("UI_AssetUserRoles")
                    .IsUnique();

                entity.Property(e => e.AssetUserRoleCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

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

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AssetUsers>(entity =>
            {
                entity.ToTable("AssetUsers", "ka");

                entity.Property(e => e.Id).HasColumnName("AssetUsersId");

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

                entity.HasOne(d => d.AssetUserRole)
                    .WithMany(p => p.AssetUsers)
                    .HasForeignKey(d => d.AssetUserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetUsers_AssetUserRoles");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.AssetUsers)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetUsers_KnowledgeAssets");
            });

            modelBuilder.Entity<AuthorizationLog>(entity =>
            {
                entity.ToTable("AuthorizationLog", "admin");

                entity.Property(e => e.Id).HasColumnName("AuthorizationLogId");

                entity.Property(e => e.ContentId)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.ForwardedFor)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.Groups)
                    .IsRequired()
                    .HasMaxLength(2048);

                entity.Property(e => e.RemoteAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");
            });

            modelBuilder.Entity<Confidentialities>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Confidentialities", "ref");

                entity.Property(e => e.Id).HasColumnName("ConfidentialityId");

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

            modelBuilder.Entity<Connectors>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Connectors", "maps");

                entity.Property(e => e.Id).HasColumnName("ConnectorId");

                entity.Property(e => e.BackgroundColor).HasMaxLength(256);

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.CaptionEnd).HasMaxLength(256);

                entity.Property(e => e.CaptionMiddle).HasMaxLength(256);

                entity.Property(e => e.CaptionStart).HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

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

                entity.Property(e => e.ExcludedInd).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ChildActivityBlock)
                    .WithMany(p => p.ConnectorsChildActivityBlock)
                    .HasForeignKey(d => d.ChildActivityBlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Connectors_ParentShapes");

                entity.HasOne(d => d.ParentActivityBlock)
                    .WithMany(p => p.ConnectorsParentActivityBlock)
                    .HasForeignKey(d => d.ParentActivityBlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Connectors_ChildShapes");
            });

            modelBuilder.Entity<ContainerItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ContainerItem");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<ContainerItems>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ContainerItems", "ka");

                entity.Property(e => e.Id).HasColumnName("ContainerItemId");

                entity.HasIndex(e => new { e.ContainerKnowledgeAssetId, e.ParentContainerItemId, e.Index })
                    .HasName("UI_ContainerItems")
                    .IsUnique();

                entity.Property(e => e.AssetContentId)
                    .IsRequired()
                    .HasMaxLength(12);

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

                entity.HasOne(d => d.ContainerKnowledgeAsset)
                    .WithMany(p => p.ContainerItems)
                    .HasForeignKey(d => d.ContainerKnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContainerItems_KnowledgeAsset");

                entity.HasOne(d => d.ParentContainerItem)
                    .WithMany(p => p.InverseParentContainerItem)
                    .HasForeignKey(d => d.ParentContainerItemId)
                    .HasConstraintName("FK_ContainerItems_ContainerItems");
            });

            modelBuilder.Entity<ControllingPrograms>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ControllingPrograms", "ref");

                entity.Property(e => e.Id).HasColumnName("ControllingProgramId");

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

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Disciplines>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Disciplines", "ref");

                entity.Property(e => e.Id).HasColumnName("DisciplineId");

                entity.HasIndex(e => new { e.Discipline1, e.Discipline2, e.Discipline3, e.Discipline4, e.DisciplineCode })
                    .HasName("UI_Disciplines")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Discipline1)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Discipline2).HasMaxLength(128);

                entity.Property(e => e.Discipline3).HasMaxLength(128);

                entity.Property(e => e.Discipline4).HasMaxLength(128);

                entity.Property(e => e.DisciplineCode).HasMaxLength(5);

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

            modelBuilder.Entity<ExcludedContainerItems>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ExcludedContainerItems", "task");

                entity.Property(e => e.Id).HasColumnName("ExcludedContainerItemId");

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

                entity.HasOne(d => d.ActivityState)
                    .WithMany(p => p.ExcludedContainerItems)
                    .HasForeignKey(d => d.ActivityStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExcludedContainerItems_ActivityStates");

                entity.HasOne(d => d.ContainerItem)
                    .WithMany(p => p.ExcludedContainerItems)
                    .HasForeignKey(d => d.ContainerItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExcludedContainerItems_ContainerItems");
            });

            modelBuilder.Entity<ExecutionStates>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ExecutionStates", "ref");

                entity.Property(e => e.Id).HasColumnName("ExecutionStateId");

                entity.HasIndex(e => e.ExecutionStateCode)
                    .HasName("UI_ExecutionStates")
                    .IsUnique();

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

                entity.Property(e => e.ExecutionStateCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ExportCompliances>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ExportCompliances", "ref");

                entity.Property(e => e.Id).HasColumnName("ExportComplianceId");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.DocumentContentId)
                    .IsRequired()
                    .HasMaxLength(12);

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

                entity.Property(e => e.ProcessMapTitle)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Keywords>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Keywords", "ka");

                entity.Property(e => e.Id).HasColumnName("KeywordId");

                entity.HasIndex(e => e.Keyword)
                    .HasName("UI_Keywords")
                    .IsUnique();

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

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<KnowledgeAssets>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_KnowledgeAsset");

                entity.ToTable("KnowledgeAssets", "ka");

                entity.Property(e => e.Id).HasColumnName("KnowledgeAssetId");

                entity.HasIndex(e => new { e.ContentId, e.AssetStatusId })
                    .HasName("I_KnowledgeAssets_ContentId_AssetStatusId");

                entity.HasIndex(e => new { e.ContentId, e.Version })
                    .HasName("UI_KnowledgeAssets_ContentId_Version")
                    .IsUnique();

                entity.Property(e => e.AssetTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClassificationDate).HasColumnType("date");

                entity.Property(e => e.ContentId)
                    .HasMaxLength(14)
                    .HasComputedColumnSql("(CONVERT([nvarchar](14),((case when [DisciplineCode] IS NOT NULL then [DisciplineCode]+'-' else '' end+[AssetTypeCode])+'-')+case when [ContentNumber]<(10000) then right(CONVERT([nvarchar](5),[ContentNumber]+(10000)),(4)) when [ContentNumber]>(1000000) then CONVERT([nvarchar],[ContentNumber]) else right(CONVERT([nvarchar](7),[ContentNumber]+(1000000)),(6)) end))");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DisciplineCode).HasMaxLength(5);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([datetime2](7),N'12/31/9999'))");

                entity.Property(e => e.ExportPdfurl)
                    .HasColumnName("ExportPDFUrl")
                    .HasMaxLength(250);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.PresentationUrl).HasMaxLength(250);

                entity.Property(e => e.SourceFileUrl).HasMaxLength(250);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Tpmdate)
                    .HasColumnName("TPMDate")
                    .HasColumnType("date");

                entity.Property(e => e.Usclassification)
                    .HasColumnName("USClassification")
                    .HasMaxLength(20);

                entity.Property(e => e.UsclassificationId).HasColumnName("USClassificationId");

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.ApprovalRequirement)
                //    .WithMany(p => p.KnowledgeAssets)
                //    .HasForeignKey(d => d.ApprovalRequirementId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_KnowledgeAssets_ApprovalRequirements");

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.AssetStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeAssets_AssetStatuses");

                entity.HasOne(d => d.AssetTypeCodeNavigation)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.AssetTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeAssets_AssetTypes");

                entity.HasOne(d => d.Confidentiality)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.ConfidentialityId)
                    .HasConstraintName("FK_KnowledgeAssets_Confidentialities");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_KnowledgeAssets_Disciplines");

                entity.HasOne(d => d.RevisionType)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.RevisionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeAssets_RevisionTypes");

                entity.HasOne(d => d.UsclassificationNavigation)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.UsclassificationId)
                    .HasConstraintName("FK_KnowledgeAssets_USClassifications");

                entity.HasOne(d => d.Usjurisdiction)
                    .WithMany(p => p.KnowledgeAssets)
                    .HasForeignKey(d => d.UsjurisdictionId)
                    .HasConstraintName("FK_KnowledgeAssets_USJurisdictions");
            });

            modelBuilder.Entity<Kpacks>(entity =>
            {
                entity.HasKey(e => e.KnowledgeAssetId);

                entity.ToTable("KPacks", "ka");

                entity.Property(e => e.KnowledgeAssetId).ValueGeneratedNever();

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

                //entity.HasOne(d => d.Applicability)
                //    .WithMany(p => p.Kpacks)
                //    .HasForeignKey(d => d.ApplicabilityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_KPacks_Applicabilities");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithOne(p => p.Kpacks)
                    .HasForeignKey<Kpacks>(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KPacks_KnowledgeAssets");

                //entity.HasOne(d => d.Level)
                //    .WithMany(p => p.Kpacks)
                //    .HasForeignKey(d => d.LevelId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_KPacks_Levels");
            });

            modelBuilder.Entity<PhasesMap>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Phases", "maps");

                entity.Property(e => e.Id).HasColumnName("PhaseId");

                entity.Property(e => e.Caption)
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

                entity.Property(e => e.ExcludedInd).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.LastUpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Location).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ProtectedInd).HasDefaultValueSql("((1))");

                entity.Property(e => e.Size).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.PhasesMap)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phases_Maps");
            });

            modelBuilder.Entity<PhasesRef>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Phases", "ref");

                entity.Property(e => e.Id).HasColumnName("PhaseId");

                entity.HasIndex(e => e.PhaseCode)
                    .HasName("UI_Phases")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

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

                entity.Property(e => e.PhaseCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PrivateAssets>(entity =>
            {
                entity.HasKey(e => e.KnowledgeAssetId);

                entity.ToTable("PrivateAssets", "ka");

                entity.Property(e => e.KnowledgeAssetId).ValueGeneratedNever();

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
                    .WithOne(p => p.PrivateAssetsKnowledgeAsset)
                    .HasForeignKey<PrivateAssets>(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrivateAssets_KnowledgeAssets");

                entity.HasOne(d => d.ParentAsset)
                    .WithMany(p => p.PrivateAssetsParentAsset)
                    .HasForeignKey(d => d.ParentAssetId)
                    .HasConstraintName("FK_PrivateAssets_ParentKnowledgeAssets");

                //entity.HasOne(d => d.ParentTask)
                //    .WithMany(p => p.PrivateAssets)
                //    .HasForeignKey(d => d.ParentTaskId)
                //    .HasConstraintName("FK_PrivateAssets_Tasks");
            });

            modelBuilder.Entity<RelatedContentCategories>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("RelatedContentCategories", "ka");

                entity.Property(e => e.Id).HasColumnName("RelatedContentCategoryId");

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

                //entity.HasOne(d => d.Category)
                //    .WithMany(p => p.RelatedContentCategories)
                //    .HasForeignKey(d => d.CategoryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_RelatedContentCategories_Categories");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.RelatedContentCategories)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedContentCategories_KnowledgeAssets");
            });

            modelBuilder.Entity<RevisionTypes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("RevisionTypes", "ref");

                entity.Property(e => e.Id).HasColumnName("RevisionTypeId");

                entity.HasIndex(e => e.RevisionTypeCode)
                    .HasName("UI_RevisionTypes")
                    .IsUnique();

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

            modelBuilder.Entity<StatementPhases>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("StatementPhases", "ka");

                entity.Property(e => e.Id).HasColumnName("StatementPhaseId");

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

                entity.HasOne(d => d.AssetStatement)
                    .WithMany(p => p.StatementPhases)
                    .HasForeignKey(d => d.AssetStatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementPhases_AssetStatements");

                entity.HasOne(d => d.Phase)
                    .WithMany(p => p.StatementPhases)
                    .HasForeignKey(d => d.PhaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementPhases_Phases");
            });

            modelBuilder.Entity<StatementTags>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("StatementTags", "ka");

                entity.Property(e => e.Id).HasColumnName("StatementTagId");

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

                entity.HasOne(d => d.AssetStatement)
                    .WithMany(p => p.StatementTags)
                    .HasForeignKey(d => d.AssetStatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTags_AssetStatements");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.StatementTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTags_Tags");
            });

            modelBuilder.Entity<SwimLanes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("SwimLanes", "maps");

                entity.Property(e => e.Id).HasColumnName("SwimLaneId");

                entity.Property(e => e.BorderColor).HasMaxLength(256);

                entity.Property(e => e.BorderStyle).HasMaxLength(256);

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Color).HasMaxLength(256);

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
                    .HasMaxLength(256);

                entity.Property(e => e.ProtectedInd).HasDefaultValueSql("((1))");

                entity.Property(e => e.Size).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.SwimLanes)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK_SwimLanes_Disciplines");

                entity.HasOne(d => d.KnowledgeAsset)
                    .WithMany(p => p.SwimLanes)
                    .HasForeignKey(d => d.KnowledgeAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SwimLanes_Maps");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Tags", "ref");

                entity.Property(e => e.Id).HasColumnName("TagId");

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

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Usclassifications>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_USClassification");

                entity.ToTable("USClassifications", "ref");

                entity.Property(e => e.Id).HasColumnName("UsclassificationId");

                entity.Property(e => e.Id)
                    .HasColumnName("USClassificationId")
                    .ValueGeneratedNever();

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UsjurisdictionId).HasColumnName("USJurisdictionId");

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.ExportAuthority)
                //    .WithMany(p => p.Usclassifications)
                //    .HasForeignKey(d => d.ExportAuthorityId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_USClassifications_ExportAuthorities");

                entity.HasOne(d => d.Usjurisdiction)
                    .WithMany(p => p.Usclassifications)
                    .HasForeignKey(d => d.UsjurisdictionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USClassifications_USJurisdictions");
            });

            modelBuilder.Entity<UsersCache>(entity =>
            {
                entity.HasKey(e => e.GlobalUid);

                entity.ToTable("UsersCache", "audit");

                entity.Property(e => e.GlobalUid)
                    .HasColumnName("GlobalUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Aadid)
                    .IsRequired()
                    .HasColumnName("AADId")
                    .HasMaxLength(50);

                entity.Property(e => e.BusinessUnitName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClockId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Groups).IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdateDateTime).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.MajorDepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PwemploymentCode)
                    .IsRequired()
                    .HasColumnName("PWEmploymentCode")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usjurisdictions>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("USJurisdictions", "ref");

                entity.Property(e => e.Id).HasColumnName("USJurisdictionId");

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Version).HasDefaultValueSql("((1))");
            });

            modelBuilder.HasSequence<int>("ContentSeqPrivate", "ref")
                .StartsAt(1000000)
                .HasMin(1000000)
                .HasMax(9999999);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
