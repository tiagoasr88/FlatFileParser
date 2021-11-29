using FlatFileParser.Attributes;
using FlatFileParser.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlatFileParserTests.FixedLengthTests.DTOs
{
    public class ExampleFileDTO
    {
        public ExampleHeaderDTO Header { get; set; }
        public List<ExampleLineDTO> Details { get; set; }
        public ExampleTrailerDTO Trailer { get; set; }

        public ExampleFileDTO()
        {
            Header = new ExampleHeaderDTO();
            Details = new List<ExampleLineDTO>();
            Trailer = new ExampleTrailerDTO();
        }

        public void Read(Stream stream)
        {
            using (stream)
            using (var sReader = new StreamReader(stream))
                while (!sReader.EndOfStream)
                {
                    var line = sReader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var lineHeader = GetLineHeader(line);

                        switch (lineHeader)
                        {
                            case "H":
                                Header.Read(line);
                                break;
                            case "D":
                                Details.Add(new ExampleLineDTO());
                                Details.Last().Read(line);
                                break;
                            case "T":
                                Trailer.Read(line);
                                break;
                            default:
                                break;
                        }
                    }
                }
        }

        private string GetLineHeader(string line)
        {
            return line.Substring(0, 1);
        }

    }
    public class ExampleHeaderDTO : FlatFileFixedLengthLine
    {
        [FixedLengthField(0, 1)]
        public string Identifier { get; set; } = string.Empty;

        [FixedLengthField(1, 4)]
        public string FileVersion { get; set; } = string.Empty;

        [FixedLengthField(5, 8, "yyyyMMdd")]
        public DateTimeOffset Date { get; set; }
    }

    public class ExampleLineDTO : FlatFileFixedLengthLine
    {
        [FixedLengthField(0, 1)]
        public string Identifier { get; set; } = string.Empty;

        [FixedLengthField(1, 2)]
        public int Id { get; set; }
        [FixedLengthField(3, 10)]
        public string CustomerName { get; set; } = string.Empty;
        [FixedLengthField(13, 8, dateFormat: "yyyyMMdd")]
        public DateTime CreationDate { get; set; }
        [FixedLengthField(21, 6, cultureInfoName: "pt-Br")]
        public decimal Amount { get; set; }
        [FixedLengthField(27, 6, cultureInfoName: "en-Us")]
        public double Weight { get; set; }
        [FixedLengthField(33, 8, dateFormat: "ddMMyyyy")]
        public DateTimeOffset BillingDate { get; set; }
    }

    public class ExampleTrailerDTO : FlatFileFixedLengthLine
    {
        [FixedLengthField(0, 1)]
        public string Identifier { get; set; } = string.Empty;
        [FixedLengthField(1, 7)]
        public int DetailsCount { get; set; }
    }
}
