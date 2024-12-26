using MediatR;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.Handler
{
    public class RemoveStudentHandler : IRequestHandler<RemoveStudentCommand, bool>
    {
        private readonly IStudent _studentRepository;

        public RemoveStudentHandler(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<bool> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            // Use the repository to delete the student by ID
            return await _studentRepository.DeleteStudentAsync(request.StudentId);
        }
    }
}
