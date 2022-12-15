using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId {get;set;}
	[Required]
	public string Chef {get;set;}
	[Required]
	public string Name {get;set;}
	[Required]
	[Range(0,25000)]
	public int Calories {get;set;}
	[Required]
	[Range(0, 5)]	
	public int Tastiness {get;set;}
	[Required]
	public string Description {get;set;}
	[Required]
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime UpdatedAt { get; set; } = DateTime.Now;
}