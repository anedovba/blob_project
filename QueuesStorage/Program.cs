using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types

namespace QueuesStorage
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Давайте прочтем сообщения из очереди в AZURE\n");
            String s;
            while (true)
            {
                GettingStarted getStarted = new GettingStarted();
                getStarted.RunQueueStorageOperationsAsync().Wait();
                Console.WriteLine("получить следующее сообщение?\nВыберите \"1\" если ДА, или нажмите любую клавишу, если НЕТ");
                s = Console.ReadLine();
                if (s != "1")
                {
                    break;
                }
            }
            

            Console.WriteLine("Вы вышли из программы\n");
            Console.WriteLine("Нажмите любую клавишу");
            Console.Read();

        }
        

            }

        }

