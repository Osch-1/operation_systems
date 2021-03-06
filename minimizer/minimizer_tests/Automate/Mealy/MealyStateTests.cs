using System;
using System.Collections.Generic;
using minimizer.Automate;
using minimizer.Automate.Mealy;
using NUnit.Framework;

namespace minimizer_tests.Automate.Mealy
{
    public class MealyStateTests
    {
        [Test]
        public void Expect_Automate_ToBeCorrectlyRepresentAsString()
        {
            //Arrange
            //Arrange
            MealyState ms0 = new( "q0" );
            MealyState ms1 = new( "q1" );
            MealyState ms2 = new( "q2" );

            List<SignalToAction<MealyAction>> signalsToActionsList = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms1, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms0, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions = new( signalsToActionsList );
            ms0.SetSignalsToActions( signalsToActions );

            List<SignalToAction<MealyAction>> signalsToActionsList1 = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms0, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms2, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions1 = new( signalsToActionsList1 );
            ms1.SetSignalsToActions( signalsToActions1 );

            List<SignalToAction<MealyAction>> signalsToActionsList2 = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms0, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms2, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions2 = new( signalsToActionsList2 );
            ms2.SetSignalsToActions( signalsToActions2 );

            MealyAutomate automate = new( new List<MealyState> { ms0, ms1, ms2 } );

            //Act
            string result = automate.ToString();

            //Assert
            Assert.AreEqual( $"q0 q1 q2{Environment.NewLine}x1: q1/y1 q0/y1 q0/y1{Environment.NewLine}x2: q0/y1 q2/y1 q2/y1{Environment.NewLine}", result );
        }

        [Test]
        public void Expect_States_To_Be_Correctly_Compared_For_Equality()
        {
            //Arrange
            MealyState ms0 = new( "q0" );
            MealyState ms1 = new( "q1" );
            MealyState ms2 = new( "q2" );

            //Act
            List<SignalToAction<MealyAction>> signalsToActionsList = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms1, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms0, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions = new( signalsToActionsList );
            ms0.SetSignalsToActions( signalsToActions );

            List<SignalToAction<MealyAction>> signalsToActionsList1 = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms0, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms2, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions1 = new( signalsToActionsList1 );
            ms1.SetSignalsToActions( signalsToActions1 );

            List<SignalToAction<MealyAction>> signalsToActionsList2 = new()
            {
                new SignalToAction<MealyAction>( new Signal( "x1" ), new MealyAction( ms0, new Output( "y1" ) ) ),
                new SignalToAction<MealyAction>( new Signal( "x2" ), new MealyAction( ms2, new Output( "y1" ) ) )
            };
            SignalsToActions<MealyAction> signalsToActions2 = new( signalsToActionsList2 );
            ms2.SetSignalsToActions( signalsToActions2 );

            //Assert
            Assert.AreEqual( ms1, ms2 );
            Assert.AreNotEqual( ms0, ms1 );
            Assert.AreNotEqual( ms0, ms2 );
        }
    }
}
