using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmLightWP8.Models;

namespace MvvmLightWP8.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Friend>> GetFriends();
        
        Task<string> Save(Friend updatedFriend);
    }
}
