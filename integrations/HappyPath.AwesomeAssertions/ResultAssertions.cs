using AwesomeAssertions;
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;

namespace HappyPath.AwesomeAssertions;

public class ResultAssertions<T>(Result<T> subject, AssertionChain assertionChain)
	: ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>(subject, assertionChain)
{
	protected override string Identifier => "Result";

	public AndConstraint<ResultAssertions<T>> BeValid(string because = "", params object[] becauseArgs)
	{
		Subject.Match<bool>(
			value => true,
			error =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:Result} to be successful{reason}, but was error: {0}.", error.Message);

				return false;
			});

		return new AndConstraint<ResultAssertions<T>>(this);
	}

	public AndWhichConstraint<ResultAssertions<T>, T> BeValid(T expected, string because = "", params object[] becauseArgs)
	{
		T? matchedValue = default;

		Subject.Match(
			value =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.ForCondition(value is not null)
					.FailWith("Expected {context:Result} to have a value equivalent to {0}{reason}, but value was null.", expected);

				value.Should().BeEquivalentTo(expected, because, becauseArgs);

				matchedValue = value;
				return true;
			},
			error =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:Result} to be successful{reason}, but was error: {0}.", error.Message);

				return false;
			});

		return new AndWhichConstraint<ResultAssertions<T>, T>(this, matchedValue!);
	}

	public AndConstraint<ResultAssertions<T>> BeValid(Action<T> action, string because = "", params object[] becauseArgs)
	{
		Subject.Match<bool>(
			value =>
			{
				action(value);
				return true;
			},
			error =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:Result} to be successful{reason}, but was error: {0}.", error.Message);

				return false;
			});

		return new AndConstraint<ResultAssertions<T>>(this);
	}

	public ErrorAssertionConstraint<T> BeError(string because = "", params object[] becauseArgs)
	{
	Error? matchedError = null;

	Subject.Match<bool>(
	value =>
	{
	assertionChain
		.BecauseOf(because, becauseArgs)
		.FailWith("Expected {context:Result} to be an error{reason}, but was successful with value: {0}.", value);

	return false;
	},
	error =>
	{
	matchedError = error;
	return true;
	});

	return new ErrorAssertionConstraint<T>(this, matchedError!, assertionChain);
	}

	public AndWhichConstraint<ResultAssertions<T>, Error> BeError(Error expected, string because = "", params object[] becauseArgs)
	{
		Error? matchedError = null;

		Subject.Match(
			value =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:Result} to be an error{reason}, but was successful with value: {0}.", value);

				return false;
			},
			error =>
			{
				error.Should().BeOfType(expected.GetType(), because, becauseArgs);
				error.Should().BeEquivalentTo(expected, because, becauseArgs);
				matchedError = error;
				return true;
			});

		return new AndWhichConstraint<ResultAssertions<T>, Error>(this, matchedError!);
	}

	public AndConstraint<ResultAssertions<T>> BeError(Action<Error> action, string because = "", params object[] becauseArgs)
	{
		Subject.Match<bool>(
			value =>
			{
				assertionChain
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:Result} to be an error{reason}, but was successful with value: {0}.", value);

				return false;
			},
			error =>
			{
				action(error);
				return true;
			});

		return new AndConstraint<ResultAssertions<T>>(this);
	}
}

public class ErrorAssertionConstraint<T>(ResultAssertions<T> parentAssertions, Error error, AssertionChain assertionChain)
{
	public AndConstraint<ResultAssertions<T>> And => new(parentAssertions);

	public ErrorAssertionConstraint<T> WithMessage(string expected, string because = "", params object[] becauseArgs)
	{
		assertionChain
			.BecauseOf(because, becauseArgs)
			.ForCondition(error.Message == expected)
			.FailWith("Expected {context:Result} error to have message {0}{reason}, but found {1}.", expected, error.Message);

		return this;
	}

	public ErrorAssertionConstraint<T> OfType<TError>(string because = "", params object[] becauseArgs) where TError : Error
	{
		assertionChain
			.BecauseOf(because, becauseArgs)
			.ForCondition(error.GetType() == typeof(TError))
			.FailWith("Expected {context:Result} error to be of type {0}{reason}, but found {1}.", typeof(TError), error.GetType());

		return this;
	}
}
