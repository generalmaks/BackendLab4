using System.ComponentModel.DataAnnotations;

namespace BackendLab3.DataAccess.Models;

public class Currency
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public int Code { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(5)]
    public string Symbol { get; set; }
}