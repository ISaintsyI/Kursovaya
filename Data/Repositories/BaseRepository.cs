using Data.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IHaveId
    {
        private readonly DbSet<TEntity> _set;
        private readonly IAppDbContext _context;

        public BaseRepository(IAppDbContext context)
        {
            _set = context.Set<TEntity>();
            _context = context;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var entry = await _set.AddAsync(entity)
                ?? throw new InvalidOperationException("Cannot add entity");

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<TEntity> Delete(Guid id)
        {
            var entity = await _set.FindAsync(id)
                ?? throw new InvalidOperationException("Cannot find entity");
            var entry = _set.Remove(entity)
                ?? throw new InvalidOperationException("Cannot delete entity");

            await _context.SaveChangesAsync();

            return entry.Entity;
        }
    }

    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private readonly DbSet<Car> _cars;

        public CarRepository(IAppDbContext context) : base(context)
        {
            _cars = context.Set<Car>();
        }

        public async Task<Car?> GetByPlateNumber(string plateNumber)
        {
            var car = await _cars.FirstOrDefaultAsync(x => x.PlateNumber == plateNumber);

            return car;
        }
    }
}
