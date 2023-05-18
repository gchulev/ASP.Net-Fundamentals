int rangeStart = int.Parse(Console.ReadLine()!);
int endRange = int.Parse(Console.ReadLine()!);

Thread evenNums = new Thread(() => PrintEvenNumbers(rangeStart, endRange));

evenNums.Start();
evenNums.Join();
Console.WriteLine("Thread finished work");

//testing gitignore

static void PrintEvenNumbers(int start, int end)
{
	for (int i = start; i <= end; i++)
	{
        if (i % 2 == 0)
        {
            Console.WriteLine(i);
        }
    }
}