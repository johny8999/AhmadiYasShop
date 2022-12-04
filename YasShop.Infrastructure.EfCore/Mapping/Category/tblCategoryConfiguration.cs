using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YasShop.Domain.Category.Entities;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Mapping.Category
{
    public class tblCategoryConfiguration : IEntityTypeConfiguration<tblCategory>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TopicId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblTopics)
                   .WithMany(a => a.tblCategory)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.TopicId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
