using ModsenTask.Queries.Event.Abstrations.Models;

namespace ModsenTask.Queries.Event.Abstrations.Interfaces;

public interface IQuery
{
}

public interface IQueryHandler<T> 
    where T : IQuery
{
    Task<IEnumerable<IModelDto>> Handle(T query);
}

public interface IQueryDispatcher
{
    Task<IEnumerable<IModelDto>> Send<T>(T query) where T : IQuery;
}