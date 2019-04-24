using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ts.Domain
{
    public partial class PolicyCenterCentralDevContext : DbContext
    {
        public PolicyCenterCentralDevContext()
        {
        }

        public PolicyCenterCentralDevContext(DbContextOptions<PolicyCenterCentralDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcknowledgeFeedback> AcknowledgeFeedback { get; set; }
        public virtual DbSet<AcknowledgeImpermanents> AcknowledgeImpermanents { get; set; }
        public virtual DbSet<AcknowledgeServers> AcknowledgeServers { get; set; }
        public virtual DbSet<Acknowledges> Acknowledges { get; set; }
        public virtual DbSet<BehaviorForceExecution> BehaviorForceExecution { get; set; }
        public virtual DbSet<BehaviorPolicyAbstract> BehaviorPolicyAbstract { get; set; }
        public virtual DbSet<BehaviorPolicyNotification> BehaviorPolicyNotification { get; set; }
        public virtual DbSet<BehaviorPolicySchedulabe> BehaviorPolicySchedulabe { get; set; }
        public virtual DbSet<BehaviorPublication> BehaviorPublication { get; set; }
        public virtual DbSet<BehaviorSendEmail> BehaviorSendEmail { get; set; }
        public virtual DbSet<CollectionDefinition> CollectionDefinition { get; set; }
        public virtual DbSet<CollectionMixedResolved> CollectionMixedResolved { get; set; }
        public virtual DbSet<CollectionResolved> CollectionResolved { get; set; }
        public virtual DbSet<ComputerAssignationServers> ComputerAssignationServers { get; set; }
        public virtual DbSet<IdentityAbstract> IdentityAbstract { get; set; }
        public virtual DbSet<IdentityComputer> IdentityComputer { get; set; }
        public virtual DbSet<IdentityServer> IdentityServer { get; set; }
        public virtual DbSet<IdentityUser> IdentityUser { get; set; }
        public virtual DbSet<IdentityUserComputerAssign> IdentityUserComputerAssign { get; set; }
        public virtual DbSet<LocalizedSiteServer> LocalizedSiteServer { get; set; }
        public virtual DbSet<LocalizedSites> LocalizedSites { get; set; }
        public virtual DbSet<MessageContent> MessageContent { get; set; }
        public virtual DbSet<MessageVariables> MessageVariables { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<PolicyAbstract> PolicyAbstract { get; set; }
        public virtual DbSet<PolicyApplication> PolicyApplication { get; set; }
        public virtual DbSet<PolicyNotification> PolicyNotification { get; set; }
        public virtual DbSet<PolicyReboot> PolicyReboot { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<PublicationOnCollection> PublicationOnCollection { get; set; }
        public virtual DbSet<SecurableObject> SecurableObject { get; set; }
        public virtual DbSet<SecurityIdentity> SecurityIdentity { get; set; }
        public virtual DbSet<SecurityIdentityRights> SecurityIdentityRights { get; set; }
        public virtual DbSet<SecurityPermissions> SecurityPermissions { get; set; }
        public virtual DbSet<SecurityRoles> SecurityRoles { get; set; }
        public virtual DbSet<SecurityScopes> SecurityScopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=192.32.5.33\\MSPAREUSL03;Initial Catalog=PolicyCenterCentralDev;User id=sa; password=azertyuiop1$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AcknowledgeFeedback>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<AcknowledgeImpermanents>(entity =>
            {
                entity.HasIndex(e => e.FeedbackId)
                    .HasName("IX_FeedbackID");

                entity.HasIndex(e => e.IdentityId)
                    .HasName("IX_IdentityID");

                entity.HasIndex(e => e.MixedIdentityId)
                    .HasName("IX_MixedIdentityID");

                entity.HasIndex(e => e.PolicyId)
                    .HasName("IX_PolicyID");

                entity.HasIndex(e => e.PublicationId1)
                    .HasName("IX_Publication_ID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");

                entity.Property(e => e.IdentityId).HasColumnName("IdentityID");

                entity.Property(e => e.MixedIdentityId).HasColumnName("MixedIdentityID");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.Property(e => e.PublicationId1).HasColumnName("Publication_ID");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.AcknowledgeImpermanents)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK_dbo.AcknowledgeImpermanents_dbo.AcknowledgeFeedback_FeedbackID");

                entity.HasOne(d => d.Identity)
                    .WithMany(p => p.AcknowledgeImpermanents)
                    .HasForeignKey(d => d.IdentityId)
                    .HasConstraintName("FK_dbo.AcknowledgeImpermanents_dbo.IdentityAbstract_IdentityID");

                entity.HasOne(d => d.MixedIdentity)
                    .WithMany(p => p.AcknowledgeImpermanents)
                    .HasForeignKey(d => d.MixedIdentityId)
                    .HasConstraintName("FK_dbo.AcknowledgeImpermanents_dbo.IdentityUserComputerAssign_MixedIdentityID");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.AcknowledgeImpermanents)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_dbo.AcknowledgeImpermanents_dbo.PolicyAbstract_PolicyID");

                entity.HasOne(d => d.PublicationId1Navigation)
                    .WithMany(p => p.AcknowledgeImpermanents)
                    .HasForeignKey(d => d.PublicationId1)
                    .HasConstraintName("FK_dbo.AcknowledgeImpermanents_dbo.Publication_Publication_ID");
            });

            modelBuilder.Entity<AcknowledgeServers>(entity =>
            {
                entity.HasKey(e => new { e.PublicationId, e.ServerId })
                    .HasName("PK_dbo.AcknowledgeServers");

                entity.HasIndex(e => e.PublicationId1)
                    .HasName("IX_Publication_ID");

                entity.HasIndex(e => e.ServerId)
                    .HasName("IX_ServerID");

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.Property(e => e.ServerId).HasColumnName("ServerID");

                entity.Property(e => e.PublicationId1).HasColumnName("Publication_ID");

                entity.HasOne(d => d.PublicationId1Navigation)
                    .WithMany(p => p.AcknowledgeServers)
                    .HasForeignKey(d => d.PublicationId1)
                    .HasConstraintName("FK_dbo.AcknowledgeServers_dbo.Publication_Publication_ID");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.AcknowledgeServers)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.AcknowledgeServers_dbo.IdentityServer_ServerID");
            });

            modelBuilder.Entity<Acknowledges>(entity =>
            {
                entity.HasIndex(e => e.FeedbackId)
                    .HasName("IX_FeedbackID");

                entity.HasIndex(e => e.IdentityId)
                    .HasName("IX_IdentityID");

                entity.HasIndex(e => e.MixedIdentityId)
                    .HasName("IX_MixedIdentityID");

                entity.HasIndex(e => e.PublicationId1)
                    .HasName("IX_Publication_ID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");

                entity.Property(e => e.IdentityId).HasColumnName("IdentityID");

                entity.Property(e => e.MixedIdentityId).HasColumnName("MixedIdentityID");

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.Property(e => e.PublicationId1).HasColumnName("Publication_ID");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Acknowledges)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK_dbo.Acknowledges_dbo.AcknowledgeFeedback_FeedbackID");

                entity.HasOne(d => d.Identity)
                    .WithMany(p => p.Acknowledges)
                    .HasForeignKey(d => d.IdentityId)
                    .HasConstraintName("FK_dbo.Acknowledges_dbo.IdentityAbstract_IdentityID");

                entity.HasOne(d => d.MixedIdentity)
                    .WithMany(p => p.Acknowledges)
                    .HasForeignKey(d => d.MixedIdentityId)
                    .HasConstraintName("FK_dbo.Acknowledges_dbo.IdentityUserComputerAssign_MixedIdentityID");

                entity.HasOne(d => d.PublicationId1Navigation)
                    .WithMany(p => p.Acknowledges)
                    .HasForeignKey(d => d.PublicationId1)
                    .HasConstraintName("FK_dbo.Acknowledges_dbo.Publication_Publication_ID");
            });

            modelBuilder.Entity<BehaviorForceExecution>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.BehaviorForceExecution)
                    .HasForeignKey<BehaviorForceExecution>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorForceExecution_dbo.BehaviorPublication_ID");
            });

            modelBuilder.Entity<BehaviorPolicyAbstract>(entity =>
            {
                entity.HasIndex(e => e.PolicyId)
                    .HasName("IX_PolicyId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.BehaviorPolicyAbstract)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorPolicyAbstract_dbo.PolicyAbstract_PolicyId");
            });

            modelBuilder.Entity<BehaviorPolicyNotification>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.HasIndex(e => e.MessageId)
                    .HasName("IX_MessageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.BehaviorPolicyNotification)
                    .HasForeignKey<BehaviorPolicyNotification>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorPolicyNotification_dbo.BehaviorPolicyAbstract_ID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.BehaviorPolicyNotification)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK_dbo.BehaviorPolicyNotification_dbo.MessageContent_MessageID");
            });

            modelBuilder.Entity<BehaviorPolicySchedulabe>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.BehaviorPolicySchedulabe)
                    .HasForeignKey<BehaviorPolicySchedulabe>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorPolicySchedulabe_dbo.BehaviorPolicyAbstract_ID");
            });

            modelBuilder.Entity<BehaviorPublication>(entity =>
            {
                entity.HasIndex(e => e.PublicationId)
                    .HasName("IX_PublicationID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.BehaviorPublication)
                    .HasForeignKey(d => d.PublicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorPublication_dbo.Publication_PublicationID");
            });

            modelBuilder.Entity<BehaviorSendEmail>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.BehaviorSendEmail)
                    .HasForeignKey<BehaviorSendEmail>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BehaviorSendEmail_dbo.BehaviorPublication_ID");
            });

            modelBuilder.Entity<CollectionDefinition>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.HasIndex(e => e.ParentCollectionId)
                    .HasName("IX_ParentCollectionID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Dspassword).HasColumnName("DSPassword");

                entity.Property(e => e.DsuserName).HasColumnName("DSUserName");

                entity.Property(e => e.LastResolutionDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ParentCollectionId).HasColumnName("ParentCollectionID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.CollectionDefinition)
                    .HasForeignKey<CollectionDefinition>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.CollectionDefinition_dbo.SecurableObject_ID");

                entity.HasOne(d => d.ParentCollection)
                    .WithMany(p => p.InverseParentCollection)
                    .HasForeignKey(d => d.ParentCollectionId)
                    .HasConstraintName("FK_dbo.CollectionDefinition_dbo.CollectionDefinition_ParentCollectionID");
            });

            modelBuilder.Entity<CollectionMixedResolved>(entity =>
            {
                entity.HasKey(e => new { e.IdCollectionId, e.ComputerAssignationId })
                    .HasName("PK_dbo.CollectionMixedResolved");

                entity.HasIndex(e => e.ComputerAssignationId)
                    .HasName("IX_ComputerAssignation_ID");

                entity.HasIndex(e => e.IdCollectionId)
                    .HasName("IX_IdCollection_ID");

                entity.Property(e => e.IdCollectionId).HasColumnName("IdCollection_ID");

                entity.Property(e => e.ComputerAssignationId).HasColumnName("ComputerAssignation_ID");

                entity.HasOne(d => d.ComputerAssignation)
                    .WithMany(p => p.CollectionMixedResolved)
                    .HasForeignKey(d => d.ComputerAssignationId)
                    .HasConstraintName("FK_dbo.CollectionMixedResolved_dbo.IdentityUserComputerAssign_ComputerAssignation_ID");

                entity.HasOne(d => d.IdCollection)
                    .WithMany(p => p.CollectionMixedResolved)
                    .HasForeignKey(d => d.IdCollectionId)
                    .HasConstraintName("FK_dbo.CollectionMixedResolved_dbo.CollectionDefinition_IdCollection_ID");
            });

            modelBuilder.Entity<CollectionResolved>(entity =>
            {
                entity.HasKey(e => new { e.CollectionId, e.IdentityId })
                    .HasName("PK_dbo.CollectionResolved");

                entity.HasIndex(e => e.CollectionId)
                    .HasName("IX_CollectionId");

                entity.HasIndex(e => e.IdentityId)
                    .HasName("IX_IdentityId");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.CollectionResolved)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_dbo.CollectionResolved_dbo.CollectionDefinition_CollectionId");

                entity.HasOne(d => d.Identity)
                    .WithMany(p => p.CollectionResolved)
                    .HasForeignKey(d => d.IdentityId)
                    .HasConstraintName("FK_dbo.CollectionResolved_dbo.IdentityAbstract_IdentityId");
            });

            modelBuilder.Entity<ComputerAssignationServers>(entity =>
            {
                entity.HasKey(e => new { e.ComputerAssignationId, e.DbServerIdentityId })
                    .HasName("PK_dbo.ComputerAssignationServers");

                entity.HasIndex(e => e.ComputerAssignationId)
                    .HasName("IX_ComputerAssignation_ID");

                entity.HasIndex(e => e.DbServerIdentityId)
                    .HasName("IX_DbServerIdentity_ID");

                entity.Property(e => e.ComputerAssignationId).HasColumnName("ComputerAssignation_ID");

                entity.Property(e => e.DbServerIdentityId).HasColumnName("DbServerIdentity_ID");

                entity.HasOne(d => d.ComputerAssignation)
                    .WithMany(p => p.ComputerAssignationServers)
                    .HasForeignKey(d => d.ComputerAssignationId)
                    .HasConstraintName("FK_dbo.ComputerAssignationServers_dbo.IdentityUserComputerAssign_ComputerAssignation_ID");

                entity.HasOne(d => d.DbServerIdentity)
                    .WithMany(p => p.ComputerAssignationServers)
                    .HasForeignKey(d => d.DbServerIdentityId)
                    .HasConstraintName("FK_dbo.ComputerAssignationServers_dbo.IdentityServer_DbServerIdentity_ID");
            });

            modelBuilder.Entity<IdentityAbstract>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<IdentityComputer>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AodVersion).HasColumnName("AOD_Version");

                entity.Property(e => e.ComputerDataXml).HasColumnType("xml");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.IdentityComputer)
                    .HasForeignKey<IdentityComputer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IdentityComputer_dbo.IdentityAbstract_ID");
            });

            modelBuilder.Entity<IdentityServer>(entity =>
            {
                entity.HasIndex(e => e.ParentServerId)
                    .HasName("IX_ParentServerID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cpu).HasColumnName("CPU");

                entity.Property(e => e.Ip).HasColumnName("IP");

                entity.Property(e => e.LastConnectionDate).HasColumnType("datetime");

                entity.Property(e => e.Os).HasColumnName("OS");

                entity.Property(e => e.ParentServerId).HasColumnName("ParentServerID");

                entity.Property(e => e.XmapCoord).HasColumnName("XMapCoord");

                entity.Property(e => e.YmapCoord).HasColumnName("YMapCoord");

                entity.HasOne(d => d.ParentServer)
                    .WithMany(p => p.InverseParentServer)
                    .HasForeignKey(d => d.ParentServerId)
                    .HasConstraintName("FK_dbo.IdentityServer_dbo.IdentityServer_ParentServerID");
            });

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.IdentityUser)
                    .HasForeignKey<IdentityUser>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IdentityUser_dbo.IdentityAbstract_ID");
            });

            modelBuilder.Entity<IdentityUserComputerAssign>(entity =>
            {
                entity.HasIndex(e => e.ComputerId)
                    .HasName("IX_ComputerID");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComputerId).HasColumnName("ComputerID");

                entity.Property(e => e.LastConnectionDate).HasColumnType("datetime");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Computer)
                    .WithMany(p => p.IdentityUserComputerAssign)
                    .HasForeignKey(d => d.ComputerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IdentityUserComputerAssign_dbo.IdentityComputer_ComputerID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdentityUserComputerAssign)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IdentityUserComputerAssign_dbo.IdentityUser_UserID");
            });

            modelBuilder.Entity<LocalizedSiteServer>(entity =>
            {
                entity.HasKey(e => new { e.SiteId, e.ServerId })
                    .HasName("PK_dbo.LocalizedSiteServer");

                entity.HasIndex(e => e.ServerId)
                    .HasName("IX_ServerId");

                entity.HasIndex(e => e.SiteId)
                    .HasName("IX_SiteId");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.LocalizedSiteServer)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.LocalizedSiteServer_dbo.IdentityServer_ServerId");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.LocalizedSiteServer)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_dbo.LocalizedSiteServer_dbo.LocalizedSites_SiteId");
            });

            modelBuilder.Entity<LocalizedSites>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<MessageContent>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.MessageContent)
                    .HasForeignKey<MessageContent>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.MessageContent_dbo.SecurableObject_ID");
            });

            modelBuilder.Entity<MessageVariables>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<PolicyAbstract>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PolicyAbstract)
                    .HasForeignKey<PolicyAbstract>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PolicyAbstract_dbo.SecurableObject_ID");
            });

            modelBuilder.Entity<PolicyApplication>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PolicyApplication)
                    .HasForeignKey<PolicyApplication>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PolicyApplication_dbo.PolicyAbstract_ID");
            });

            modelBuilder.Entity<PolicyNotification>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PolicyNotification)
                    .HasForeignKey<PolicyNotification>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PolicyNotification_dbo.PolicyAbstract_ID");
            });

            modelBuilder.Entity<PolicyReboot>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PolicyReboot)
                    .HasForeignKey<PolicyReboot>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PolicyReboot_dbo.PolicyAbstract_ID");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_ID");

                entity.HasIndex(e => e.PolicyId)
                    .HasName("IX_PolicyID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ExpectedDate).HasColumnType("datetime");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Publication)
                    .HasForeignKey<Publication>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Publication_dbo.SecurableObject_ID");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Publication)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Publication_dbo.PolicyAbstract_PolicyID");
            });

            modelBuilder.Entity<PublicationOnCollection>(entity =>
            {
                entity.HasKey(e => new { e.CollectionId, e.PublicationId })
                    .HasName("PK_dbo.PublicationOnCollection");

                entity.HasIndex(e => e.CollectionId)
                    .HasName("IX_CollectionId");

                entity.HasIndex(e => e.PublicationId)
                    .HasName("IX_PublicationId");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.PublicationOnCollection)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PublicationOnCollection_dbo.CollectionDefinition_CollectionId");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.PublicationOnCollection)
                    .HasForeignKey(d => d.PublicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.PublicationOnCollection_dbo.Publication_PublicationId");
            });

            modelBuilder.Entity<SecurableObject>(entity =>
            {
                entity.HasIndex(e => e.SecurityScopeId)
                    .HasName("IX_SecurityScopeId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.SecurityScope)
                    .WithMany(p => p.SecurableObject)
                    .HasForeignKey(d => d.SecurityScopeId)
                    .HasConstraintName("FK_dbo.SecurableObject_dbo.SecurityScopes_SecurityScopeId");
            });

            modelBuilder.Entity<SecurityIdentity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<SecurityIdentityRights>(entity =>
            {
                entity.HasIndex(e => e.AdministrativeIdentityId)
                    .HasName("IX_AdministrativeIdentityId");

                entity.HasIndex(e => e.SecurityRoleId)
                    .HasName("IX_SecurityRoleId");

                entity.HasIndex(e => e.SecurityScopeId)
                    .HasName("IX_SecurityScopeId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.AdministrativeIdentity)
                    .WithMany(p => p.SecurityIdentityRights)
                    .HasForeignKey(d => d.AdministrativeIdentityId)
                    .HasConstraintName("FK_dbo.SecurityIdentityRights_dbo.SecurityIdentity_AdministrativeIdentityId");

                entity.HasOne(d => d.SecurityRole)
                    .WithMany(p => p.SecurityIdentityRights)
                    .HasForeignKey(d => d.SecurityRoleId)
                    .HasConstraintName("FK_dbo.SecurityIdentityRights_dbo.SecurityRoles_SecurityRoleId");

                entity.HasOne(d => d.SecurityScope)
                    .WithMany(p => p.SecurityIdentityRights)
                    .HasForeignKey(d => d.SecurityScopeId)
                    .HasConstraintName("FK_dbo.SecurityIdentityRights_dbo.SecurityScopes_SecurityScopeId");
            });

            modelBuilder.Entity<SecurityPermissions>(entity =>
            {
                entity.HasIndex(e => e.SecurityRoleId)
                    .HasName("IX_SecurityRoleId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ace).HasColumnName("ACE");

                entity.HasOne(d => d.SecurityRole)
                    .WithMany(p => p.SecurityPermissions)
                    .HasForeignKey(d => d.SecurityRoleId)
                    .HasConstraintName("FK_dbo.SecurityPermissions_dbo.SecurityRoles_SecurityRoleId");
            });

            modelBuilder.Entity<SecurityRoles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<SecurityScopes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });
        }
    }
}
