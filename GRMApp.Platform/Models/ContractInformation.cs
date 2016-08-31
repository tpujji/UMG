using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMApp.Platform.Models {
    public class ContractInformation {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Usages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<string> UsageList {
            get {
                return Usages.Split(',').Select(p => p.Trim()).ToList();
            }
        }
    }
}
