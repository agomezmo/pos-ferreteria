using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("SystemSettings")]
public class SystemSetting
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Key { get; set; }

	[Required]
	public string Value { get; set; }

	[StringLength(200)]
	public string? Description { get; set; }
}
