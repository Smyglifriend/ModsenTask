using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModsenTask.Core.Abstractions.Constants;

namespace ModsenTask.DataAccess.Domain.Models.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .Property(e => e.Topic)
            .IsRequired();

        builder
            .Property(e => e.Description)
            .IsRequired();

        builder
            .Property(e => e.Location)
            .IsRequired();

        builder
            .Property(e => e.Speaker)
            .IsRequired();
        
        builder
            .Property(e => e.Sponsor)
            .IsRequired();

        builder
            .Property(e => e.EventDate)
            .IsRequired();
    }
}