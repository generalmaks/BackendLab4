using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendLab3.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = null!;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    
    public int? CurrencyId { get; set; }
    
    [ForeignKey(nameof(CurrencyId))]
    public Currency? Currency { get; set; }
}