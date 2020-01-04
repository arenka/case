using System;

namespace Scrapper.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Management.Core.Scrapper scrapper = new Management.Core.Scrapper();
            scrapper.GetTitles();
        }
    }
}
