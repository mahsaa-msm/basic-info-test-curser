using Master.Data.Core.Resources;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Exceptions;

namespace Master.Data.Core.Domain.Common.Guards;

public static class EntityGuard
{
    public static void ThrowIfNull<TEntity, TId>(TEntity entity, string name)
        where TEntity : Entity<TId>
        where TId : struct, IComparable, IComparable<TId>, IConvertible, IEquatable<TId>, IFormattable
    {
        if (entity is null)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST, name);
    }

    public static void ThrowIfNullWithIntId<TEntity>(TEntity entity, string name)
        where TEntity : Entity<int>
    {
        if (entity is null)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST, name);
    }

    public static void ThrowIfNullWithLongId<TEntity>(TEntity? entity, string name)
        where TEntity : Entity<long>
    {
        if (entity is null)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST, name);
    }

    public static void ThrowIfListIsEmpty<TEntity, TId>(List<TEntity> entities, string name)
        where TEntity : Entity<TId>
        where TId : struct, IComparable, IComparable<TId>, IConvertible, IEquatable<TId>, IFormattable
    {
        if (entities is null || !entities.Any())
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST_ANY, name);
    }

    public static void ThrowIfListIsEmptyWithLongId<TEntity>(List<TEntity> entities, string name)
    where TEntity : Entity<long>
    {
        if (entities is null || !entities.Any())
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_NOT_EXIST_ANY, name);
    }
}