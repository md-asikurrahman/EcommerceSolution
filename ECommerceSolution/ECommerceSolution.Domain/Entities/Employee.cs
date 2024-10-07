using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobile { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ImageUrl { get; set; }
        public string VotarIdCardNo { get; set; }
        public string VotarIdCardUrl { get; set; }
        public string BirthRegiNo { get; set; }
        public string BirthRegiUrl { get; set; }
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public IList<Address> AddressList { get; set; } = new List<Address>();
        public IList<AcademicQualification> academicQualifications { get; set; } = new List<AcademicQualification>();
    }
}
