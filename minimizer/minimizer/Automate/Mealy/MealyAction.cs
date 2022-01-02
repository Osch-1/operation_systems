﻿namespace minimizer.Automate.Mealy
{
    public class MealyAction : IAction, IEquatable<MealyAction>
    {
        private readonly MealyState _state;
        private readonly Output _output;

        public MealyState State => _state;
        public Output Output => _output;

        public MealyAction( MealyState state, Output output )
        {
            _state = state;
            _output = output;
        }

        public bool Equals( MealyAction other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            return _state.Name.Equals( other._state.Name )
                && _output.Equals( other._output );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MealyAction );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _state, _output );
        }

        public override string ToString()
        {
            return $"{_state}/{_output}";
        }
    }
}