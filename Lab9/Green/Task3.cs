namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _pattern;
        private string[] _output;
        public string[] Output => _output;

        public Task3(string text, string pattern) : base(text)
        {
            _pattern = pattern ?? string.Empty;
            _output = [];
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
            string[] words = cleaned.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            _output = words
                .Where(w => w.Contains(_pattern, StringComparison.OrdinalIgnoreCase))
                .GroupBy(w => w.ToLower())
                .Select(g => g.First())
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join("\n", _output);
        }
    }
}