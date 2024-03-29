using System.Collections;

namespace AdventOfCode2022;

public class InputReader : IEnumerable<Token>
{
    private readonly FileInfo _file;

    public InputReader(string filename)
    {
        _file = new FileInfo($"{filename}.txt");
    }

    IEnumerator<Token> IEnumerable<Token>.GetEnumerator()
    {
        return new InputLineEnumerator(_file);
    }

    public IEnumerator GetEnumerator()
    {
        return new InputLineEnumerator(_file);
    }

    private class InputLineEnumerator : IEnumerator<Token>
    {
        public Token Current => _current!;


        private readonly StreamReader _reader;
        private Token? _current;

        public InputLineEnumerator(FileInfo file)
        {
            try
            {
                _reader = file.OpenText();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Cannot open file");
            }
            _current = default;
        }

        public bool MoveNext()
        {
            if (_reader.EndOfStream) return false;
            var line = _reader.ReadLine();

            if (line == null) return false;

            _current = new Token(line);
            return true;
        }

        public void Reset()
        {
            _reader.DiscardBufferedData();
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        object? IEnumerator.Current => _current;

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}

public class Token
{
    private readonly string _segment;

    public Token(string segment)
    {
        _segment = segment;
    }

    public Token[] GetChildTokens(params char[] separators) => _segment.Split(separators).Select(s => new Token(s)).ToArray();

    public bool IsEmpty => string.IsNullOrWhiteSpace(_segment);

    public string StringValue => _segment;
    public int? NumericValue => int.TryParse(_segment, out var result) ? result : (int?)null;
}
