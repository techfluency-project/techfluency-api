using MongoDB.Driver;
using TechFluency.Models;
using TechFluency.Context;
using MongoDB.Bson;

namespace TechFluency.Repository
{
    public interface ITechFluencyRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Add(T entity);
        void Update(string id, T entity);
        void Delete(string id);

    }
}
