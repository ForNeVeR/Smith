namespace Smith.Services.Authentication
{
    public enum AuthenticationState
    {
        WaitingHomeserver,
        WaitingLoginAndPassword,
        Authenticating,
        Authenticated
    }
}
