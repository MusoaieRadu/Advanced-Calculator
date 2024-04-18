using System.Drawing;
using System.Windows.Forms;

namespace Calculator.Forms
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            helpText.Text = "1) Access mathematical functions by pressing the \"f(x)\" button in the main window\r\n\r\n" +
                      "2) Access keyboard by pressing the \"abc\" button in the main window\r\n\r\n" +
                      "3) View your history log by pressing on the \"View\" tab in the main window, then \"History\"\r\n\r\n" +
                      "4) Create a function by writing it in the input box. Example(x is always the only considered variable) : \r\n\t" +
                      "   a) 2*x^2 + 4*x\r\n\t   b) exp(-b*x^2)\r\n\r\n" +
                      "5) Call your function just like a normal function in the input box. Example : \r\n\t" +
                      "    f(5), where f(x) = ln(sin(x))\r\n\r\n" +
                      "6) View your defined functions by pressing on the \"View\" tab, then \"Functions\"\r\n\r\n" +
                      "7) In the \"Functions\" window, specify the parameters using the dropdown \"Parameter\" menu\r\n\r\n" +
                      "8) In the \"Functions\" window, plot your functions by specifying the minimum and maximum value(xMin, xMax)\r\n\r\n" +
                      "9)NOTE : IF THE FUNCTION YOU ARE TRYING TO GRAPH DOESN'T HAVE A \"REAL\" POINT, THAT POINT WON'T BE SHOWN(IF THE ENTIRE FUNCTION DOESN'T HAVE ANY REAL POINTS, THE PLOT WILL BE A BLANK PAGE).\r\n";
        }
    }
}
