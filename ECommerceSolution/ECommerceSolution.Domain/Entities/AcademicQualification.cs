using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class AcademicQualification : BaseEntity
    {
        public string EducationLevel { get; set; }
        public string EducationTitle { get; set; }
        public string Group {  get; set; }
        public string InstituteName {  get; set; }
        public string Result {  get; set; }
        public string MarkOrGrade { get; set; }
        public string? GradeScale { get; set; }
        public string passingYear { get; set; }
        public string AcademicDuration { get; set; }
        public string Achievement {  get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
