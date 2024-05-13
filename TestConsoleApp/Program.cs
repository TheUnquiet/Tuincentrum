using BL.Interfaces;
using BL.Managers;
using DL_DataUpload;

namespace TestConsoleApp 
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProcessor fp = new FileProcessor();
            FileManager fm = new FileManager(fp);
            
        }
    }
}