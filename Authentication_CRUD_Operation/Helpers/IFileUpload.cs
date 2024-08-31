using Authentication_CRUD_Operation.enums;

namespace Authentication_CRUD_Operation.Helpers
{
    public interface IFileUpload
    {
        public (string, string) UploadFile(IFormFile formFilestring, UploadDirectoriesEnum FileFor);
    }
}
