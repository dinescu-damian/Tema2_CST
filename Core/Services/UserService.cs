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

        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var hashedPassword = authService.HashPassword(registerData.Password);

            var user = new User
            {
                Username = registerData.Username,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                RoleId = registerData.RoleId,
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }

        public string Validate(LoginDto payload)
        {
            var user = unitOfWork.Users.GetByUsername(payload.Username);
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

        // Get the grades from the logged in student
        public List<GradeWithDetailsDto> GetGradesOfStudent(int userId)
        {
            //Firstly get the student associated with the user
            var student = unitOfWork.Students.GetByUserId(userId);

            var grades = new GradesByStudent(student);
            //var grades = student.Grades.ToList();
            // Use mapping to convert the grades to gradeDtos
            //var gradeDtos = grades.ToGradeDtos();

            return grades.Grades;
        }

        // As a teacher, get the grades of every student in the database
        public List<GradeWithDetailsDto> GetGradesOfAllStudents()
        {
            var students = unitOfWork.Students.GetAllPlusDependencies();
            // for each student, get the grades
            var grades = new List<GradeWithDetailsDto>();
            foreach (var student in students)
            {
                var gradesByStudent = new GradesByStudent(student);
                grades.AddRange(gradesByStudent.Grades);
            }
            return grades;
        }



        public List<User> GetAll()
        {
            var results = unitOfWork.Users.GetAll();

            return results;
        }
    }
}
