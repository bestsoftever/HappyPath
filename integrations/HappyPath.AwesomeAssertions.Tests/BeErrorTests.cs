using AwesomeAssertions;
using Xunit.Sdk;

namespace HappyPath.AwesomeAssertions.Tests;

public class TestError(string message, string content = "default content") : Error(message)
{
	public string Content { get; } = content;
}

public class DerivedTestError(string message) : TestError(message);

public class AnotherError(string message) : Error(message);

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
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError(new Error("error!"));

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithExpected_SameError_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError(new TestError("error!"));

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
		Result<string> result = new Error("error!");

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
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError(error =>
		{
			error.Should().BeOfType<TestError>();
			error.Should().BeEquivalentTo(new TestError("error!"));
		});

		act.Should().NotThrow();
	}

	[Fact]
	public void OfType_BaseType_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<Error>();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void OfType_ExactType_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<TestError>();

		act.Should().NotThrow();
	}

	[Fact]
	public void OfType_DerivedType_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<DerivedTestError>();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void OfType_DifferentType_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<AnotherError>();

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithMessage_MatchingMessage_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().WithMessage("error!");

		act.Should().NotThrow();
	}

	[Fact]
	public void WithMessage_DifferentMessage_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().WithMessage("different message");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void OfType_WithMessage_Chained_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<TestError>().WithMessage("error!");

		act.Should().NotThrow();
	}

	[Fact]
	public void OfType_WithMessage_Chained_WrongType_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<AnotherError>().WithMessage("error!");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void OfType_WithMessage_Chained_WrongMessage_Fails()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().OfType<TestError>().WithMessage("wrong");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void WithMessage_OfType_Chained_Passes()
	{
		Result<string> result = new TestError("error!");

		var act = () => result.Should().BeError().WithMessage("error!").OfType<TestError>();

		act.Should().NotThrow();
	}

	[Fact]
	public void WithMessage_ValidValue_Fails()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError().WithMessage("error!");

		act.Should().Throw<XunitException>();
	}

	[Fact]
	public void OfType_ValidValue_Fails()
	{
		Result<string> result = "valid text";

		var act = () => result.Should().BeError().OfType<TestError>();

		act.Should().Throw<XunitException>();
	}
}
