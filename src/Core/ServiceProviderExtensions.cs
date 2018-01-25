using System;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Parsers;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddVisualStudioFiles(this IServiceCollection source)
        {

            return source
                .AddScoped<ISystemIO>(provider => new SystemIO())
                .AddScoped<ISolutionFileParser, SolutionFileParser>()
                .AddScoped<IPackageReferenceFileReader, PackageReferenceFileReader>()
                .AddScoped<IRunSettingsFileReader, RunSettingsFileReader>()
                .AddScoped<IProjectFactory, ProjectFactory>()
                .AddScoped<ISolutionFactory, SolutionFactory>()
                .AddScoped<ISolutionReader, SolutionReader>();
        }
    }
}