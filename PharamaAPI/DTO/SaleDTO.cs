using System;

namespace PharmaAPI.DTO;

public class SaleDTO
{
    public int SalesId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public string? FilePath { get; set; }
    public int DrugId { get; set; }
}

public class CreateSaleDTO
{
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public string? FilePath { get; set; }
    public int DrugId { get; set; }
}

public class UpdateSaleDTO
{
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public string? FilePath { get; set; }
    public int DrugId { get; set; }
}
