using System.Collections.Generic;

namespace practice.ViewModels
{
    public class PostViewModel
    {
        public long? pid { get; set; }
        public string? title { get; set; }
        public string? body { get; set; } // up to 140 char
        public long? answercount { get; set; }
        public long? votecount { get; set; }
        public long? owneruserid { get; set; }
        public string? displayname { get; set; }
        public long? reputation { get; set; }
        public HashSet<string>? bages { get; set; }
    }
}
