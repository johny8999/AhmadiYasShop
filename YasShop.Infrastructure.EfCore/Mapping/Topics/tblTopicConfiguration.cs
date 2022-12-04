using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Topics.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Topics
{
    public class tblTopicConfiguration : IEntityTypeConfiguration<tblTopics>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblTopics> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);
        }
    }
}
