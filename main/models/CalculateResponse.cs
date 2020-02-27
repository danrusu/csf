
using System.Collections.Generic;

namespace test
{
    public class CalculateResponse
    {

        public List<string> Numbers { get; }
        public string Operation { get; }
        public string Result { get; }

        public CalculateResponse(List<string> numbers, string operation, string result)
        {
            this.Numbers = numbers;
            this.Operation = operation;
            this.Result = result;
        }

        public override string ToString()
        {
            return $"numbers {string.Join(", ", Numbers.ToArray())}, operation={Operation}, result={Result}";
        }
    }
}
