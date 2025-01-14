﻿using SchoolManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interface
{
    public interface IStudent
    {
        public Task<List<CreateUpdateStudentDto>> GetStudentsAsync();
        public Task<CreateUpdateStudentDto> GetStudentByIdAsync(int id);
        public Task<CreateUpdateStudentDto> CreateStudentAsync(CreateUpdateStudentDto student);

        public Task<bool> DeleteStudentAsync(int id);
        Task<CreateUpdateStudentDto> UpdateStudentAsync(CreateUpdateStudentDto studentDto);
    }
}
