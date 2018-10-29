using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.UserProfileService
{
    public interface IUserProfileService
    {
        IEnumerable<UserProfile> Profiles { get; }
        UserProfile Edit(UserProfile entry);
        UserProfile Edit(UserProfile entry,string userId);

        IEnumerable<UserProfile> Get(Func<UserProfile, bool> predicate);
        UserProfile Get(int id);        
        UserProfile Get(string userId);
        bool Remove(int id);
        bool UploadAvatar(string path,AppUser user);
    }
}
