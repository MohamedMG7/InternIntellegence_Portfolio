namespace InternIntellegence_Portfolio.Dto
{
	public class PortfolioReadDto
	{
		public ICollection<AchivementReadDto> Achivements { get; set; } = new HashSet<AchivementReadDto>();
		public ICollection<ContactReadDto> Contacts { get; set; } = new HashSet<ContactReadDto>();
		public ICollection<SkillsReadDto> Skills { get; set; } = new HashSet<SkillsReadDto>();
		public ICollection<ProjectsReadDto> Projects { get; set; } = new HashSet<ProjectsReadDto>();
	}
}
