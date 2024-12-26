using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public string Gender  { get; set; }
        public DateTime DOB  { get; set; }

        // Navigation property
        public ICollection<StudentAddress> Addresses { get; set; }
    }
}
