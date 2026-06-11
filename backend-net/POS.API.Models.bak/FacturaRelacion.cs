using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("FacturaRelaciones")]
public class FacturaRelacion
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

	public int FacturaId
	{
		[CompilerGenerated]
		get
		{
			return _003CFacturaId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFacturaId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("FacturaId")]
	public Factura Factura
	{
		[CompilerGenerated]
		get
		{
			return _003CFactura_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFactura_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(10)]
	public string TipoRelacion
	{
		[CompilerGenerated]
		get
		{
			return _003CTipoRelacion_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTipoRelacion_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(50)]
	public string UuidRelacionado
	{
		[CompilerGenerated]
		get
		{
			return _003CUuidRelacionado_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUuidRelacionado_003Ek__BackingField = value;
		}
	}

	public FacturaRelacion()
	{
		_003CTipoRelacion_003Ek__BackingField = string.Empty;
		_003CUuidRelacionado_003Ek__BackingField = string.Empty;
		base._002Ector();
	}
}
