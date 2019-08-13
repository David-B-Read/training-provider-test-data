
namespace TrainingProviderTestData.Application.Models
{
    using CsvHelper.Configuration.Attributes;
    using System;

    public class CompaniesHouseDataEntry
    {
        [Name(" CompanyNumber")]
        public string CompanyNumber { get; set; }

        public string CompanyName { get; set; }

        public DateTime? IncorporationDate { get; set; }

        public DateTime? DissolutionDate { get; set; }

        public string CompanyCategory { get; set; }

        public string CompanyStatus { get; set; }

        public string CountryOfOrigin { get; set; }

        public int DirectorsCount { get; set; }

        public int PersonsSignificantControlCount { get; set; }
    }
}
