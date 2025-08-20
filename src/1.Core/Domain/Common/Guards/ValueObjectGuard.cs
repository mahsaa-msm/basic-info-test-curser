using Master.Data.Core.Resources;
using Zamin.Core.Domain.Exceptions;

namespace Master.Data.Core.Domain.Common.Guards;
public static class ValueObjectGuard
{
    public static void ThrowIfNull<TValue>(TValue value, string name)
    {
        if (value is null)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_REQUIRED, name);
    }
    public static void ThrowIfNotValid(bool condition, string name)
    {
        if (!condition)
            throw new InvalidValueObjectStateException(ProjectValidationError.VALIDATION_ERROR_NOT_VALID, name);
    }

    #region string
    public static void ThrowIfStringNullOrWhiteSpace(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidValueObjectStateException(ProjectValidationError.VALIDATION_ERROR_REQUIRED, name);
    }
    public static void ThrowIfStringLenghtIsNotBetween(string? value, int min, int max, string name)
    {
        if (value is null || value?.Length < min || value?.Length > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_BETWEEN, name, min.ToString(), max.ToString()));
    }
    public static void ThrowIfStringLenghtGreaterThan(string? value, int max, string name)
    {
        if (value is null || value?.Length > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_STRING_MAX_LENGTH, name, max.ToString()));
    }
    public static void ThrowIfStringLenghtIsNotEqualTo(string? value, int lenght, string name)
    {
        if (value is null || value?.Length != lenght)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL, name, lenght));
    }
    #endregion

    #region Number

    #region ThrowIfIsNotBetween
    /// <summary>
    /// </summary>
    /// <param name="value">double</param>
    /// <param name="min">double</param>
    /// <param name="max">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotBetween(double value, double min, double max, string name)
    {
        if (value < min || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN, name, min.ToString(), max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">int</param>
    /// <param name="min">int</param>
    /// <param name="max">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotBetween(int value, int min, int max, string name)
    {
        if (value < min || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN, name, min.ToString(), max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">double?</param>
    /// <param name="min">double</param>
    /// <param name="max">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotBetween(double? value, double min, double max, string name)
    {
        if (value is null || value < min || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN, name, min.ToString(), max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">int?</param>
    /// <param name="min">int</param>
    /// <param name="max">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotBetween(int? value, int min, int max, string name)
    {
        if (value is null || value < min || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_BETWEEN, name, min.ToString(), max.ToString()));
    }
    #endregion

    #region ThrowIfIsNotGraterOrEqualThan
    /// <summary>
    /// </summary>
    /// <param name="value">int</param>
    /// <param name="min">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(int value, int min, string name)
    {
        if (value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">double</param>
    /// <param name="min">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(double value, double min, string name)
    {
        if (value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">long</param>
    /// <param name="min">long</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(long value, long min, string name)
    {
        if (value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">int?</param>
    /// <param name="min">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(int? value, int min, string name)
    {
        if (value is null || value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }

    /// <summary>
    /// </summary>
    /// <param name="value">double?</param>
    /// <param name="min">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(double? value, double min, string name)
    {
        if (value is null || value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">long</param>
    /// <param name="min">long</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotGraterOrEqualThan(long? value, long min, string name)
    {
        if (value is null || value < min)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, min.ToString()));
    }
    #endregion

    #region ThrowIfIsNotLessOrEqualThan
    /// <summary>
    /// </summary>
    /// <param name="value">double</param>
    /// <param name="max">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotLessOrEqualThan(double value, double max, string name)
    {
        if (value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">int</param>
    /// <param name="max">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotLessOrEqualThan(int value, int max, string name)
    {
        if (value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">double?</param>
    /// <param name="max">double</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotLessOrEqualThan(double? value, double max, string name)
    {
        if (value is null || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, max.ToString()));
    }
    /// <summary>
    /// </summary>
    /// <param name="value">int?</param>
    /// <param name="max">int</param>
    /// <param name="name"></param>
    /// <exception cref="InvalidValueObjectStateException"></exception>
    public static void ThrowIfIsNotLessOrEqualThan(int? value, int max, string name)
    {
        if (value is null || value > max)
            throw new InvalidValueObjectStateException
                (string.Format(ProjectValidationError.VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN, name, max.ToString()));
    }
    #endregion

    #endregion

    #region DateTime
    public static void ThrowIfDefault(DateTime value, string name)
    {
        if (value == default)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_REQUIRED, name);
    }
    public static void ThrowIfDefault(DateTime? value, string name)
    {
        if (value == default)
            throw new InvalidEntityStateException(ProjectValidationError.VALIDATION_ERROR_REQUIRED, name);
    }
    #endregion
}