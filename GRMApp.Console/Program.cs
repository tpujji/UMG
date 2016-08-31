using GRMApp.Platform.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMApp.Console {
    class Program {
        static void Main(string[] args) {
            System.Console.WriteLine("Please provide contract file path (ex C:\\Working\\musiccontract.txt):");
            string contractFilePath = System.Console.ReadLine();

            System.Console.WriteLine("Please provide partner file path (ex C:\\Working\\distributionpartners.txt):");
            string partnerFilePath = System.Console.ReadLine();

            System.Console.WriteLine("Please provide partner name (ex YouTube):");
            var partnerName = System.Console.ReadLine();

            System.Console.WriteLine("Please provide release date (ex 03-12-2012):");
            var tmpDate = System.Console.ReadLine();

            var releaseDate = Convert.ToDateTime(tmpDate);

            var musicContractUseCase = new MusicContractUseCase();
            var distributionPartnerUseCase = new DistributionPartnerUseCase();

            var processStatus = musicContractUseCase.UploadContractInformation(contractFilePath);
            processStatus = distributionPartnerUseCase.UploadDistributionPartners(partnerFilePath);
            var partnerList = distributionPartnerUseCase.GetAllDistributionPartners();

            var actualResultData = musicContractUseCase.GetContractByArtistAndReleaseDate(partnerName, releaseDate, partnerList);

            System.Console.WriteLine("|{0,-20}|{1,-30}|{2,-20}|{3,-20}|{4,-20}|",
                "Artist","Title","Usage","StartDate","EndDate"
                );

            foreach (var match in actualResultData) {
                System.Console.WriteLine("|{0,-20}|{1,-30}|{2,-20}|{3,-20}|{4,-20}|",
                    match.Artist
                 , match.Title
                 , match.Usages
                 , match.StartDate.ToString("MM-dd-yyyy")
                 , (match.EndDate == null ? "" : match.EndDate.Value.ToString("MM-dd-yyyy"))

               );
            }
            

        }
    }
}


