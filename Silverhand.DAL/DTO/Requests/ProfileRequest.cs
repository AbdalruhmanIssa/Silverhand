using Microsoft.AspNetCore.Http;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Requests
{
    public class ProfileRequest
    {

        // Basic Info
        public string Name { get; set; }
        public IFormFile? AvatarUrl { get; set; }

        // Preferences
        public ProfileLanguage LanguagePreference { get; set; }
        public MaturityRating MaturityRating { get; set; } 

        public ProfileTheme Theme { get; set; } = ProfileTheme.Dark;


    }
}
