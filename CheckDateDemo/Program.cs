using System;
using NetEti.Globals;
using Vishnu.Interchange;

namespace CheckDateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckDate.CheckDate checkDate = new CheckDate.CheckDate();
            checkDate.NodeProgressChanged += SubNodeProgressChanged;

            checkDate.Run("500", new TreeParameters("MainTree", null), null);
            Console.WriteLine("Result: {0}", checkDate.ReturnObject.ToString());

            Console.ReadLine();
        }

        static void SubNodeProgressChanged(object sender, CommonProgressChangedEventArgs args)
        {
            Console.WriteLine("{0}: {1} von {2}", args.ItemName, args.CountSucceeded, args.CountAll);
        }
    }
}
