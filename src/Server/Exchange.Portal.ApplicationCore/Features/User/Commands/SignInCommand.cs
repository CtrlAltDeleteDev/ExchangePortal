namespace Exchange.Portal.ApplicationCore.Features.User.Commands;

public static class SignInCommand
{
    public record User(string Login, string Password) : IRequest<Unit>;
    
    public class Handler : IRequestHandler<User, Unit>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public Handler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(User request, CancellationToken cancellationToken)
        {
            IdentityUser? user = await _signInManager.UserManager.FindByNameAsync(request.Login);
            if (user is null)
            {
                return Unit.Value;
            }

            SignInResult isPasswordCorrect =
                await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (isPasswordCorrect.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(request.Login, request.Password, false, false);
            }

            return Unit.Value;
        }
    }
}