using System;


namespace ChatRoom.ConsoleReport
{
    public static class Info
    {
        public static void Output(string massage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(massage);
            Console.ResetColor();
        }
    }
}
