using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendLab3.DataAccess.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;
    
    [Required]
    [EmailAddress, MaxLength(50)]
    public string Email { get; set; } = null!;

    [Required] 
    [MaxLength(200)] 
    public string HashedPassword { get; set; } = null!;

    [Required]
    public int DefaultCurrencyId { get; set; }
    
    [ForeignKey(nameof(DefaultCurrencyId))]
    public Currency DefaultCurrency { get; set; } = null!;

    public ICollection<Expense>? Expenses { get; } = [];
}