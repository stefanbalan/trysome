using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022
{
    public class InputReader : IEnumerable<InputTextLine>
    {
        private readonly FileInfo _file;

        public InputReader(string filename)
        {
            _file = new FileInfo($"{filename}.txt");
        }

        IEnumerator<InputTextLine> IEnumerable<InputTextLine>.GetEnumerator()
        {
            return new InputLineEnumerator(_file);
        }

        public IEnumerator GetEnumerator()
        {
            return new InputLineEnumerator(_file);
        }

        private class InputLineEnumerator : IEnumerator<InputTextLine>
        {
            public InputTextLine Current { get; private set; }

            private readonly StreamReader _reader;

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
            }

            public bool MoveNext()
            {
                if (_reader.EndOfStream) return false;
                var line = _reader.ReadLine();
                Current = new InputTextLine(line);
                return true;
            }

            public void Reset()
            {
                _reader.DiscardBufferedData();
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _reader?.Dispose();
            }
        }
    }

    public class InputTextLine
    {
        public InputTextLine(string line)
        {
            StringValue = line;
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(StringValue);
        public string StringValue { get; }
        public int? NumericValue => int.TryParse(StringValue, out var result) ? result : (int?)null;
    }
}
