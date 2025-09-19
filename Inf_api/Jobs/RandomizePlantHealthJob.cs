using Inf_api.Helpers;
using Inf_api.SignalrRNotifications;
using Inf_Data;
using Inf_Data.Entities;
using Inf_Repository.Repository.Plant;
using Microsoft.EntityFrameworkCore;

namespace Inf_api.Jobs
{
    public class RandomizePlantHealthJob : IRandomizePLantHealthJob
    {
        private readonly InfDbContext _context;
        private readonly IPlantRepository _plantRepository;
        private readonly IPlantHelper _plantHelper;
        private readonly ISignalRService _signalRService;

        public RandomizePlantHealthJob(
            InfDbContext context,
            IPlantRepository plantRepository,
            IPlantHelper plantHelper,
            ISignalRService signalRService)
        {
            _context = context;
            _plantRepository = plantRepository;
            _plantHelper = plantHelper;
            _signalRService = signalRService;
        }

        public async Task RandomlyAdjustStateOfThePlant()
        {
            var plants = await _plantRepository
                .GetAllWithTracking()
                .Include(x => x.PlantSpecification)
                .ToListAsync();

            var random = new Random();

            foreach (var plant in plants)
            {
                // Adjust plant's current state randomly
                plant.CurrentWattering = _plantHelper.AdjustRandomly((float)plant.PlantSpecification.Wattering, random);
                plant.CurrentTemperature = _plantHelper.AdjustRandomly((float)plant.PlantSpecification.Temperature, random);
                plant.CurrentIllumination = _plantHelper.AdjustRandomly((float)plant.PlantSpecification.Illumination, random);

                // Check plant health
                var healthStatus = _plantHelper.CheckPlantHealth(plant);

                // If the plant is in warning or dead status, create a notification
                if (healthStatus != NotificationType.Normal)
                {
                    var notification = new Notifications
                    {
                        UserId = plant.UserId,
                        Text = healthStatus == NotificationType.Warning
                            ? $"Warning: Plant {plant.LocalName} with code {plant.Code}  is close to dying."
                            : $"Error: Plant {plant.LocalName} with code {plant.Code} has died.",
                        Date = DateTime.UtcNow,
                        IsRead = false,
                        NotificaitonType= (Inf_Data.Enums.NotificaitonType)healthStatus
                    };
                    if(healthStatus == NotificationType.Error)
                    {
                        var user = await _context.Users.FindAsync(plant.UserId);
                        if (user != null)
                        {
                            user.NumberOfDeadPlants++;
                        }
                    }
                    // Send real-time notification
                    await _signalRService.NotifyUser(notification);

                    // Save notification to the database
                    _context.Notifications.Add(notification);
                }
            }

            await _context.SaveChangesAsync();
        }


    }
}
