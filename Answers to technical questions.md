How long did you spend on the coding challenge?

Between 6-8 hours total. A considerable chunk of that time was spent in researching which third party API to use and how to use it.

-----------------------------------------------------------------------------------

What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.

The two main things I would have added should I have had more time are logging and some sort of chaching mechanism like Redis for the api results.

Logging because it's critical for any production ready application and caching because I can envision the services being called repeteadly and caching would help with performance and resiliency.

Other things I would have liked is to make this a RESTFul API, probably containerise it with Docker so we could deploy this very easily as an independent microservice in some container cloud and maybe even show case a minimal build pipeline with CakeBuild.

Should I have gone with the RESTful API approach I would have liked to build a minimal web application to allow a user to consume these services.

-----------------------------------------------------------------------------------

What was the most useful feature that was added to the latest version of your chosen language? Please include a snippet of code that shows how you've used it.

I think one of the most useful features in C# 8 is the addition of variable level using declarations. When declaring a variable this way, it will be disposed automatically at the end of the enclosing scope and makes the code much more readable.

using var response = await _httpClient.GetAsync(url);

In C# 7.1 we got async main methods, so we no longer need to make a blocking call to GetAwaiter().GetResult() to obtain the result of an async methods call withing the Main method of a program:

class Program
{
	static async Task Main(string[] args)
	{
		await Task.CompletedTask;
	}
}

-----------------------------------------------------------------------------------

How would you track down a performance issue in production? Have you ever had to do this?

I had to do this a few times in my career.

The main tools to identify problems in production nowdays are application and system logs, monitoring and profiling.

You could also try to take a memory and thread dump and analize it, although this is usually very hard and should be left as the last resource.

Profilers could be standard profilers like the ANTS memory or performance profiler for .Net or the JProfiler for Java.

Monitoring could be done through APM tools like Dynatrace, New Relic, Datadog and others.

If we identify the database being the bottleneck and queries timing out we could run a database profiler and then try to get a query execution plan for the queries that take long to complete, to identify what changes we could make to improve performance (add an index, denormalise, etc).

There are a few other techniques that could also be useful like having session trace information to identify the journey of a request through a system, this is especially useful if the system is heavily distributed. Also using chaos engineering principles to add confidence in a system and ensure no single part will bring the whole system down when possible.

-----------------------------------------------------------------------------------

Please describe yourself using JSON.

{
	"Name": "Pablo Bessone",
	"Occupation": "Software Architect",
	"Contact": {
		"Email": "some_email@email.com",
		"Address": {
			"City": "Edinburgh",
			"Country": "United Kingdom"
		}
	},
	"Hobbies": [
		"Reading",
		"Gaming",
		"Hiking"
	]
}