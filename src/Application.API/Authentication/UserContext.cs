using System.Security.Authentication;
using System.Security.Claims;

namespace Application.API.Authentication;

public class UserContext
{
    private readonly string _nameIdentifier;
    private readonly string _userName;
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string _emailAddress;


    public UserContext(ClaimsPrincipal principal = null!)
    {
        if (!principal.Identity?.IsAuthenticated ?? true)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        this._nameIdentifier = principal.FindFirst("sub")?.Value ?? throw new NullReferenceException("Subject (sub) claim not found");

        this._userName = principal.FindFirst("preferred_username")?.Value ?? throw new NullReferenceException($"Preferred username claim not found for user {NameIdentifier}");

        this._firstName = principal.FindFirst("given_name")?.Value ?? throw new NullReferenceException($"Given name claim not found for user {NameIdentifier}");

        this._lastName = principal.FindFirst("family_name")?.Value ?? throw new NullReferenceException($"Family name claim not found for user {NameIdentifier}");

        this._emailAddress = principal.FindFirst("email")?.Value ?? throw new NullReferenceException($"Email claim not found for user {NameIdentifier}");
    }

    public string NameIdentifier => this._nameIdentifier;
    public string UserName => this._userName;
    public string FirstName => this._firstName;
    public string LastName => this._lastName;
    public string EmailAddress => this._emailAddress;
}