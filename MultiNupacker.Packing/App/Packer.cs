using System;
using System.IO;
using MultiNupacker.Packing.Results;
using NuGet.Packaging;
using NuGet.Versioning;

namespace MultiNupacker.Packing.App
{
    public class Packer
    {
        private readonly AssemblyFiles _assemblyFiles;

        public Packer(AssemblyFiles assemblyFiles)
        {
            _assemblyFiles = assemblyFiles;
        }

        internal PackingResult Pack()
        {
            try
            {
                var nuspecFileName = CreateSinglePackage();
                return new SuccessfulPackingResult(nuspecFileName);
            }
            catch (Exception exception)
            {
                return new FailedPackingResult(exception);
            }
        }

        private string CreateSinglePackage()
        {
            var assemblyInfo = new AssemblyInfo(_assemblyFiles.DllFilePAth);

            var builder = new PackageBuilder();

            _assemblyFiles.Paths.ForEach(x =>
                builder.AddFiles(string.Empty, x,
                    Path.Combine(assemblyInfo.DesiredNugetDirectoryPath, Path.GetFileName(x))));

            builder.Populate(assemblyInfo.ManifestMetadata);

            var currentDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

            var nuspecFileName = $"{assemblyInfo.NameAndVersion}.nupkg";

            using (var stream = new FileStream(Path.Combine(currentDirectoryPath, nuspecFileName), FileMode.Create))
            {
                builder.Save(stream);
            }

            return nuspecFileName;
        }


    }
}