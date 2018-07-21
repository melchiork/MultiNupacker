using System.IO;
using System.Linq;

namespace MultiNupacker.Packing.App
{
    internal class FileToPackSelector
    {
        private readonly string[] _extensions;

        public FileToPackSelector(CommandLineOptions commandLineOptions)
        {
            _extensions = commandLineOptions
                .DesiredFileExtensions
                .Select(x => x.ToLower())
                .Select(EnsureLeadingDot)
                .ToArray();
        }

        public bool IsDesired(string fileName)
        {
            return _extensions.Contains(Path.GetExtension(fileName.ToLower()));
        }

        private static string EnsureLeadingDot(string extension)
        {
            var result = extension.StartsWith(".") ? extension : $".{extension}";
            
            return result;
        }
    }
}