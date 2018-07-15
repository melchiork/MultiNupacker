using System;
using System.IO;
using System.Linq;

namespace MultiNupacker.Packing.App
{
    public class MultiAssemblyDirectoryPacker
    {
        private readonly string _directoryPath;
        private readonly string _filter;
        private readonly Action<string> _log;

        public MultiAssemblyDirectoryPacker(string directoryPath, string filter, Action<string> log)
        {
            _directoryPath = directoryPath;
            _filter = filter;
            _log = log;
        }

        public void PackAllAssembliesInDirectory()
        {
            var allFiles = Directory.EnumerateFiles(_directoryPath, _filter);

            var assemblyFilesCollection = allFiles.Where(IsDesiredFile).GroupBy(Path.GetFileNameWithoutExtension)
                .Select(AssemblyFiles.Create).ToList();

            foreach (var assemblyFiles in assemblyFilesCollection)
            {
                _log($"Packing: {assemblyFiles.AssemblyName}");
                var packer = new Packer(assemblyFiles);
                var result = packer.Pack();
                _log(result.ToString());
            }
        }

        private static bool IsDesiredFile(string fileName)
        {
            return new[] {".dll", ".pdb", ".xml"}.Contains(Path.GetExtension(fileName.ToLower()));
        }
    }
}