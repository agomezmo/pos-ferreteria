using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("daily")]
    public async Task<ActionResult<DailyReportDTO>> GetDailyReport([FromQuery] DateTime? date)
    {
        var reportDate = date ?? DateTime.UtcNow.Date;
        var report = await _reportService.GetDailyReportAsync(reportDate);
        return Ok(report);
    }

    [HttpGet("top-products")]
    public async Task<ActionResult<List<TopProductDTO>>> GetTopProducts([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int top = 10)
    {
        var start = startDate ?? DateTime.UtcNow.AddMonths(-1);
        var end = endDate ?? DateTime.UtcNow;
        var products = await _reportService.GetTopProductsAsync(start, end, top);
        return Ok(products);
    }

    [HttpGet("inventory")]
    public async Task<ActionResult<InventoryReportDTO>> GetInventoryReport()
    {
        var report = await _reportService.GetInventoryReportAsync();
        return Ok(report);
    }
}
