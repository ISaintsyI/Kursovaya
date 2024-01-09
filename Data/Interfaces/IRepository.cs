using Domain.Interfaces;
using Domain.Models;

namespace Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IHaveId
    { 
        public Task<TEntity> Create(TEntity entity);
        public Task<TEntity> Delete(Guid id);
    }

    public interface ICarRepository : IRepository<Car>
    {
        public Task<Car?> GetByPlateNumber(string plateNumber);
    }
}
