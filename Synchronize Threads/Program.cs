class Program
{
    static readonly AutoResetEvent thread1Step = new AutoResetEvent(false);
    static readonly AutoResetEvent thread2Step = new AutoResetEvent(false);

    static void Main(string[] args)
    {
        new Thread(new ThreadStart(Thread1Main)).Start();
        new Thread(new ThreadStart(Thread2Main)).Start();
    }

    private static void Thread1Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 != 0)
            {
                Console.WriteLine("thread1 " + i);
            }
            thread1Step.Set();
            thread2Step.WaitOne();
        }
        for (int i = 9; i >= 0; i--)
        {
            if (i % 2 != 0)
            {
                Console.WriteLine("thread1 " + i);
            }
            thread1Step.Set();
            thread2Step.WaitOne();
        }
    }

    private static void Thread2Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine("thread2 " + i);
            }
            thread2Step.Set();
            thread1Step.WaitOne();
        }
        for (int i = 9; i >= 0; i--)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine("thread2 " + i);
            }
            thread2Step.Set();
            thread1Step.WaitOne();
        }
    }
}