namespace InternIntellegence_Portfolio.Models
{
	public class Projects
	{
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDiscription { get; set; } = null!;
        public string ProjectLink { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;
		public string ApplicationUserId { get; set; } = null!;

	}
}
