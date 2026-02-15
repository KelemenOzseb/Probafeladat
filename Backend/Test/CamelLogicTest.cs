using Data.Repository;
using Entity.Dtos.Camel;
using Entity.Models;
using Logic.Helper;
using Logic.Logic;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class CamelLogicTest
    {
        DtoProvider _dtoProvider;
        CamelLogic _camelLogic;
        Mock<IRepository<Camel>> _repositoryMock;
        public CamelLogicTest()
        {
            _repositoryMock = new Mock<IRepository<Camel>>();
            _dtoProvider = new DtoProvider();
            _camelLogic = new CamelLogic(_repositoryMock.Object, _dtoProvider);
        }

        [Fact]
        public async Task CreateCamel_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            var createCamelDto = new CreateCamelDto
            {
                Name = null,
                HumpCount = 1,
                LastFed = DateTime.Now
            };
            await Assert.ThrowsAsync<ArgumentNullException>(() => _camelLogic.CreateCamel(createCamelDto));

            _repositoryMock.Verify(r => r.Create(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task CreateCamel_ShouldThrowArgumentException_WhenHumpCountIsInvalid()
        {
            var createCamelDto = new CreateCamelDto
            {
                Name = "Test Camel",
                HumpCount = 3,
                LastFed = DateTime.Now
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _camelLogic.CreateCamel(createCamelDto));

            _repositoryMock.Verify(r => r.Create(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task CreateCamel_ShouldThrowArgumentException_WhenLastFedIsInTheFuture()
        {
            var createCamelDto = new CreateCamelDto
            {
                Name = "Test Camel",
                HumpCount = 1,
                LastFed = DateTime.Now.AddDays(1)
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _camelLogic.CreateCamel(createCamelDto));

            _repositoryMock.Verify(r => r.Create(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task CreateCamel_ShouldCreateCamel_WhenDataIsValid()
        {
            var createCamelDto = new CreateCamelDto
            {
                Name = "Test Camel",
                HumpCount = 1,
                LastFed = DateTime.Now
            };
            var camel = new Camel(createCamelDto.Name)
            {
                Id = "1",
                HumpCount = createCamelDto.HumpCount,
                LastFed = createCamelDto.LastFed
            };
            _repositoryMock.Setup(r => r.Create(It.IsAny<Camel>())).ReturnsAsync(camel);
            var result = await _camelLogic.CreateCamel(createCamelDto);
            Assert.Equal(camel.Id, result.Id);
            Assert.Equal(camel.Name, result.Name);
            Assert.Equal(camel.HumpCount, result.HumpCount);
            Assert.Equal(camel.LastFed, result.LastFed);
            _repositoryMock.Verify(r => r.Create(It.IsAny<Camel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCamel_ShouldThrowKeyNotFoundException_WhenCamelDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetOne("100")).ReturnsAsync((Camel)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _camelLogic.DeleteCamel("100"));

            _repositoryMock.Verify(r => r.DeleteById(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCamel_ShouldDeleteCamel_WhenCamelExists()
        {
            var camel = new Camel("Test Camel") { Id = "1" };
            _repositoryMock.Setup(r => r.GetOne(It.IsAny<string>())).ReturnsAsync(camel);
            await _camelLogic.DeleteCamel("1");
            _repositoryMock.Verify(r => r.DeleteById("1"), Times.Once);
        }

        [Fact]
        public async Task UpdateCamel_ShouldThrowKeyNotFoundException_WhenCamelDoesNotExist()
        {
            var updateCamelDto = new UpdateCamelDto
            {
                Name = "Updated Camel",
                HumpCount = 2,
                LastFed = DateTime.Now
            };
            _repositoryMock.Setup(r => r.GetOne("100")).ReturnsAsync((Camel)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _camelLogic.UpdateCamel(updateCamelDto, "100"));
            _repositoryMock.Verify(r => r.Update(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCamel_ShouldThrowArgumentException_WhenHumpCountIsInvalid()
        {
            var updateCamelDto = new UpdateCamelDto
            {
                Name = "Updated Camel",
                HumpCount = 3,
                LastFed = DateTime.Now
            };
            var camel = new Camel("Test Camel") { Id = "1" };
            _repositoryMock.Setup(r => r.GetOne("1")).ReturnsAsync(camel);
            await Assert.ThrowsAsync<ArgumentException>(() => _camelLogic.UpdateCamel(updateCamelDto, "1"));
            _repositoryMock.Verify(r => r.Update(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCamel_ShouldThrowArgumentException_WhenLastFedIsInTheFuture()
        {
            var updateCamelDto = new UpdateCamelDto
            {
                Name = "Updated Camel",
                HumpCount = 2,
                LastFed = DateTime.Now.AddDays(1)
            };
            var camel = new Camel("Test Camel") { Id = "1" };
            _repositoryMock.Setup(r => r.GetOne("1")).ReturnsAsync(camel);
            await Assert.ThrowsAsync<ArgumentException>(() => _camelLogic.UpdateCamel(updateCamelDto, "1"));
            _repositoryMock.Verify(r => r.Update(It.IsAny<Camel>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCamel_ShouldUpdateCamel_WhenDataIsValid()
        {
            var updateCamelDto = new UpdateCamelDto
            {
                Name = "Updated Camel",
                HumpCount = 2,
                LastFed = DateTime.Now
            };
            var camel = new Camel("Test Camel") { Id = "1" };
            var updatedCamel = new Camel(updateCamelDto.Name)
            {
                Id = "1",
                HumpCount = updateCamelDto.HumpCount,
                LastFed = updateCamelDto.LastFed
            };
            _repositoryMock.Setup(r => r.GetOne("1")).ReturnsAsync(camel);
            _repositoryMock.Setup(r => r.Update(It.IsAny<Camel>())).ReturnsAsync(updatedCamel);
            var result = await _camelLogic.UpdateCamel(updateCamelDto, "1");
            Assert.Equal(updatedCamel.Id, result.Id);
            Assert.Equal(updatedCamel.Name, result.Name);
            Assert.Equal(updatedCamel.HumpCount, result.HumpCount);
            Assert.Equal(updatedCamel.LastFed, result.LastFed);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Camel>()), Times.Once);
        }

        [Fact]
        public async Task GetCamelById_ShouldThrowKeyNotFoundException_WhenCamelDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetOne("100")).ReturnsAsync((Camel)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _camelLogic.GetCamel("100"));
        }

        [Fact]
        public async Task GetCamelById_ShouldReturnCamel_WhenCamelExists()
        {
            var camel = new Camel("Test Camel") { Id = "1" };
            _repositoryMock.Setup(r => r.GetOne("1")).ReturnsAsync(camel);
            var result = await _camelLogic.GetCamel("1");
            Assert.Equal(camel.Id, result.Id);
            Assert.Equal(camel.Name, result.Name);
            Assert.Equal(camel.HumpCount, result.HumpCount);
            Assert.Equal(camel.LastFed, result.LastFed);
        }
    }
}
