namespace Calculator;

public partial class CalculatorApp : Form
{
    private readonly SynchronizationContext? _sync;

    public CalculatorApp()
    {
        InitializeComponent();
        _sync = SynchronizationContext.Current;
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        //var jacco = SynchronizationContext.Current
        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b)) 
        {
            //var result = LongAdd(a, b);
            //UpdateAnswer(result);

            //Task.Run(()=>LongAdd(a, b))
            //    .ContinueWith(t => {
            //        _sync.Send(UpdateAnswer, t.Result);
            //    });

            //var result = await LongAddAsync(a, b);
            var result = DoeIets(a, b).Result;
            UpdateAnswer(result);
        }
    }

    private async Task<int> DoeIets(int a, int b)
    {
        var tx = LongAddAsync(a, b); //.ConfigureAwait(false);
        return await tx;
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
    private Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}