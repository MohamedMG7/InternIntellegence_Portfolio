namespace InternIntellegence_Portfolio.Models
{
	public class Skills
	{
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public string SkillDescription { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;
		public string ApplicationUserId { get; set; } = null!;

	}
}
