using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GRMApp.Platform.UseCases;
using GRMApp.Platform.Models;
using System.Collections.Generic;
using System.IO;

namespace GRMApp.Test {
    [TestClass]
    public class ContractSearchTest: BaseTest {
        
        [TestMethod]
        public void SenarioTestShouldReturnExpectedList_1() {
            // Arrange
            string partner = "ITunes";
            DateTime releaseDate = Convert.ToDateTime("03/01/2012");
            string contractFileFullPath = string.Format("{0}\\Data\\musiccontract.txt", BasePath);
            string distributionPartnerFileFullPath = string.Format("{0}\\Data\\distributionpartners.txt", BasePath);

            var musicContractUseCase = new MusicContractUseCase();
            var distributionPartnerUseCase = new DistributionPartnerUseCase();
            
            var processStatus = musicContractUseCase.UploadContractInformation(contractFileFullPath);
            processStatus = distributionPartnerUseCase.UploadDistributionPartners(distributionPartnerFileFullPath);
            var partnerList = distributionPartnerUseCase.GetAllDistributionPartners();

            var expectedResultData = FetchMockDataForSenario1();
            
            // Act
            var actualResultData = musicContractUseCase.GetContractByArtistAndReleaseDate(partner, releaseDate, partnerList);

            // Assert
            Assert.AreEqual(expectedResultData.Count, actualResultData.Count);
           
            

        }
        [TestMethod]
        public void SenarioTestShouldReturnExpectedList_2() {
            // Arrange
            string partner = "YouTube";
            DateTime releaseDate = Convert.ToDateTime("12/27/2012");
            string contractFileFullPath = @"C:\Working\musiccontract.txt";
            var musicContractUseCase = new MusicContractUseCase();

            string distributionPartnerFileFullPath = @"C:\Working\distributionpartners.txt";
            var distributionPartnerUseCase = new DistributionPartnerUseCase();

            var processStatus = musicContractUseCase.UploadContractInformation(contractFileFullPath);
            processStatus = distributionPartnerUseCase.UploadDistributionPartners(distributionPartnerFileFullPath);
            var partnerList = distributionPartnerUseCase.GetAllDistributionPartners();

            var expectedResultData = FetchMockDataForSenario2();

            // Act
            var actualResultData = musicContractUseCase.GetContractByArtistAndReleaseDate(partner, releaseDate, partnerList);

            // Assert
            Assert.AreEqual(expectedResultData.Count, actualResultData.Count);



        }

        [TestMethod]
        public void SenarioTestShouldReturnExpectedList_3() {
            // Arrange
            string partner = "YouTube";
            DateTime releaseDate = Convert.ToDateTime("04/01/2012");
            string contractFileFullPath = @"C:\Working\musiccontract.txt";
            var musicContractUseCase = new MusicContractUseCase();

            string distributionPartnerFileFullPath = @"C:\Working\distributionpartners.txt";
            var distributionPartnerUseCase = new DistributionPartnerUseCase();

            var processStatus = musicContractUseCase.UploadContractInformation(contractFileFullPath);
            processStatus = distributionPartnerUseCase.UploadDistributionPartners(distributionPartnerFileFullPath);
            var partnerList = distributionPartnerUseCase.GetAllDistributionPartners();

            var expectedResultData = FetchMockDataForSenario3();

            // Act
            var actualResultData = musicContractUseCase.GetContractByArtistAndReleaseDate(partner, releaseDate, partnerList);

            // Assert
            Assert.AreEqual(expectedResultData.Count, actualResultData.Count);



        }

        private List<ContractInformation> FetchMockDataForSenario1() {
            return HyderateContractBySenario("Senario1DataFile");
        }
        private List<ContractInformation> FetchMockDataForSenario2() {
            return HyderateContractBySenario("Senario2DataFile");
        }
        private List<ContractInformation> FetchMockDataForSenario3() {
            return HyderateContractBySenario("Senario3DataFile");
        }

        private List<ContractInformation> HyderateContractBySenario(string senarioId) {
            var dataFilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

            string senarioDataFile = string.Format("{0}\\Data\\{1}.txt",dataFilePath,senarioId);

            var ContractInformationList = new List<ContractInformation>();
            string line;

            using (StreamReader sr = new StreamReader(senarioDataFile)) {
                var lineCounter = 0;

                while ((line = sr.ReadLine()) != null) {
                    if (lineCounter!=0) {
                        // Read from second line skipping the line header info
                        // Parse
                        string[] segments = line.Split('|');

                        // Hyderate
                        DateTime tempDT;
                        var musicContract = new ContractInformation();
                        // Error checking can be applied
                        musicContract.Artist = segments[0];
                        musicContract.Title = segments[1];
                        musicContract.Usages = segments[2];
                        musicContract.StartDate =  (DateTime.TryParse(segments[3], out tempDT) ? tempDT : DateTime.MinValue);
                        if (DateTime.TryParse(segments[4], out tempDT)) {
                            musicContract.EndDate = tempDT;
                        } else {
                            musicContract.EndDate = null;
                        }

                        // Add to collection
                        ContractInformationList.Add(musicContract);
                    }

                    lineCounter++;
                }
            }

            return ContractInformationList;
        }
    }
}
