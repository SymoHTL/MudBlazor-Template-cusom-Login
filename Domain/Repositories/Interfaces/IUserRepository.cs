namespace Domain.Repositories.Interfaces; 

public interface IUserRepository : IRepository<User> {
    Task<User?> ReadAuthGraphAsync(int id, CancellationToken ct);
    Task<User?> ReadAuthGraphAsync(string email, CancellationToken ct);
    Task<bool> CheckLoginDetailsAsync(string email, string password, CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ctsToken);
    Task RegisterAsync(User regUser, CancellationToken ct);
}