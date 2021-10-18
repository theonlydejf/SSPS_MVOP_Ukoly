namespace BracketValidator
{
    public struct BracketValidatorSettings
    {
        public char[] OpenBrackets { get; set; }
        public char[] CloseBrackets { get; set; }

        public static BracketValidatorSettings Default
        {
            get => new BracketValidatorSettings(new[] { '(', '[', '{', }, new[] { ')', ']', '}', });
        }

        public BracketValidatorSettings(char[] openBrackets, char[] closeBrackets)
        {
            OpenBrackets = openBrackets;
            CloseBrackets = closeBrackets;
        }
    }
}