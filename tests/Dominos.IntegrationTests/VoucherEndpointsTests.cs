using System.Net;
using System.Net.Http.Json;
using Dominos.IntegrationTests.Models;
using FluentAssertions;
using Flurl;
namespace Dominos.IntegrationTests;

public class VoucherEndpointsTests : IClassFixture<VouchersApiFactory>
{
    private readonly VouchersApiFactory _factory;
    private readonly HttpClient _client;

    public VoucherEndpointsTests(VouchersApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllVouchers_ShouldBeValid_WhenNoNameSearch()
    {
        // Arrange

        // Act
        var uri = "api/vouchers".SetQueryParams(new
        {
            limit = 20,
            offset = 0,
        }).ToUri();
        var response = await _client.GetFromJsonAsync<VoucherCollectionTestDto>(uri);

        // Assert
        response.Should().NotBeNull();
        response!.Items.Should().NotBeNullOrEmpty()
                 .And.HaveCount(20)
                 .And.AllSatisfy(x =>
                 {
                     x.Id.Should().NotBeEmpty();
                     x.Name.Should().NotBeEmpty();
                     x.Price.Should().BeGreaterThan(0);
                     x.ProductCodes.Should().NotBeNullOrEmpty();
                 });
    }

    [Fact]
    public async Task GetAllVouchers_ShouldHaveBadRequest_WhenInvalidRequest()
    {
        // Arrange

        // Act
        var uri = "api/vouchers".SetQueryParams(new
        {
            limit = 10,
            offset = -10,
        }).ToUri();
        var response = await _client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetVoucher_ShouldBeValid_WhenExists()
    {
        // Arrange
        var id = new Guid("0002e109-7dc6-4ae1-a61b-7ef5f10b11a6");

        // Act
        var uri = $"api/vouchers/{id}".SetQueryParams(new
        {
            limit = 20,
            offset = 0,
        }).ToUri();
        var voucher = await _client.GetFromJsonAsync<VoucherTestModel>(uri);

        // Assert
        voucher.Should().NotBeNull();
        voucher!.Id.Should().Be(id);
        voucher.Name.Should().NotBeEmpty();
        voucher.Price.Should().BeGreaterThan(0);
        voucher.ProductCodes.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetVoucher_ShouldBeNotFound_WhenNoExists()
    {
        // Arrange
        var id = new Guid("0002e109-7dc6-4ae1-a61b-7ef5f10b11a7");

        // Act
        var uri = $"api/vouchers/{id}".SetQueryParams(new
        {
            limit = 10,
            offset = -10,
        }).ToUri();
        var response = await _client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}