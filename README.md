[![Build status](http://img.shields.io/appveyor/ci/Zoltu/zoltu-webapi-routeconstraints.svg)](https://ci.appveyor.com/project/Zoltu/zoltu-webapi-routeconstraints)
[![NuGet Status](http://img.shields.io/nuget/v/Zoltu.WebApi.RouteConstraints.svg)](https://www.nuget.org/packages/Zoltu.WebApi.RouteConstraints/)

WebApi.RouteConstraints
=======================

Custom route constraints for WebAPI 2.2

Usage
-----
The following code snippets assume you have an enum declared somewhere named "Colors".

In Global.asax.cs:
```c#
var constraintResolver = new DefaultInlineConstraintResolver();
constraintResolver.ConstraintMap.Add("colors", typeof(EnumRouteConstraint<Colors>));
GlobalConfiguration.Configuration.MapHttpAttributeRoutes(constraintResolver);
```

Alternatively, in WebApiConfig.cs:
```c#
public static void Register(HttpConfiguration config)
{
	SetupRoutes(config);
}

private static void SetupRoutes(HttpConfiguration config)
{
	var constraintResolver = new DefaultInlineConstraintResolver();
	constraintResolver.ConstraintMap.Add("colors", typeof(EnumRouteConstraint<Colors>));
	config.MapHttpAttributeRoutes(constraintResolver);
}
```

In a Controller:
```c#
[RoutePrefix("api/someplace")]
public class MyController : ApiController
{
	[HttpGet]
	[Route("{color:colors}")]
	public async Task<String> GetColor(Color color)
	{
		return color.ToString();
	}
	
	[HttpGet]
	[Route("{notAColor}")]
	public async Task<String> GetId(String notAColor)
	{
		return notAColor + " is not a color!";
	}
}
```
