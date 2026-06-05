using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("CatFormasPago")]
public class CatFormaPago
{
	[Key]
	[StringLength(10)]
	public string Id { get; set; }

	[Required]
	[StringLength(200)]
	public string Description { get; set; }

	public bool IsActive { get; set; }
}
