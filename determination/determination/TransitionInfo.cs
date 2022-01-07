
namespace determination
{
    public class TransitionInfo
    {
        public const string _finalState = "F";

        public string From { get; set; }
        public string Signal { get; set; }
        public string To { get; set; }

        public override string ToString()
        {
            return $"{From}({Signal})->{To}";
        }

        public static TransitionInfo FromSymbolAndRightTransition( string symbol, string transition )
        {
            if ( transition.Length == 1 )
            {
                return new TransitionInfo
                {
                    From = symbol,
                    Signal = transition,
                    To = _finalState
                };
            }

            return new TransitionInfo
            {
                From = symbol,
                Signal = transition[ 0 ].ToString(),
                To = transition[ 1 ].ToString()
            };
        }

        public static TransitionInfo FromSymbolAndLeftTransition( string symbol, string transition )
        {
            if ( transition.Length == 1 )
            {
                return new TransitionInfo
                {
                    From = _finalState,
                    Signal = transition,
                    To = symbol
                };
            }

            return new TransitionInfo
            {
                From = transition[ 0 ].ToString(),
                Signal = transition[ 1 ].ToString(),
                To = symbol
            };
        }
    }
}