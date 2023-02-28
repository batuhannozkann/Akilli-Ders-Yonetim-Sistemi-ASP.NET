using Adys.Core.DTOs;
using Adys.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Core.Services
{
    public interface ILessonFileService:IService<LessonFile>
    {
        Task<CustomNoResponseDto> AddFileAsync(AddLessonFileDto addLessonFileDto);
        CustomResponseDto<LessonFileDto> DeleteFile(DeleteFileDto deleteFileDto);
    }
}
