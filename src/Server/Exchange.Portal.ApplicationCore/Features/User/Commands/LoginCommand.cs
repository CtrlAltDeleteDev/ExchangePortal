using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.User.Commands;

public record LoginCommand(string Login, string Password) : IRequest<Result<Unit>>;