using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class BeValidTests
{
	[Fact]
	public void NoParams_Error_Fails()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void NoParams_DifferentValue_Passes()
	{
		Result<string> result = "different text";

		var act = () => result.Should().BeValid();

		act.Should().NotThrow();
	}

	[Fact]
	public void NoParams_ValidValue_Passes()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid();

		act.Should().NotThrow();
	}

	[Fact]
	public void WithExpected_Error_Fails()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_DifferentValue_Fails()
	{
		Result<string> result = "different text";

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_SameValue_Passes()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().NotThrow();
	}

	[Fact]
	public void WithAction_Error_Fails()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid(_ => { });

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithAction_DifferentValue_Fails()
	{
		Result<string> result = "wrong text";

		var act = () => result.Should().BeValid(value => value.Should().Be("valid text"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithAction_ValidValue_Passes()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid(value => value.Should().Be("valid text"));

		act.Should().NotThrow();
	}
}
