using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiNupacker.Packing.App
{
    public class AssemblyFiles
    {
        public List<string> Paths { get; }

        public string AssemblyName { get; }

        public string DllFilePAth => Paths.Single(x => Path.GetExtension(x) == ".dll");

        public static AssemblyFiles Create(IGrouping<string, string> groupOfAssemblyFiles)
        {
            return new AssemblyFiles(groupOfAssemblyFiles);
        }

        private AssemblyFiles(IGrouping<string, string> groupOfAssemblyFiles)
        {
            Paths = groupOfAssemblyFiles.ToList();
            AssemblyName = groupOfAssemblyFiles.Key;
        }
    }
}