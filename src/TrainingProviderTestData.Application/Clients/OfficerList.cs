using System;
using System.Collections.Generic;

namespace TrainingProviderTestData.Application.Clients
{
    public class OfficerList
    {
        public int active_count { get; set; }
        public int inactive_count { get; set; }
        public int resigned_count { get; set; }
        public List<Officer> items { get; set; }
    }

    public class Officer
    {
        public string officer_role { get; set; }
        public DateTime? resigned_on { get; set; }
    }
}
