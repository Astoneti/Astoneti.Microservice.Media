using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Controllers;
using Astoneti.Microservice.Media.Models;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.Media.Tests.Controller
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _mockCommentService;
        private readonly IMapper _mapper;

        private readonly CommentController _controller;

        public CommentControllerTests()
        {
            _mockCommentService = new Mock<ICommentService>();

            _mapper = new MapperConfiguration(
                    config => config.AddMaps(
                        typeof(Startup).Assembly
                    )
                )
                .CreateMapper();

            _controller = new CommentController(
                _mockCommentService.Object,
                _mapper
            );
        }

        [Fact]
        public void GetList_WhenParametersIsValid_Should_ReturnExpectedResult()
        {
            // Arrange
            var dtos = new List<CommentDto>()
            {
                new CommentDto()
                {
                    Id = 1,
                    Text = "Test Text",
                    CreatedDate = new DateTime(2022, 03, 13)
                },

                new CommentDto()
                {
                    Id = 1,
                    Text = "Test Second Text",
                    CreatedDate = new DateTime(2022, 03, 14)
                }
            };

            _mockCommentService
                .Setup(x => x.GetList())
                .Returns(dtos);

            var expectedResultValue = _mapper.Map<IList<CommentModel>>(dtos);

            // Act
            var result = _controller.GetList();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            var resultValue = Assert.IsAssignableFrom<IList<CommentModel>>(okObjectResult.Value);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }
    }
}
