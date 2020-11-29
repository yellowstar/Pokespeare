using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Providers.Pokemon;
using Pokespeare.Service;
using Pokespeare.Service.Interfaces;
using PokeApiNet;
using PokeApiNet.Interfaces;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Refit;
using System;

namespace Pokespeare
{
	public class Startup
	{
		// Get this from config eventually
		private readonly string _translationUrl = "https://api.funtranslations.com/translate";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSingleton<IPokespeareWorker, PokespeareWorker>();
			services.AddSingleton<IPokeApiClient, PokeApiClient>();
			services.AddSingleton<IPokemonProvider, PokemonProvider>();
			services.AddRefitClient<ITranslationApi>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(_translationUrl));

			ConfigureDynamicServices(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static void ConfigureDynamicServices(IServiceCollection services)
		{
			List<Assembly> assemblies = new List<Assembly>();

			foreach (string assemblyPath in Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories))
			{
				var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
				assemblies.Add(assembly);
			}
			//.. register
			services.Scan(scan => scan
			.FromAssemblies(assemblies)
			.AddClasses(classes => classes.AssignableTo<ITranslationProvider>(), publicOnly: false)
			.AsImplementedInterfaces()
			.WithSingletonLifetime());
		}
	}
}