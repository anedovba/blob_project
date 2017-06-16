using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueuesStorage
{
    public class GettingStarted
    {
        /// <summary>
        /// Test some of the file storage operations.
        /// </summary>
        public async Task RunQueueStorageOperationsAsync()
        {
            // Create the queue name -- use a guid in the name so it's unique.
            string queueName = "newqueue";

            // Create or reference an existing queue.
            CloudQueue queue = CreateQueueAsync(queueName).Result;

            // Demonstrate basic queue functionality.  
            await BasicQueueOperationsAsync(queue);

          
        }

        /// <summary>
        /// Create a queue for the sample application to process messages in. 
        /// </summary>
        /// <returns>A CloudQueue object</returns>
        public async Task<CloudQueue> CreateQueueAsync(string queueName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString(Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();


            CloudQueue queue = queueClient.GetQueueReference(queueName);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator.  ess the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return queue;
        }

        /// <summary>
        /// Demonstrate basic queue operations such as adding a message to a queue, peeking at the front of the queue and dequeing a message.
        /// </summary>
        /// <param name="queue">The sample queue</param>
        public async Task BasicQueueOperationsAsync(CloudQueue queue)
        {
            //// Insert a message into the queue using the AddMessage method. 
            //Console.WriteLine("2. Insert a single message into a queue");
            //await queue.AddMessageAsync(new CloudQueueMessage("Congratulation!"));

            CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();
            if (peekedMessage != null)
            {
                Console.WriteLine("Новое сообщение: {0}", peekedMessage.AsString);
            }

           
            CloudQueueMessage message = await queue.GetMessageAsync();
            if (message != null)
            {
                Console.WriteLine("Удаляем прочитанное сообщение: {0}", message.AsString);
                await queue.DeleteMessageAsync(message);
            }
        }

             
   

        /// <summary>
        /// Delete the queue that was created for this sample
        /// </summary>
        /// <param name="queue">The sample queue to delete</param>
        public async Task DeleteQueueAsync(CloudQueue queue)
        {
            Console.WriteLine("10. Delete the queue");
            await queue.DeleteAsync();
        }
    }
}
