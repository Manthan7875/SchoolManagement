using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class CreateUpdateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }

        public List<StudentAddressDto>? studentAddressDtos { get; set; }
    }

    public class StudentAddressDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }
}
