using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("CatClavesProdServ")]
public class CatClaveProdServ
{
	[Key]
	[StringLength(10)]
	public string Id { get; set; }

	[Required]
	[StringLength(300)]
	public string Description { get; set; }

	public bool IsActive { get; set; }
}
