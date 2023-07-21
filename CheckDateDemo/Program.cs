using System.ComponentModel;
using Vishnu.Interchange;

namespace CheckDateDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CheckDate.CheckDate checkDate = new CheckDate.CheckDate();
            checkDate.NodeProgressChanged += SubNodeProgressChanged;

            checkDate.Run("500", new TreeParameters("MainTree", null), TreeEvent.UndefinedTreeEvent);
            Console.WriteLine("Result: {0}", checkDate.ReturnObject?.ToString());

            Console.ReadLine();
        }

        static void SubNodeProgressChanged(object? sender, ProgressChangedEventArgs args)
        {
            Console.WriteLine(args.ProgressPercentage);
        }
    }
}