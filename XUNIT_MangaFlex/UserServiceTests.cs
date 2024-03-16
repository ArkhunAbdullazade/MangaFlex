using Moq;
using MangaFlex.Core.Data.Users.Models;
using Microsoft.AspNetCore.Identity;
using MangaFlex.Infrastructure.Data.User.Services;
using Microsoft.AspNetCore.Http;

namespace XUNIT_MangaFlex
{
    public class UserServiceTests
    {
        private UserService userService;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<SignInManager<User>> signInManagerMock;
        private Mock<IRoleStore<IdentityRole>> roleStoreMock;
        private RoleManager<IdentityRole> roleManager;
        public UserServiceTests()
        {
            userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            signInManagerMock = new Mock<SignInManager<User>>(
               userManagerMock.Object,
               Mock.Of<IHttpContextAccessor>(),
               Mock.Of<IUserClaimsPrincipalFactory<User>>(),
               null, null, null, null);

            roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleManager = new RoleManager<IdentityRole>(roleStoreMock.Object, null, null, null, null);
            userService = new UserService(userManagerMock.Object, signInManagerMock.Object, roleManager);
        }

        [Fact]
        public async Task Login_NullUser_ThrowsNullReferenceException()
        {
            string userName = null;
            string password = null;

            userManagerMock.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NullReferenceException>(() => userService.LoginAsync(userName, password));
        }

        [Fact]
        public async Task Signup_Valid_ReturnsSuccess()
        {
            var user = new User { UserName = "newUser", Email = "newEmail@example.com" };
            string password = "password";

            var identityResult = IdentityResult.Success;

            // Мокируем UserManager для возврата успешного результата при создании пользователя
            userManagerMock.Setup(x => x.CreateAsync(user, password)).ReturnsAsync(identityResult);

            // Вызываем метод регистрации пользователя
            await userService.SignupAsync(user, password);

            // Проверяем, что метод создания пользователя был вызван один раз с правильными параметрами
            userManagerMock.Verify(x => x.CreateAsync(user, password), Times.Once);
            // Проверяем, что метод создания роли был вызван один раз
            roleStoreMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole>(), default(CancellationToken)), Times.Once);

            // Проверяем, что метод добавления пользователя в роль "User" был вызван один раз
            userManagerMock.Verify(x => x.AddToRoleAsync(user, "User"), Times.Once);
            Assert.NotNull(user); // Пользователь создан
        }

        public async Task Login_InvalidPassword_ReturnsArgumentException()
        {
            var user = new User { UserName = "existingUser", Email = "existingEmail@example.com" };
            string password = "incorrectPassword";

            userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
            signInManagerMock.Setup(x => x.PasswordSignInAsync(user, password, true, true))
                            .ReturnsAsync(SignInResult.Failed);

            await Assert.ThrowsAsync<ArgumentException>(() => userService.LoginAsync(user.UserName, password));
        }

    }
}
