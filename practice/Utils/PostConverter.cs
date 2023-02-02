using practice.ViewModels;
using System.Collections.Generic;

namespace practice.Utils
{
    public class PostConverter
    {

        public static List<ReqOneViewModel> MyCconverter(List<PostViewModel> pvm)
        {
            List<ReqOneViewModel> res = new List<ReqOneViewModel>();
            foreach (PostViewModel p in pvm)
            {
                res.Add(new ReqOneViewModel
                {
                    title= p.title,
                    body = p.body,
                    answercount=p.answercount,
                    votecount=p.votecount,
                    displayname=p.displayname,
                    reputation = p.reputation,
                    bages=p.bages
                });
            }
            return res;
        }
    }
}
