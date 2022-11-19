using Licenta1.Contracts;
using Licenta1.Data;

namespace Licenta1.Repository
{


    public class StationRepository : GenericRepository<Station>, IStationsRepository
    {
        
        public StationRepository(ApplicationDbContext context) : base(context)
        { 
            
        }


    }
}
