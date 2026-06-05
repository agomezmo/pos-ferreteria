using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("CatUsosCfdi")]
public class CatUsoCfdi
{
	[Key]
	[StringLength(10)]
	public string Id { get; set; }

	[Required]
	[StringLength(200)]
	public string Description { get; set; }

	public bool IsActive { get; set; }
}
