using ExamTask.Domain.Common;

namespace ExamTask.Domain.Entities
{
    public class BaseAttachment : BaseResponse
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
