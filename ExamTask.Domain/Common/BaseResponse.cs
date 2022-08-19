namespace ExamTask.Domain.Common
{
    public class BaseResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
