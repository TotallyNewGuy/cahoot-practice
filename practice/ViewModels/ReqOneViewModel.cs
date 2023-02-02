namespace practice.ViewModels
{
    public class ReqOneViewModel
    {
        public string? title { get; set; }
        public string? body { get; set; } // up to 140 char
        public long? answercount { get; set; }
        public long? votecount { get; set; }
        public string? displayname { get; set; }
        public long? reputation { get; set; }
        public HashSet<string>? bages { get; set; }
    }
}
