using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Users")]
public class User
{
	[Key]
	public int Id
	{
		[CompilerGenerated]
		get
		{
			return _003CId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CId_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(50)]
	public string Username
	{
		[CompilerGenerated]
		get
		{
			return _003CUsername_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUsername_003Ek__BackingField = value;
		}
	}

	[Required]
	public string PasswordHash
	{
		[CompilerGenerated]
		get
		{
			return _003CPasswordHash_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPasswordHash_003Ek__BackingField = value;
		}
	}

	[StringLength(100)]
	public string? Email
	{
		[CompilerGenerated]
		get
		{
			return _003CEmail_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CEmail_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(100)]
	public string FullName
	{
		[CompilerGenerated]
		get
		{
			return _003CFullName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFullName_003Ek__BackingField = value;
		}
	}

	public int RoleId
	{
		[CompilerGenerated]
		get
		{
			return _003CRoleId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRoleId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("RoleId")]
	public Role Role
	{
		[CompilerGenerated]
		get
		{
			return _003CRole_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRole_003Ek__BackingField = value;
		}
	}

	public bool IsActive
	{
		[CompilerGenerated]
		get
		{
			return _003CIsActive_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsActive_003Ek__BackingField = value;
		}
	}

	public System.DateTime? LastLogin
	{
		[CompilerGenerated]
		get
		{
			return _003CLastLogin_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLastLogin_003Ek__BackingField = value;
		}
	}

	public System.DateTime CreatedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CCreatedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCreatedAt_003Ek__BackingField = value;
		}
	}

	public System.DateTime? UpdatedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CUpdatedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUpdatedAt_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Sale> Sales
	{
		[CompilerGenerated]
		get
		{
			return _003CSales_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSales_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<CashRegisterSession> CashRegisterSessions
	{
		[CompilerGenerated]
		get
		{
			return _003CCashRegisterSessions_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCashRegisterSessions_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<InventoryMovement> InventoryMovements
	{
		[CompilerGenerated]
		get
		{
			return _003CInventoryMovements_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CInventoryMovements_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<LoginLog> LoginLogs
	{
		[CompilerGenerated]
		get
		{
			return _003CLoginLogs_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLoginLogs_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Alert> Alerts
	{
		[CompilerGenerated]
		get
		{
			return _003CAlerts_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAlerts_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<Expense> Expenses
	{
		[CompilerGenerated]
		get
		{
			return _003CExpenses_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CExpenses_003Ek__BackingField = value;
		}
	}

	public User()
	{
		_003CUsername_003Ek__BackingField = string.Empty;
		_003CPasswordHash_003Ek__BackingField = string.Empty;
		_003CFullName_003Ek__BackingField = string.Empty;
		_003CIsActive_003Ek__BackingField = true;
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CSales_003Ek__BackingField = (System.Collections.Generic.ICollection<Sale>)new List<Sale>();
		_003CCashRegisterSessions_003Ek__BackingField = (System.Collections.Generic.ICollection<CashRegisterSession>)new List<CashRegisterSession>();
		_003CInventoryMovements_003Ek__BackingField = (System.Collections.Generic.ICollection<InventoryMovement>)new List<InventoryMovement>();
		_003CLoginLogs_003Ek__BackingField = (System.Collections.Generic.ICollection<LoginLog>)new List<LoginLog>();
		_003CAlerts_003Ek__BackingField = (System.Collections.Generic.ICollection<Alert>)new List<Alert>();
		_003CExpenses_003Ek__BackingField = (System.Collections.Generic.ICollection<Expense>)new List<Expense>();
		base._002Ector();
	}
}
