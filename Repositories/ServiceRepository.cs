using HairdresserAPI.DatabaseContext;
using HairdresserAPI.HairdresserDomain.Aggregate;
using HairdresserAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAPI.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddService(Service service)
    {
        await _context.Service.AddAsync(service);
        await _context.SaveChangesAsync();
    }
}