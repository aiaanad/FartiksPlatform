using MediatR;
using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetRoundHistory;

public record GetRoundHistoryQuery(Guid PlayerId) : IRequest<Result<GetRoundHistoryResponse>>;
