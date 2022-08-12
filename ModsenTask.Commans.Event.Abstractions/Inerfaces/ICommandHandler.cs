namespace ModsenTask.Commans.Event.Abstractions.Inerfaces
{
    public interface ICommand
    {}

    public interface ICommandHandler<T> 
        where T : ICommand
    {
        Task Handle(T command);
    }

    public interface ICommandDispatcher
    {
        Task Send<T>(T command) where T : ICommand;
    }
}
