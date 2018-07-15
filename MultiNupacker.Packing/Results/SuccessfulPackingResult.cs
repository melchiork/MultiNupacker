namespace MultiNupacker.Packing.Results
{
    internal class SuccessfulPackingResult : PackingResult
    {
        private readonly string _nuspecFileName;

        public SuccessfulPackingResult(string nuspecFileName)
        {
            _nuspecFileName = nuspecFileName;
        }

        public override string ToString()
        {
            return $"Success! File created: {_nuspecFileName}";
        }
    }
}