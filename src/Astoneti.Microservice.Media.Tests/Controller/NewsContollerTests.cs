using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Controllers;
using Astoneti.Microservice.Media.Models;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.Media.Tests
{
    public class NewsContollerTests
    {
        private readonly Mock<INewsService> _mockNewsService;
        private readonly IMapper _mapper;

        private readonly NewsController _controller;

        public NewsContollerTests()
        {
            _mockNewsService = new Mock<INewsService>();

            _mapper = new MapperConfiguration(
                    config => config.AddMaps(
                        typeof(Startup).Assembly
                    )
                )
                .CreateMapper();

            _controller = new NewsController(
                _mockNewsService.Object,
                _mapper
            );
        }

        [Fact]
        public void GetList_WhenParametersIsValid_Should_ReturnExpectedResult()
        {
            // Arrange
            var dtos = new List<NewsDto>()
            {
                new NewsDto()
                {
                    Id = 1,
                    Title = "Test Title"
                },

                 new NewsDto()
                 {
                     Id = 2,
                     Title = "New Test Title"
                 }
            };

            _mockNewsService
                .Setup(x => x.GetList())
                .Returns(dtos);

            var expectedResultValue = _mapper.Map<IList<NewsModel>>(dtos);

            // Act
            var result = _controller.GetList();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = Assert.IsAssignableFrom<IList<NewsModel>>(okObjectResult.Value);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }


        [Fact]
        public void Get_WhenItemExists_Should_ReturnItemById()
        {
            // Arrange
            const int id = 1;

            var newsDto = new NewsDto()
            {
                Id = id,
                Title = "Test Title"
            };

            _mockNewsService
                .Setup(x => x.Get(id))
                .Returns(newsDto);

            var expectedResultValue = _mapper.Map<NewsDto>(newsDto);

            // Act
            var result = _controller.Get(id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = Assert.IsType<NewsModel>(okObjectResult.Value);

            Assert.IsType<OkObjectResult>(result as OkObjectResult);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Get_WhenItemNotExists_Should_ReturnNotFound()
        {
            // Arrange
            const int id = 1;

            _mockNewsService
                .Setup(_ => _.Get(id))
                .Returns(() => null);

            // Act
            var result = _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange           
            var model = new NewsPostModel
            {
                Title = "Test Title",
                Body = "Test Body"
            };

            var dto = new NewsDto
            {
                Title = "Test Title",
                Body = "Test Body"
            };

            _mockNewsService
                .Setup(x => x.Add(model))
                .Returns(dto);

            var expectedResultValue = _mapper.Map<NewsModel>(dto);

            // Act
            var result = _controller.Post(model);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Null(createdAtActionResult.ControllerName);
            var idRouteValue = Assert.Single(createdAtActionResult.RouteValues);
            Assert.Equal("id", idRouteValue.Key);
            Assert.Equal(expectedResultValue.Id, idRouteValue.Value);

            var resultValue = Assert.IsType<NewsModel>(createdAtActionResult.Value);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Put_WhenModelNotValid_Should_ReturnBadRequest()
        {
            // Arrange
            const int id = 1;

            var model = new NewsPutModel()
            {
                Id = 2,
                Title = "Test New Title",
            };

            var dto = new NewsDto()
            {
                Id = id,
                Title = "Test Title",
            };

            _mockNewsService
                .Setup(_ => _.Edit(model))
                .Returns(dto);

            // Act
            var result = _controller.Put(id, model);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_WhenItemNotExists_Should_ReturnNotFound()
        {
            // Arrange
            const int id = 1;

            var model = new NewsPutModel()
            {
                Id = id,
                Title = "Test New Title",
            };

            _mockNewsService
                .Setup(_ => _.Edit(model))
                .Returns(() => null);

            // Act
            var result = _controller.Put(id, model);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_Should_UpdateCreatedItem()
        {
            const int id = 1;
            // Arrange
            var model = new NewsPutModel
            {
                Id = id,
                Title = "Test Owner",
            };

            var dto = new NewsDto()
            {
                Id = id,
                Title = "New Test Owner",
            };

            _mockNewsService
                .Setup(x => x.Edit(model))
                .Returns(dto);

            // Act
            var result = _controller.Put(id, model);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_WhenItemIsNotExists_Should_ReturnNotFound()
        {
            // Arrange
            const int id = 1;

            _mockNewsService
                .Setup(x => x.Get(id))
                .Returns(() => null);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Should_RemovesItem()
        {
            // Arrange
            const int id = 1;

            _mockNewsService
                .Setup(x => x.Delete(id))
                .Returns(true);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
