namespace InternIntellegence_Portfolio.Models
{
	public class Achivements
	{
        public int Id { get; set; }
        public string AchivementName { get; set; } = null!;
        public string AchivementDescription { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ApplicationUserId { get; set; } = null!;
    }
}
