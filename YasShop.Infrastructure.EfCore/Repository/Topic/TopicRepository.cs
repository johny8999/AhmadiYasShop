using YasShop.Domain.Topics.Contracts;
using YasShop.Domain.Topics.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository.Topic
{
    public class TopicRepository : BaseRepository<tblTopics>, ITopicRepository
    {
        public TopicRepository(MainContext Context) : base(Context)
        {

        }
    }
}
