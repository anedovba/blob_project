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
            CloudBlockBlob blob = container.GetBlockBlobReference("tomato");
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\ANNA\Pictures\tomato_PNG12530.png"))
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
           
            List<Dictionary<string, string>> blobs = new List<Dictionary<string, string>>();


            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob bl = (CloudBlockBlob)item;
                    Dictionary<string, string> blob = new Dictionary<string, string>();
                    blob.Add("Uri", bl .Uri.AbsoluteUri);
                    blob.Add("Name", bl.Name);
                    blobs.Add(blob);
                
                }
                
            }
            ViewBag.blobs = blobs;
           
            return View(blobs);
           
        }
        public EmptyResult DownloadBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            CloudBlockBlob blob = container.GetBlockBlobReference("carrot");
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\ANNA\Pictures\carrot_new.jpg"))
            {
                blob.DownloadToStream(fileStream);
            }
            return new EmptyResult();
        }

        public EmptyResult DeleteBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("nedovba_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            CloudBlockBlob blob = container.GetBlockBlobReference("carrot");
            blob.Delete();

            return new EmptyResult();
        }
    }
}