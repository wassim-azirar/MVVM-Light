using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmLightWP8.Models;
using MvvmLightWP8.Services;

namespace MvvmLightWP8.Design
{
    public class DesignFriendsService : IDataService
    {
        public Task<IEnumerable<Friend>> GetFriends()
        {
            var result = new List<Friend>();

            for (var index = 0; index < 10; index++)
            {
                result.Add(
                    new Friend
                    {
                        FirstName = "FirstName" + index,
                        LastName = "LastName" + index,
                        Id = index,
                        PictureUri = new Uri("http://www.galasoft.ch/logo/LogoHead128.png")
                    });
            }

            return Task.FromResult((IEnumerable<Friend>)result);
        }

        public Task<string> Save(Friend updatedFriend)
        {
            throw new NotImplementedException();
        }
    }
}
