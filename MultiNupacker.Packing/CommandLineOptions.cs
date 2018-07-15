using CommandLine;

namespace MultiNupacker.Packing
{
    internal class CommandLineOptions
    {
        [Option('d', "assembliesDirectory", Default = @"..\..\..\MultiNupacker.LibraryToPack\bin\Debug\")]
        public string AssembliesDirectory { get; set; }

        [Option('f', "filter", Default = "*Library*")]
        public string Filter { get; set; }
    }
}