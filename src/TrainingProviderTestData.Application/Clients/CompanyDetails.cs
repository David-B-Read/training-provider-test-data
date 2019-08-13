using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProviderTestData.Application.Clients
{
    public class CompanyDetails
    {
        public string company_name { get; set; }
        public string company_number { get; set; }
        public string company_status { get; set; }
        public DateTime? date_of_cessation { get; set; }
        public DateTime? date_of_creation { get; set; }
        public bool? has_been_liquidated { get; set; }
        public string partial_data_available { get; set; }
        public List<string> sic_codes { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
        public bool? undeliverable_registered_office_address { get; set; }
    }
}
