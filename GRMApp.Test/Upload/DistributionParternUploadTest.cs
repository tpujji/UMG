using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GRMApp.Platform.Common;
using GRMApp.Platform.UseCases;

namespace GRMApp.Test.Upload {
    [TestClass]
    public class DistributionParternUploadTest {
        [TestMethod]
        public void ValidFilePathShouldUPloadDistributionPartners() {
            // Arrange
            string distributionPartnerFileFullPath = @"C:\Working\distributionpartners.txt";
            var distributionPartnerUseCase = new DistributionPartnerUseCase();

            // Act
            var processStatus = distributionPartnerUseCase.UploadDistributionPartners(distributionPartnerFileFullPath);

            // Assert
            Assert.AreEqual(ActionStatus.Success, processStatus);
        }
    }
}
