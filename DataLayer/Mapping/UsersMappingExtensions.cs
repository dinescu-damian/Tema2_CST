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
        /*
         public static StudentDto ToStudentDto(this Student student)
        {
            if (student == null) return null;

            var result = new StudentDto();
            result.Id = student.Id;
            result.FullName = student.FirstName + " " + student.LastName;
            result.ClassId = student.ClassId;
            result.ClassName = student.Class?.Name;
            result.Grades = student.Grades.ToGradeDtos();

            return result;
        }
            */
        
        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.Id = user.Id;
            result.Username = user.Username;
            result.PasswordHash = user.PasswordHash;
            result.Email = user.Email;
            result.RoleId = user.RoleId;
            result.RoleName = user.Role?.Name;

            return result;
        }
    }
}
