using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpecificationPatternConsoleApp.Database.Entites;
using SpecificationPatternConsoleApp.Specifications;

namespace SpecificationPatternConsoleApp.Database.Repositories
{
    public interface ICarRepository
    {
        Task<List<CarEntity>> GetAllAsync(Specification<CarEntity> spec);
        Task<CarEntity> GetAsync(Guid carId);
    }

    public class CarRepository : ICarRepository
    {
        private readonly Context _context;

        public CarRepository()
        {
            var options = new DbContextOptionsBuilder<Context>()
                  .UseInMemoryDatabase("Cars")
                  .Options;

              _context = new Context(options);
        }

        public async Task<List<CarEntity>> GetAllAsync(Specification<CarEntity> spec)
        {
            IQueryable<CarEntity> cars = _context.Cars.AsNoTracking();

            if ( spec != null )
            {
                cars = cars.Where( spec.ToExpression() );            
            }

            return await cars.ToListAsync();
        }

        public async Task<CarEntity> GetAsync(Guid carId)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == carId);
        }
    }
}