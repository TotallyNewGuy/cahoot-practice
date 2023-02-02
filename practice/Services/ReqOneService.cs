using Microsoft.EntityFrameworkCore;
using practice.Models;
using practice.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace practice.Services
{
    public class ReqOneService
    {
        private readonly StackOverFlow2010Context _context;

        public ReqOneService()
        {
            _context = new StackOverFlow2010Context();
            _context.Database.SetCommandTimeout(400);
        }

        public List<PostViewModel> GetReqAns(string query)
        {
            var ans = from p in _context.Posts
                      join u in _context.Users on p.OwnerUserId equals u.Id
                      where (p.Title.Contains(query) || p.Body.Contains(query))
                      select new PostViewModel
                      {
                          pid = p.Id,
                          title = p.Title,
                          body = SubStr(p.Body, 140),
                          owneruserid = p.OwnerUserId,
                          answercount = p.AnswerCount,
                          displayname = u.DisplayName,
                          reputation = u.Reputation
                      };

            Console.WriteLine("34");
            var temp = ans.ToList();
            foreach (PostViewModel p in temp)
            {
                var pid = p.pid;
                var nv = from v in _context.Votes
                         where v.PostId == pid
                         group v by v.PostId into g
                         select new
                         {
                             numOfVote = g.Count()
                         };
                var num = nv.FirstOrDefault() == null ? 0 : nv.FirstOrDefault().numOfVote;
                p.votecount = num;

                var bg = from b in _context.Badges
                         where b.UserId == p.owneruserid
                         select new BageViewModel
                         {
                             name = b.Name
                         };

                var tempBg = bg.ToArray();

                foreach (BageViewModel b in tempBg)
                {
                    if (p.bages == null)
                    {
                        p.bages = new HashSet<string>();
                    }
                    p.bages.Add(b.name);
                }
            }
            Console.WriteLine("67");
            return temp;
        }

        public List<PostViewModel> TestPage()
        {
            var query = "cs";
            var ans = from p in _context.Posts
                      join u in _context.Users on p.OwnerUserId equals u.Id
                      where (p.Title.Contains(query) || p.Body.Contains(query))
                      select new PostViewModel
                      {
                          pid = p.Id,
                          title = p.Title,
                          body = SubStr(p.Body,140),
                          owneruserid = p.OwnerUserId,
                          answercount = p.AnswerCount,
                          displayname = u.DisplayName,
                          reputation = u.Reputation
                      };

            var temp = ans.Skip(0).Take(20).ToList();

            foreach (PostViewModel p in temp)
            {
                var pid = p.pid;
                var nv = from v in _context.Votes
                         where v.PostId == pid
                         group v by v.PostId into g
                         select new
                         {
                             numOfVote = g.Count()
                         };
                var num = nv.FirstOrDefault() == null ? 0 : nv.FirstOrDefault().numOfVote;
                p.votecount = num;

                var bg = from b in _context.Badges
                         where b.UserId == p.owneruserid
                         select new BageViewModel
                         {
                             name = b.Name
                         };

                var tempBg = bg.ToArray();

                foreach (BageViewModel b in tempBg)
                {
                    if (p.bages == null)
                    {
                        p.bages = new HashSet<string>();
                    }
                    p.bages.Add(b.name);
                }
            }

            return temp;
        }
        public static string SubStr(string sString, int nLength)
        {
            if (sString.Length <= nLength)
            {
                return sString;
            }
            string sNewStr = sString.Substring(0, nLength);
            sNewStr = sNewStr + "...";
            return sNewStr;
        }
    }
    
}
