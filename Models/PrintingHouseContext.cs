using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PrintingHouse.ViewModels;

namespace PrintingHouse.Models
{
    public partial class PrintingHouseContext : DbContext
    {
        public PrintingHouseContext(DbContextOptions<PrintingHouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProperty> OrderProperty { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ProdductService> ProdductService { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductProperty> ProductProperty { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<SubAccount> SubAccount { get; set; }
        public virtual DbSet<Suplier> Suplier { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=PrintingHouse; integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.OrderDeadline)
                    .HasColumnName("orderDeadline")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__order__customerI__19AACF41");
            });

            modelBuilder.Entity<OrderProperty>(entity =>
            {
                entity.HasKey(e => e.OrderTypeId)
                    .HasName("PK__orderTyp__42A1B592E10B78CB");

                entity.ToTable("orderProperty");

                entity.Property(e => e.OrderTypeId).HasColumnName("orderTypeID");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.ProductPropertyId).HasColumnName("productPropertyID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SupFare)
                    .HasColumnName("supFare")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SupId).HasColumnName("supID");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProperty)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__orderType__order__2057CCD0");

                entity.HasOne(d => d.ProductProperty)
                    .WithMany(p => p.OrderProperty)
                    .HasForeignKey(d => d.ProductPropertyId)
                    .HasConstraintName("FK__orderProp__produ__41B8C09B");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.PayedAmount)
                    .HasColumnName("payedAmount")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("paymentDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__payment__orderID__151B244E");
            });

            modelBuilder.Entity<ProdductService>(entity =>
            {
                entity.HasKey(e => e.ProdductService1)
                    .HasName("PK__prodduct__EDEFB117EB6EAC7C");

                entity.ToTable("prodductService");

                entity.Property(e => e.ProdductService1).HasColumnName("prodductService");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProdductService)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__prodductS__produ__7CD98669");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ProdductService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__prodductS__Servi__74794A92");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<ProductProperty>(entity =>
            {
                entity.ToTable("productProperty");

                entity.Property(e => e.ProductPropertyId).HasColumnName("productPropertyID");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProPertyId).HasColumnName("proPertyID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.ProPerty)
                    .WithMany(p => p.ProductProperty)
                    .HasForeignKey(d => d.ProPertyId)
                    .HasConstraintName("FK__productPr__proPe__19DFD96B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProperty)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__productPr__produ__7BE56230");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("property");

                entity.Property(e => e.ProPertyId).HasColumnName("proPertyID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SubAccount>(entity =>
            {
                entity.ToTable("subAccount");

                entity.Property(e => e.SubAccountId).HasColumnName("subAccountID");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SubAccount)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__subAccoun__accou__308E3499");
            });

            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.ToTable("suplier");

                entity.Property(e => e.SuplierId).HasColumnName("suplierID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Debit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.SubAccountId).HasColumnName("subAccountID");

                entity.Property(e => e.SuplierId).HasColumnName("suplierID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__Transacti__payme__52E34C9D");

                entity.HasOne(d => d.SubAccount)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.SubAccountId)
                    .HasConstraintName("FK__Transacti__subAc__36470DEF");

                entity.HasOne(d => d.Suplier)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.SuplierId)
                    .HasConstraintName("FK__orderTran__supli__14270015");
            });
        }

        public DbSet<PrintingHouse.ViewModels.ProductViewModel> ProductViewModel { get; set; }
    }
}
