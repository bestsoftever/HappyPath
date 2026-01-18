# HappyPath

Focus on the happy path - stop using exceptions and if statements to control the flow in your code!
With this approach your code can be much shorter, making it easier to understand and maintain.

Tricks that allows to achieve this are:
- Stop using exceptions to represent errors - return `Result<T>` from all methods.
- Implement flow as a chain of methods.
- Stop using `null` to represent the lack of a value.

# Features

1. Type that represents the result of an operation - `Result<T>`. The variable of that type can store either data of type `T` or an `Error`.
1. `Error` - the base class for errors returned from methods. It support nesting/aggregate errors.
1. Set of `Then` methods, which allows to invoke next operation on `Result<T>`.
1. `Result.None` - represents the lack of a value returned from a method.
1. Set of `Act<T>` methods, which allows to invoke an action if the result of the previous operation is `T`.
1. Set of `Swap<T>` methods, which allows to swap the result type if the result of the previous operation is `T`.
1. `Match<T>` method, which handles both success and error cases and returns a value of another type `T`. This ends the `Result<>` chain.
1. `async` support - all `Then`, `Act`, and `Swap` methods have `async` overrides.

## What is not implemented

1. Methods like:
	a. `return AbstractResultFactoryBeanImpl.CreateResultOf<string>(...)` 
	b. `return Result.Of<string>(...)` 
	c. `return new Result<string>(...)`
1. No `result.IsError` - the goal of this library is to prevent people from using `if` statements, so such a property would make no sense.
1. No `result.IsValid` or `result.IsSuccess` - these are even worse than `IsError`, because errors needs special handling, not valid results!
1. No parameterless constructors available for users to prevent from creating uninitialized results.

# Migration

## Introducing Result\<T>

Let's take a method:

```csharp
public static string ReverseString(string input)
{
	if (string.IsNullOrWhiteSpace(input))
	{
		throw new ArgumentException(nameof(input));
	}

	return new string(input.Reverse().ToArray());
}
```

With `HappyPath` we only need to replace throwing exceptions with returning an Error:

```csharp
public static Result<string> ReverseString(string input)
{
	if (string.IsNullOrWhiteSpace(input))
	{
		return new Error(ErrorMessage);
	}

	return new string(input.Reverse().ToArray());
}
```

No need to change valid cases or wrapping anything in `Result<T>`.

## Flow simplification

We can convert this:

```csharp
public string ProcessAnimal(Guid id)
{
	Animal animal = null!;
	try
	{
		animal = GetRandomPetFromOracle();
	}
	catch (ThisParrotIsDeadException pe)
	{
		errorMessage = pe.Message;
		animal = new Cat("Kitty");
	}
	catch (Exception e)
	{
		errorMessage = e.Message;
	}

	if (animal is Dog d)
	{
		isPies = true;
		animal = new Cat(d.Name);
	}

	var result = TestService.ToUpperCase($"{animal.GetType().Name} is {animal.Name}");
	return result;
}
```

to more concise and "fluent" version:

```csharp
public Result<string> ProcessAnimal(Guid id)
{
	return GetRandomPetFromOracle()
		.Act<Error>(e => errorMessage = e.Message)
		.Act<Dog>(d => isDog = true)
		.Swap<ThisParrotIsDeadError>(e => new Cat("Kitty"))
		.Swap<Dog>(d => new Cat(d.Name))
		.Then(c => TestService.ToUpperCase($"{c.GetType().Name} is {c.Name}"));
}
```

Of course, the `GetPetFromOracle` method must return `Result<Animal>` and use errors instead of exceptions, but this is an easy migration as shown before.
