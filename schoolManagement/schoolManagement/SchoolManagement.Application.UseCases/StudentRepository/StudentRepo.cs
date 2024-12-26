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

        public async Task<CreateUpdateStudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _dbContext.Students
                .Include(s => s.Addresses) // Include StudentAddress table
                                           //.ThenInclude(sa => sa.AnotherTable) // Include AnotherTable if needed
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return null;

            // Map the Student entity to the CreateUpdateStudentDto
            var studentDto = new CreateUpdateStudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender,
                DOB = student.DOB,
                studentAddressDtos = student.Addresses.Select(address => new StudentAddressDto
                {
                    Id = address.Id,
                    StudentId = address.StudentId,
                    Address = address.Address,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Country = address.Country
                }).ToList()
            };

            return studentDto;
        }


        public async Task<List<CreateUpdateStudentDto>> GetStudentsAsync()
        {
            // Fetch all students and their addresses in a single query to avoid N+1 problem
            var students = await _dbContext.Students
                .Select(student => new CreateUpdateStudentDto
                {
                    Id = student.Id,
                    Name = student.Name, //
                    Gender= student.Gender,
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

        public async Task<CreateUpdateStudentDto?> UpdateStudentAsync(CreateUpdateStudentDto studentDto)
        {
            var student = await _dbContext.Students
                .Include(s => s.Addresses)
                .FirstOrDefaultAsync(s => s.Id == studentDto.Id);

            if (student == null) return null;

            // Update student properties
            student.Name = studentDto.Name ?? student.Name;
            student.Gender = studentDto.Gender ?? student.Gender;
            student.DOB = studentDto.DOB != default ? studentDto.DOB : student.DOB;

            // Update or add addresses
            foreach (var addressDto in studentDto.studentAddressDtos)
            {
                var existingAddress = student.Addresses.FirstOrDefault(a => a.Id == addressDto.Id);
                if (existingAddress != null)
                {
                    // Update existing address
                    existingAddress.Address = addressDto.Address ?? existingAddress.Address;
                    existingAddress.City = addressDto.City ?? existingAddress.City;
                    existingAddress.State = addressDto.State ?? existingAddress.State;
                    existingAddress.PostalCode = addressDto.PostalCode ?? existingAddress.PostalCode;
                    existingAddress.Country = addressDto.Country ?? existingAddress.Country;
                }
                else
                {
                    // Add new address
                    student.Addresses.Add(new StudentAddress
                    {
                        StudentId = student.Id, // Associate with the student
                        Address = addressDto.Address,
                        City = addressDto.City,
                        State = addressDto.State,
                        PostalCode = addressDto.PostalCode,
                        Country = addressDto.Country
                    });
                }
            }

            await _dbContext.SaveChangesAsync();

            // Map updated entity back to DTO
            return new CreateUpdateStudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender,
                DOB = student.DOB,
                studentAddressDtos = student.Addresses.Select(a => new StudentAddressDto
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    Address = a.Address,
                    City = a.City,
                    State = a.State,
                    PostalCode = a.PostalCode,
                    Country = a.Country
                }).ToList()
            };
        }

    }
}
