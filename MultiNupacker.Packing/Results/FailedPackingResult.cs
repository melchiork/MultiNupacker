using System;

namespace MultiNupacker.Packing.Results
{
    internal class FailedPackingResult : PackingResult
    {
        private readonly Exception _error;

        public FailedPackingResult(Exception error)
        {
            _error = error;
        }

        public override string ToString()
        {
            return $"Error: {_error}";
        }
    }
}