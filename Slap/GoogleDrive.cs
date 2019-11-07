using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Slap
{
    class GoogleDrive
    {
        //Google Drive API
        private static string[] scopes = { DriveService.Scope.Drive };
        private static string appname = "GoogleDriveAPIStart";

        // Google API Functions
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
                    new FileDataStore(creadPath, true)
                    ).Result;
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

        public static bool FindFileFolder(string fileFolderName)
        {
            DriveService service = GetDriveService();

            string pageToken = null;

            var request = service.Files.List();

            // Query to search for file/folder
            request.Q = "name contains '" + fileFolderName + "'";
            request.Fields = "nextPageToken, files(id, name, mimeType)";
            request.PageToken = pageToken;

            var result = request.Execute();

            if(result.Files.Count > 0)
            {
                return true;
            }

            return false;
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

        public static void UploadPdf(string PDFFilePath, string fileName, string folderID)
        {
            using (DriveService service = GetDriveService())
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    Parents = new List<String> { folderID }
                };

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(PDFFilePath, FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream, "application/pdf");
                    request.Upload();
                }

                var file = request.ResponseBody;
            }

            //MessageBox.Show(file.Id);
        }
    }
}
