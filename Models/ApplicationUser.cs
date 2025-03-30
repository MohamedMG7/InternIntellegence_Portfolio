using Microsoft.AspNetCore.Identity;

namespace InternIntellegence_Portfolio.Models
{
	public class ApplicationUser : IdentityUser 
	{
		public ICollection<Achivements> Achivements { get; set; } = new HashSet<Achivements>();
		public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
		public ICollection<Projects> Projects { get; set; } = new HashSet<Projects>();
		public ICollection<Skills> Skills { get; set; } = new HashSet<Skills>();
		public ICollection<ContactForm> ContactForms { get; set; } = new HashSet<ContactForm>();

	}
}
