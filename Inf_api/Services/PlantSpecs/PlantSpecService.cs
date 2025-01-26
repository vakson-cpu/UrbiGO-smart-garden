using Inf_api.Services.PlantSpecs;
using Inf_Data.Entities;
using Inf_Repository.Repository.PlantSpecs;
using Inf_Transfer.utils;
using Microsoft.EntityFrameworkCore;

public class PlantSpecService : IPlantSpecService
{
    private readonly IPlantSpecificationRepository _plantSpecificationRepository;

    public PlantSpecService(IPlantSpecificationRepository plantSpecificationRepository)
    {
        _plantSpecificationRepository = plantSpecificationRepository;
    }

    public async Task<QueryResponse<PlantSpecifications>> GetAllPlantSpecs(string search, int pageSize = 10, int page = 1)
    {
        var query =  _plantSpecificationRepository.GetAll();

        // Apply search filter
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(plant => plant.SpecieName.Contains(search));
        }

        // Get the total count before applying paging
        var totalCount = await query.CountAsync();

        // Apply paging
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        // Execute the query and get the result
        var plants = await query.ToListAsync();

        // Prepare the result
        var result = new QueryResponse<PlantSpecifications>()
        {
            Items = plants,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page
        };

        return result;
    
    }

}
