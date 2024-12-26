using MediatR;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Handler
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, CreateUpdateStudentDto>
    {
        private readonly IStudent _studentRepository;

        public GetStudentByIdHandler(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public Task<CreateUpdateStudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return _studentRepository.GetStudentByIdAsync(request.StudentId);
        }
    }
}
