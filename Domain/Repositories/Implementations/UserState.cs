namespace Domain.Repositories.Implementations;

public class UserState : IUserState {
    // display loading screen
    public bool HasLoaded { get; set; } = false;

    public User? User { get; set; }

    public event Action? DataChange;

    public void NotifyDataChange() => DataChange?.Invoke();
    

}