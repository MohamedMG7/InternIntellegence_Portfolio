namespace InternIntellegence_Portfolio.Dto{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string? UserId { get; set; }
        public List<string>? Errors { get; set; }
    }
}