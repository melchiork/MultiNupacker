using System;
using System.IO;
using MultiNupacker.Packing.Results;
using NuGet.Packaging;
using NuGet.Versioning;

namespace MultiNupacker.Packing.App
{
    internal class Packer
    {
        private readonly AssemblyFiles _assemblyFiles;
        private readonly FilesystemAbstraction _filesystemAbstraction;

        public Packer(AssemblyFiles assemblyFiles, FilesystemAbstraction filesystemAbstraction)
        {
            _assemblyFiles = assemblyFiles;
            _filesystemAbstraction = filesystemAbstraction;
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

            var nuspecFileName = $"{assemblyInfo.NameAndVersion}.nupkg";

            using (var stream = _filesystemAbstraction.OpenStreamToOutputLocation(nuspecFileName))
            {
                builder.Save(stream);
            }

            return nuspecFileName;
        }


    }
}