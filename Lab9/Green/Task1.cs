namespace Lab9.Green{
public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;

        public Task1(string text) : base(text)
        {
            _output = [];
        }

        public override void Review()
        {
            if (Input == null) return;

            string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string text = Input.ToLower();

            int totalLetters = text.Count(c => char.IsLetter(c));
            if (totalLetters == 0)
            {
                _output =[];
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
}