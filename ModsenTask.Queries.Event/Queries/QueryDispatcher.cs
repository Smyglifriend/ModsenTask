using ModsenTask.Queries.Event.Abstrations.Interfaces;
using ModsenTask.Queries.Event.Abstrations.Models;

namespace ModsenTask.Queries.Event.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _service;


    public QueryDispatcher(IServiceProvider service)
    {
        _service = service;
    }


    public Task<IEnumerable<IModelDto>> Send<T>(T query)
        where T : IQuery
    {
        var handler = _service.GetService(typeof(IQueryHandler<T>));
        if (handler != null)
            return ((IQueryHandler<T>)handler).Handle(query); 

        throw new Exception($"Query doesn't have any handler {query.GetType().Name}");
    }
}