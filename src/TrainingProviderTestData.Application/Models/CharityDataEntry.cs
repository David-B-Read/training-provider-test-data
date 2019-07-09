
namespace TrainingProviderTestData.Application.Models
{
    using System;
    using CsvHelper.Configuration.Attributes;

    public class CharityDataEntry
    {
        [Name("title")]
        public string CharityName { get; set; }
        [Name("charity_number")]
        public string CharityNumber { get; set; }
        [Name("date_registered")]
        public DateTime? DateRegistered { get; set; }
        [Name("date_removed")]
        public DateTime? DateRemoved { get; set; }
    }
}
