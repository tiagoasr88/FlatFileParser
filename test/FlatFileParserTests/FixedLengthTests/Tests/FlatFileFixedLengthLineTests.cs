using FlatFileParserTests.FixedLengthTests.DTOs;
using System;
using Xunit;

namespace FlatFileParserTests.FixedLengthTests.Tests
{
    public class FlatFileFixedLengthLineTests
    {
        [Fact]
        public void ParseTestWithSuccess()
        {
            ExampleLineDTO lineDto = new ExampleLineDTO();

            lineDto.Read("D01John Doe  20210702100,12987.8918022021");

            Assert.Equal("D", lineDto.Identifier);
            Assert.Equal(1, lineDto.Id);
            Assert.Equal("John Doe", lineDto.CustomerName);
            Assert.Equal(new DateTime(2021, 07, 02), lineDto.CreationDate);
            Assert.Equal(100.12m, lineDto.Amount);
            Assert.Equal(987.89d, lineDto.Weight);
            Assert.Equal(new DateTimeOffset(new DateTime(2021, 02, 18)), lineDto.BillingDate);
        }

        [Fact]
        public void ParseTestWithFormatException()
        {
            ExampleLineDTO lineDto = new ExampleLineDTO();

            Assert.Throws<FormatException>(() => lineDto.Read("D01John Doe  A0210702100,12987.8918022021"));
        }
    }
}
