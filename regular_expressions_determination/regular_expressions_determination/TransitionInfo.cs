namespace determination
{
    public class TransitionInfo : IEquatable<TransitionInfo>
    {
        public const string _finalState = "F";

        public string From { get; set; }
        public string Signal { get; set; }
        public string To { get; set; }

        public static TransitionInfo FromSymbolAndTransition( string symbol, string transition )
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

        public override string ToString()
        {
            string nonFinalTo = To == _finalState ? null : To;
            return $"{Signal}{nonFinalTo}";
        }

        public bool Equals( TransitionInfo other )
        {
            return other is TransitionInfo connection
                     && From == connection.From
                     && Signal == connection.Signal
                     && To == connection.To;
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as TransitionInfo );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( From, Signal, To );
        }
    }
}