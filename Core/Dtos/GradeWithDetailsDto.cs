using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class GradeWithDetailsDto
    {
        public double Value { get; set; }
        public CourseType Course { get; set; }
        public string CourseName { get; set; }
        public string Student { get; set; }
    }
}
