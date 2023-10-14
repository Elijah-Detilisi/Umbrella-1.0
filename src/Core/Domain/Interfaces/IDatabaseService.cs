namespace Domain.Interfaces;

internal interface IDatabaseService : IAsyncDisposable
{
    IList<Type> TableNames { get; }
    void CreateTable<T>() where T : new();
    Task<T> SaveEntityAsync<T>(T entity) where T : Entity, new();

    Task<List<T>> LoadAllEntitiesAsync<T>() where T : Entity, new();
    Task<T> LoadEntityByIdAsync<T>(int entityId) where T : Entity, new();
    Task<List<T>> LoadEntitiesBySqlQueryAsync<T>(string sqlQuery) where T : Entity, new();

    Task DeleteEntityAsync<T>(T entity) where T : Entity;
    Task MarkDeletedAsync<T>(int entityId) where T : Entity;
}
