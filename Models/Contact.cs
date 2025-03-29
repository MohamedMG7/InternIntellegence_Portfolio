namespace InternIntellegence_Portfolio.Models
{
	public class Contact
	{
        public int Id { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumebr { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;
		public string ApplicationUserId { get; set; } = null!;
	}
}
