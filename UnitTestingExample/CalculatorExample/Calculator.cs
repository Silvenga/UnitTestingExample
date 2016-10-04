namespace UnitTestingExample.CalculatorExample
{
    using System;

    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Substract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Divide(int a, int b)
        {
            if (b <= 0)
            {
                throw new ArgumentOutOfRangeException("b");
            }

            return a / b;
        }
    }
}