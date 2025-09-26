using Inf_Data;
using Inf_Repository.Repository.Generic;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Inf_Data.Entities.Plant> BuyPlant(int specieId, int userId)
        {
            try
            {
                var result = await _context.PlantSpecifications
                    .Where(item => item.Id == specieId)
                    .FirstOrDefaultAsync();

                if (result is null) return null;
                if (result.Count == 0)
                    throw new Exception("No more plant species left");
                // Initialize Random instance
                Random random = new Random();

                var plant = new Inf_Data.Entities.Plant()
                {
                    LatinName = result.SpecieName,
                    LocalName = result.SpecieName,
                    CurrentIllumination = GenerateRandomValue((float)result.Illumination, random),
                    PlantSpecificationId = result.Id,
                    PlantSpecification = result,
                    CurrentWattering = GenerateRandomValue((double)result.Wattering, random),
                    CurrentTemperature = GenerateRandomValue((float)result.Temperature, random),
                    UserId = userId
                };

                result.Count -= 1; // Reduce the count of available plants in specifications

                await _context.Plants.AddAsync(plant);
                await _context.SaveChangesAsync();
                return plant;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        // Helper method to generate a random value within ±5 range
        private static float GenerateRandomValue(float baseValue, Random random)
        {
            float min = baseValue - 5;
            float max = baseValue + 5;
            return (float)(random.NextDouble() * (max - min) + min);
        }

        private static double GenerateRandomValue(double baseValue, Random random)
        {
            double min = baseValue - 5;
            double max = baseValue + 5;
            return random.NextDouble() * (max - min) + min;
        }

    }
}
