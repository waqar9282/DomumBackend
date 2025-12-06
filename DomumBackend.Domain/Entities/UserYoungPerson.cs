namespace DomumBackend.Domain.Entities
{
    public class UserYoungPerson
    {
        public string UserId { get; set; }

        public string YoungPersonId { get; set; }
        public YoungPerson YoungPerson { get; set; }
    }
}

