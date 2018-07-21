namespace MultiNupacker.Packing.App
{
    internal class PackerFactory
    {
        private readonly FilesystemAbstraction _filesystemAbstraction;

        public PackerFactory(FilesystemAbstraction filesystemAbstraction)
        {
            _filesystemAbstraction = filesystemAbstraction;
        }

        public Packer Create(AssemblyFiles assemblyFiles)
        {
            return new Packer(assemblyFiles, _filesystemAbstraction);
        }
    }
}