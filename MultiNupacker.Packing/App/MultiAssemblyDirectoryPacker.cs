using System;
using System.IO;
using System.Linq;

namespace MultiNupacker.Packing.App
{
    internal class MultiAssemblyDirectoryPacker
    {
        private readonly string _directoryPath;
        private readonly string _filter;
        private readonly FilesystemAbstraction _filesystemAbstraction;
        private readonly FileToPackSelector _fileToPackSelector;
        private readonly PackerFactory _packerFactory;
        private readonly Action<string> _log;

        public MultiAssemblyDirectoryPacker(CommandLineOptions options, 
            FilesystemAbstraction filesystemAbstraction, 
            FileToPackSelector fileToPackSelector, 
            PackerFactory packerFactory,
            Action<string> log)
        {
            _directoryPath = options.InputDirectoryPath;
            _filter = options.Filter;
            _filesystemAbstraction = filesystemAbstraction;
            _fileToPackSelector = fileToPackSelector;
            _packerFactory = packerFactory;
            _log = log;
        }

        public void PackAllAssembliesInDirectory()
        {
            var allFiles = _filesystemAbstraction.EnumerateFiles(_directoryPath, _filter);

            var assemblyFilesCollection = allFiles
                .Where(_fileToPackSelector.IsDesired)
                .GroupBy(Path.GetFileNameWithoutExtension)
                .Select(AssemblyFiles.Create)
                .ToList();

            foreach (var assemblyFiles in assemblyFilesCollection)
            {
                _log($"Packing: {assemblyFiles.AssemblyName}");
                var packer = _packerFactory.Create(assemblyFiles);
                var result = packer.Pack();
                _log(result.ToString());
            }
        }
    }
}