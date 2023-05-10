using DataLayer.Dtos;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mapping
{
    public static class UsersMappingExtensions
    {

        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            if (users == null)
            {
                return null;
            }

            var results = users.Select(e => e.ToUserDto()).ToList();

            return results;
        }



        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.Id = user.Id;
            result.Email = user.Email;
            result.PasswordHash = user.PasswordHash;
            result.Name = user.Name;
            result.Surname = user.Surname;
            result.RoleId = user.RoleId;
            result.RoleName = user.Role?.Name;

            return result;
        }
    }
}
