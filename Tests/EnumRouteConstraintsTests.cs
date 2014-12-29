using System;
using Zoltu.WebApi.RouteConstraints;
using Xunit;
using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Tests
{
	public enum TestEnum
	{
		Foo,
		Bar,
		Baz,
		Zip,
		Zap,
	}

	public class EnumRouteConstraintsTests
	{
		[Fact]
		public void when_invalid_type_then_TypeInitializationException()
		{
			Assert.Throws<TypeInitializationException>(() => new EnumRouteConstraint<Int32>());
		}

		[Fact]
		public void when_parameter_is_not_a_string_then_false()
		{
			var enumRouteConstraint = new EnumRouteConstraint<TestEnum>();
			var result = enumRouteConstraint.Match(null, null, "foo", new Dictionary<String, Object> { { "foo", 5 } }, HttpRouteDirection.UriResolution);

			Assert.False(result);
		}

		[Fact]
		public void when_parameter_is_not_found_then_false()
		{
			var enumRouteConstraint = new EnumRouteConstraint<TestEnum>();
			var result = enumRouteConstraint.Match(null, null, "foo", new Dictionary<String, Object> { { "bar", "Bar" } }, HttpRouteDirection.UriResolution);

			Assert.False(result);
		}

		[Fact]
		public void when_parameter_is_not_in_enum_then_false()
		{
			var enumRouteConstraint = new EnumRouteConstraint<TestEnum>();
			var result = enumRouteConstraint.Match(null, null, "foo", new Dictionary<String, Object> { { "foo", "aoe" } }, HttpRouteDirection.UriResolution);

			Assert.False(result);
		}

		[Fact]
		public void when_case_does_not_match_then_true()
		{
			var enumRouteConstraint = new EnumRouteConstraint<TestEnum>();
			var result = enumRouteConstraint.Match(null, null, "foo", new Dictionary<String, Object> { { "foo", "foo" } }, HttpRouteDirection.UriResolution);

			Assert.True(result);
		}

		[Fact]
		public void when_found_then_true()
		{
			var enumRouteConstraint = new EnumRouteConstraint<TestEnum>();
			var result = enumRouteConstraint.Match(null, null, "foo", new Dictionary<String, Object> { { "foo", "Zip" } }, HttpRouteDirection.UriResolution);

			Assert.True(result);
		}
	}
}
