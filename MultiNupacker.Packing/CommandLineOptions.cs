using System.Collections.Generic;
using CommandLine;

namespace MultiNupacker.Packing
{
    internal class CommandLineOptions
    {
        public CommandLineOptions(string inputDirectoryPath, 
            string filter, 
            IEnumerable<string> desiredFileExtensions, 
            string outputDirectoryPath)
        {
            InputDirectoryPath = inputDirectoryPath;
            Filter = filter;
            DesiredFileExtensions = desiredFileExtensions;
            OutputDirectoryPath = outputDirectoryPath;
        }

        [Option('i', "inputDirectory", Default = @"..\..\..\MultiNupacker.LibraryToPack\bin\Debug\")]
        public string InputDirectoryPath { get; }

        [Option('f', "filter", Default = "*Library*")]
        public string Filter { get; }

        [Option('e', "extensions", Default = new [] {"dll", "pdb", "xml"}, Separator = ',')]
        public IEnumerable<string> DesiredFileExtensions { get; }

        [Option('o', "outputDirectory", Default = ".")]
        public string OutputDirectoryPath { get; }
    }
}