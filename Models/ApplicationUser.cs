using Microsoft.AspNetCore.Identity;

namespace InternIntellegence_Portfolio.Models
{
	public class ApplicationUser : IdentityUser 
	{
		ICollection<Achivements> Achivements { get; set; } = new HashSet<Achivements>();
		ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
		ICollection<Projects> Projects { get; set; } = new HashSet<Projects>();
		ICollection<Skills> Skills { get; set; } = new HashSet<Skills>();
	}
}
