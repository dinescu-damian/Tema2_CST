using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService { get; set; }

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDto registerData)
        {
            if (registerData == null)
            {
                return BadRequest();
            }
            userService.Register(registerData);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto payload)
        {
            var jwtToken = userService.Validate(payload);
            if (jwtToken == null)
            {
                return Unauthorized();
            }
            return Ok(new {token = jwtToken});
        }

        [HttpGet("students-only")]
        [Authorize(Roles = "Student")]
        public ActionResult<string> HelloStudents()
        {
            return Ok("Hello students!");
        }

        [HttpGet("teacher-only")]
        [Authorize(Roles = "Teacher")]
        public ActionResult<string> HelloTeachers()
        {
            return Ok("Hello teachers!");
        }

        // Endpoint that returns only the grades of the student that is logged in
        // or the grades of all students if the logged in user is a teacher
        [HttpGet("grades-by-role")]
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult<List<GradeWithDetailsDto>> GradesByRole()
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Student")
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var grades = userService.GetGradesOfStudent(int.Parse(id));
                return Ok(grades);
            }
            else
            {
                var grades = userService.GetGradesOfAllStudents();
                return Ok(grades);
            }
        }


    }
}
