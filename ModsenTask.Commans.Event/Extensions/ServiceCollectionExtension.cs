using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ModsenTask.Commans.Event.Abstractions.Inerfaces;
using ModsenTask.Commans.Event.Commands;
using ModsenTask.Commans.Event.Commands.AddNewEvent;
using ModsenTask.Commans.Event.Commands.DeleteEvent;
using ModsenTask.Commans.Event.Commands.UpdateDateEvent;

namespace ModsenTask.Commans.Event.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        => services
            .AddScoped<ICommandHandler<AddNewEventCommand>, AddNewEventCommandHandler>()
            .AddScoped<ICommandHandler<DeleteEventCommand>, DeleteEventCommandHandler>()
            .AddScoped<ICommandHandler<UpdateEventCommand>, UpdateDateEventCommandHandler>()
            .AddScoped<ICommandDispatcher, CommandDispatcher>();

    public static IServiceCollection AddCommandMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}