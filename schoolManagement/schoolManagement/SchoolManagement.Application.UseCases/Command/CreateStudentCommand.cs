using MediatR;
using SchoolManagement.Application.Dto;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Command
{
    public class CreateStudentCommand : IRequest<CreateUpdateStudentDto>
    {

        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public List<StudentAddress> StudentAddress { get; set; }

        public CreateStudentCommand(CreateUpdateStudentDto student)
        {
            Name = student.Name;
            Gender = student.Gender;
            DOB = student.DOB;
            // Map StudentAddressDtos to StudentAddress
            StudentAddress = student.studentAddressDtos.Select(dto => new StudentAddress
            {
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            }).ToList();
        }
    }
}
