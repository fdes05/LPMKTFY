using NUnit.Framework;
using System;

namespace Utilities.Test
{    
    public class StringToolsTest
    {
        //[Setup] this is a way to setup testing for all the methods in this Test class so you don't have to do the 
        //'[Test]' annotation on each method

        //put the 'Test' annotation on the method that NUnit needs to pick up for testing a class or function
        [Test]
        public void SpringTools_Combine_ReturnsCorrectResult()
        {
            //Arrange (in case some data modification needs to happen to setup a test case)

            //Act
            var result = StringTools.Combine("Part1", "Part2");

            //Assert
            Assert.That(result, Is.EqualTo("Part1 Part2"));
        }

        [Test]
        public void SpringTools_SubString_ReturnsEmptyOnToShortString()
        {
            //Arrange (in case some data modification needs to happen to setup a test case)

            //Act
            var result = StringTools.SubString("My String", 7, 12);

            //Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void SpringTools_SubString_ReturnsCorrectSubString()
        {
            //Arrange (in case some data modification needs to happen to setup a test case)

            //Act
            var result = StringTools.SubString("My String", 3, 6);

            //Assert
            Assert.That(result, Is.EqualTo("String"));
        }
    }
}
