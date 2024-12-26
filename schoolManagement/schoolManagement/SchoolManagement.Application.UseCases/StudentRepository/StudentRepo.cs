using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface;
using SchoolManagement.Domain.Entities;
using ShoolManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UseCases.StudentRepository
{
    public class StudentRepo : IStudent
    {
        private readonly SchoolManagementContext _dbContext;
        public StudentRepo(SchoolManagementContext dbContext) => _dbContext = dbContext;

        public async Task<CreateUpdateStudentDto> CreateStudentAsync(CreateUpdateStudentDto studentDto)
        {
            // Create a new Student entity
            var student = new Student
            {
                Name = studentDto.Name,
                DOB = studentDto.DOB,
                Gender = studentDto.Gender
            };

            // Add the student to the database
            await _dbContext.AddAsync(student);
            await _dbContext.SaveChangesAsync(); // Save the student to get the auto-generated Id

            // Check if the student has addresses to add
            if (studentDto.studentAddressDtos != null && studentDto.studentAddressDtos.Any())
            {
                foreach (var addressDto in studentDto.studentAddressDtos)
                {
                    // Create a new StudentAddress entity
                    var studentAddress = new StudentAddress
                    {
                        StudentId = student.Id, // Associate the address with the student
                        Address = addressDto.Address,
                        City = addressDto.City,
                        State = addressDto.State,
                        PostalCode = addressDto.PostalCode,
                        Country = addressDto.Country
                    };

                    // Add the address to the database
                    await _dbContext.AddAsync(studentAddress);
                }

                // Save all the addresses
                await _dbContext.SaveChangesAsync();
            }

            // Return the DTO with updated data
            studentDto.Id = student.Id; // Update the DTO with the student's database ID
            return studentDto;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            // Check if the student exists
            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return false;
            }

            // Remove the student and save changes
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CreateUpdateStudentDto>> GetStudentsAsync()
        {
            // Fetch all students and their addresses in a single query to avoid N+1 problem
            var students = await _dbContext.Students
                .Select(student => new CreateUpdateStudentDto
                {
                    Id = student.Id,
                    Name = student.Name, // Assuming there's a Name property
                    studentAddressDtos = _dbContext.StudentAddresses
                        .Where(address => address.StudentId == student.Id)
                        .Select(address => new StudentAddressDto
                        {
                            Id = address.Id,
                            StudentId = address.StudentId,
                            Address = address.Address,
                            State = address.State,
                            PostalCode = address.PostalCode,
                            City = address.City,
                            Country = address.Country,
                           
                        }).ToList()
                }).ToListAsync();

            return students;
        }

    }
}
