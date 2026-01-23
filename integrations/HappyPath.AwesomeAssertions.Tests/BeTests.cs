//using AwesomeAssertions;
//using Xunit.Sdk;

//namespace HappyPath.AwesomeAssertions.Tests;

//public class BeTests
//{
//	[Fact]
//	public void FailWhenError()
//	{
//		Result<string> result = new Error("error!");

//		var act = () => result.Should().Be("valid text");

//		act.Should().Throw<XunitException>();
//	}

//	[Fact]
//	public void FailWhenDiffers()
//	{
//		Result<string> result = "wrong text";

//		var act = () => result.Should().Be("valid text");

//		act.Should().Throw<XunitException>();
//	}

//	[Fact]
//	public void DoesNotFailWhenSame()
//	{
//		Result<string> result = "valid text";

//		var act = () => result.Should().Be("valid text");

//		act.Should().NotThrow();
//	}
//}
