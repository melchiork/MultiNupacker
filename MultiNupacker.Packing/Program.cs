using System;
using CommandLine;
using MultiNupacker.Packing.App;
using TinyIoC;

namespace MultiNupacker.Packing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(PackAllAssembliesInDirectory)
                .WithNotParsed(Console.WriteLine);
        }

        private static void PackAllAssembliesInDirectory(CommandLineOptions commandLineOptions)
        {
            var kernel = CreateKernel(commandLineOptions);

            var multiAssemblyDirectoryPacker = kernel.Resolve<MultiAssemblyDirectoryPacker>();

            multiAssemblyDirectoryPacker.PackAllAssembliesInDirectory();
        }

        private static TinyIoCContainer CreateKernel(CommandLineOptions commandLineOptions)
        {
            var kernel = TinyIoCContainer.Current;
            kernel.Register<Action<string>>(Console.WriteLine);
            kernel.Register(commandLineOptions);
            kernel.Register<FileToPackSelector>();
            kernel.Register<PackerFactory>();
            kernel.Register<MultiAssemblyDirectoryPacker>();

            return kernel;
        }
    }
}