using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetById(int id);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task AddAsync(UserEntity user);
        Task Update(UserEntity user);
        Task DeleteAsync(int id);
    }
}
