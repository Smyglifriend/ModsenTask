using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Domain.Abstraction.Models;

public abstract class BaseEntity : IEntity
{
    public long Id { get; set; }
}