using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using MultiNupacker.Packing.Extensions;
using NuGet.Packaging;
using NuGet.Versioning;

namespace MultiNupacker.Packing.App
{
    internal class AssemblyInfo
    {
        private readonly string _fileVersion;
        
        public string FrameworkNugetName { get; }
        public string Name { get; }
        public ManifestMetadata ManifestMetadata { get; }

        public string NameAndVersion => $"{Name}.{_fileVersion}";
        public string DesiredNugetDirectoryPath => $@"lib\{ToNugetName(FrameworkNugetName)}";

        public AssemblyInfo(string assemblyDllPath)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assemblyDllPath);

            var assembly = Assembly.LoadFrom(assemblyDllPath);
            var assemblyCustomAttributes = assembly.GetCustomAttributes(true);

            var frameworkDisplayName = assemblyCustomAttributes
                .OfType<TargetFrameworkAttribute>()
                .Single()
                .FrameworkDisplayName;

            var author = assemblyCustomAttributes
                .OfType<AssemblyCompanyAttribute>()
                .SingleOrDefault()?
                .Company
                .UnknownIfEmptyOrNull();

            var description = assemblyCustomAttributes
                .OfType<AssemblyDescriptionAttribute>()
                .SingleOrDefault()?
                .Description
                .UnknownIfEmptyOrNull();

            var assemblyFileVersion = fileVersionInfo.FileVersion;
            var assemblyName = fileVersionInfo.ProductName;

            FrameworkNugetName = frameworkDisplayName;
            _fileVersion = assemblyFileVersion;
            Name = assemblyName;
            ManifestMetadata = CreateMetadata(assemblyName, _fileVersion, author, description);
        }

        private static string ToNugetName(string assemblyDisplayName)
        {
            return assemblyDisplayName.Replace(".NET Framework ", "net").Replace(".", string.Empty);
        }

        private static ManifestMetadata CreateMetadata(string assemblyName, string version, string author, string description)
        {
            var metadata = new ManifestMetadata
            {
                Version = new NuGetVersion(version),
                Id = assemblyName,
                Authors = new[] { author },
                Description = description
            };

            return metadata;
        }
    }
}