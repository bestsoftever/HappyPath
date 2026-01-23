using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class BeErrorTests
{
	[Fact]
	public void WithAction_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError(_ => { });

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithAction_DoesNotFailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError(error => error.Message.Should().Be("error!"));

		act.Should().NotThrow();
	}

	[Fact]
	public void NoParams_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void NoParams_DoesNotFailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError();

		act.Should().NotThrow();
	}

	[Fact]
	public void WithExpected_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_FailWhenDiffers()
	{
		Result<string> result = new Error("wrong error");

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_DoesNotFailWhenSame()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().NotThrow();
	}
}
