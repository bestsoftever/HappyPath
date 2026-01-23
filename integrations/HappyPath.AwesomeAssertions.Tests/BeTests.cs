using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class BeTests
{
	[Fact]
	public void Be_FailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().Be("valid text");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void Be_FailWhenDiffers()
	{
		Result<string> result = "wrong text";

		var act = () => result.Should().Be("valid text");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void Be_DoesNotFailWhenSame()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().Be("valid text");

		act.Should().NotThrow();
	}
}
