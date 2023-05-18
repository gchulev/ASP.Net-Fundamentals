while (true)
{
    string cmd = Console.ReadLine();
    if (cmd == "show")
    {
        long result = SumAsync();
        Console.WriteLine(result);
        break;
    }
}

static long SumAsync()
{
    return Task.Run(() =>
    {
        int sum = 0;

        for (int i = 0; i < 1000; i++)
        {
            if (i % 2 == 0)
            {
                sum += i;
            }
        }
        return sum;
    }).Result;
}