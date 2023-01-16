namespace Exchange.Portal.ApplicationCore.Features.User.Commands;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<Unit>>
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginCommandHandler(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<Result<Unit>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        IdentityUser? user = await _signInManager.UserManager.FindByNameAsync(request.Login);
        if (user is null)
        {
            ValidationException validation = new ValidationException(new[]
                { new ValidationFailure(string.Empty, "Invalid login or password.") });
            return new Result<Unit>(validation);
        }

        SignInResult isPasswordCorrect =
            await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!isPasswordCorrect.Succeeded)
        {
            ValidationException validation = new ValidationException(new[]
                { new ValidationFailure(string.Empty, "Invalid login or password.") });
            return new Result<Unit>(validation);
        }

        await _signInManager.PasswordSignInAsync(request.Login, request.Password, false, false);

        return Unit.Value;
    }
}