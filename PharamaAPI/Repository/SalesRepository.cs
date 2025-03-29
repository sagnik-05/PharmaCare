using PharmaAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaAPI.DTO;
using PharmaAPI.Interface;
using PharmaAPI.Models;

namespace PharmaAPI.Repository;

public class SalesRepository : ISalesRepository
{
    private readonly ApplicationDbContext _context;

    public SalesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleDTO>> GetSalesAsync()
    {
        var sales = await _context.Sales.ToListAsync();
        return sales.Select(s => new SaleDTO
        {
            SalesId = s.SalesId,
            Date = s.Date,
            TotalSales = s.TotalSales,
            FilePath = s.FilePath,
            DrugId = s.DrugId
        }).ToList();
    }

    public async Task<SaleDTO> GetSaleAsync(int salesId)
    {
        var sale = await _context.Sales.FindAsync(salesId);
        if (sale == null)
            return null;

        return new SaleDTO
        {
            SalesId = sale.SalesId,
            Date = sale.Date,
            TotalSales = sale.TotalSales,
            FilePath = sale.FilePath,
            DrugId = sale.DrugId
        };
    }

    public async Task<SaleDTO> CreateSaleAsync(CreateSaleDTO createSaleDTO)
    {
        var sale = new Sales
        {
            Date = createSaleDTO.Date,
            TotalSales = createSaleDTO.TotalSales,
            FilePath = createSaleDTO.FilePath,
            DrugId = createSaleDTO.DrugId
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return new SaleDTO
        {
            SalesId = sale.SalesId,
            Date = sale.Date,
            TotalSales = sale.TotalSales,
            FilePath = sale.FilePath,
            DrugId = sale.DrugId
        };
    }

    public async Task<SaleDTO> UpdateSaleAsync(int salesId, UpdateSaleDTO updateSaleDTO)
    {
        var sale = await _context.Sales.FindAsync(salesId);
        if (sale == null)
            return null;

        sale.Date = updateSaleDTO.Date;
        sale.TotalSales = updateSaleDTO.TotalSales;
        sale.FilePath = updateSaleDTO.FilePath;
        sale.DrugId = updateSaleDTO.DrugId;

        _context.Entry(sale).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SaleExists(salesId))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return new SaleDTO
        {
            SalesId = sale.SalesId,
            Date = sale.Date,
            TotalSales = sale.TotalSales,
            FilePath = sale.FilePath,
            DrugId = sale.DrugId
        };
    }

    public async Task<bool> DeleteSaleAsync(int salesId)
    {
        var sale = await _context.Sales.FindAsync(salesId);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool SaleExists(int salesId)
    {
        return _context.Sales.Any(e => e.SalesId == salesId);
    }
}