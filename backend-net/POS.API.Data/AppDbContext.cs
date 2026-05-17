using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Expected O, but got Unknown
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected O, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Expected O, but got Unknown
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Expected O, but got Unknown
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Expected O, but got Unknown
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Expected O, but got Unknown
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Expected O, but got Unknown
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Expected O, but got Unknown
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Expected O, but got Unknown
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Expected O, but got Unknown
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Expected O, but got Unknown
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Expected O, but got Unknown
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Expected O, but got Unknown
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Expected O, but got Unknown
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Expected O, but got Unknown
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Expected O, but got Unknown
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Expected O, but got Unknown
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Expected O, but got Unknown
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Expected O, but got Unknown
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Expected O, but got Unknown
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a51: Expected O, but got Unknown
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Expected O, but got Unknown
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Expected O, but got Unknown
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Expected O, but got Unknown
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Expected O, but got Unknown
		//IL_0bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Expected O, but got Unknown
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfe: Expected O, but got Unknown
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4c: Expected O, but got Unknown
		//IL_0c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8d: Expected O, but got Unknown
		//IL_0cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Expected O, but got Unknown
		//IL_0d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2f: Expected O, but got Unknown
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d83: Expected O, but got Unknown
		//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Expected O, but got Unknown
		//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Expected O, but got Unknown
		//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Expected O, but got Unknown
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Expected O, but got Unknown
		//IL_0eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef5: Expected O, but got Unknown
		//IL_0f3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Expected O, but got Unknown
		//IL_0f93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9d: Expected O, but got Unknown
		base.OnModelCreating(modelBuilder);
		System.Collections.Generic.IEnumerator<IMutableEntityType> enumerator = modelBuilder.Model.GetEntityTypes().GetEnumerator();
		try
		{
			while (((System.Collections.IEnumerator)enumerator).MoveNext())
			{
				IMutableEntityType current = enumerator.get_Current();
				current.SetTableName(current.GetTableName()!.ToLowerInvariant());
				System.Collections.Generic.IEnumerator<IMutableProperty> enumerator2 = current.GetProperties().GetEnumerator();
				try
				{
					while (((System.Collections.IEnumerator)enumerator2).MoveNext())
					{
						IMutableProperty current2 = enumerator2.get_Current();
						current2.SetColumnName(current2.GetColumnName().ToLowerInvariant());
					}
				}
				finally
				{
					((System.IDisposable)enumerator2)?.Dispose();
				}
			}
		}
		finally
		{
			((System.IDisposable)enumerator)?.Dispose();
		}
		EntityTypeBuilder<Product> entityTypeBuilder = modelBuilder.Entity<Product>();
		ParameterExpression val = Expression.Parameter(typeof(Product), "p");
		entityTypeBuilder.HasIndex(Expression.Lambda<Func<Product, object>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).IsUnique();
		EntityTypeBuilder<Customer> entityTypeBuilder2 = modelBuilder.Entity<Customer>();
		val = Expression.Parameter(typeof(Customer), "c");
		entityTypeBuilder2.HasIndex(Expression.Lambda<Func<Customer, object>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).IsUnique();
		EntityTypeBuilder<Sale> entityTypeBuilder3 = modelBuilder.Entity<Sale>();
		val = Expression.Parameter(typeof(Sale), "s");
		entityTypeBuilder3.HasIndex(Expression.Lambda<Func<Sale, object>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).IsUnique();
		EntityTypeBuilder<SystemSetting> entityTypeBuilder4 = modelBuilder.Entity<SystemSetting>();
		val = Expression.Parameter(typeof(SystemSetting), "s");
		entityTypeBuilder4.HasIndex(Expression.Lambda<Func<SystemSetting, object>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).IsUnique();
		EntityTypeBuilder<CashRegisterSession> entityTypeBuilder5 = modelBuilder.Entity<CashRegisterSession>();
		val = Expression.Parameter(typeof(CashRegisterSession), "cs");
		ReferenceNavigationBuilder<CashRegisterSession, CashRegister> referenceNavigationBuilder = entityTypeBuilder5.HasOne<CashRegister>(Expression.Lambda<Func<CashRegisterSession, CashRegister>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(CashRegister), "cr");
		referenceNavigationBuilder.WithMany(Expression.Lambda<Func<CashRegister, System.Collections.Generic.IEnumerable<CashRegisterSession>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<CashRegisterSession> entityTypeBuilder6 = modelBuilder.Entity<CashRegisterSession>();
		val = Expression.Parameter(typeof(CashRegisterSession), "cs");
		ReferenceNavigationBuilder<CashRegisterSession, User> referenceNavigationBuilder2 = entityTypeBuilder6.HasOne<User>(Expression.Lambda<Func<CashRegisterSession, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder2.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<CashRegisterSession>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Sale> entityTypeBuilder7 = modelBuilder.Entity<Sale>();
		val = Expression.Parameter(typeof(Sale), "s");
		ReferenceNavigationBuilder<Sale, User> referenceNavigationBuilder3 = entityTypeBuilder7.HasOne<User>(Expression.Lambda<Func<Sale, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder3.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<Sale>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Sale> entityTypeBuilder8 = modelBuilder.Entity<Sale>();
		val = Expression.Parameter(typeof(Sale), "s");
		ReferenceNavigationBuilder<Sale, Customer> referenceNavigationBuilder4 = entityTypeBuilder8.HasOne<Customer>(Expression.Lambda<Func<Sale, Customer>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Customer), "c");
		referenceNavigationBuilder4.WithMany(Expression.Lambda<Func<Customer, System.Collections.Generic.IEnumerable<Sale>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<Sale> entityTypeBuilder9 = modelBuilder.Entity<Sale>();
		val = Expression.Parameter(typeof(Sale), "s");
		ReferenceNavigationBuilder<Sale, CashRegisterSession> referenceNavigationBuilder5 = entityTypeBuilder9.HasOne<CashRegisterSession>(Expression.Lambda<Func<Sale, CashRegisterSession>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(CashRegisterSession), "cs");
		referenceNavigationBuilder5.WithMany(Expression.Lambda<Func<CashRegisterSession, System.Collections.Generic.IEnumerable<Sale>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<SaleItem> entityTypeBuilder10 = modelBuilder.Entity<SaleItem>();
		val = Expression.Parameter(typeof(SaleItem), "si");
		ReferenceNavigationBuilder<SaleItem, Sale> referenceNavigationBuilder6 = entityTypeBuilder10.HasOne<Sale>(Expression.Lambda<Func<SaleItem, Sale>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Sale), "s");
		referenceNavigationBuilder6.WithMany(Expression.Lambda<Func<Sale, System.Collections.Generic.IEnumerable<SaleItem>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Cascade);
		EntityTypeBuilder<SaleItem> entityTypeBuilder11 = modelBuilder.Entity<SaleItem>();
		val = Expression.Parameter(typeof(SaleItem), "si");
		ReferenceNavigationBuilder<SaleItem, Product> referenceNavigationBuilder7 = entityTypeBuilder11.HasOne<Product>(Expression.Lambda<Func<SaleItem, Product>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Product), "p");
		referenceNavigationBuilder7.WithMany(Expression.Lambda<Func<Product, System.Collections.Generic.IEnumerable<SaleItem>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Payment> entityTypeBuilder12 = modelBuilder.Entity<Payment>();
		val = Expression.Parameter(typeof(Payment), "p");
		ReferenceNavigationBuilder<Payment, Sale> referenceNavigationBuilder8 = entityTypeBuilder12.HasOne<Sale>(Expression.Lambda<Func<Payment, Sale>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Sale), "s");
		referenceNavigationBuilder8.WithMany(Expression.Lambda<Func<Sale, System.Collections.Generic.IEnumerable<Payment>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Cascade);
		EntityTypeBuilder<Return> entityTypeBuilder13 = modelBuilder.Entity<Return>();
		val = Expression.Parameter(typeof(Return), "r");
		ReferenceNavigationBuilder<Return, Sale> referenceNavigationBuilder9 = entityTypeBuilder13.HasOne<Sale>(Expression.Lambda<Func<Return, Sale>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Sale), "s");
		referenceNavigationBuilder9.WithMany(Expression.Lambda<Func<Sale, System.Collections.Generic.IEnumerable<Return>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Return> entityTypeBuilder14 = modelBuilder.Entity<Return>();
		val = Expression.Parameter(typeof(Return), "r");
		entityTypeBuilder14.HasOne<User>(Expression.Lambda<Func<Return, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<ReturnItem> entityTypeBuilder15 = modelBuilder.Entity<ReturnItem>();
		val = Expression.Parameter(typeof(ReturnItem), "ri");
		ReferenceNavigationBuilder<ReturnItem, Return> referenceNavigationBuilder10 = entityTypeBuilder15.HasOne<Return>(Expression.Lambda<Func<ReturnItem, Return>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Return), "r");
		referenceNavigationBuilder10.WithMany(Expression.Lambda<Func<Return, System.Collections.Generic.IEnumerable<ReturnItem>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Cascade);
		EntityTypeBuilder<ReturnItem> entityTypeBuilder16 = modelBuilder.Entity<ReturnItem>();
		val = Expression.Parameter(typeof(ReturnItem), "ri");
		ReferenceNavigationBuilder<ReturnItem, Product> referenceNavigationBuilder11 = entityTypeBuilder16.HasOne<Product>(Expression.Lambda<Func<ReturnItem, Product>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Product), "p");
		referenceNavigationBuilder11.WithMany(Expression.Lambda<Func<Product, System.Collections.Generic.IEnumerable<ReturnItem>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<InventoryMovement> entityTypeBuilder17 = modelBuilder.Entity<InventoryMovement>();
		val = Expression.Parameter(typeof(InventoryMovement), "im");
		ReferenceNavigationBuilder<InventoryMovement, Product> referenceNavigationBuilder12 = entityTypeBuilder17.HasOne<Product>(Expression.Lambda<Func<InventoryMovement, Product>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Product), "p");
		referenceNavigationBuilder12.WithMany(Expression.Lambda<Func<Product, System.Collections.Generic.IEnumerable<InventoryMovement>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<InventoryMovement> entityTypeBuilder18 = modelBuilder.Entity<InventoryMovement>();
		val = Expression.Parameter(typeof(InventoryMovement), "im");
		ReferenceNavigationBuilder<InventoryMovement, User> referenceNavigationBuilder13 = entityTypeBuilder18.HasOne<User>(Expression.Lambda<Func<InventoryMovement, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder13.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<InventoryMovement>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<LoginLog> entityTypeBuilder19 = modelBuilder.Entity<LoginLog>();
		val = Expression.Parameter(typeof(LoginLog), "l");
		ReferenceNavigationBuilder<LoginLog, User> referenceNavigationBuilder14 = entityTypeBuilder19.HasOne<User>(Expression.Lambda<Func<LoginLog, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder14.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<LoginLog>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<Alert> entityTypeBuilder20 = modelBuilder.Entity<Alert>();
		val = Expression.Parameter(typeof(Alert), "a");
		ReferenceNavigationBuilder<Alert, User> referenceNavigationBuilder15 = entityTypeBuilder20.HasOne<User>(Expression.Lambda<Func<Alert, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder15.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<Alert>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<Expense> entityTypeBuilder21 = modelBuilder.Entity<Expense>();
		val = Expression.Parameter(typeof(Expense), "e");
		ReferenceNavigationBuilder<Expense, User> referenceNavigationBuilder16 = entityTypeBuilder21.HasOne<User>(Expression.Lambda<Func<Expense, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(User), "u");
		referenceNavigationBuilder16.WithMany(Expression.Lambda<Func<User, System.Collections.Generic.IEnumerable<Expense>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<Product> entityTypeBuilder22 = modelBuilder.Entity<Product>();
		val = Expression.Parameter(typeof(Product), "p");
		ReferenceNavigationBuilder<Product, Category> referenceNavigationBuilder17 = entityTypeBuilder22.HasOne<Category>(Expression.Lambda<Func<Product, Category>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Category), "c");
		referenceNavigationBuilder17.WithMany(Expression.Lambda<Func<Category, System.Collections.Generic.IEnumerable<Product>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Product> entityTypeBuilder23 = modelBuilder.Entity<Product>();
		val = Expression.Parameter(typeof(Product), "p");
		ReferenceNavigationBuilder<Product, Supplier> referenceNavigationBuilder18 = entityTypeBuilder23.HasOne<Supplier>(Expression.Lambda<Func<Product, Supplier>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Supplier), "s");
		referenceNavigationBuilder18.WithMany(Expression.Lambda<Func<Supplier, System.Collections.Generic.IEnumerable<Product>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<User> entityTypeBuilder24 = modelBuilder.Entity<User>();
		val = Expression.Parameter(typeof(User), "u");
		ReferenceNavigationBuilder<User, Role> referenceNavigationBuilder19 = entityTypeBuilder24.HasOne<Role>(Expression.Lambda<Func<User, Role>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Role), "r");
		referenceNavigationBuilder19.WithMany(Expression.Lambda<Func<Role, System.Collections.Generic.IEnumerable<User>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Factura> entityTypeBuilder25 = modelBuilder.Entity<Factura>();
		val = Expression.Parameter(typeof(Factura), "f");
		entityTypeBuilder25.HasOne<Sale>(Expression.Lambda<Func<Factura, Sale>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.Restrict);
		EntityTypeBuilder<Factura> entityTypeBuilder26 = modelBuilder.Entity<Factura>();
		val = Expression.Parameter(typeof(Factura), "f");
		entityTypeBuilder26.HasOne<User>(Expression.Lambda<Func<Factura, User>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<FacturaItem> entityTypeBuilder27 = modelBuilder.Entity<FacturaItem>();
		val = Expression.Parameter(typeof(FacturaItem), "fi");
		ReferenceNavigationBuilder<FacturaItem, Factura> referenceNavigationBuilder20 = entityTypeBuilder27.HasOne<Factura>(Expression.Lambda<Func<FacturaItem, Factura>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Factura), "f");
		referenceNavigationBuilder20.WithMany(Expression.Lambda<Func<Factura, System.Collections.Generic.IEnumerable<FacturaItem>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Cascade);
		EntityTypeBuilder<FacturaItem> entityTypeBuilder28 = modelBuilder.Entity<FacturaItem>();
		val = Expression.Parameter(typeof(FacturaItem), "fi");
		entityTypeBuilder28.HasOne<Product>(Expression.Lambda<Func<FacturaItem, Product>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<FacturaRelacion> entityTypeBuilder29 = modelBuilder.Entity<FacturaRelacion>();
		val = Expression.Parameter(typeof(FacturaRelacion), "fr");
		ReferenceNavigationBuilder<FacturaRelacion, Factura> referenceNavigationBuilder21 = entityTypeBuilder29.HasOne<Factura>(Expression.Lambda<Func<FacturaRelacion, Factura>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val }));
		val = Expression.Parameter(typeof(Factura), "f");
		referenceNavigationBuilder21.WithMany(Expression.Lambda<Func<Factura, System.Collections.Generic.IEnumerable<FacturaRelacion>>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).OnDelete(DeleteBehavior.Cascade);
		EntityTypeBuilder<Customer> entityTypeBuilder30 = modelBuilder.Entity<Customer>();
		val = Expression.Parameter(typeof(Customer), "c");
		entityTypeBuilder30.HasOne<CatRegimenFiscal>(Expression.Lambda<Func<Customer, CatRegimenFiscal>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<Customer> entityTypeBuilder31 = modelBuilder.Entity<Customer>();
		val = Expression.Parameter(typeof(Customer), "c");
		entityTypeBuilder31.HasOne<CatUsoCfdi>(Expression.Lambda<Func<Customer, CatUsoCfdi>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
		EntityTypeBuilder<CompanyInfo> entityTypeBuilder32 = modelBuilder.Entity<CompanyInfo>();
		val = Expression.Parameter(typeof(CompanyInfo), "ci");
		entityTypeBuilder32.HasOne<CatRegimenFiscal>(Expression.Lambda<Func<CompanyInfo, CatRegimenFiscal>>((Expression)(object)Expression.Property((Expression)(object)val, (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), (ParameterExpression[])(object)new ParameterExpression[1] { val })).WithMany().OnDelete(DeleteBehavior.SetNull);
	}
}
