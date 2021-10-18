using System;
using Krcmar.MyStack;

namespace BracketValidator
{
    public class BracketValidator
    {
        private BracketValidatorSettings settings;

        public BracketValidator(BracketValidatorSettings settings)
        {
            this.settings = settings;
        }

        public BracketValidator() : this(BracketValidatorSettings.Default) { }

        public int LastErrorPosition { get; private set; }
        public string LastErrorMessage { get; private set; }

        public bool IsSequenceValid(string sequence)
        {
            LastErrorMessage = "None";
            LastErrorPosition = 0;

            MyStack<int> bracketsQueue = new MyStack<int>();

            foreach (var currChar in sequence)
            {
                LastErrorPosition++;

                int openBracketID = GetOpenBracketID(currChar);
                int closeBracketID = GetCloseBracketID(currChar);
                if(openBracketID >= 0)
                {
                    bracketsQueue.Push(openBracketID);
                    continue;
                }
                if(closeBracketID >= 0)
                {
                    if (bracketsQueue.IsEmpty())
                    {
                        LastErrorMessage = "Extra closing bracket";
                        LastErrorPosition--;
                        return false;
                    }
                    if (bracketsQueue.Peek() != closeBracketID)
                    {
                        LastErrorMessage = "Wrong closing bracket";
                        LastErrorPosition--;
                        return false;
                    }
                    
                    bracketsQueue.Pop();
                }
            }

            if (!bracketsQueue.IsEmpty())
                LastErrorMessage = "Forgotten closing bracket";

            return bracketsQueue.IsEmpty();
        }

        private int GetOpenBracketID(char c) => Array.IndexOf(settings.OpenBrackets, c);
        private int GetCloseBracketID(char c) => Array.IndexOf(settings.CloseBrackets, c);

    }
}
