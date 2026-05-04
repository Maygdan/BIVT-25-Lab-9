namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;
        public char[] Output => _output;

        public Task2(string text) : base(text)
        {
            _output = [];
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
            string[] tokens = cleaned.Split([ ' ' ], StringSplitOptions.RemoveEmptyEntries);

            var words = tokens.Where(t => !t.Any(char.IsDigit) && t.Any(char.IsLetter)).ToArray();

            if (words.Length == 0)
            {
                _output = [];
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
}