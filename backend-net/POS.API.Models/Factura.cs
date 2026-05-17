using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace POS.API.Models;

[Table("Facturas")]
public class Factura
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
	public string Uuid
	{
		[CompilerGenerated]
		get
		{
			return _003CUuid_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUuid_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(20)]
	public string Serie
	{
		[CompilerGenerated]
		get
		{
			return _003CSerie_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSerie_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(20)]
	public string Folio
	{
		[CompilerGenerated]
		get
		{
			return _003CFolio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFolio_003Ek__BackingField = value;
		}
	}

	public int SaleId
	{
		[CompilerGenerated]
		get
		{
			return _003CSaleId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSaleId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("SaleId")]
	public Sale Sale
	{
		[CompilerGenerated]
		get
		{
			return _003CSale_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSale_003Ek__BackingField = value;
		}
	}

	public int? CustomerId
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomerId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomerId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("CustomerId")]
	public Customer? Customer
	{
		[CompilerGenerated]
		get
		{
			return _003CCustomer_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCustomer_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal
	{
		[CompilerGenerated]
		get
		{
			return _003CSubtotal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSubtotal_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Total
	{
		[CompilerGenerated]
		get
		{
			return _003CTotal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CTotal_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Iva
	{
		[CompilerGenerated]
		get
		{
			return _003CIva_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIva_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal IvaRetenido
	{
		[CompilerGenerated]
		get
		{
			return _003CIvaRetenido_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIvaRetenido_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal IsrRetenido
	{
		[CompilerGenerated]
		get
		{
			return _003CIsrRetenido_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsrRetenido_003Ek__BackingField = value;
		}
	}

	[Column(TypeName = "decimal(18,2)")]
	public decimal Descuento
	{
		[CompilerGenerated]
		get
		{
			return _003CDescuento_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDescuento_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(10)]
	public string FormaPago
	{
		[CompilerGenerated]
		get
		{
			return _003CFormaPago_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFormaPago_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(10)]
	public string MetodoPago
	{
		[CompilerGenerated]
		get
		{
			return _003CMetodoPago_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CMetodoPago_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(10)]
	public string UsoCfdi
	{
		[CompilerGenerated]
		get
		{
			return _003CUsoCfdi_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CUsoCfdi_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(200)]
	public string LugarExpedicion
	{
		[CompilerGenerated]
		get
		{
			return _003CLugarExpedicion_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CLugarExpedicion_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(10)]
	public string RegimenFiscal
	{
		[CompilerGenerated]
		get
		{
			return _003CRegimenFiscal_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRegimenFiscal_003Ek__BackingField = value;
		}
	}

	[StringLength(500)]
	public string? XmlContent
	{
		[CompilerGenerated]
		get
		{
			return _003CXmlContent_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CXmlContent_003Ek__BackingField = value;
		}
	}

	[StringLength(500)]
	public string? PdfContent
	{
		[CompilerGenerated]
		get
		{
			return _003CPdfContent_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPdfContent_003Ek__BackingField = value;
		}
	}

	[Required]
	[StringLength(20)]
	public string Status
	{
		[CompilerGenerated]
		get
		{
			return _003CStatus_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStatus_003Ek__BackingField = value;
		}
	}

	public int? CreatedByUserId
	{
		[CompilerGenerated]
		get
		{
			return _003CCreatedByUserId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCreatedByUserId_003Ek__BackingField = value;
		}
	}

	[ForeignKey("CreatedByUserId")]
	public User? CreatedByUser
	{
		[CompilerGenerated]
		get
		{
			return _003CCreatedByUser_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCreatedByUser_003Ek__BackingField = value;
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

	public System.DateTime? CancelledAt
	{
		[CompilerGenerated]
		get
		{
			return _003CCancelledAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCancelledAt_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<FacturaItem> Items
	{
		[CompilerGenerated]
		get
		{
			return _003CItems_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CItems_003Ek__BackingField = value;
		}
	}

	public System.Collections.Generic.ICollection<FacturaRelacion> Relaciones
	{
		[CompilerGenerated]
		get
		{
			return _003CRelaciones_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRelaciones_003Ek__BackingField = value;
		}
	}

	public Factura()
	{
		_003CUuid_003Ek__BackingField = string.Empty;
		_003CSerie_003Ek__BackingField = string.Empty;
		_003CFolio_003Ek__BackingField = string.Empty;
		_003CFormaPago_003Ek__BackingField = string.Empty;
		_003CMetodoPago_003Ek__BackingField = string.Empty;
		_003CUsoCfdi_003Ek__BackingField = string.Empty;
		_003CLugarExpedicion_003Ek__BackingField = string.Empty;
		_003CRegimenFiscal_003Ek__BackingField = string.Empty;
		_003CStatus_003Ek__BackingField = "Pending";
		_003CCreatedAt_003Ek__BackingField = System.DateTime.get_UtcNow();
		_003CItems_003Ek__BackingField = (System.Collections.Generic.ICollection<FacturaItem>)new List<FacturaItem>();
		_003CRelaciones_003Ek__BackingField = (System.Collections.Generic.ICollection<FacturaRelacion>)new List<FacturaRelacion>();
		base._002Ector();
	}
}
