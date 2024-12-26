using MediatR;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Handler
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, CreateUpdateStudentDto>
    {
        private readonly IStudent _studentRepository;
        public UpdateStudentHandler(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<CreateUpdateStudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var updatedStudent = await _studentRepository.UpdateStudentAsync(request.StudentDto);

            if (updatedStudent == null)
            {
                throw new Exception("Failed to update student. Student not found or an error occurred.");
            }

            return updatedStudent;
        }
    }
}
