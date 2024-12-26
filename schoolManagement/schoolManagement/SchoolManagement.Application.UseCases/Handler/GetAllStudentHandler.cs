using MediatR;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Command;
using SchoolManagement.Application.UseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Handler
{
    public class GetAllStudentHandler : IRequestHandler<GetAllStudentsQuery, List<CreateUpdateStudentDto>>
    {
        private readonly IStudent _studentRepository;

        public GetAllStudentHandler(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<CreateUpdateStudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetStudentsAsync();
        }
    }
}
