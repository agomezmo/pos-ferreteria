using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("FacturaItems")]
public class FacturaItem
{
	[Key]
	public int Id { get; set; }

	public int FacturaId { get; set; }

	[ForeignKey("FacturaId")]
	public Factura Factura { get; set; }

	public int? ProductoId { get; set; }

	[ForeignKey("ProductoId")]
	public Product? Producto { get; set; }

	[Required]
	[StringLength(10)]
	public string ClaveProdServ { get; set; }

	[Required]
	[StringLength(10)]
	public string ClaveUnidad { get; set; }

	[Required]
	[StringLength(200)]
	public string Descripcion { get; set; }

	[Column(TypeName = "decimal(18,6)")]
	public decimal Cantidad { get; set; }

	[StringLength(20)]
	public string Unidad { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal ValorUnitario { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Importe { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Descuento { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Iva { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal IvaTasa { get; set; }
}
