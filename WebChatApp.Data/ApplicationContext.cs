using Microsoft.EntityFrameworkCore;
using System;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public ApplicationContext()
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<AccessRuleEntity> AccessRules { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<RoleAccessRule> RoleAccessRules { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleAccessRule>().ToTable("RoleAccessRules");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserChat>().ToTable("UserChats");

            modelBuilder.Entity<MessageEntity>().HasOne(x => x.UserCreator)// у сообщения есть один создатель
                .WithMany(x => x.Messages) // у создателя есть много сообщений
                .HasForeignKey(x => x.UserCreatorId) // внешний ключ по отношению к таблице 
                .HasPrincipalKey(x => x.Id) // ключ из таблицы Пользователя
                .OnDelete(DeleteBehavior.Cascade); // разрешить каскадное удаление

            modelBuilder.Entity<ChatEntity>().HasOne(x => x.UserCreator)
                .WithMany()
                .HasForeignKey(x => x.UserCreatorId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserChat>().HasOne(x => x.Chat)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserChat>().HasOne(x => x.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoleAccessRule>().HasOne(x => x.AccessRule)
               .WithMany(x => x.Roles)
               .HasForeignKey(x => x.AccessRuleId)
               .HasPrincipalKey(x => x.Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoleAccessRule>().HasOne(x => x.Role)
               .WithMany(x => x.AccessRules)
               .HasForeignKey(x => x.RoleId)
               .HasPrincipalKey(x => x.Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>().HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
