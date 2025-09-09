using HappyPath;

namespace HappyPath.Tests;

public class ActErrorTests
{
	[Fact]
	public void SyncToSync_Valid()
	{
		string errorMessage = string.Empty;
		Result<string> result = TestService.ReverseString("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCase(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public void SyncToSync_Error()
	{
		string errorMessage = string.Empty;
		Result<string> result = TestService.ReverseString("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCase(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task SyncToAsync_Valid()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseString("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task SyncToAsync_Error()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseString("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public void SyncToNone_Valid()
	{
		string errorMessage = string.Empty;
		Result<None> result = TestService.ReverseString("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothing(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public void SyncToNone_Error()
	{
		string errorMessage = string.Empty;
		Result<None> result = TestService.ReverseString("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothing(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task SyncToNoneAsync_Valid()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseString("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothingAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task SyncToNoneAsync_Error()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseString("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothingAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}


	[Fact]
	public async Task AsyncToSync_Valid()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseStringAsync("abc")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCase(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task AsyncToSync_Error()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseStringAsync("    ")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCase(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task AsyncToAsync_Valid()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseStringAsync("abc")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task AsyncToAsync_Error()
	{
		string errorMessage = string.Empty;
		Result<string> result = await TestService.ReverseStringAsync("    ")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task AsyncToNone_Valid()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseStringAsync("abc")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothing(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task AsyncToNone_Error()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseStringAsync("    ")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothing(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task AsyncToNoneAsync_Valid()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseStringAsync("abc")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothingAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task AsyncToNoneAsync_Error()
	{
		string errorMessage = string.Empty;
		Result<None> result = await TestService.ReverseStringAsync("    ")
			.Act<string, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(s => TestService.DoNothingAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}


	[Fact]
	public void NoneToSync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = TestService.DoNothing("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCase(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public void NoneToSync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = TestService.DoNothing("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCase(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task NoneToAsync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothing("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task NoneToAsync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothing("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public void NoneToNone_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = TestService.DoNothing("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothing(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public void NoneToNone_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = TestService.DoNothing("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothing(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task NoneToNoneAsync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothing("abc")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothingAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task NoneToNoneAsync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothing("    ")
			.Act<Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothingAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}


	[Fact]
	public async Task NoneAsyncToSync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothingAsync("abc")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCase(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task NoneAsyncToSync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothingAsync("    ")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCase(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task NoneAsyncToAsync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothingAsync("abc")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe("CBA");
	}

	[Fact]
	public async Task NoneAsyncToAsync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<string> result = await TestService.DoNothingAsync("    ")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.ToUpperCaseAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task NoneAsyncToNone_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothingAsync("abc")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothing(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task NoneAsyncToNone_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothingAsync("    ")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothing(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}

	[Fact]
	public async Task NoneAsyncToNoneAsync_Valid()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothingAsync("abc")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothingAsync(s));

		errorMessage.ShouldBeEmpty();
		result.ShouldBe(Result.None);
	}

	[Fact]
	public async Task NoneAsyncToNoneAsync_Error()
	{
		string errorMessage = string.Empty;
		string s = "cba";
		Result<None> result = await TestService.DoNothingAsync("    ")
			.Act<None, Error>(e => errorMessage = $"message logged: {e.Message}")
			.Then(_ => TestService.DoNothingAsync(s));

		errorMessage.ShouldBe("message logged: Input value can't be empty");
		result.ShouldBe(new Error(TestService.ErrorMessage));
	}
}
