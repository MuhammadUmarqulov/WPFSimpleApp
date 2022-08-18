
using ExamTask.Main.Models;
using ExamTask.Main.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamTask.Main.Contracts
{
    public interface IStudentService : IDisposable
    {
        Task<Student> GetAsync(long id);

        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> CreateAsync(StudentForCreation student);

        Task<Student> UpdateAsync(long id, StudentForCreation student);

        Task<bool> DeleteAsync(long id);

        Task UploadPicturesAsync(long id, string imagePath, string passportPath);
    }
}
