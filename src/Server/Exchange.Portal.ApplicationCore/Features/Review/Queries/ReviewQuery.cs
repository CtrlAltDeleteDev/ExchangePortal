using Exchange.Portal.ApplicationCore.Common;

namespace Exchange.Portal.ApplicationCore.Features.Review.Queries;

public record ReviewQuery(IOffsetPagination Pagination) : IRequest<Result<IReadOnlyCollection<Models.Review>>>;