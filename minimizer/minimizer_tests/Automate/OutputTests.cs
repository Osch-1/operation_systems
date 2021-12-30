using minimizer.Automate;
using NUnit.Framework;

namespace minimizer_tests.Automate
{
    public class OutputTests
    {
        [Test]
        public void Output_Equals_Null_False()
        {
            //Arrange
            Output src = new( "y0" );
            Output other = null;

            //Act
            bool result = src.Equals( other );

            //Assert
            Assert.False( result, $"{src} can't be equal to null" );
        }

        [Test]
        public void Output_Equals_ReferenceEqualObject_False()
        {
            //Arrange
            Output src = new( "y0" );
            Output other = src;

            //Act
            bool result = src.Equals( other );

            //Assert
            Assert.True( result, $"{src} must be reflexive" );
        }

        [Test]
        public void Output_Equals_EqualByFields_False()
        {
            //Arrange
            Output src = new( "y0" );
            Output other = new( "y0" );

            //Act
            bool result = src.Equals( other );

            //Assert
            Assert.True( result, $"{src} must be equal to {other}" );
        }

        [Test]
        public void Output_Equals_NonEqualByFields_False()
        {
            //Arrange
            Output src = new( "y0" );
            Output other = new( "y1" );

            //Act
            bool result = src.Equals( other );

            //Assert
            Assert.False( result, $"{src} must be non equal to {other}" );
        }
    }
}
