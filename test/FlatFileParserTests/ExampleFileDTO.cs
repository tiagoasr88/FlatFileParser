using FlatFileParser.Attributes;
using FlatFileParser.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileParserTests
{
    public class ExampleFileDTO
    {
        public ExampleHeaderDTO Header { get; set; } = new ExampleHeaderDTO();
        public List<ExampleLineDTO> Details { get; set; } = new List<ExampleLineDTO>();
        public ExampleTrailerDTO Trailer { get; set; } = new ExampleTrailerDTO();

        public ExampleFileDTO()
        {
            this.Header = new ExampleHeaderDTO();
            this.Details = new List<ExampleLineDTO>();
            this.Trailer = new ExampleTrailerDTO();
        }

        public void Parse(Stream stream)
        {
            using (stream)
            using (var sReader = new StreamReader(stream))
                while (!sReader.EndOfStream)
                {
                    var line = sReader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var lineHeader = this.GetLineHeader(line);

                        switch (lineHeader)
                        {
                            case "H":
                                this.Header.Parse(line);
                                break;
                            case "D":
                                this.Details.Add(new ExampleLineDTO());
                                this.Details.Last().Parse(line);
                                break;
                            case "T":
                                this.Trailer.Parse(line);
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
        public string Identifier { get; set; } = String.Empty;

        [FixedLengthField(1, 4)]
        public string FileVersion { get; set; } = string.Empty;

        [FixedLengthField(5, 8, "yyyyMMdd")]
        public DateTimeOffset Date { get; set; }
    }

    public class ExampleLineDTO : FlatFileFixedLengthLine
    {
        [FixedLengthField(0, 1)]
        public string Identifier { get; set; } = String.Empty;

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
        public string Identifier { get; set; } = String.Empty;
        [FixedLengthField(1, 7)]
        public int DetailsCount { get; set; }
    }
}
