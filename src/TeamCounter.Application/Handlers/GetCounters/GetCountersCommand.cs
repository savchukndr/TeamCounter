using MediatR;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Handlers.GetCounters;

public record GetCountersCommand(Guid TeamId) : IRequest<IEnumerable<CounterSummaryDto>>;