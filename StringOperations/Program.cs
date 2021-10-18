using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StringOperations
{
    class Program
    {
        const char TAG_OPEN = '<';
        const char TAG_CLOSE = '>';
        const char TAG_PAIR_CLOSE = '/';

        static void Main(string[] args)
        {
            //string test = "xx<upcase>aaa</upcase>xx<upcase>bbb</upcase>xx";
            //Console.WriteLine(ProcessUpcase(test));
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            string s = "";
            for (int i = 0; i < 100000; i++)
            {
                s += "a";
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append("a");
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        static int GetNextOpenTagIndex(string s, int startIndex) => s.IndexOf(TAG_OPEN, startIndex);
        static int GetNextCloseTagIndex(string s, int startIndex) => s.IndexOf(TAG_CLOSE, startIndex);

        static string ProcessUpcase(string s)
        {
            ProcessUpcaseResult result = ProcessNextUpcase(s, 0);
            if (!result.Success)
                return s;

            while (result.Success)
            {
                s = result.Result;
                result = ProcessNextUpcase(s, 0);
            }
            return s;
        }

        static ProcessUpcaseResult ProcessNextUpcase(string s, int startIndex)
        {
            try
            {
                PairTag t = ReadNextPairTag(s, startIndex);
                while (t.OpenTag.Name != "upcase")
                    t = ReadNextPairTag(s, t.CloseTag.LocationInText + t.CloseTag.Name.Length + 2);

                string result = ReplacePairTagInString(s, t, t.Contents.ToUpper());

                return new ProcessUpcaseResult(result, t, true);
            }
            catch (TagNotFoundException)
            {
                return new ProcessUpcaseResult(null, null, false);
            }
        }

        static string ReplacePairTagInString(string s, PairTag pairTag, string contents)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s[0..pairTag.OpenTag.LocationInText]);
            sb.Append(contents);
            sb.Append(s[(pairTag.CloseTag.LocationInText + pairTag.CloseTag.Name.Length + 2)..s.Length]);
            return sb.ToString();
        }

        static PairTag ReadNextPairTag(string s, int startIndex)
        {
            Tag startTag = ReadNextTag(s, startIndex);
            while (startTag.Name.StartsWith('/'))
                startTag = ReadNextTag(s, startTag.LocationInText + startTag.Name.Length + 2);

            Tag endTag = ReadNextTag(s, startTag.LocationInText + startTag.Name.Length + 2);
            while(endTag.Name != TAG_PAIR_CLOSE + startTag.Name)
                endTag = ReadNextTag(s, endTag.LocationInText + endTag.Name.Length + 2);

            return new PairTag(startTag, endTag, s[(startTag.LocationInText + startTag.Name.Length + 2)..endTag.LocationInText]);
        }

        static Tag ReadNextTag(string s, int startIndex)
        {
            int tagStart = GetNextOpenTagIndex(s, startIndex);
            int tagEnd = GetNextCloseTagIndex(s, tagStart + 1);
            if (tagStart < 0 || tagEnd < 0)
                throw new TagNotFoundException();

            return new Tag(s[(tagStart + 1)..tagEnd], tagStart);
        }
    }

    public struct PairTag
    {
        public PairTag(Tag openTag, Tag closeTag, string contents)
        {
            OpenTag = openTag;
            CloseTag = closeTag;
            Contents = contents;
        }

        public Tag OpenTag { get; set; }
        public Tag CloseTag { get; set; }
        public string Contents { get; set; }
    }

    public struct Tag
    {
        public Tag(string name, int locationInText)
        {
            Name = name;
            LocationInText = locationInText;
        }

        public string Name { get; set; }
        public int LocationInText { get; set; }
    }

    public struct ProcessUpcaseResult
    {
        public ProcessUpcaseResult(string result, PairTag? surroundingTag, bool success)
        {
            Result = result;
            SurroundingTag = surroundingTag;
            Success = success;
        }

        public string Result { get; set; }
        public PairTag? SurroundingTag { get; set; }
        public bool Success { get; set; }
    }

    public class TagNotFoundException : ApplicationException
    {

    }
}
