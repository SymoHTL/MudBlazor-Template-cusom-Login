namespace Domain.Repositories.Interfaces;

public interface IUserState {
    bool HasLoaded { get; set; }
    User? User { get; set; }

    event Action? DataChange;
    void NotifyDataChange();
}