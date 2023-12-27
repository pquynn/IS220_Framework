using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAnFramework.Models
{
    public partial class book_shop_dbContext : DbContext
    {
        public book_shop_dbContext()
        {
        }

        public book_shop_dbContext(DbContextOptions<book_shop_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookImage> BookImages { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VSMK6LD6\\SQLEXPRESS;Initial Catalog=book_shop_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("BLOG");

                entity.Property(e => e.BlogId).HasColumnName("BLOG_ID");

                entity.Property(e => e.BlogDay)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("BLOG_DAY");

                entity.Property(e => e.BlogImage)
                    .HasColumnType("image")
                    .HasColumnName("BLOG_IMAGE");

                entity.Property(e => e.BlogTitle)
                    .HasMaxLength(100)
                    .HasColumnName("BLOG_TITLE");

                entity.Property(e => e.Content)
                    .HasColumnType("ntext")
                    .HasColumnName("CONTENT");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BLOG__USER_ID__5DCAEF64");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("BOOK");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .HasColumnName("AUTHOR");

                entity.Property(e => e.BookCover)
                    .HasColumnName("BOOK_COVER");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("NAME");

                entity.Property(e => e.Pages).HasColumnName("PAGES");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PUBLISHER");

                entity.Property(e => e.YearPublish)
                    .HasColumnType("date")
                    .HasColumnName("YEAR_PUBLISH");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__BOOK__CATEGORY_I__5812160E");
            });

            modelBuilder.Entity<BookImage>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK__BOOK_IMA__054D27E40D9A0E7F");

                entity.ToTable("BOOK_IMAGE");

                entity.Property(e => e.BookId)
                    .ValueGeneratedNever()
                    .HasColumnName("BOOK_ID");

                entity.Property(e => e.BackCover)
                    .HasColumnType("image")
                    .HasColumnName("BACK_COVER");

                entity.Property(e => e.FisrtImage)
                    .HasColumnType("image")
                    .HasColumnName("FISRT_IMAGE");

                entity.Property(e => e.FrontCover)
                    .HasColumnType("image")
                    .HasColumnName("FRONT_COVER");

                entity.Property(e => e.SecondImage)
                    .HasColumnType("image")
                    .HasColumnName("SECOND_IMAGE");

                entity.Property(e => e.ThirdImage)
                    .HasColumnType("image")
                    .HasColumnName("THIRD_IMAGE");

                entity.HasOne(d => d.Book)
                    .WithOne(p => p.BookImage)
                    .HasForeignKey<BookImage>(d => d.BookId)
                    .HasConstraintName("FK_BOOK_IMAGE");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.NameCategory)
                    .HasMaxLength(20)
                    .HasColumnName("NAME_CATEGORY");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CmtId)
                    .HasName("PK__COMMENT__7A2EC3EFB470898D");

                entity.ToTable("COMMENT");

                entity.Property(e => e.CmtId).HasColumnName("CMT_ID");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.BookName)
                    .HasMaxLength(30)
                    .HasColumnName("BOOK_NAME");

                entity.Property(e => e.CmtDay)
                    .HasColumnType("date")
                    .HasColumnName("CMT_DAY");

                entity.Property(e => e.Content)
                    .HasColumnType("ntext")
                    .HasColumnName("CONTENT");

                entity.Property(e => e.Score).HasColumnName("SCORE");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__COMMENT__BOOK_ID__60A75C0F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__COMMENT__USER_ID__619B8048");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserLogin)
                    .HasName("PK__LOGIN__07FD4E9557931B40");

                entity.ToTable("LOGIN");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_LOGIN");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASSWORD");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__LOGIN__ROLE_ID__5070F446");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.Pay)
                    .HasMaxLength(20)
                    .HasColumnName("PAY");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.TotalBooks).HasColumnName("TOTAL_BOOKS");

                entity.Property(e => e.TotalPrice).HasColumnName("TOTAL_PRICE");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserTelephone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("USER_TELEPHONE")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__ORDERS__USER_ID__6477ECF3");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("ORDER_DETAIL");

                entity.Property(e => e.OrderDetailId).HasColumnName("ORDER_DETAIL_ID");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.BookImage)
                    .HasColumnType("image")
                    .HasColumnName("BOOK_IMAGE");

                entity.Property(e => e.BookName)
                    .HasMaxLength(30)
                    .HasColumnName("BOOK_NAME");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__ORDER_DET__BOOK___68487DD7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ORDER_DET__ORDER__6754599E");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(20)
                    .HasColumnName("NAME_ROLE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Birthday)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.DayAdd)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DAY_ADD");

                entity.Property(e => e.Gender)
                    .HasMaxLength(3)
                    .HasColumnName("GENDER");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_LOGIN");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserTelephone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("USER_TELEPHONE")
                    .IsFixedLength();

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__USERS__USER_LOGI__534D60F1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
