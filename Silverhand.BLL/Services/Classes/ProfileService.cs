using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.Data;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using Silverhand.DAL.Repository.Classes;
using Silverhand.DAL.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class ProfileService:IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ProfileService(
            IProfileRepository profileRepository,
            ApplicationDbContext context,
            IFileService fileService)
        {
            _profileRepository = profileRepository;
            _context = context;
            _fileService = fileService;
        }

        // ----------------------------------------------------
        // CREATE PROFILE
        // ----------------------------------------------------
        public async Task<ProfileResponse> CreateProfileAsync(Guid userId, ProfileRequest request)
        {
            // Validate user exists
            bool userExists = await _context.Users
       .AnyAsync(u => u.Id == userId.ToString());

            if (!userExists)
                throw new Exception("User not found.");

            // Map safe fields
            var entity = request.Adapt<Profile>();
            entity.UserId = userId;
            entity.CreatedAt = DateTime.UtcNow;

            // Upload avatar
            if (request.AvatarUrl != null)
            {
                var filePath = await _fileService.UploadAsync(request.AvatarUrl);
                entity.AvatarUrl = filePath;
            }

            // Save
            await _profileRepository.AddAsync(entity);

            return entity.Adapt<ProfileResponse>();
        }

        // ----------------------------------------------------
        // GET ALL PROFILES FOR USER
        // ----------------------------------------------------
        public async Task<IEnumerable<ProfileResponse>> GetAllByUserAsync(Guid userId)
        {
            var profiles = await _profileRepository.GetAllByUserIdAsync(userId);
            return profiles.Adapt<IEnumerable<ProfileResponse>>();
        }

    }
}
