using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;
using POS.API.Models;

namespace POS.API.Services;

public class CashRegisterService
{
    private readonly AppDbContext _context;

    public CashRegisterService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CashRegisterDTO>> GetCashRegistersAsync()
    {
        return await _context.CashRegisters
            .Select(cr => new CashRegisterDTO
            {
                Id = cr.Id,
                Name = cr.Name,
                Location = cr.Location,
                IsActive = cr.IsActive,
                CreatedAt = cr.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<CashRegisterDTO> CreateCashRegisterAsync(CreateCashRegisterRequest request)
    {
        var entity = new CashRegister
        {
            Name = request.Name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _context.CashRegisters.Add(entity);
        await _context.SaveChangesAsync();

        return new CashRegisterDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task<CashRegisterSessionDTO?> OpenSessionAsync(OpenSessionRequest request, int userId)
    {
        var activeSession = await _context.CashRegisterSessions
            .FirstOrDefaultAsync(s => s.CashRegisterId == request.CashRegisterId && s.IsActive);
        if (activeSession != null) return null;

        var session = new CashRegisterSession
        {
            CashRegisterId = request.CashRegisterId,
            UserId = userId,
            OpeningAmount = request.InitialAmount ?? 0,
            OpenedAt = DateTime.UtcNow,
            Status = "Open",
            IsActive = true
        };
        _context.CashRegisterSessions.Add(session);
        await _context.SaveChangesAsync();

        return new CashRegisterSessionDTO
        {
            Id = session.Id,
            CashRegisterId = session.CashRegisterId,
            UserId = session.UserId,
            OpeningAmount = session.OpeningAmount,
            OpenedAt = session.OpenedAt,
            Status = session.Status
        };
    }

    public async Task<CashRegisterSessionDTO?> CloseSessionAsync(int sessionId, CloseSessionRequest request, int userId)
    {
        var session = await _context.CashRegisterSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.IsActive);
        if (session == null) return null;

        session.ClosedAt = DateTime.UtcNow;
        session.ClosingAmount = request.ClosingAmount;
        session.IsActive = false;
        session.Status = "Closed";
        await _context.SaveChangesAsync();

        return new CashRegisterSessionDTO
        {
            Id = session.Id,
            CashRegisterId = session.CashRegisterId,
            UserId = session.UserId,
            OpeningAmount = session.OpeningAmount,
            ClosingAmount = session.ClosingAmount,
            OpenedAt = session.OpenedAt,
            ClosedAt = session.ClosedAt,
            Status = session.Status
        };
    }

    public async Task<CashRegisterSessionDTO?> GetCurrentSessionAsync(int cashRegisterId)
    {
        var session = await _context.CashRegisterSessions
            .Where(s => s.CashRegisterId == cashRegisterId && s.IsActive)
            .Select(s => new CashRegisterSessionDTO
            {
                Id = s.Id,
                CashRegisterId = s.CashRegisterId,
                UserId = s.UserId,
                OpeningAmount = s.OpeningAmount,
                OpenedAt = s.OpenedAt,
                Status = s.Status
            })
            .FirstOrDefaultAsync();

        return session;
    }

    public async Task<List<CashRegisterSessionDTO>> GetSessionsAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.CashRegisterSessions.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(s => s.OpenedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(s => s.OpenedAt <= endDate.Value);

        return await query
            .Select(s => new CashRegisterSessionDTO
            {
                Id = s.Id,
                CashRegisterId = s.CashRegisterId,
                UserId = s.UserId,
                OpeningAmount = s.OpeningAmount,
                ClosingAmount = s.ClosingAmount,
                OpenedAt = s.OpenedAt,
                ClosedAt = s.ClosedAt,
                Status = s.Status
            })
            .ToListAsync();
    }
}
