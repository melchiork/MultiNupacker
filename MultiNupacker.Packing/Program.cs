using System;
using CommandLine;
using MultiNupacker.Packing.App;

namespace MultiNupacker.Packing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(opt => PackAllAssembliesInDirectory(opt.AssembliesDirectory, opt.Filter))
                .WithNotParsed(Console.WriteLine);
        }

        private static void PackAllAssembliesInDirectory(string directory, string filter)
        {
            var multiAssemblyDirectoryPacker = new MultiAssemblyDirectoryPacker(directory, filter, Console.WriteLine);

            multiAssemblyDirectoryPacker.PackAllAssembliesInDirectory();
        }
    }
}