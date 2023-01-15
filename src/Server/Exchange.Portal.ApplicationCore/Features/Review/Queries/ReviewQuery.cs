using Exchange.Portal.ApplicationCore.Common;
using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.Review.Queries;

public record ReviewQuery(IOffsetPagination Pagination) : IRequest<Result<IReadOnlyCollection<Models.Review>>>;