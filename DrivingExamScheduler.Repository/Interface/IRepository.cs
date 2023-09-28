using DrivingExamScheduler.Domain.Models;
using System.Linq.Expressions;

namespace DrivingExamScheduler.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T Get(Guid? id, params Expression<Func<T, object>>[] includes);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}