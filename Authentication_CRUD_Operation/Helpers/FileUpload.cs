using Authentication_CRUD_Operation.enums;

namespace Authentication_CRUD_Operation.Helpers
{
    public class FileUpload : IFileUpload
    {
        private readonly string FileDirectory = Directory.GetCurrentDirectory();
        private readonly string MainFolder = "wwwroot/";
        public (string, string) UploadFile(IFormFile File, UploadDirectoriesEnum FileFor)
        {
            //Get File Upload For
            string UploadDirectory = FileFor.ToString();
            //Get Full Folder Path
            string FolderFullPath = Path.Combine(FileDirectory, MainFolder, UploadDirectory);
            //Make File Name Unique
            string FileName = string.Concat(Guid.NewGuid().ToString().Replace("-", string.Empty), File.FileName);
            //Get File Total Path on System
            string filePath = Path.Combine(FolderFullPath, FileName);
            //Get File Extension
            string Extension = Path.GetExtension(File.FileName).Remove(0, 1);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            File.CopyTo(fileStream);
            return (FileName, Extension);
        }
    }
}
