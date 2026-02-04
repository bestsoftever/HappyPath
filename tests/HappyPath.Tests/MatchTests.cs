namespace HappyPath.Tests;

public class MatchTests
{
	[Fact]
	public void SyncToSync_WhenSuccess()
	{
		Result<string> result = TestService.ReverseString("abc");

		string output = result.Match(
			value => $"Success: {value}",
			error => $"Error: {error.Message}");

		output.Should().Be("Success: cba");
		}

		[Fact]
		public void SyncToSync_WhenError()
	{
		Result<string> result = TestService.ReverseString("");

		string output = result.Match(
			value => $"Success: {value}",
			error => $"Error: {error.Message}");

		output.Should().Be($"Error: {TestService.ErrorMessage}");
		}

		[Fact]
		public async Task AsyncToSync_WhenSuccess()
	{
		Result<string> result = await TestService.ReverseStringAsync("abc");

		string output = result.Match(
			value => $"Success: {value}",
			error => $"Error: {error.Message}");

		output.Should().Be("Success: cba");
		}

		[Fact]
		public async Task AsyncToSync_WhenError()
	{
		Result<string> result = await TestService.ReverseStringAsync("");

		string output = result.Match(
			value => $"Success: {value}",
			error => $"Error: {error.Message}");

		output.Should().Be($"Error: {TestService.ErrorMessage}");
		}

		[Fact]
		public async Task SyncToAsync_WhenSuccess()
	{
		Result<string> result = TestService.ReverseString("abc");

		string output = await result.Match(
			async value => await Task.FromResult($"Success: {value}"),
			async error => await Task.FromResult($"Error: {error.Message}"));

		output.Should().Be("Success: cba");
		}

		[Fact]
		public async Task SyncToAsync_WhenError()
	{
		Result<string> result = TestService.ReverseString("");

		string output = await result.Match(
			async value => await Task.FromResult($"Success: {value}"),
			async error => await Task.FromResult($"Error: {error.Message}"));

		output.Should().Be($"Error: {TestService.ErrorMessage}");
		}

		[Fact]
		public async Task AsyncToAsync_WhenSuccess()
	{
		Result<string> result = await TestService.ReverseStringAsync("abc");

		string output = await result.Match(
			async value => await Task.FromResult($"Success: {value}"),
			async error => await Task.FromResult($"Error: {error.Message}"));

		output.Should().Be("Success: cba");
		}

		[Fact]
		public async Task AsyncToAsync_WhenError()
	{
		Result<string> result = await TestService.ReverseStringAsync("");

		string output = await result.Match(
			async value => await Task.FromResult($"Success: {value}"),
			async error => await Task.FromResult($"Error: {error.Message}"));

		output.Should().Be($"Error: {TestService.ErrorMessage}");
		}
		}
