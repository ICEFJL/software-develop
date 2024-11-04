public class Calculator
{


    public void Calculate(ref double currentNumber, ref double lastNumber,string currentOperator)
    {
        switch (currentOperator)
        {
            case "+":
                lastNumber += currentNumber;
                break;
            case "-":
                lastNumber -= currentNumber;
                break;
            case "*":
                lastNumber *= currentNumber;
                break;
            case "/":
                if (currentNumber == 0)
                {
                    throw new DivideByZeroException();
                }
                lastNumber /= currentNumber;
                break;
            case "^":
                lastNumber = Math.Pow(lastNumber, currentNumber);
                break;
            default:
                break;
        }
        lastNumber = Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}