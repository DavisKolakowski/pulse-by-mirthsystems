using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Domain.Entities;

namespace Application.Infrastructure.Data.Configurations;

public class VenueInvitationEntityConfiguration : EntityBaseConfiguration<VenueInvitationEntity>
{
    public override void Configure(EntityTypeBuilder<VenueInvitationEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(vi => new { vi.EmailAddress, vi.VenueId, vi.IsActive })
               .HasDatabaseName("ix_venue_invitations_email_venue_active");
        builder.HasIndex(vi => vi.VenueId)
               .HasDatabaseName("ix_venue_invitations_venue_id");
        builder.HasIndex(vi => vi.InvitedByUserId)
               .HasDatabaseName("ix_venue_invitations_invited_by");
        builder.HasIndex(vi => vi.ExpiresAt)
               .HasDatabaseName("ix_venue_invitations_expires_at");
        builder.HasIndex(vi => new { vi.IsActive, vi.AcceptedAt })
               .HasDatabaseName("ix_venue_invitations_active_accepted");

        builder.HasOne(vi => vi.Venue)
               .WithMany()
               .HasForeignKey(vi => vi.VenueId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(vi => vi.Role)
               .WithMany()
               .HasForeignKey(vi => vi.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(vi => vi.InvitedByUser)
               .WithMany(u => u.SentInvitations)
               .HasForeignKey(vi => vi.InvitedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(vi => vi.AcceptedByUser)
               .WithMany(u => u.ReceivedInvitations)
               .HasForeignKey(vi => vi.AcceptedByUserId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}