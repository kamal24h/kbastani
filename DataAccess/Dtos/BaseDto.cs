using System.Text;

namespace DataAccess.Dtos
{
    public class BaseDto
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }


        protected bool _isValid;
        protected StringBuilder _validationMessage = new();

        public virtual bool IsValid()
        {
            _isValid = true;
            return _isValid;
        }

        public string GetValidationMessage()
        {
            return _validationMessage.ToString();
        }
        public virtual void PrepareDto(Guid currentUserId)
        {
        }
    }
}
