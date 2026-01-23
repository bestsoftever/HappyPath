using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class TestError(string message, string content = "default content") : Error(message)
{
	public string Content { get; } = content;
}

public class BeErrorTests
{
	[Fact]
	public void NoParams_ValidValue_Fails()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void NoParams_Error_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError();

		act.Should().NotThrow();
	}

	[Fact]
	public void WithExpected_ValidValue_Fails()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError(new TestError("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_DifferentError_Fails()
	{
		Result<string> result = new TestError("wrong error");

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_SameError_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().NotThrow();
	}

	[Fact]
	public void WithAction_ValidValue_Fails()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError(_ => { });

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithAction_DifferentError_Fails()
	{
		Result<string> result = new Error("wrong error");

		var act = () => result.Should().BeError(error =>
		{
			error.Should().BeOfType<TestError>();
			error.Should().BeEquivalentTo(new TestError("error!"));
		});

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithAction_SameError_Passes()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError(error =>
		{
			error.Should().BeOfType<TestError>();
			error.Should().BeEquivalentTo(new TestError("error!"));
		});

		act.Should().NotThrow();
	}
}
