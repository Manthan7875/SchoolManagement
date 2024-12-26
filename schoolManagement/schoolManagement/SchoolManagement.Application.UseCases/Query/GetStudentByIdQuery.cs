using MediatR;
using SchoolManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Query
{
    public class GetStudentByIdQuery : IRequest<CreateUpdateStudentDto>
    {
        public int StudentId { get; set; }

        public GetStudentByIdQuery(int studentId)
        {
            StudentId = studentId;
        }
    }
}
