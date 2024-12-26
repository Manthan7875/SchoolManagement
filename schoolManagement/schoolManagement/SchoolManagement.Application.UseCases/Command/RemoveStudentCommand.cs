using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Command
{
    public class RemoveStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }

        public RemoveStudentCommand(int studentId)
        {
            StudentId = studentId;
        }
    }

}
