using FlatFileParserTests.FixedLengthTests.DTOs;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace FlatFileParserTests.FixedLengthTests.Tests
{
    public class ParseFileTests
    {
        [Fact]
        public void ParseFileWithTwoDetails()
        {
            var obj = new ExampleFileDTO();

            using (var stream = CreateStreamFromString(TestFileWithTwoDetails()))
            {
                obj.Read(stream);
            }

            Assert.Equal("H", obj.Header.Identifier);
            Assert.Equal("0001", obj.Header.FileVersion);
            Assert.Equal(new DateTime(2021, 07, 02), obj.Header.Date);

            Assert.Equal("D", obj.Details[0].Identifier);
            Assert.Equal(1, obj.Details[0].Id);
            Assert.Equal("John Doe", obj.Details[0].CustomerName);
            Assert.Equal(new DateTime(2021, 07, 02), obj.Details[0].CreationDate);
            Assert.Equal(100.12m, obj.Details[0].Amount);
            Assert.Equal(987.89d, obj.Details[0].Weight);
            Assert.Equal(new DateTimeOffset(new DateTime(2021, 02, 18)), obj.Details[0].BillingDate);

            Assert.Equal("D", obj.Details[1].Identifier);
            Assert.Equal(2, obj.Details[1].Id);
            Assert.Equal("Mary Sue", obj.Details[1].CustomerName);
            Assert.Equal(new DateTime(2020, 08, 03), obj.Details[1].CreationDate);
            Assert.Equal(123.12m, obj.Details[1].Amount);
            Assert.Equal(65.90d, obj.Details[1].Weight);
            Assert.Equal(new DateTimeOffset(new DateTime(2020, 01, 20)), obj.Details[1].BillingDate);

            Assert.Equal("T", obj.Trailer.Identifier);
            Assert.Equal(2, obj.Trailer.DetailsCount);
        }

        private string TestFileWithTwoDetails()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("H000120210702");
            strBuilder.AppendLine("D01John Doe  20210702100,12987.8918022021");
            strBuilder.AppendLine("D02Mary Sue  20200803123,12065.9020012020");
            strBuilder.AppendLine("T0000002");

            return strBuilder.ToString();
        }

        private Stream CreateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}