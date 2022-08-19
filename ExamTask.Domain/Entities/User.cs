using ExamTask.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamTask.Domain.Entities
{
    public class User : BaseResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }

        public long? PassportId { get; set; }

        [ForeignKey(nameof(PassportId))]
        public BaseAttachment Passport { get; set; }

        public long? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public BaseAttachment Image { get; set; }
    }
}
