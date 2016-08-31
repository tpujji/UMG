using GRMApp.Platform.Common;
using GRMApp.Platform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMApp.Platform.UseCases {
    public class MusicContractUseCase: BaseUseCase{
        private List<ContractInformation> _ContractInformationList;
        public MusicContractUseCase() {
            _ContractInformationList = new List<ContractInformation>();
        }

        public ActionStatus UploadContractInformation(string fileLocation) {
            // clear existing contract info
            _ContractInformationList.Clear();

            try {
                string line;

                using (StreamReader sr = new StreamReader(fileLocation)) {
                    var lineCounter = 0;

                    while ((line = sr.ReadLine()) != null) {
                        if (lineCounter!=0) {
                            // Read from second line skipping the line header info
                            // Parse
                            string[] segments = line.Split('|');

                            string[] usageSegment = segments[2].Split(',');

                            foreach (var usageItem in usageSegment) {
                                // Hyderate
                                DateTime tempDT;
                                var musicContract = new ContractInformation();
                                // Error checking can be applied
                                musicContract.Artist = segments[0];
                                musicContract.Title = segments[1];

                                musicContract.Usages = usageItem.Trim();

                                musicContract.StartDate =  (DateTime.TryParse(segments[3], out tempDT) ? tempDT : DateTime.MinValue);
                                if (DateTime.TryParse(segments[4], out tempDT)) {
                                    musicContract.EndDate = tempDT;
                                } else {
                                    musicContract.EndDate = null;
                                }


                                // Add to collection
                                _ContractInformationList.Add(musicContract);
                            }

                           
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

        public List<ContractInformation> GetAllMusicContracts() {
            return _ContractInformationList;
        }

        public List<ContractInformation> GetContractByArtistAndReleaseDate(string Partner, DateTime releaseDateTime, List<DistributionPartner> distributionPartner) {
            

            try {
                var matchPartner = (from d in distributionPartner
                             where d.Partner.Equals(Partner,StringComparison.OrdinalIgnoreCase)
                             select d).First();
                var matchedContracts = (from c in _ContractInformationList
                                        
                                        where c.UsageList.Contains(matchPartner.Usage)
                                              
                                           && (releaseDateTime.Ticks > c.StartDate.Ticks && (releaseDateTime.Ticks < c.EndDate.GetValueOrDefault().Ticks || c.EndDate == null))
                                        select c).OrderBy(a=>a.Artist).ThenBy(t=>t.Title).ToList();

                return matchedContracts;
            } catch (Exception exp) {
                // log exception

                // and rethrow if required

                return null;
            }
        }

    }
}
