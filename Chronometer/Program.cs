using System.Runtime.CompilerServices;
using System.Xml.Serialization;

var chronometer = new Chronometer.Chronometer();
string cmd;

while ((cmd = Console.ReadLine()) != "exit")
{
    if (cmd == "exit")
    {
        return;
    }
    else if (cmd == "start")
    {
        Task.Run(() =>
        {
            chronometer.Start();
        });
    }
    else if (cmd == "stop")
    {
        chronometer.Stop();
    }
    else if (cmd == "lap")
    {
        Console.WriteLine(chronometer.Lap());
    }
    else if (cmd == "laps")
    {
        if (chronometer.Laps.Count == 0)
        {
            Console.WriteLine("Laps: no laps");
        }
        else
        {
            for (int i = 0; i < chronometer.Laps.Count; i++)
            {
                Console.WriteLine(@$"{i}. {chronometer.Laps[i]}");
            }
        }
    }
    else if (cmd == "reset")
    {
        chronometer.Reset();
    }
    else if (cmd == "time")
    {
        Console.WriteLine(chronometer.GetTime);
    }
}
chronometer.Stop();