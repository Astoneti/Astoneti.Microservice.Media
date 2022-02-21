using Astoneti.Microservice.Media.Data.Entities;
using Astoneti.Microservice.Media.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Astoneti.Microservice.Media.IntegrationTests.Controller
{
    public class CommentControllerTests : IDisposable
    {
        private readonly TestWebApplicationFactory _factory;

        private HttpClient _client { get; set; }

        public CommentControllerTests(ITestOutputHelper output)
        {
            _factory = new TestWebApplicationFactory
            {
                Output = output
            };

            Seed();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }

        private void Seed()
        {
            _factory.DbContext.Set<CommentEntity>().AddRange(
                new CommentEntity()
                {
                    Id = 1,
                    Text = "Test Text",
                    CreatedDate = new DateTime(2022, 03, 13),
                },

                new CommentEntity()
                {
                    Id = 2,
                    Text = "Test Second Text",
                    CreatedDate = new DateTime(2022, 03, 14),
                }
            );

            _factory.DbContext.SaveChanges();
        }

        [Fact]
        public async Task GetListAsync_ReturnsOkWithList()
        {
            // Arrange
            var expectedValue = _factory.DbContext.Set<CommentEntity>().ToList();

            _client = _factory.CreateClient();

            // Act
            var result = await _client.GetAsync(new Uri("/comments", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var resultValue = await result.Content.ReadFromJsonAsync<IList<CommentModel>>();

            Assert.Equal(expectedValue.Count, resultValue.Count);

            resultValue
                .Should()
                .BeEquivalentTo(expectedValue);
        }
    }
}
