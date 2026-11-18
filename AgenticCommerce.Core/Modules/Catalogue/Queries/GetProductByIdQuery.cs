using AgenticCommerce.Core.Modules.Catalogue.Dtos;
using MediatR;

namespace AgenticCommerce.Core.Modules.Catalogue.Queries;

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductSummary>;
