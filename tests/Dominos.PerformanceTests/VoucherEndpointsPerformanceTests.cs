using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using FluentAssertions;
using Flurl;
using Xunit.Abstractions;
namespace Dominos.PerformanceTests;

public class VoucherEndpointsPerformanceTests : IClassFixture<VouchersApiFactory>
{
    private readonly VouchersApiFactory _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    public VoucherEndpointsPerformanceTests(VouchersApiFactory factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllVouchers_ShouldRespondUpTo5Seconds_When10000RequestsIsMade()
    {
        // Arrange

        // Act
        var watch = Stopwatch.StartNew();
        var uri = "api/vouchers".SetQueryParams(new
        {
            limit = 50,
            offset = 0,
        }).ToUri();

        const int count = 10_000;
        var bag = new ConcurrentBag<HttpStatusCode>();
        var options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 20,
        };
        await Parallel.ForEachAsync(Enumerable.Range(0, count), options, async (i, ct) =>
        {
            using var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, ct);
            bag.Add(response.StatusCode);
        });
        watch.Stop();

        // Assert
        bag.Should().AllSatisfy(x => x.Should().Be(HttpStatusCode.OK));

        watch.Elapsed.TotalMilliseconds.Should().BeLessThan(5000);
        var elapsedSec = watch.Elapsed.ToString(@"ss\.fff");
        _testOutputHelper.WriteLine($"Retrieving {count} vouchers took: {elapsedSec} sec.");
    }
}