using System.Collections.Generic;
using System.IO;

namespace MultiNupacker.Packing.App
{
    internal class FilesystemAbstraction
    {
        private string _directoryPath;
        public FilesystemAbstraction(CommandLineOptions commandLineOptions)
        {
            _directoryPath = commandLineOptions.OutputDirectoryPath;
        }

        public IEnumerable<string> EnumerateFiles(string directoryPath, string filter)
        {
            return Directory.EnumerateFiles(directoryPath, filter);
        }

        public Stream OpenStreamToOutputLocation(string fileName)
        {
            var path = Path.Combine(_directoryPath, fileName);

            return new FileStream(path, FileMode.Create);
        }
    }
}