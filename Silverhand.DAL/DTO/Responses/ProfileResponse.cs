using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.DTO.Responses
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        
      
       

        // Basic Info
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }

        // Preferences
        public ProfileLanguage LanguagePreference { get; set; } = ProfileLanguage.English;
        public MaturityRating MaturityRating { get; set; } = MaturityRating.Mature;

        public ProfileTheme Theme { get; set; } = ProfileTheme.Dark;



        // Toggles
        //   public bool AutoplayNextEpisode { get; set; } = true;
        //   public bool AutoplayPreviews { get; set; } = true;
        //  public bool ContinueWatchingEnabled { get; set; } = true;

        // Kids hard mode
        // public bool AllowKidsContentOnly { get; set; } = false;

        // Tracking
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUsedAt { get; set; }

    }
}
