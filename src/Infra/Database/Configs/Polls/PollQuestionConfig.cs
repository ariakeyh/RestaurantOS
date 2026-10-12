using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollQuestionConfig : IEntityTypeConfiguration<PollQuestion>
{
    public void Configure(EntityTypeBuilder<PollQuestion> builder)
    {
        builder.HasKey(x => x.Id);
    }
}