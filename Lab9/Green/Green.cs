using System;
using System.Linq;

namespace Lab9.Green
{
    public abstract class Green
    {
        private string _input;
        public string Input => _input;

        protected Green(string text)
        {
            _input = text ?? string.Empty;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text ?? string.Empty;
            Review();
        }
    }

    public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;

        public Task1(string text) : base(text)
        {
            _output = new (char, double)[0];
        }

        public override void Review()
        {
            if (Input == null) return;

            string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string text = Input.ToLower();

            int totalLetters = text.Count(c => char.IsLetter(c));
            if (totalLetters == 0)
            {
                _output = new (char, double)[0];
                return;
            }

            _output = russianAlphabet
                .Select(letter => (
                    letter,
                    (double)text.Count(c => c == letter) / totalLetters
                ))
                .Where(x => x.Item2 > 0)
                .OrderBy(x => x.Item1)
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join("\n", _output.Select(x => $"{x.Item1}:{x.Item2:F4}"));
        }
    }

    public class Task2 : Green
    {
        private char[] _output;
        public char[] Output => _output;

        public Task2(string text) : base(text)
        {
            _output = new char[0];
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input)) return;

            char[] chars = Input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsLetterOrDigit(chars[i]) && chars[i] != '-' && chars[i] != '`' && chars[i] != '\'')
                    chars[i] = ' ';
            }

            string cleaned = new string(chars);
            string[] tokens = cleaned.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var words = tokens.Where(t => !t.Any(char.IsDigit) && t.Any(char.IsLetter)).ToArray();

            if (words.Length == 0)
            {
                _output = new char[0];
                return;
            }

            _output = words
                .Select(w => char.ToLower(w[0]))
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Select(g => g.Key)
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(", ", _output);
        }
    }

    public class Task3 : Green
    {
        private string _pattern;
        private string[] _output;
        public string[] Output => _output;

        public Task3(string text, string pattern) : base(text)
        {
            _pattern = pattern ?? string.Empty;
            _output = new string[0];
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(_pattern)) return;

            char[] chars = Input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsLetter(chars[i]) && chars[i] != '-' && chars[i] != '`')
                    chars[i] = ' ';
            }

            string cleaned = new string(chars);
            string[] words = cleaned.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            _output = words
                .Where(w => w.Contains(_pattern, StringComparison.OrdinalIgnoreCase))
                .GroupBy(w => w.ToLower())
                .Select(g => g.First())
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }
    }

    public class Task4 : Green
    {
        private string[] _output;
        public string[] Output => _output;

        public Task4(string text) : base(text)
        {
            _output = new string[0];
        }

        public override void Review()
        {
            if (Input == null) return;

            string[] names = Input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 0) return;

            for (int i = 0; i < names.Length - 1; i++)
            {
                for (int j = 0; j < names.Length - i - 1; j++)
                {
                    if (IsGreater(names[j], names[j + 1]))
                    {
                        string temp = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = temp;
                    }
                }
            }
            _output = names;
        }

        private bool IsGreater(string s1, string s2)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int minLen = len1 < len2 ? len1 : len2;

            for (int i = 0; i < minLen; i++)
            {
                if (s1[i] > s2[i]) return true;
                if (s1[i] < s2[i]) return false;
            }
            return len1 > len2;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }
    }
}