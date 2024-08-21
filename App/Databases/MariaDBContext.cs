using Microsoft.EntityFrameworkCore;
using Project.App.Mqtt;

namespace Project.App.Databases
{
    public partial class MariaDBContext : DbContext
    {
        public MariaDBContext(DbContextOptions<MariaDBContext> options) : base(options)
        {
        }

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<AccountTw> AccountTws { get; set; }

        //public DbSet<AccountPermission> AccountPermissions { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Permission> Permissions { get; set; }
        //public DbSet<RolePermission> RolePermissions { get; set; }

        //public DbSet<Media> Medias { get; set; }
        //public DbSet<MediaFormats> MediaFormats { get; set; }
        //public DbSet<Template> Templates { get; set; }
        //public DbSet<TemplateMedias> TemplateMedias { get; set; }
        //public DbSet<Playlist> Playlists { get; set; }
        //public DbSet<PlaylistDetail> PlaylistDetails { get; set; }
        //public DbSet<Share> Shares { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Feedback> Feedbacks { get; set; }
        //public DbSet<FeedbackAssign> FeedbackAssigns { get; set; }
        //public DbSet<Survey> Surveys { get; set; }
        //public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        //public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        //public DbSet<SurveyReply> SurveyReplies { get; set; }
        //public DbSet<ScheduleRecord> ScheduleRecords { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        //public DbSet<MqttClient> MqttClients { get; set; }
        //public DbSet<DeviceSpeaker> DeviceSpeakers { get; set; }
        //public DbSet<TwProvinceInfo> TwProvinceInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Area>()
            //.HasOne(x => x.ParentArea)
            //.WithMany(x => x.ChildAreas)
            //.HasForeignKey(x => x.ParentAreaId)
            //.IsRequired(false);

            //modelBuilder.Entity<Area>()
            //    .HasMany(x => x.ChildAreas)
            //    .WithOne(x => x.Parent)
            //    .HasForeignKey(x => x.ParentId)
            //    .IsRequired(false);

            //modelBuilder.Entity<Area>()
            //    .HasMany(x => x.ParentAreas)
            //    .WithOne(x => x.Child)
            //    .HasForeignKey(x => x.ChildId)
            //    .IsRequired(false);

            //modelBuilder.Entity<AreaRelation>().HasKey(c => new { c.ParentId, c.ChildId });

            //modelBuilder.Entity<Playlist>()
            //    .HasOne(x => x.Field)
            //    .WithMany(x => x.Playlists)
            //    .HasForeignKey(x => x.FieldId);

            //modelBuilder.Entity<PlaylistDetail>()
            //    .HasOne<Playlist>(e => e.Playlist)
            //    .WithMany(d => d.PlaylistDetails)
            //    .HasForeignKey(e => e.PlaylistId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ScheduleArea>()
            //    .HasOne<Schedule>(e => e.Schedule)
            //    .WithMany(d => d.ScheduleAreas)
            //    .HasForeignKey(e => e.ScheduleId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ScheduleTime>()
            //    .HasOne<Schedule>(e => e.Schedule)
            //    .WithMany(d => d.ScheduleTimes)
            //    .HasForeignKey(e => e.ScheduleId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ScheduleDetail>()
            //    .HasOne<Schedule>(e => e.Schedule)
            //    .WithMany(d => d.ScheduleDetails)
            //    .HasForeignKey(e => e.ScheduleId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<TwProvinceInfo>()
            //    .HasKey(x => new {x.Id } );

            //modelBuilder.Entity<Role>()
            //    .HasOne(e => e.Creator)
            //    .WithMany()
            //    .HasForeignKey("CreatorId");

            //modelBuilder.Entity<BroadcastProgramDocument>()
            //    .HasKey(x => new{x.BroadcastProgramId,x.DocumentId});

            //modelBuilder.Entity<BroadcastProgramPost>()
            //    .HasKey(x => new { x.BroadcastProgramId, x.PostId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
