using MediatR;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Command;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Handler
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, CreateUpdateStudentDto>
    {
        private readonly IStudent _studentRepository;

        public CreateStudentHandler(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<CreateUpdateStudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // Map CreateStudentCommand to CreateUpdateStudentDto
            var createStudentDto = new CreateUpdateStudentDto
            {
                Name = request.Name,
                Gender = request.Gender,
                DOB = request.DOB,
                studentAddressDtos = request.StudentAddress.Select(address => new StudentAddressDto
                {
                    Address = address.Address,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Country = address.Country
                }).ToList()
            };

            // Use the repository to create the student
            var createdStudent = await _studentRepository.CreateStudentAsync(createStudentDto);

            // Map the created student back to CreateUpdateStudentDto
            var resultDto = new CreateUpdateStudentDto
            {
                Name = createdStudent.Name,
                Gender = createdStudent.Gender,
                DOB = createdStudent.DOB,
                studentAddressDtos = createdStudent.studentAddressDtos.Select(address => new StudentAddressDto
                {
                    Address = address.Address,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Country = address.Country
                }).ToList()
            };

            return resultDto;
        }
    }


}
