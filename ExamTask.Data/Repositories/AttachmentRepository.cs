using ExamTask.Data.IRepositories;
using ExamTask.Domain.Entities;

namespace ExamTask.Data.Repositories
{
    public class AttachmentRepository : GenericRepository<BaseAttachment>, IAttachmentRepository
    {
    }
}
