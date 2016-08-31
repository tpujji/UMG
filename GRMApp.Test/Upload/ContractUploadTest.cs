using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GRMApp.Platform.UseCases;
using GRMApp.Platform.Common;

namespace GRMApp.Test.Upload {
    [TestClass]
    public class ContractUploadTest {
        [TestMethod]
        public void ValidFilePathShouldUPloadContracts() {
            // Arrange
            string contractFileFullPath = @"C:\Working\musiccontract.txt";
            var musicContractUseCase = new MusicContractUseCase();

            // Act
            var processStatus = musicContractUseCase.UploadContractInformation(contractFileFullPath);

            // Assert
            Assert.AreEqual(ActionStatus.Success, processStatus);
        }
    }
}
