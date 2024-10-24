using Inf_Data;
using Inf_Data.Entities;
using Inf_Repository.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Repository.Repository.Plant
{
    public class PlantRepository : GenericRepository<Inf_Data.Entities.Plant>, IPlantRepository
    {
        public InfDbContext _context { get; set; }

        public PlantRepository(InfDbContext context):base(context)
        {
            _context = context;            
        }

        public async Task<List<Inf_Data.Entities.Plant>> GetAllPlants()
        {
            try
            {
                var result = GetAll();
                return await result.ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
    }
}
