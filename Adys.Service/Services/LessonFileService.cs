using Adys.Core.DTOs;
using Adys.Core.Entities;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adys.Service.Services
{
    public class LessonFileService : Service<LessonFile>, ILessonFileService
    {
        private readonly ILessonFileRepository _lessonFileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LessonFileService(IGenericRepository<LessonFile> genericRepository, IUnitOfWork unitOfWork, ILessonFileRepository lessonFileRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _lessonFileRepository = lessonFileRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomNoResponseDto> AddFileAsync(AddLessonFileDto addLessonFileDto)
        {
            if (addLessonFileDto.FileName == null) return CustomNoResponseDto.Fail(400, "File is required");
            var file = _lessonFileRepository.Where(x => x.FileName == addLessonFileDto.FileName && x.LessonId == addLessonFileDto.LessonId).FirstOrDefault();
            if (file != null) return CustomNoResponseDto.Fail(400, "File is exist");
            await _lessonFileRepository.AddFileAsync(Convert.ToInt32(addLessonFileDto.LessonId), addLessonFileDto.FileName, addLessonFileDto.FileUrl);
            await _unitOfWork.CommitAsync();
            return CustomNoResponseDto.Succes(200);
        }
        public CustomResponseDto<LessonFileDto> DeleteFile(DeleteFileDto deleteFileDto)
        {
            var file = _lessonFileRepository.Where(c => c.Id == deleteFileDto.Id).FirstOrDefault();
            LessonFileDto deletedFileDto =_mapper.Map<LessonFileDto>(file);
            if (file == null) return CustomResponseDto<LessonFileDto>.Fail(400, "File doesn't exist");
            _lessonFileRepository.Remove(file);
            _unitOfWork.Commit();
            return CustomResponseDto<LessonFileDto>.Succes(200, deletedFileDto);

        }
            
        


    }
}
