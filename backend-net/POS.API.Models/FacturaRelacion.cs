using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("FacturaRelaciones")]
public class FacturaRelacion
{
	[Key]
	public int Id { get; set; }

	public int FacturaId { get; set; }

	[ForeignKey("FacturaId")]
	public Factura Factura { get; set; }

	[Required]
	[StringLength(10)]
	public string TipoRelacion { get; set; }

	[Required]
	[StringLength(50)]
	public string UuidRelacionado { get; set; }
}
