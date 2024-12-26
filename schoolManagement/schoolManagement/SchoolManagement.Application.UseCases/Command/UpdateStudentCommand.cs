using MediatR;
using SchoolManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Command
{
    public class UpdateStudentCommand : IRequest<CreateUpdateStudentDto>
    {
        public CreateUpdateStudentDto StudentDto { get; set; }

        public UpdateStudentCommand(CreateUpdateStudentDto studentDto)
        {
            StudentDto = studentDto;
        }
    }
}
