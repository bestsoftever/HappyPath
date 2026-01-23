using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class BeValidTests
{
	[Fact]
	public void BeValid_FailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid(_ => { });

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValid_DoesNotFailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid(value => value.Should().Be("valid text"));

		act.Should().NotThrow();
	}

	[Fact]
	public void BeValid_NoParams_FailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValid_NoParams_DoesNotFailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid();

		act.Should().NotThrow();
	}

	[Fact]
	public void BeValid_WithExpected_FailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValid_WithExpected_FailWhenDiffers()
	{
		Result<string> result = "wrong text";

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValid_WithExpected_DoesNotFailWhenSame()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValid("valid text", because: "");

		act.Should().NotThrow();
	}
}
