using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class ExtensionTests
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
	public void BeError_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError(_ => { });

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeError_DoesNotFailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError(error => error.Message.Should().Be("error!"));

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
	public void BeValidWith_FailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeValidWith("valid text");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValidWith_FailWhenDiffers()
	{
		Result<string> result = "wrong text";

		var act = () => result.Should().BeValidWith("valid text");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeValidWith_DoesNotFailWhenSame()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeValidWith("valid text");

		act.Should().NotThrow();
	}

	[Fact]
	public void BeError_NoParams_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeError_NoParams_DoesNotFailWhenError()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeError();

		act.Should().NotThrow();
	}

	[Fact]
	public void BeErrorWith_FailWhenSuccess()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeErrorWith(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeErrorWith_FailWhenDiffers()
	{
		Result<string> result = new Error("wrong error");

		var act = () => result.Should().BeErrorWith(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void BeErrorWith_DoesNotFailWhenSame()
	{
		Result<string> result = new Error("error!");

		var act = () => result.Should().BeErrorWith(new Error("error!"));

		act.Should().NotThrow();
	}
}
