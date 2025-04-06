using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;
using System;
using System.Collections.Generic;

namespace TechFluency.Repository
{
    public class TechFluencyRepository<T> : ITechFluencyRepository<T> where T : class, IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public TechFluencyRepository(MongoDbContext context, string collectionName)
        {
            _collection = context.Database.GetCollection<T>(collectionName);
        }

        public void Add(T entity)
        {
            try
            {
                _collection.InsertOne(entity);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Erro ao inserir o elemento: {ex.Message}");
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(x => x.Id, id);
                var result = _collection.DeleteOne(filter);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Erro ao deletar o elemento: {ex.Message}");
            }
        }

        public T Get(string id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(x => x.Id, id);
                return _collection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar o documento: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _collection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar documentos: {ex.Message}");
                return new List<T>();
            }
        }

        public void Update(string id, T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(x => x.Id, id);
                var result = _collection.ReplaceOne(filter, entity);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Erro ao atualizar o documento: {ex.Message}");
            }
        }
    }
}
