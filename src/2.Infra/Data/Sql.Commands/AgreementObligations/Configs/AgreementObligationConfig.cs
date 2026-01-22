using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Resources;
using Master.Data.Infra.Data.Sql.Commands.AgreementObligations.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Master.Data.Infra.Data.Sql.Commands.AgreementObligations.Configs;

public sealed class AgreementObligationConfig : IEntityTypeConfiguration<AgreementObligation>
{
    public void Configure(EntityTypeBuilder<AgreementObligation> builder)
    {
        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.BusinessId).IsRequired();

        builder.Property(c => c.CoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Title).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.DisplayTitle).HasMaxLength(ProjectConsts.TITLE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.Code).HasMaxLength(ProjectConsts.CODE_MAX_LENGTH).IsRequired();

        builder.Property(c => c.AgreementNumber).HasMaxLength(ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH).IsRequired();

        builder.Property(c => c.AgreementObligationNumber).HasMaxLength(ProjectConsts.AGREEMENT_NUMBER_MAX_LENGTH).IsRequired();

        #region IssuanceSchemeCoreIds
        builder.Ignore(c => c.IssuanceSchemeCoreIds);

        builder.Property<HashSet<CoreId>>("_issuanceSchemeCoreIds")
               .HasColumnName("IssuanceSchemeCoreIds")
               .HasMaxLength(2000)
               .IsRequired()
               .HasConversion<IssuanceSchemeCoreIdsConversion>()
               .Metadata.SetValueComparer(IssuanceSchemeCoreIdsComparer.CoreIdHashSetComparer);
        #endregion

        builder.Property(c => c.AgreementCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.Property(c => c.InsuranceTypeCoreId).HasMaxLength(ProjectConsts.CORE_ID_MAX_LENGTH).IsRequired();

        builder.HasIndex(c => c.BusinessId).IsUnique();


        builder.HasIndex(c => c.CoreId);
        builder.HasIndex(c => c.AgreementCoreId);
        builder.HasIndex(c => c.InsuranceTypeCoreId);
        builder.HasIndex(c => new { c.TenantId, c.CoreId }).IsUnique();


        //builder
        //.HasOne<InsuranceType>()
        //.WithMany()
        //.HasPrincipalKey(c => new { c.TenantId, c.CoreId }) // کلید اصلی ترکیبی
        //.HasForeignKey(p => new { p.TenantId, p.InsuranceTypeCoreId }) // کلید خارجی ترکیبی
        //.OnDelete(DeleteBehavior.NoAction);
    }
}