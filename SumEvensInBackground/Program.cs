long sum = 0;

Task.Run(() =>
{
	for (int i = 0; i < 1000000000; i++)
	{
		if (i % 2 == 0)
		{
			sum += i;
		}
	}
});

while (true)
{
	string cmd = Console.ReadLine()!;
	if (cmd == "show")
	{
        Console.WriteLine(sum);
    }
	if (cmd == "exit")
	{
		return;
	}
}