using Core.Dtos;
//using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Mapping;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private AuthorizationService authService { get; set; }

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public void Register(UserRegisterDTO registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var hashedPassword = authService.HashPassword(registerData.Password);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Name = registerData.Name,
                Surname = registerData.Surname,
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }

        public string Validate(UserLoginDTO payload)
        {
            var user = unitOfWork.Users.GetByEmail(payload.Email);
            if (user == null)
            {
                return null;
            }

            var passwordFine = authService.VerifyPassword(payload.Password, user.PasswordHash);

            if (passwordFine)
            {
                var role = unitOfWork.Roles.GetById(user.RoleId);

                return authService.GenerateToken(user, role.Name);
            }
            else
            {
                return null;
            }

        }


        public List<User> GetAll()
        {
            var results = unitOfWork.Users.GetAll();

            return results;
        }
    }
}
