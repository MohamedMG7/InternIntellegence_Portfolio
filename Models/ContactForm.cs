namespace InternIntellegence_Portfolio.Models{
    public class ContactForm{
        public int MessageId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public ApplicationUser user { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}