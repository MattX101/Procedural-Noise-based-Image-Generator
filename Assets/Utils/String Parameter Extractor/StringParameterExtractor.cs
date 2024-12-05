namespace Utils.StringParameterExtractor
{
    public static class StringParameterExtractor
    {
        public static string ExtractBase(string text)
        {
            string[] split = text.Split(':');

            if (IsNull(split))
                return text;

            return split[0];
        }

        public static string[] ExtractParameters(string text)
        {
            string[] split = text.Split(':');

            if (!ValidSplit(text, split))
                return null;

            return split[1].Split(',');
        }

        private static bool ValidSplit(string text, string[] split)
        {
            if (IsNull(split) || !IsValidLength(split) || IsBlank(split[1]))
                return false;

            return true;
        }

        private static bool IsNull(string[] split)
        {
            return split == null;
        }

        public static bool IsValidLength(string[] split)
        {
            return split.Length > 1;
        }

        private static bool IsBlank(string split)
        {
            return split.Equals("");
        }

        public static int ExtractInt(string parameter)
        {
            try   { return int.Parse(parameter); }
            catch { return 0; }
        }

        public static float ExtractFloat(string parameter)
        {
            try   { return float.Parse(parameter); }
            catch { return 0.0f; }
        }

        public static string ExtractString(string parameter)
        {
            try   { return parameter; }
            catch { return "error"; }
        }

        public static char ExtractChar(string parameter)
        {
            try   { return char.Parse(parameter); }
            catch { return 'e'; }
        }

        public static bool ExtractBool(string parameter)
        {
            try   { return bool.Parse(parameter); }
            catch { return false; }
        }
    }
}
