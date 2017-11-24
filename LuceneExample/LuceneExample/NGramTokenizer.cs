using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;
using System;
using System.IO;

namespace LuceneExample
{
    public class NGramTokenizer : Tokenizer
    {
        public const int DefaultNGramSize = 3;

        private int _mGramSize;
        private int _mCurrentGramSize;
        private int _mPos;
        private int _mInLen;
        private string _mInStr;
        private bool _mStarted;

        private TermAttribute _mTermAtt;
        private OffsetAttribute _mOffsetAtt;

        public NGramTokenizer(TextReader input, int gramSize)
            : base(input)
        {
            Init(gramSize);
        }

        public NGramTokenizer(AttributeSource source, TextReader input,
                int gramSize)
            : base(source, input)
        {
            Init(gramSize);
        }

        public NGramTokenizer(AttributeFactory factory, TextReader input,
                int gramSize)
            : base(factory, input)
        {
            Init(gramSize);
        }

        public NGramTokenizer(TextReader input)
            : this(input, DefaultNGramSize)
        {
        }

        private void Init(int gramSize)
        {
            if (gramSize < 1)
            {
                throw new ArgumentException(
                        "minGram must be greater than zero");
            }
            _mGramSize = gramSize;

            _mTermAtt = (TermAttribute)AddAttribute(typeof(TermAttribute));
            _mOffsetAtt = (OffsetAttribute)AddAttribute(typeof(OffsetAttribute));
        }

        /** Returns the next token in the stream, or null at EOS. */

        public override bool IncrementToken()
        {
            ClearAttributes();
            if (!_mStarted)
            {
                _mStarted = true;
                _mCurrentGramSize = _mGramSize;
                var chars = new char[1024];
                var read = input.Read(chars, 0, chars.Length);
                _mInStr = new string(chars, 0, read).Trim(); // remove any trailing empty strings
                _mInLen = _mInStr.Length;
            }

            if (_mPos + _mCurrentGramSize > _mInLen)
            {
                _mPos = 0;
                _mCurrentGramSize++; // increase n-gram size
                if (_mCurrentGramSize > _mGramSize)
                    return false;
                if (_mPos + _mGramSize > _mInLen)
                    return false;
            }

            var oldPos = _mPos;
            _mPos++;
            _mTermAtt.SetTermBuffer(_mInStr, oldPos, _mCurrentGramSize);
            _mOffsetAtt.SetOffset(CorrectOffset(oldPos), CorrectOffset(oldPos + _mCurrentGramSize));

            return true;
        }

        public override void End()
        {
            // set final offset
            _mOffsetAtt.SetOffset(_mInLen, _mInLen);
        }

        public override void Reset(TextReader trInput)
        {
            base.Reset(trInput);
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            _mStarted = false;
            _mPos = 0;
        }
    }
}