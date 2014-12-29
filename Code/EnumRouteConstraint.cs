using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Zoltu.WebApi.RouteConstraints
{
	public class EnumRouteConstraint<TEnum> : IHttpRouteConstraint where TEnum : struct, IConvertible
	{
		static EnumRouteConstraint()
		{
			if (!typeof (TEnum).IsEnum)
				throw new InvalidOperationException("Attempted to use EnumRouteConstraint with a TEnum that was not an enum.");
		}

		public Boolean Match(HttpRequestMessage request, IHttpRoute route, String parameterName, IDictionary<String, Object> values, HttpRouteDirection routeDirection)
		{
			if (values == null)
				return false;

			if (parameterName == null)
				return false;

			Object valueObject;
			if (!values.TryGetValue(parameterName, out valueObject))
				return false;

			var value = valueObject as String;
			if (value == null)
				return false;

			TEnum result;
			if (!Enum.TryParse<TEnum>(value, true, out result))
				return false;

			return true;
		}
	}
}
