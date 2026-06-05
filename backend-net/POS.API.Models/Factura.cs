using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Facturas")]
public class Factura
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string Uuid { get; set; }

	[Required]
	[StringLength(20)]
	public string Serie { get; set; }

	[Required]
	[StringLength(20)]
	public string Folio { get; set; }

	public int SaleId { get; set; }

	[ForeignKey("SaleId")]
	public Sale Sale { get; set; }

	public int? CustomerId { get; set; }

	[ForeignKey("CustomerId")]
	public Customer? Customer { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Total { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Iva { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal IvaRetenido { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal IsrRetenido { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Descuento { get; set; }

	[Required]
	[StringLength(10)]
	public string FormaPago { get; set; }

	[Required]
	[StringLength(10)]
	public string MetodoPago { get; set; }

	[Required]
	[StringLength(10)]
	public string UsoCfdi { get; set; }

	[Required]
	[StringLength(200)]
	public string LugarExpedicion { get; set; }

	[Required]
	[StringLength(10)]
	public string RegimenFiscal { get; set; }

	[StringLength(500)]
	public string? XmlContent { get; set; }

	[StringLength(500)]
	public string? PdfContent { get; set; }

	[Required]
	[StringLength(20)]
	public string Status { get; set; }

	public int? CreatedByUserId { get; set; }

	[ForeignKey("CreatedByUserId")]
	public User? CreatedByUser { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.DateTime? CancelledAt { get; set; }

	public System.Collections.Generic.ICollection<FacturaItem> Items { get; set; }

	public System.Collections.Generic.ICollection<FacturaRelacion> Relaciones { get; set; }
}
