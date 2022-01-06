using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace minimizer.Automate.Moore
{
    public class MooreAutomate : Automate<MooreState>
    {
        public MooreAutomate()
            : base()
        {
        }

        public MooreAutomate( IEnumerable<MooreState> states )
            : base( states )
        {
        }

        public override void Minimize()
        {
            var states = States.ToList();
            List<IGrouping<Output, MooreState>> statesByStaGroup = states.GroupBy( s => s.Output ).ToList();
            List<EqualityClass> equalityClasses = statesByStaGroup.Select( s => new EqualityClass( s ) ).ToList();
            foreach ( EqualityClass equalityClass in equalityClasses )
            {
                foreach ( StateToEqualityClasses stec in equalityClass.StatesToEqualityClass )
                {
                    MooreState state = stec.State;
                    foreach ( SignalToAction<MooreAction> signalToActions in state.SignalsToActions.SignalToActions )
                    {
                        MooreState actionState = signalToActions.Action.State;
                        EqualityClass eqClassByActionState = equalityClasses.Find( ec => ec.StatesToEqualityClass.Exists( stc => stc.State.Name.Equals( actionState.Name ) ) );
                        stec.EqualityClasses.Add( eqClassByActionState );
                    }
                }
            }

            List<EqualityClass> prevEqualityClasses = equalityClasses.ToList();
            List<EqualityClass> currentEqualityClasses = null;
            do
            {
                if ( currentEqualityClasses is not null )
                {
                    prevEqualityClasses = currentEqualityClasses.ToList();
                }
                List<EqualityClass> tempPrev = prevEqualityClasses.ToList();
                currentEqualityClasses = new();
                //generate new equality classes
                foreach ( EqualityClass equalityClass in tempPrev )
                {
                    List<IGrouping<List<EqualityClass>, StateToEqualityClasses>> groupOfEcByAction = equalityClass
                        .StatesToEqualityClass
                        .GroupBy( stec => stec.EqualityClasses, new StateToEqualityClassesListComparer() )
                        .ToList();

                    currentEqualityClasses.AddRange( groupOfEcByAction.Select( g => new EqualityClass( g.Select( st => st.State ) ) ) );
                }
                //generate new equality classes

                //fill state to equality classes in new grouping
                foreach ( EqualityClass newEqualityClass in currentEqualityClasses )
                {
                    foreach ( StateToEqualityClasses stec in newEqualityClass.StatesToEqualityClass )
                    {
                        MooreState state = stec.State;
                        foreach ( SignalToAction<MooreAction> signalToAction in state.SignalsToActions.SignalToActions )
                        {
                            MooreState actionState = signalToAction.Action.State;
                            EqualityClass eqClassByActionState = currentEqualityClasses.Find( ec => ec.StatesToEqualityClass.Exists( stc => stc.State.Name.Equals( actionState.Name ) ) );
                            stec.EqualityClasses.Add( eqClassByActionState );
                        }
                    }
                }
                //fill state to equality classes in new grouping
            } while ( prevEqualityClasses.Count != currentEqualityClasses.Count );

            var newStatesNames = currentEqualityClasses.Select( ec => ec.StatesToEqualityClass.First().State.Name );
            List<MooreState> newStates = States.Where( s => newStatesNames.Contains( s.Name ) ).ToList();
            foreach ( MooreState newState in newStates )
            {
                List<SignalToAction<MooreAction>> newSignalsToActions = new();
                foreach ( SignalToAction<MooreAction> signalToAction in newState.SignalsToActions.SignalToActions )
                {
                    MooreState actionState = signalToAction.Action.State;
                    EqualityClass equalityClassByActionState = currentEqualityClasses.Where( ec => ec.StatesToEqualityClass.Exists( stec => stec.State.Name.Equals( actionState.Name ) ) ).First();
                    string stateNameFromFinalList = equalityClassByActionState.StatesToEqualityClass.First().State.Name;
                    MooreState stateFromFinalList = newStates.First( s => s.Name.Equals( stateNameFromFinalList ) );
                    SignalToAction<MooreAction> newsignalToAction = new( new Signal( signalToAction.Signal.Name ), new MooreAction( stateFromFinalList ) );
                    newSignalsToActions.Add( newsignalToAction );
                }
                newState.SetSignalsToActions( new( newSignalsToActions ) );
            }
            SetStates( newStates );
        }

        public override string ToString()
        {
            StringBuilder result = new( 300 );

            string[] states = States.Select( s => s.ToString() ).ToArray();
            result.AppendLine( string.Join( ' ', states ) );

            List<string> signals = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Select( sta => sta.Signal.Name ).Distinct().ToList();
            for ( int i = 0; i < signals.Count; i++ )
            {
                var signal = signals[ i ];
                List<MooreAction> actions = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Where( sta => sta.Signal.Name.Equals( signal ) ).Select( sta => sta.Action ).ToList();

                result.Append( $"{signal}: " );
                result.AppendLine( string.Join( ' ', actions ) );
            }

            return result.ToString();
        }

        private class StateToEqualityClassesListComparer : IEqualityComparer<List<EqualityClass>>
        {
            public bool Equals( List<EqualityClass> x, List<EqualityClass> y )
            {
                foreach ( EqualityClass s in x )
                {
                    if ( !y.Contains( s ) )
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode( [DisallowNull] List<EqualityClass> obj )
            {
                unchecked
                {
                    int hash = 19;

                    foreach ( EqualityClass s in obj )
                    {
                        hash = hash * s.GetHashCode() ^ 31;
                    }

                    return hash;
                }
            }
        }

        private class StateToEqualityClasses : IEquatable<StateToEqualityClasses>
        {
            public MooreState State { get; set; }
            public List<EqualityClass> EqualityClasses { get; set; } = new();

            public StateToEqualityClasses( MooreState state )
            {
                State = state;
            }

            public bool Equals( StateToEqualityClasses other )
            {
                if ( other is null )
                {
                    return false;
                }

                if ( ReferenceEquals( this, other ) )
                {
                    return true;
                }

                foreach ( EqualityClass s in EqualityClasses )
                {
                    if ( !other.EqualityClasses.Contains( s ) )
                    {
                        return false;
                    }
                }

                return true;
            }

            public override bool Equals( object obj )
            {
                return Equals( obj as StateToEqualityClasses );
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 19;

                    foreach ( var ec in EqualityClasses )
                    {
                        hash = hash * ec.GetHashCode() ^ 31;
                    }

                    return hash;
                }
            }
        }

        private class EqualityClass : IEquatable<EqualityClass>
        {
            private static int counter = 1;

            public string Name { get; set; }
            public List<StateToEqualityClasses> StatesToEqualityClass { get; set; }

            public EqualityClass( IEnumerable<MooreState> states )
            {
                Name = $"A{counter}";
                StatesToEqualityClass = states.Select( s => new StateToEqualityClasses( s ) ).ToList();
                counter++;
            }

            public override string ToString()
            {
                return Name;
            }

            public bool Equals( EqualityClass other )
            {
                if ( other is null )
                {
                    return false;
                }

                if ( ReferenceEquals( this, other ) )
                {
                    return true;
                }

                return Name.Equals( other.Name );
            }

            public override bool Equals( object obj )
            {
                return Equals( obj as EqualityClass );
            }

            public override int GetHashCode()
            {
                return HashCode.Combine( Name );
            }
        }        
    }
}
