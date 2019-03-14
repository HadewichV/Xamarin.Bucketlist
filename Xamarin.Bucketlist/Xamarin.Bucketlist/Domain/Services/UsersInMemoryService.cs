using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Bucketlist.Domain.Models;

namespace Xamarin.Bucketlist.Domain.Services
{
    public class UsersInMemoryService
    {
        private static List<User> users = new List<User>
        {
            new User
            {
                Id = Guid.Empty,
                UserName = "Uthred",
                Email="uthred@bucketlists.test"
            }
        };

        public async Task<User> GetUserById(Guid id)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task SaveUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            oldUser = user;
        }
    }
}
