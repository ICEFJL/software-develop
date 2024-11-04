namespace CALC
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private Calculator calculator = new Calculator();
        private double currentNumber = 0;
        private double lastNumber = 0;
        private bool hasNextNumber = false;
        private string preNumber = "";
        private string currentOperator = "";
        private bool isResult = false;

        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;

            // 如果当前显示的是结果，或者是0，就清空显示屏
            if (isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                isResult = false;
            }

            preNumber = displayLabel.Text;
            try
            {
                // 将数字追加到显示屏，并更新当前输入的数字
                displayLabel.Text += number;
                currentNumber = double.Parse(displayLabel.Text);
                hasNextNumber = true;
            }
            catch (FormatException)
            {
                currentNumber = double.Parse(preNumber); 
                displayLabel.Text = preNumber;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误: " + ex.Message);
            }
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var op = button.Text;

            // 如果当前的运算符不为空，就执行上一次选择的运算，并显示结果
            if (currentOperator != "")
            {
                if (!hasNextNumber){
                    currentOperator = op;
                    opreationLabel.Text = lastNumber.ToString() + currentOperator;
                    return;
                }
                try { 
                    Calculate();
                }
                catch (DivideByZeroException)
                {
                    opreationLabel.Text += currentNumber.ToString();
                    return;
                }
            }
            else
            {
                // 否则，就将当前输入的数字赋值给上一次计算的结果
                lastNumber = currentNumber;
                displayLabel.Text = "0";
                currentNumber = 0;
                isResult = false;
            }
            hasNextNumber = false;
            currentOperator = op;
            opreationLabel.Text = lastNumber.ToString() + currentOperator;

        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {
            // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
            if (currentOperator != "")
            {
                opreationLabel.Text += currentNumber.ToString() + "=";
                try
                {
                    Calculate();
                }
                catch (DivideByZeroException)
                {
                }
                currentOperator = "";
           
            }
        }
    
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentNumber = 0;
            lastNumber = 0;
            currentOperator = "";
            isResult = false;
            displayLabel.Text = "0";
            opreationLabel.Text = "";
        }
      
        private void OnBackClicked(object sender, EventArgs e)
        {
            displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
            if (displayLabel.Text == "")
            {
                displayLabel.Text = "0";
            }
            currentNumber = double.Parse(displayLabel.Text);
        }

        private void OnClearEntryClicked(object sender, EventArgs e)
        {
            
            displayLabel.Text = "0";
            currentNumber = 0;
           
        }

        private void Calculate()
        {
            try
            {
                calculator.Calculate(ref currentNumber, ref lastNumber, currentOperator);
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
            catch (DivideByZeroException)
            {
                
                displayLabel.Text = "除数不能为0";
                currentNumber = 0;
                lastNumber = 0;
                currentOperator = "";
                isResult = true;
                throw new DivideByZeroException();
            }
        }
    }

}
