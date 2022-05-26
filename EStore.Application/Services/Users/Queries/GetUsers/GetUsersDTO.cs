namespace EStore.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
