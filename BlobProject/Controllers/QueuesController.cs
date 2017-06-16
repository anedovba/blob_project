using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types
using System.Threading.Tasks;

namespace BlobProject.Controllers
{
    public class QueuesController : Controller
    {
        // GET: Queues
        public ActionResult Index()
        {
            return View();
        }


        // POST: Queues/Create
        [HttpPost]
        public ActionResult Index(string mes)
        {
            try
            {
                //CloudQueue queue = CreateQueueAsync("nedovba_AzureStorageConnectionString").Result;
                //// Retrieve storage account information from connection string.
                //AddMessageAsync(queue, str).Wait();
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));

                // Create the queue client.
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                // Retrieve a reference to a queue.
                CloudQueue queue = queueClient.GetQueueReference("newqueue");

                // Create the queue if it doesn't already exist.
                queue.CreateIfNotExists();

                // Create a message and add it to the queue.
                CloudQueueMessage message = new CloudQueueMessage(mes);
                queue.AddMessage(message);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<CloudQueue> CreateQueueAsync(string queueName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting(queueName));

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();


            CloudQueue queue = queueClient.GetQueueReference(queueName);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {
                throw;
            }

            return queue;
        }

        public async Task AddMessageAsync(CloudQueue queue, String str)
        {
            await queue.AddMessageAsync(new CloudQueueMessage(str));
        }


    }
}
