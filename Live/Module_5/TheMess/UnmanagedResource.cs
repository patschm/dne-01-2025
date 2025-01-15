namespace TheMess;

internal class UnmanagedResource : IDisposable
{
    private static bool _isOpen = false;
    private FileStream _stream;
    private bool _disposed = false;

    public void Open()
    {
        Console.WriteLine("Trying to open...");
        if (_isOpen)
        {
            Console.WriteLine("Already in use");
            return;
        }
        _isOpen = true;
        _stream = File.OpenRead("blah.txt");
        Console.WriteLine("Open");
    }
    public void Close()
    {
        Console.WriteLine("Closing....");
        _isOpen = false;
        _stream.Close();
           
    }

    protected void RuimOp(bool fromFinalizer)
    {
        if (!_disposed)
        {
            if (!fromFinalizer)
            {
                _stream.Dispose();
            }
            Close();
            _disposed = true;
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        RuimOp(false);
    }

    ~UnmanagedResource()
    {       
        RuimOp(true);
    }
}
