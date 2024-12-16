using System;
using System.Threading;

public class Stopwatch
{
    // Fields
    private int _timeElapsed; // Time elapsed in seconds
    private bool _isRunning;

    // Delegates and Events
    public delegate void StopwatchEventHandler(string message);
    public event StopwatchEventHandler OnStarted = delegate { };
    public event StopwatchEventHandler OnStopped = delegate { };
    public event StopwatchEventHandler OnReset = delegate { };

    // Properties
    public int TimeElapsed => _timeElapsed;
    public bool IsRunning => _isRunning;

    // Methods
    public void Start()
    {
        if (_isRunning)
        {
            Console.WriteLine("Stopwatch is already running.");
            return;
        }
        _isRunning = true;
        OnStarted?.Invoke("Stopwatch Started!");
        RunStopwatch();
    }

    public void Stop()
    {
        if (!_isRunning)
        {
            Console.WriteLine("Stopwatch is not running.");
            return;
        }
        _isRunning = false;
        OnStopped?.Invoke("Stopwatch Stopped!");
    }

    public void Reset()
    {
        _isRunning = false;
        _timeElapsed = 0;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void RunStopwatch()
    {
        new Thread(() =>
        {
            while (_isRunning)
            {
                Thread.Sleep(1000);
                _timeElapsed++;
                Console.WriteLine($"Time Elapsed: {_timeElapsed} seconds");
            }
        }).Start();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        // Subscribe to events
        stopwatch.OnStarted += message => Console.WriteLine(message);
        stopwatch.OnStopped += message => Console.WriteLine(message);
        stopwatch.OnReset += message => Console.WriteLine(message);

        Console.WriteLine("Stopwatch Console Application");
        Console.WriteLine("Press S to Start, T to Stop, R to Reset, and Q to Quit.");

        bool quit = false;
        while (!quit)
        {
            char input = Console.ReadKey(true).KeyChar;

            switch (char.ToUpper(input))
            {
                case 'S':
                    stopwatch.Start();
                    break;
                case 'T':
                    stopwatch.Stop();
                    break;
                case 'R':
                    stopwatch.Reset();
                    break;
                case 'Q':
                    quit = true;
                    Console.WriteLine("Exiting Stopwatch...");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please press S, T, R, or Q.");
                    break;
            }
        }
    }
}
