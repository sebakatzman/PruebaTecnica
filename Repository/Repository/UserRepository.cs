using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using SQLite;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _connection;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(IOptions<DatabaseOptions> options, ILogger<UserRepository> logger)
        {
            var databasePath = options.Value.DatabasePath;
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTable<UserEntity>();
            _logger = logger;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        public async Task<UserEntity> GetById(int id)
        {
            return await Task.Run(() => _connection.Table<UserEntity>().FirstOrDefault(x => x.Id == id));
        }

        /// <summary>
        /// Obtiene todos los usuarios de la base de datos.
        /// </summary>
        /// <returns>Una lista de usuarios.</returns>
        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await Task.Run(() => _connection.Table<UserEntity>().ToList());
        }

        /// <summary>
        /// Agrega un nuevo usuario a la base de datos.
        /// </summary>
        /// <param name="user">Entidad del usuario a agregar.</param>
        public async Task AddAsync(UserEntity user)
        {
            await Task.Run(() =>
            {
                _connection.Insert(user);
            });
        }

        /// <summary>
        /// Actualiza la información de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="user">Entidad del usuario con la información actualizada.</param>
        public async Task Update(UserEntity user)
        {
            await Task.Run(() =>
            {
                _connection.Update(user);
            });
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await Task.Run(() =>
            {
                var user = _connection.Table<UserEntity>().FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    _connection.Delete(user);
                    _logger.LogInformation("Usuario eliminado correctamente: {@User}", user);
                }
                else
                {
                    _logger.LogWarning("El usuario no fue encontrado.");
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el usuario con ID: {Id}", id);
            throw;
        }
        }
    }
}
