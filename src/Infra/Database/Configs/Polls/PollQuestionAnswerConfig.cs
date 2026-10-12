using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollQuestionAnswerConfig :  IEntityTypeConfiguration<PollQuestionAnswer>
{
    public void Configure(EntityTypeBuilder<PollQuestionAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Answer).HasMaxLength(512);
        
        builder.HasOne(x=> x.Question)
            .WithMany(x=>x.Answers)
            .HasForeignKey(x=> x.QuestionId);
        
        builder.HasOne(x=> x.Response)
            .WithMany(x=>x.QuestionAnswers)
            .HasForeignKey(x=> x.ResponseId);
    }
}