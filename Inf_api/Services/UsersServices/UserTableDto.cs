namespace Inf_api.Services.UsersServices
{
    public class UserTableDto
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int NumberOfDeadPlants { get; set; }

        public bool IsBanned { get; set; }

        public bool IsApproved { get; set; }
    }
}
