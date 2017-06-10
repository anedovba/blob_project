using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobProject.Controllers
{
    public class BlobsController : Controller
    {
        // GET: Blobs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            ViewBag.Success = container.CreateIfNotExists();
            ViewBag.BlobContainerName = container.Name;

            return View();
        }
        public EmptyResult UploadBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            CloudBlockBlob blob = container.GetBlockBlobReference("test_image");
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\ANNA\Pictures\2996471-p-LARGE_SEARCH.jpg"))
            {
                blob.UploadFromStream(fileStream);
            }
            return new EmptyResult();
        }
        public ActionResult ListBlobs()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            List<string> blobs = new List<string>();

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dir = (CloudBlobDirectory)item;
                    blobs.Add(dir.Uri.ToString());
                }
            }

            return View(blobs);
           
        }
        public EmptyResult DownloadBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            CloudBlockBlob blob = container.GetBlockBlobReference("test_image");
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\ANNA\Pictures\red_shoes.jpg"))
            {
                blob.DownloadToStream(fileStream);
            }
            return new EmptyResult();
        }
    }
}