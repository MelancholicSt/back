using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Data.dao;
/// <summary>
/// Gets geo of any account, not depended on role
/// </summary>
public class AccountGeolocation
{
    public ulong Id { get; set; }
    
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? LocalAddress { get; set; } 
    public string? FullAddress { get; set; }
}