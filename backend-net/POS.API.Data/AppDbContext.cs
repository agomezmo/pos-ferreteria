using Microsoft.EntityFrameworkCore;
using POS.API.Models;

namespace POS.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<CashRegister> CashRegisters => Set<CashRegister>();
    public DbSet<CashRegisterSession> CashRegisterSessions => Set<CashRegisterSession>();
    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();
    public DbSet<TaxRate> TaxRates => Set<TaxRate>();
    public DbSet<CompanyInfo> CompanyInfos => Set<CompanyInfo>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();
    public DbSet<Return> Returns => Set<Return>();
    public DbSet<ReturnItem> ReturnItems => Set<ReturnItem>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<CatRegimenFiscal> CatRegimenesFiscales => Set<CatRegimenFiscal>();
    public DbSet<CatUsoCfdi> CatUsosCfdi => Set<CatUsoCfdi>();
    public DbSet<CatFormaPago> CatFormasPago => Set<CatFormaPago>();
    public DbSet<CatMetodoPago> CatMetodosPago => Set<CatMetodoPago>();
    public DbSet<CatClaveProdServ> CatClavesProdServ => Set<CatClaveProdServ>();
    public DbSet<CatClaveUnidad> CatClavesUnidad => Set<CatClaveUnidad>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<FacturaItem> FacturaItems => Set<FacturaItem>();
    public DbSet<FacturaRelacion> FacturaRelaciones => Set<FacturaRelacion>();
    public DbSet<PromoCampaign> PromoCampaigns => Set<PromoCampaign>();
    public DbSet<PromoCampaignProduct> PromoCampaignProducts => Set<PromoCampaignProduct>();
    public DbSet<PromoCampaignCustomer> PromoCampaignCustomers => Set<PromoCampaignCustomer>();
    public DbSet<PromoCampaignLog> PromoCampaignLogs => Set<PromoCampaignLog>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Lowercase all table and column names for PostgreSQL
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.GetTableName() is string tableName)
                entity.SetTableName(tableName.ToLowerInvariant());
            foreach (var prop in entity.GetProperties())
            {
                if (prop.GetColumnName() is string columnName)
                    prop.SetColumnName(columnName.ToLowerInvariant());
            }
        }

        // ── Unique indexes ──
        modelBuilder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<Sale>().HasIndex(s => s.ReceiptNumber).IsUnique();
        modelBuilder.Entity<SystemSetting>().HasIndex(s => s.Key).IsUnique();

        // ── Product → Category (Restrict) ──
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Product → Supplier (SetNull) ──
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Sale → User (Restrict) ──
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sales)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Sale → Customer (SetNull) ──
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Sale → CashRegisterSession (Restrict) ──
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.CashRegisterSession)
            .WithMany(cs => cs.Sales)
            .HasForeignKey(s => s.CashRegisterSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── SaleItem → Sale (Cascade) ──
        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── SaleItem → Product (Restrict) ──
        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Product)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Payment → Sale (Cascade) ──
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Sale)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── CashRegisterSession → CashRegister (Restrict) ──
        modelBuilder.Entity<CashRegisterSession>()
            .HasOne(cs => cs.CashRegister)
            .WithMany(cr => cr.Sessions)
            .HasForeignKey(cs => cs.CashRegisterId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── CashRegisterSession → User (Restrict) ──
        modelBuilder.Entity<CashRegisterSession>()
            .HasOne(cs => cs.User)
            .WithMany(u => u.CashRegisterSessions)
            .HasForeignKey(cs => cs.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Return → Sale (Restrict) ──
        modelBuilder.Entity<Return>()
            .HasOne(r => r.Sale)
            .WithMany(s => s.Returns)
            .HasForeignKey(r => r.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Return → User (SetNull) ──
        modelBuilder.Entity<Return>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── ReturnItem → Return (Cascade) ──
        modelBuilder.Entity<ReturnItem>()
            .HasOne(ri => ri.Return)
            .WithMany(r => r.ReturnItems)
            .HasForeignKey(ri => ri.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── ReturnItem → Product (Restrict) ──
        modelBuilder.Entity<ReturnItem>()
            .HasOne(ri => ri.Product)
            .WithMany(p => p.ReturnItems)
            .HasForeignKey(ri => ri.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── InventoryMovement → Product (Restrict) ──
        modelBuilder.Entity<InventoryMovement>()
            .HasOne(im => im.Product)
            .WithMany(p => p.InventoryMovements)
            .HasForeignKey(im => im.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── InventoryMovement → User (SetNull) ──
        modelBuilder.Entity<InventoryMovement>()
            .HasOne(im => im.User)
            .WithMany(u => u.InventoryMovements)
            .HasForeignKey(im => im.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── User → Role (Restrict) ──
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── LoginLog → User (SetNull) ──
        modelBuilder.Entity<LoginLog>()
            .HasOne(l => l.User)
            .WithMany(u => u.LoginLogs)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Alert → User (SetNull) ──
        modelBuilder.Entity<Alert>()
            .HasOne(a => a.User)
            .WithMany(u => u.Alerts)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Expense → User (SetNull) ──
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Factura → Sale (Restrict) ──
        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Sale)
            .WithMany()
            .HasForeignKey(f => f.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Factura → User (SetNull) ──
        modelBuilder.Entity<Factura>()
            .HasOne(f => f.CreatedByUser)
            .WithMany()
            .HasForeignKey(f => f.CreatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── FacturaItem → Factura (Cascade) ──
        modelBuilder.Entity<FacturaItem>()
            .HasOne(fi => fi.Factura)
            .WithMany(f => f.Items)
            .HasForeignKey(fi => fi.FacturaId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── FacturaItem → Product (SetNull) ──
        modelBuilder.Entity<FacturaItem>()
            .HasOne(fi => fi.Producto)
            .WithMany()
            .HasForeignKey(fi => fi.ProductoId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── FacturaRelacion → Factura (Cascade) ──
        modelBuilder.Entity<FacturaRelacion>()
            .HasOne(fr => fr.Factura)
            .WithMany(f => f.Relaciones)
            .HasForeignKey(fr => fr.FacturaId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── Customer → CatRegimenFiscal (SetNull) ──
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.RegimenFiscal)
            .WithMany()
            .HasForeignKey(c => c.RegimenFiscalId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── Customer → CatUsoCfdi (SetNull) ──
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.UsoCfdi)
            .WithMany()
            .HasForeignKey(c => c.UsoCfdiId)
            .OnDelete(DeleteBehavior.SetNull);

        // ── CompanyInfo → CatRegimenFiscal (SetNull) ──
        modelBuilder.Entity<CompanyInfo>()
            .HasOne(ci => ci.RegimenFiscal)
            .WithMany()
            .HasForeignKey(ci => ci.RegimenFiscalId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
