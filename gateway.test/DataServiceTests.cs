using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;
using gateway.api.Services;
using gateway.shared.Models;
using NewsAPI.Models;
using System.Net;
using Moq.Protected;

namespace gateway.test
{
	public class DataServiceTests
    {
        [Fact]
        public async Task GetDataAsync_ReturnsResults_WithStatusCode() {
            // Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var dataService = new DataService(mockFactory.Object);

            // Acts
            var weatherResult = await dataService.GetWeatherDataAsync(0.0, 0.0);
            var musicResult = await dataService.GetMusicDataAsync("");
            var newsResult = await dataService.GetNewsDataAsync("");

            // Asserts
            var weatherContentResult = Assert.IsType<ContentResult>(weatherResult);
            Assert.Equal("application/json", weatherContentResult.ContentType);
            Assert.Equal((int)HttpStatusCode.OK, weatherContentResult.StatusCode);

            var musicContentResult = Assert.IsType<ContentResult>(musicResult);
            Assert.Equal("application/json", musicContentResult.ContentType);
            Assert.Equal((int)HttpStatusCode.OK, musicContentResult.StatusCode);

            var newsContentResult = Assert.IsType<ContentResult>(newsResult);
            Assert.Equal("application/json", newsContentResult.ContentType);
            Assert.Equal((int)HttpStatusCode.OK, newsContentResult.StatusCode);
        }
    }
}

