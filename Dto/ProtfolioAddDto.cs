using InternIntellegence_Portfolio.Models;

namespace InternIntellegence_Portfolio.Dto
{
	public class ProtfolioAddDto
	{
        public string UserId { get; set; } = null!;
		public ICollection<AchivementDto> Achivements { get; set; } = new HashSet<AchivementDto>();
		public ICollection<ContactAddDto> Contacts { get; set; } = new HashSet<ContactAddDto>();
		public ICollection<SkillsAddDto> Skills { get; set; } = new HashSet<SkillsAddDto>();
		public ICollection<ProjectsAddDto> Projects { get; set; } = new HashSet<ProjectsAddDto>();
    }
}
