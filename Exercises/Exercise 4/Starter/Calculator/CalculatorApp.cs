namespace Calculator;

public partial class CalculatorApp : Form
{
    private readonly SynchronizationContext? _sync;

    public CalculatorApp()
    {
        InitializeComponent();
        _sync = SynchronizationContext.Current;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        //var jacco = SynchronizationContext.Current
        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b)) 
        {
            //var result = LongAdd(a, b);
            //UpdateAnswer(result);
            Task.Run(()=>LongAdd(a, b))
                .ContinueWith(t => {
                    _sync.Send(UpdateAnswer, t.Result);
                }
                );
        }
    }

    private void UpdateAnswer(object? result)
    {
        lblAnswer.Text = result?.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
}