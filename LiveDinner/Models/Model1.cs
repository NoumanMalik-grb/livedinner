namespace LiveDinner.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact_Us> Contact_Us { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sub_Category> Sub_Category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.FeedBacks)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.Account_Fid);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.Account_Fid);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Sub_Category)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Category_Fid);          
            modelBuilder.Entity<Order_Details>()
                .Property(e => e.OD_Sale_Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.OD_Purchase_Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Order_Fid);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Purchase_price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Sale_Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.FeedBacks)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_Fid);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_Fid);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Products1)
                .WithOptional(e => e.Product1)
                .HasForeignKey(e => e.Sub_Category_Fid);
        }
    }
}
