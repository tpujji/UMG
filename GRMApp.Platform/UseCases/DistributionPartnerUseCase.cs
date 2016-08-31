using GRMApp.Platform.Common;
using GRMApp.Platform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMApp.Platform.UseCases {
    public class DistributionPartnerUseCase: BaseUseCase {
        private List<DistributionPartner> _DistributionPartnerList;
        public DistributionPartnerUseCase() {
            _DistributionPartnerList = new List<DistributionPartner>();
        }

        public ActionStatus UploadDistributionPartners(string fileLocation) {
            // clear existing contract info
            _DistributionPartnerList.Clear();

            try {
                string line;

                using (StreamReader sr = new StreamReader(fileLocation)) {
                    var lineCounter = 0;

                    while ((line = sr.ReadLine()) != null) {
                        if (lineCounter!=0) {
                            // Read from second line skipping the line header info
                            // Parse
                            string[] segments = line.Split('|');

                            // Hyderate
                            var distributionPartner = new DistributionPartner();
                            // Error checking can be applied
                            distributionPartner.Partner = segments[0];
                            distributionPartner.Usage = segments[1];

                            // Add to collection
                            _DistributionPartnerList.Add(distributionPartner);
                        }

                        lineCounter++;
                    }
                }

                return ActionStatus.Success;

            } catch (Exception exp) {
                // log exception

                // and rethrow if required

                return ActionStatus.Failure;
            }

        }

        public List<DistributionPartner> GetAllDistributionPartners() {
            return _DistributionPartnerList;
        }
    }
}
