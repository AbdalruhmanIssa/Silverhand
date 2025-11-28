using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        // User Relationship
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

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
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();


    }
    public enum ProfileLanguage
    {
        English = 0,
        Arabic = 1,
        French = 2,
        Spanish = 3,
        German = 4,
        Turkish = 5
    }
    public enum MaturityRating
    {
        Kids = 0,   // Only safe content
        Teen = 1,   // PG-13
        Mature = 2  // 18+
    }
    public enum ProfileTheme
    {
        Light = 0,
        Dark = 1
    }
}
