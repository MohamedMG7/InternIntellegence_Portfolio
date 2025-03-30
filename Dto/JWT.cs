namespace InternIntellegence_Portfolio.Dto{
    public class JWT{
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audiance { get; set; } = null!;
        public double DurationInHours { get; set; }
    }
}