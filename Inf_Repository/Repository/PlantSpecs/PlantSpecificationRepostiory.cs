using Inf_Data;
using Inf_Data.Entities;
using Inf_Repository.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Repository.Repository.PlantSpecs
{
    public class PlantSpecificationRepostiory : GenericRepository<PlantSpecifications>, IPlantSpecificationRepository
    {
        private readonly InfDbContext _context;
        public PlantSpecificationRepostiory(InfDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
