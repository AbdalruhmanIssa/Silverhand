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
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        public ProfileService(
            IProfileRepository profileRepository,
            IUserRepository userRepository,
            IFileService fileService)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        // ----------------------------------------------------
        // CREATE PROFILE
        // ----------------------------------------------------
        public async Task<ProfileResponse> CreateProfileAsync(Guid userId, ProfileRequest request)
        {
            // Validate user exists
            bool userExists = await _userRepository.UserExistsAsync(userId);


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
        // UPDATE ---------------------------------------
        public async Task<ProfileResponse?> UpdateAsync(Guid profileId, ProfileRequest request, Guid userId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            if (profile == null)
                throw new Exception("Profile not found.");

            // Security: Allow update ONLY if profile belongs to user
            if (profile.UserId != userId)
                throw new Exception("You are not allowed to edit this profile.");

            // Update fields
            profile.Name = request.Name;
            profile.LanguagePreference = request.LanguagePreference;
            profile.MaturityRating = request.MaturityRating;

            // Handle avatar update
            if (request.AvatarUrl != null)
            {
                var filePath = await _fileService.UploadAsync(request.AvatarUrl);
                profile.AvatarUrl = filePath;
            }

            var updated = await _profileRepository.UpdateAsync(profile);
            return updated.Adapt<ProfileResponse>();
        }

        // DELETE ---------------------------------------
        public async Task<bool> DeleteAsync(Guid profileId, Guid userId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            if (profile == null)
                return false;

            // Security: Only owner can delete
            if (profile.UserId != userId)
                throw new Exception("You are not allowed to delete this profile.");

            return await _profileRepository.DeleteAsync(profileId);
        }
    }
}
