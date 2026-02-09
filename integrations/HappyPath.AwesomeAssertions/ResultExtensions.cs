using System;
using AwesomeAssertions.Execution;

namespace HappyPath.AwesomeAssertions;

public static class ResultExtensions
{
	public static ResultAssertions<T> Should<T>(this Result<T> result)
	{
		return new ResultAssertions<T>(result, AssertionChain.GetOrCreate());
	}
}
