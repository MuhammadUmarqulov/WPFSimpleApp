
using ExamTask.Domain.Entities;
using ExamTask.Service.Models.DTOs;

namespace ExamTask.Service.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> GetAsync(long id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> CreateAsync(StudentForCreation User);

        Task<User> UpdateAsync(long id, StudentForCreation User);

        Task<bool> DeleteAsync(long id);

        Task UploadPicturesAsync(long id, string imagePath, string passportPath);
    }
}
