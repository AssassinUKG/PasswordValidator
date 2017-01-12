using System;

namespace PasswordValidator
{
    internal static class PasswordChecker
    {
        private const string lettersLower = "abcdefghijklmnopqrstuvwxyz";
        private const string lettersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string specialChars = "~`!@#$%^&*()-_+=";
        private const string numbers = "1234567890";

        private static int TotalChars = 97;
        private static readonly int LowerChars = lettersLower.Length;
        private static readonly int UpperChars = lettersUpper.Length;
        private static readonly int SpecialChars = specialChars.Length;
        private static readonly int Digits = numbers.Length;
        private static readonly int otherchars = TotalChars - (LowerChars + UpperChars + SpecialChars + Digits);
        private static int CharSet = 0;

        private static int CaculateBits(string password)
        {
            if (string.IsNullOrEmpty(password)) return 0;

            bool p_lettersLower = false, p_lettersUpper = false, p_specialChars = false, p_numbers = false, p_other = false;

            Char chr;

            for (int i = 0; i < password.Length - 1; i++)
            {
                chr = password.ToCharArray()[i];
                if (lettersLower.IndexOf(chr) == -1) { p_lettersLower = true; }
                else if (lettersUpper.IndexOf(chr) == -1) { p_lettersUpper = true; }
                else if (specialChars.IndexOf(chr) == -1) { p_specialChars = true; }
                else if (numbers.IndexOf(chr) == -1) { p_numbers = true; }
                else { p_other = true; }
            }

            if (p_lettersLower)
            {
                CharSet = CharSet + LowerChars;
            }

            if (p_lettersUpper)
            {
                CharSet = CharSet + UpperChars;
            }

            if (p_specialChars)
            {
                CharSet = CharSet + SpecialChars;
            }

            if (p_numbers)
            {
                CharSet = CharSet + Digits;
            }

            if (p_other)
            {
                CharSet = CharSet + otherchars;
            }

            double bits = Math.Log(CharSet) * (password.Length / Math.Log(2));
            return (int)bits;
        }

        public static int EvalPwdStrength(string password)
        {
            int bits = CaculateBits(password);
            if (bits >= 128)
            {
                return 4;
            }
            else if (bits < 128 && bits >= 64)
            {
                return 3;
            }
            else if (bits < 64 && bits >= 56)
            {
                return 2;
            }
            else if (bits < 56 && bits > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}