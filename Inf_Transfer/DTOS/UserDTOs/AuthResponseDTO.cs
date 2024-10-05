namespace Inf_Transfer.DTOS.UserDTOs
{
    public class AuthResponseDTO
    {
    public string Token { get; set; }

    public int UserId { get; set; }

    public string Role { get; set; }
    
    public AuthResponseDTO(string token, int userId,string Role)
    {
        this.Token=token;
        this.UserId=userId;
        this.Role=Role;
    }
    }
    
}