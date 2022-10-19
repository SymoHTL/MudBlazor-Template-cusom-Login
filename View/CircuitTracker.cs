namespace View;

public class CircuitTracker : CircuitHandler {
    private readonly ProtectedLocalStorage _local;
    private readonly NavigationManager _navigationManager;
    private readonly IThemeHandler _theme;
    private readonly IUserRepository _userRepo;
    private readonly IUserState _userState;
    private readonly ILogger<CircuitTracker> _logger;

    public CircuitTracker(IThemeHandler theme, ProtectedLocalStorage local, NavigationManager navigationManager, IUserRepository userRepo, IUserState userState, ILogger<CircuitTracker> logger) {
        _theme = theme;
        _local = local;
        _navigationManager = navigationManager;
        _userRepo = userRepo;
        _userState = userState;
        _logger = logger;
    }

    public override async Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken) {
        await GetStuff(cancellationToken);
    }

    private async Task GetStuff(CancellationToken ct) {
        try {
            var theme = (await _local.GetAsync<Theme>("Theme")).Value;
            if (theme is not null) _theme.UpdateAll(theme);

            // user id / account section
            var id = (await _local.GetAsync<string>("Id")).Value;
            if (id is null) {
                _userState.HasLoaded = true;
                return;
            }

            var usr = await _userRepo.ReadAuthGraphAsync(int.Parse(id), ct);

            if (usr is null) {
                await _local.DeleteAsync("Id");
                _userState.HasLoaded = true;
                return;
            }

            _userState.User = usr;
            // end user id / account section
        }
        #if DEBUG
        catch (CryptographicException e) {
            _logger.Log(LogLevel.Error, e, "Failed to decrypt local storage");
            _logger.Log(LogLevel.Error, "Clearing local storage");
            await _local.DeleteAsync("Theme");
            await _local.DeleteAsync("Id");
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }
        #endif
        catch (TaskCanceledException) {
            _logger.Log(LogLevel.Error, "Task was cancelled");
        }
        catch (OperationCanceledException) {
            _logger.Log(LogLevel.Error, "Operation was cancelled");
        }
        catch (Exception e) {
            _logger.Log(LogLevel.Error, e, "Failed to read local storage");
        }
        finally {
            _userState.HasLoaded = true;
        }
    }
}