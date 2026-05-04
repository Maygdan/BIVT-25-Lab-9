namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output;
        public string[] Output => _output;

        public Task4(string text) : base(text)
        {
            _output = [];
        }

        public override void Review()
        {
            if (Input == null) return;

            string[] names = Input.Split([',', ' '], StringSplitOptions.RemoveEmptyEntries);
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
            return string.Join("\n", _output);
        }
    }
}