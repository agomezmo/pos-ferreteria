using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.API.Data;
using POS.API.DTOs;
using POS.API.Models;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CompanyController : ControllerBase
{
    private readonly AppDbContext _context;

    public CompanyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CompanyInfoDTO>> GetCompanyInfo()
    {
        var company = await _context.CompanyInfos.FirstOrDefaultAsync();
        if (company == null)
        {
            return Ok(new CompanyInfoDTO
            {
                Name = "Mi Empresa",
                Address = "",
                Phone = "",
                Email = "",
                CodigoPostal = ""
            });
        }
        return Ok(MapToDTO(company));
    }

    [HttpPut]
    public async Task<ActionResult<CompanyInfoDTO>> UpdateCompanyInfo([FromBody] UpdateCompanyInfoRequest request)
    {
        var company = await _context.CompanyInfos.FirstOrDefaultAsync();
        if (company == null)
        {
            company = new CompanyInfo
            {
                Name = request.Name ?? "Mi Empresa"
            };
            _context.CompanyInfos.Add(company);
        }
        else
        {
            if (request.Name != null) company.Name = request.Name;
        }

        if (request.BusinessName != null) company.BusinessName = request.BusinessName;
        if (request.Address != null) company.Address = request.Address;
        if (request.Phone != null) company.Phone = request.Phone;
        if (request.Email != null) company.Email = request.Email;
        if (request.LogoUrl != null) company.LogoUrl = request.LogoUrl;
        if (request.TaxId != null) company.TaxId = request.TaxId;
        if (request.ReceiptFooter != null) company.ReceiptFooter = request.ReceiptFooter;
        if (request.Slogan != null) company.Slogan = request.Slogan;
        if (request.CodigoPostal != null) company.CodigoPostal = request.CodigoPostal;

        await _context.SaveChangesAsync();
        return Ok(MapToDTO(company));
    }

    private static CompanyInfoDTO MapToDTO(CompanyInfo c)
    {
        return new CompanyInfoDTO
        {
            Id = c.Id,
            Name = c.Name,
            BusinessName = c.BusinessName,
            Address = c.Address,
            Phone = c.Phone,
            Email = c.Email,
            LogoUrl = c.LogoUrl,
            TaxId = c.TaxId,
            ReceiptFooter = c.ReceiptFooter,
            Slogan = c.Slogan,
            CodigoPostal = c.CodigoPostal
        };
    }
}
