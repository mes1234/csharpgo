using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Template;

public static class Program
{
    public delegate void CustomEventHandler(object sender, CustomEventArgs args);

    public static async Task Main(string[] args)
    {

        var cts = new CancellationTokenSource();


        var pub = new Publisher();
        var sub1 = new Subscriber(pub);


       var publishTask = Task.Run(() => runPublisher(pub, cts.Token));
        await Task.Delay(5 * 1000);
        cts.Cancel();

        await Task.Delay(1 * 1000);

        System.Console.WriteLine("Exiting...");

        Task.WaitAll(new Task[]{publishTask});
    }

    static async Task runPublisher(Publisher pub, CancellationToken token)
    {
        while (true)
        {
            if (token.IsCancellationRequested)
            {
                System.Console.WriteLine("Time to stop producing...");
                return;
            }

            pub.DoSomething("new very important data sent...");

            await Task.Delay(1000);

        }
    }
}



class Publisher
{
    public event EventHandler<CustomEventArgs> RaiseCustomEvent;

    public void DoSomething(string data)
    {
        OnRaiseCustomEvent(new CustomEventArgs(data));
    }

    protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
    {
        var raiseEvent = RaiseCustomEvent;

        raiseEvent(this, e);
    }
}

class Subscriber
{
    public Subscriber(Publisher pub)
    {
        pub.RaiseCustomEvent += HandleCustomEvent;
    }

    // Define what actions to take when the event is raised.
    void HandleCustomEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"recieved: \"{e.Message}\"");
    }
}

public class CustomEventArgs : EventArgs
{
    public CustomEventArgs(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}
