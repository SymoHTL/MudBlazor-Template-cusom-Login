using Domain.Engines;
using Model.Entities.Roles;

namespace Domain.Repositories.Implementations; 

public class UserRepository : ARepository<User>, IUserRepository{
    public UserRepository(ModelDbContext context) : base(context) {
    }

    // read the data needed for app to login/identify user
    
    /// <summary>read the data needed for app to login/identify user with id</summary>
    /// <param name="id">the id of the user to get</param>
    /// <param name="ct">A CancellationToken to cancel the task if requested</param>
    /// <returns>User object</returns>
    public async Task<User?> ReadAuthGraphAsync(int id, CancellationToken ct) =>
        await Set
            .Include(u => u.RoleClaims) // populate the RoleClaims List (Join Table)
            .ThenInclude(rc => rc.Role) // populate the Role (Role Table)
            .AsSplitQuery() // split the query to avoid the cartesian product https://en.wikipedia.org/wiki/Cartesian_product
            .FirstOrDefaultAsync(u => u.Id == id, ct); // get the user with the given id
    
    /// <summary>read the data needed for app to login/identify user with id</summary>
    /// <param name="email">the email of the user to get</param>
    /// <param name="ct">A CancellationToken to cancel the task if requested</param>
    /// <returns>User object</returns>
    public async Task<User?> ReadAuthGraphAsync(string email, CancellationToken ct) =>
        await Set
            .Include(u => u.RoleClaims) // populate the RoleClaims List (Join Table)
            .ThenInclude(rc => rc.Role) // populate the Role (Role Table)
            .AsSplitQuery() // split the query to avoid the cartesian product https://en.wikipedia.org/wiki/Cartesian_product
            .FirstOrDefaultAsync(u => u.Email == email, ct); // get the user with the given email

    public async Task<bool> CheckLoginDetailsAsync(string email, string password, CancellationToken ct) {
        // Hash the password
        password = CryptoEngine.Encrypt(password);
        // Check if the user exists
        var matchedAccounts = await Set
            .Where(u => u.Email == email && u.Password == password)
            .ToListAsync(ct);
        // Return true if the user exists
        return matchedAccounts.Any();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ctsToken) {
        return await Set.FirstOrDefaultAsync(u => u.Email == email, ctsToken);
    }

    public async Task RegisterAsync(User regUser, CancellationToken ct) {
        // Hash the password
        regUser.Password = CryptoEngine.Encrypt(regUser.Password);
        // Add the default role
        regUser.RoleClaims.Add(new RoleClaim {
            RoleId = 1, // default role
            User = regUser
        });
        // Add the user to the database
        await CreateAsync(regUser, ct);
    }
}