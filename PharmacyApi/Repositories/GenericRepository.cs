using PharmacyApi.Entity;
using System.Text.Json;

namespace PharmacyApi.Repositories
{
    public class GenericRepository<T> : IGeneriRepository<T> where T : class, IBaseEntity
    {
        private readonly string _filePath;

        public GenericRepository(IWebHostEnvironment env, string filename)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Datas", filename);
        }
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var all = await ReadFileUnsafe();
                entity.Id = all.Any() ? all.Max(x => x.Id) + 1 : 1;
                all.Add(entity);
                await WriteFileUnsafe(all);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while adding entity, error: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var all = await ReadFileUnsafe();
                var entity = all.FirstOrDefault(x => x.Id == id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity with Id {entity?.Id} not found");
                all.RemoveAll(x => x.Id == id);
                await WriteFileUnsafe(all);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while deleting the entity with Id {id}: error: {ex.Message}");
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await ReadFileUnsafe();
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while retriving data, error: {ex.Message}");
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                var all = await GetAllAsync();
                var entity = all.FirstOrDefault(x => x.Id == id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity with Id {id} not found");
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while retreving data by id: {id}, error: {ex.Message}");
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var all = await ReadFileUnsafe();
                var index = all.FindIndex(x => x.Id == entity.Id);
                if (index == -1) throw new KeyNotFoundException($"Entity with Id {entity.Id} not found");
                all[index] = entity;
                await WriteFileUnsafe(all);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while updating entity with Id {entity.Id}, error: {ex.Message}");
            }
        }

        private async Task<List<T>> ReadFileUnsafe()
        {
            if (!File.Exists(_filePath)) return new List<T>();
            var json = await File.ReadAllTextAsync(_filePath);
            return string.IsNullOrWhiteSpace(json)
                ? new List<T>()
                : JsonSerializer.Deserialize<List<T>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<T>();
        }

        private async Task WriteFileUnsafe(List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
