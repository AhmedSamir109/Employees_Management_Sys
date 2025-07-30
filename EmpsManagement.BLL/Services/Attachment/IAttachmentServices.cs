using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.Services.Attachment
{
    public interface IAttachmentServices
    {
        string? Upload(IFormFile file, string folderName);   // return the file name that is saved on the Db --> and take the file that is uploaded and save it in the server
        bool Delete(string fileName , string foldeName);
    }
}
