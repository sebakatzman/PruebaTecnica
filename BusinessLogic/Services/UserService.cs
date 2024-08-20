using Domain.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using Repository.Repository;
using Domain.Models;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <returns>Una lista de usuarios.</returns>
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserModel
            {
                Name = user.Name,
                Lastname = user.Lastname
            });
        }

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        /// <param name="user">Modelo del usuario a agregar.</param>
        public async Task Add(UserModel user)
        {
            var userEntity = new UserEntity
            {
                Name = user.Name,
                Lastname = user.Lastname
            };
            await _userRepository.AddAsync(userEntity);
            
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El modelo del usuario si es encontrado; de lo contrario, null.</returns>
        public async Task<UserModel> GetById(int id)
        {
            try
            {
                var userentity = await _userRepository.GetById(id);

                if (userentity == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                return new UserModel
                {
                    Name = userentity.Name,
                    Lastname = userentity.Lastname
                };
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="user">Modelo con la información actualizada del usuario.</param>
        /// <returns>True si el usuario fue actualizado exitosamente; false si el usuario no fue encontrado.</returns>
        public async Task<bool> Update(UserModel user)
        {
            var existingUser = await _userRepository.GetById(user.Id ?? 0);
            if (existingUser == null) return false;

            // Actualizo las propiedades
            existingUser.Name = user.Name;
            existingUser.Lastname = user.Lastname;

            //Updeteo
            await _userRepository.Update(existingUser);
            return true;
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>True si el usuario fue eliminado exitosamente; false si el usuario no fue encontrado.</returns>
        public async Task<bool> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }
    }

}
