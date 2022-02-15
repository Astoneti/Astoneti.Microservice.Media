using Astoneti.Microservice.Media.Data.Entities;
using Astoneti.Microservice.Media.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Astoneti.Microservice.Media.IntegrationTests
{
    public class NewsControllerTests : IDisposable
    {
        private readonly TestWebApplicationFactory _factory;

        private  HttpClient _client { get; set; }

        public NewsControllerTests(ITestOutputHelper output)
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
            _factory.DbContext.Set<NewsEntity>().AddRange(
                new NewsEntity
                {
                    Id = 1,
                    Title = "Test Title",
                    Body = "Test Body",
                    PublicationDate = new DateTime(2022, 03, 13)
                },
                new NewsEntity
                {
                    Id = 2,
                    Title = "Test Second Title",
                    Body = "Test Second Body",
                    PublicationDate = new DateTime(2022, 03, 14)
                }
            );

            _factory.DbContext.SaveChanges();
        }

        [Fact]
        public async Task GetListAsync_ReturnsOkWithList()
        {
            // Arrange
            var expectedlist = _factory.DbContext.Set<NewsEntity>().ToList();

            _client = _factory.CreateClient();

            // Act
            var result = await _client.GetAsync(new Uri("/news", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var resultValue = await result.Content.ReadFromJsonAsync<IList<NewsModel>>();

            Assert.Equal(expectedlist.Count, resultValue.Count);

            resultValue
                .Should()
                .BeEquivalentTo(expectedlist);
            //for (var i = 0; i < 2; i++)
            //{
            //    Assert.Equal(expectedlist[i].Id, resultValue[i].Id);
            //    Assert.Equal(expectedlist[i].Title, resultValue[i].Title);   /remove after Rodchenko approve
            //    Assert.Equal(expectedlist[i].Body, resultValue[i].Body);
            //    Assert.Equal(expectedlist[i].PublicationDate, resultValue[i].PublicationDate);
            //}
        }

        [Fact]
        public async Task GetAsync_ById_ReturnsOkWithItem()
        {
            // Arrange
            const int id = 1;
            var expectedResult = _factory.DbContext.Set<NewsEntity>().First(x => x.Id == id);

            _client = _factory.CreateClient();

            // Act
            var result = await _client.GetAsync(new Uri("/news/1", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var resultValue = await result.Content.ReadFromJsonAsync<NewsModel>();

            resultValue
                .Should()
                .BeEquivalentTo(expectedResult);

            //Assert.Equal(expectedResult.Id, resultValue.Id);
            //Assert.Equal(expectedResult.Title, resultValue.Title);          /remove after Rodchenko approve
            //Assert.Equal(expectedResult.Body, resultValue.Body);
            //Assert.Equal(expectedResult.PublicationDate, resultValue.PublicationDate);
        }

        [Fact]
        public async Task GetAsync_ById_ReturnsNotFound()
        {
            // Arrange
            const int id = 0;

            _client = _factory.CreateClient();

            // Act
            var result = await _client.GetAsync(new Uri($"/news/{id}", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task PostAsync_ReturnsCreated() 
        {
            // Arrange
            var postModel = new NewsPostModel
            {
                Id = 3,
                Title = "Test Title",
                Body = "Test Body",
            };

            _client = _factory.CreateClient();

            // Act
            var result = await _client
                .PostAsJsonAsync(
                    "/news",
                    postModel
                );

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            var resultValue = await result.Content.ReadFromJsonAsync<NewsModel>();

            resultValue
                .Should()
                .BeEquivalentTo(postModel);

            //Assert.NotNull(resultValue);
            //Assert.Equal(postModel.Id, resultValue.Id);
            //Assert.Equal(postModel.Title, resultValue.Title);  /remove after Rodchenko approve
            //Assert.Equal(postModel.Body, resultValue.Body);

            var entity = Assert.Single(_factory.DbContext.Set<NewsEntity>(), x => x.Id == postModel.Id);
            Assert.NotNull(entity);
            Assert.Equal(postModel.Id, entity.Id);
            Assert.Equal(postModel.Title, entity.Title);
            Assert.Equal(postModel.Body, resultValue.Body);
        }

        [Fact]
        public async Task PutAsync_ReturnsOk()
        {
            // Arrange
            var putModel = new NewsPutModel
            {
                Id = 1,
                Title = "New Test Title",
                Body = "New Test Body",
                PublicationDate = new DateTime(2025, 03, 13)
            };

            _client = _factory.CreateClient();

            // Act
            var result = await _client.PutAsJsonAsync(
                $"/news/{putModel.Id}",
                putModel
            );

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);

            var entity = Assert.Single(_factory.DbContext.Set<NewsEntity>().AsNoTracking(), x => x.Id == putModel.Id);

            Assert.NotNull(entity);
            Assert.Equal(putModel.Id, entity.Id);
            Assert.Equal(putModel.Title, entity.Title);
            Assert.Equal(putModel.Body, entity.Body);
            Assert.Equal(putModel.PublicationDate, entity.PublicationDate);
        }

        [Fact]
        public async Task PutAsync_ReturnsBadRequest()
        {
            // Arrange
            var putModel = new NewsPutModel
            {
                Id = 1,
                Title = "Test Title",
                Body = "Test Body",
            };

            _client = _factory.CreateClient();

            // Act
            var result = await _client.PutAsJsonAsync(
                "/news/0",
                putModel
            );

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task PutAsync_ReturnsNotFound()
        {
            // Arrange
            const int id = 0;
            var putModel = new NewsPutModel
            {
                Id = id,
                Title = "Test Title",
                Body = "Test Body",
            };

            _client = _factory.CreateClient();

            // Act
            var result = await _client.PutAsJsonAsync(
                "/news/0",
                putModel
            );

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOk()
        {
            // Arrange
            const int id = 1;

            _client = _factory.CreateClient();

            // Act
            var result = await _client.DeleteAsync(new Uri($"/news/{id}", UriKind.Relative));

            var notFoundResult = await _client.GetAsync(new Uri($"/news/{id}", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.DoesNotContain(_factory.DbContext.Set<NewsEntity>(), x => x.Id == id);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFound()
        {
            // Arrange
            const int id = 0;

            _client = _factory.CreateClient();

            // Act
            var result = await _client.DeleteAsync(new Uri($"/news/{id}", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
