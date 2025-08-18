using System.Text.RegularExpressions;

namespace Master.Data.Core.RequestResponse.Common.Extensions;

public static class NationalCodeValidatorExtenstions
{
    public static bool IsNationalCode(this string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode) || !nationalCode.IsLengthBetween(8, 10))
        {
            return false;
        }

        nationalCode = nationalCode.PadLeft(10, '0');
        if (!nationalCode.IsNumeric())
        {
            return false;
        }

        if (!IsFormat1Validate(nationalCode))
        {
            return false;
        }

        if (!IsFormat2Validate(nationalCode))
        {
            return false;
        }

        return true;
        static bool IsFormat1Validate(string nationalCode)
        {
            if (!new string[10] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" }.Contains(nationalCode))
            {
                return true;
            }

            return false;
        }

        static bool IsFormat2Validate(string nationalCode)
        {
            char[] array = nationalCode.ToCharArray();
            int num = Convert.ToInt32(array[0].ToString()) * 10;
            int num2 = Convert.ToInt32(array[1].ToString()) * 9;
            int num3 = Convert.ToInt32(array[2].ToString()) * 8;
            int num4 = Convert.ToInt32(array[3].ToString()) * 7;
            int num5 = Convert.ToInt32(array[4].ToString()) * 6;
            int num6 = Convert.ToInt32(array[5].ToString()) * 5;
            int num7 = Convert.ToInt32(array[6].ToString()) * 4;
            int num8 = Convert.ToInt32(array[7].ToString()) * 3;
            int num9 = Convert.ToInt32(array[8].ToString()) * 2;
            int num10 = Convert.ToInt32(array[9].ToString());
            int num11 = (num + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9) % 11;
            if (num11 < 2 && num10 == num11 || num11 >= 2 && 11 - num11 == num10)
            {
                return true;
            }

            return false;
        }
    }

    public static bool IsLegalNationalIdValid(this string nationalId)
    {
        if (string.IsNullOrWhiteSpace(nationalId) || !nationalId.IsLengthEqual(11))
        {
            return false;
        }

        if (!nationalId.IsNumeric())
        {
            return false;
        }

        if (!IsFormat1Validate(nationalId))
        {
            return false;
        }

        if (!IsFormat2Validate(nationalId))
        {
            return false;
        }

        return true;
        static bool IsFormat1Validate(string nationalId)
        {
            if (!new string[10] { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" }.Contains(nationalId))
            {
                return true;
            }

            return false;
        }

        static bool IsFormat2Validate(string nationalId)
        {
            char[] array = nationalId.ToCharArray();
            int num = Convert.ToInt32(nationalId[10].ToString());
            int num2 = Convert.ToInt32(nationalId[9].ToString()) + 2;
            int num3 = (0 + (num2 + Convert.ToInt32(array[0].ToString())) * 29 + (num2 + Convert.ToInt32(array[1].ToString())) * 27 + (num2 + Convert.ToInt32(array[2].ToString())) * 23 + (num2 + Convert.ToInt32(array[3].ToString())) * 19 + (num2 + Convert.ToInt32(array[4].ToString())) * 17 + (num2 + Convert.ToInt32(array[5].ToString())) * 29 + (num2 + Convert.ToInt32(array[6].ToString())) * 27 + (num2 + Convert.ToInt32(array[7].ToString())) * 23 + (num2 + Convert.ToInt32(array[8].ToString())) * 19 + (num2 + Convert.ToInt32(array[9].ToString())) * 17) % 11;
            if (num3 == 10)
            {
                num3 = 0;
            }

            return num3 == num;
        }
    }

    public static bool IsNumeric(this string nationalCode)
    {
        if (new Regex("\\d+").IsMatch(nationalCode))
        {
            return true;
        }

        return false;
    }

    public static bool IsLengthBetween(this string input, int minLength, int maxLenght)
    {
        if (input.Length <= maxLenght && input.Length >= minLength)
        {
            return true;
        }

        return false;
    }

    public static bool IsLengthLessThan(this string input, int lenght)
    {
        return input.Length < lenght;
    }

    public static bool IsLengthLessThanOrEqual(this string input, int lenght)
    {
        return input.Length <= lenght;
    }

    public static bool IsLengthGreaterThan(this string input, int lenght)
    {
        return input.Length > lenght;
    }

    public static bool IsLengthGreaterThanOrEqual(this string input, int lenght)
    {
        return input.Length >= lenght;
    }

    public static bool IsLengthEqual(this string input, int lenght)
    {
        return input.Length == lenght;
    }
}