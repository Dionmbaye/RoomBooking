using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Dal;
using RoomBooking.Dal.Repositories;

namespace RoomBooking.Test.Dal
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserRepository? _userRepository;
        private DbContextOptions<KataHotelContext>? _options;

        [TestMethod]
        public async Task Should_Get_Users()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_users")
                .Options;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUserEntity = new UserEntity
                {
                    Id = 1,
                    FirstName = "Test 1",
                    LastName =  "Test 2"
                };
                ctx.Users.Add(fakeUserEntity);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _userRepository = new UserRepository(ctx);
                var users = await _userRepository.GetUsersAsync();
         
                Assert.IsNotNull(users);
                Assert.AreEqual(users.Count(), 1);
            }
        }
    }
}
