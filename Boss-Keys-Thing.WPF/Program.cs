using System.Windows;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace BossKeysThing.WPF;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = Host.CreateDefaultBuilder(args)
			.ConfigureWpf<App>();

		var host = builder.Build()
			.InitializeSplat();

		host.Run();
	}

	private static IHostBuilder ConfigureWpf<T>(this IHostBuilder hostBuilder) 
		where T : Application
	{
		return hostBuilder.ConfigureWpf(builder => builder.UseApplication<T>())
			.ConfigureSplat()
			.UseWpfLifetime();
	}

	private static IHostBuilder ConfigureSplat(this IHostBuilder hostBuilder)
	{
		return hostBuilder.ConfigureServices((context, collection) =>
		{
			collection.UseMicrosoftDependencyResolver();

			var locator = Locator.CurrentMutable;
			locator.InitializeSplat();
			locator.InitializeReactiveUI();
		});
	}

	private static IHost InitializeSplat(this IHost host)
	{
		host.Services.UseMicrosoftDependencyResolver();

		return host;
	}
}