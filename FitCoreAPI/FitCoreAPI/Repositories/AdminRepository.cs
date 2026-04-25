using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class AdminRepository: IAdminRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public AdminRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<AdminModel?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId, ct);
    }

    public async Task<List<AdminModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Admins.Include(a => a.User).ToListAsync(ct);
    }

    public async Task CreateAsync(AdminModel admin, CancellationToken ct)
    {
        _dbContext.Admins.Add(admin);
        await _dbContext.SaveChangesAsync(ct);
    }
}