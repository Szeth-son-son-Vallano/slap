using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Download;
using System.Windows.Forms;
using System.Drawing;

namespace Slap
{
    class GoogleDrive
    {
        //Google Drive API
        private static readonly string[] scopes = { DriveService.Scope.Drive };
        private static readonly string appname = "GoogleDriveAPIStart";

        // Google Drive API Functions
        public static UserCredential GetUserCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string creadPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                creadPath = Path.Combine(creadPath, "driveApiCredentials", "drive-credentials.json");

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(creadPath, true)).Result;
            }
        }

        public static DriveService GetDriveService()
        {
            UserCredential credential = GetUserCredential();

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = appname
            });
        }

        public static FileList GetFolders(string folderName)
        {
            using (DriveService service = GetDriveService())
            {
                string pageToken = null;

                var request = service.Files.List();

                // Query to search for file/folder
                request.Q =
                    "name contains '" + folderName + "' and " +
                    "mimeType = 'application/vnd.google-apps.folder'";
                request.PageToken = pageToken;

                var result = request.Execute();

                if (result.Files.Count > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public static FileList GetFiles_PDF_CSV(string searchFileName)
        {
            try
            {
                using (DriveService service = GetDriveService())
                {
                    string pageToken = null;

                    var request = service.Files.List();

                    // Query to search for file/folder
                    request.Q =
                        "name contains '" + searchFileName + "' and " +
                        "(mimeType = 'application/pdf' or mimeType = 'application/vnd.ms-excel')";
                    request.PageToken = pageToken;

                    var result = request.Execute();

                    if (result.Files.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string CreateFolder(string folderName)
        {
            using (DriveService service = GetDriveService())
            {
                //Folder ID
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };

                var request = service.Files.Create(fileMetadata);
                request.Fields = "id";

                var file = request.Execute();

                return file.Id;
            }
        }

        public static bool UploadFile(string FilePath, string fileName, string folderID)
        {
            try
            {
                using (DriveService service = GetDriveService())
                {
                    var fileMetadata = new Google.Apis.Drive.v3.Data.File
                    {
                        Name = fileName,
                        Parents = new List<String> { folderID }
                    };

                    FilesResource.CreateMediaUpload request;

                    using (var stream = new FileStream(FilePath, FileMode.Open))
                    {
                        if (Path.GetExtension(fileName).Equals(".pdf"))
                        {
                            request = service.Files.Create(fileMetadata, stream, "application/pdf");
                            request.Upload();
                        }
                        else if (Path.GetExtension(fileName).Equals(".csv"))
                        {
                            request = service.Files.Create(fileMetadata, stream, "application/vnd.ms-excel");
                            request.Upload();
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public static List<string> DownloadFiles(string searchFileName)
        {
            try
            {
                using (DriveService service = GetDriveService())
                {
                    List<string> filePaths = new List<string>();

                    searchFileName = searchFileName + "_";
                    FileList fileList = GetFiles_PDF_CSV(searchFileName);

                    if (fileList != null)
                    {
                        foreach (var file in fileList.Files)
                        {
                            var request = service.Files.Get(file.Id);

                            using (var memoryStream = new MemoryStream())
                            {
                                request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                                {
                                    switch (progress.Status)
                                    {
                                        case DownloadStatus.Downloading:
                                        //MessageBox.Show(progress.BytesDownloaded.ToString());
                                        break;

                                        case DownloadStatus.Completed:
                                        //MessageBox.Show("Download Complete: " + file.Name);
                                        break;

                                        case DownloadStatus.Failed:
                                        //MessageBox.Show("Download failed: " + file.Name);
                                        break;
                                    }
                                };

                                request.Download(memoryStream);
                                string DownloadsPath = Properties.Settings.Default.DownloadLocation;
                                string FolderPath = Path.Combine(DownloadsPath, searchFileName.Substring(0, searchFileName.Length - 1));
                                string FileName = file.Name;

                                if (Directory.Exists(FolderPath))
                                {
                                    int i = 0;
                                    while (System.IO.File.Exists(Path.Combine(FolderPath, FileName)))
                                    {
                                        i++;
                                        FileName = Path.GetFileNameWithoutExtension(file.Name) + "(" + i + ")" + Path.GetExtension(file.Name);
                                    }
                                }
                                else
                                {
                                    Directory.CreateDirectory(FolderPath);
                                }

                                FileName = Path.Combine(FolderPath, FileName);

                                using (var fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                                {
                                    fileStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);

                                    filePaths.Add(FileName);
                                }
                            }
                        }
                        return filePaths;
                    }
                    else
                    {
                        MessageBox.Show("Files containing '" + searchFileName + "' could be found");
                        return null;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }
    }
}
