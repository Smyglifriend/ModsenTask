using Microsoft.Extensions.DependencyInjection;
using ModsenTask.Queries.Event.Abstrations.Interfaces;
using ModsenTask.Queries.Event.Queries;
using ModsenTask.Queries.Event.Queries.GetAllEvents;
using ModsenTask.Queries.Event.Queries.GetEventById;

namespace ModsenTask.Queries.Event.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        => services
            .AddScoped<IQueryHandler<GetEventByIdQuery>, GetEventByIdQueryHandler>()
            .AddScoped<IQueryHandler<GetAllEventsQuery>, GetAllEventsQueryHandler>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>();
}