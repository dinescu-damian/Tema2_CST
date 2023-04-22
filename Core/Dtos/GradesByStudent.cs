using DataLayer.Entities;

namespace Core.Dtos;

public class GradesByStudent
{

    public int? StudentId { get; set; }
    public string StudentCompleteName { get; set; }

    public List<GradeWithDetailsDto> Grades { get; set; } = new();

    public GradesByStudent(Student student)
    {

        StudentId = student?.Id;
        StudentCompleteName = student?.FirstName + " " + student?.LastName;

        if (student?.Grades != null)
        {
            Grades = student.Grades
                .Select(g => new GradeWithDetailsDto
                {
                    Value = g.Value,
                    Course = g.Course,
                    CourseName = g.Course.ToString(),
                    Student = g.Student.FirstName + " " + g.Student.LastName
                })
                .ToList();
        }
    }

}
