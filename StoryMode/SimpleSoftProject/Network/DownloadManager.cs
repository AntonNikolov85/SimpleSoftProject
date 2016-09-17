using System;
using System.Net;
using System.Threading.Tasks;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.IO;
using SimpleSoftProject.StaticData;

namespace SimpleSoftProject.Network
{
    public class DownloadManager
    {
        private WebClient webClient;

        public DownloadManager()
        {
            webClient = new WebClient();
        }

        public void Download(string fileURL)
        {
            try
            {
                OutputWriter.WriteMessageOnNewLine("Started downloading: ");

                string nameOfFile = ExtractNameOfFile(fileURL);
                string pathToDownload = SessionData.currentPath + "/" + nameOfFile;

                webClient.DownloadFile(fileURL, pathToDownload);

                OutputWriter.WriteMessageOnNewLine("Download complete");
            }
            catch (WebException)
            {
                throw new InvalidPathException();
            }
        }

        public void DownloadAsync(string fileURL)
        {
            Task currentTask = Task.Run(() => this.Download(fileURL));
            SessionData.taskPool.Add(currentTask);
        }

        private string ExtractNameOfFile(string fileURL)
        {
            int indexOfLastBackSlash = fileURL.LastIndexOf("/");

            if (indexOfLastBackSlash != -1)
            {
                return fileURL.Substring(indexOfLastBackSlash + 1);
            }

            throw new InvalidPathException();
        }
    }
}
