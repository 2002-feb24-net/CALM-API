using Xunit;
using Calm.App.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Calm.Lib;
using Moq;
using System.Threading.Tasks;
using Calm.Test;
using Microsoft.AspNetCore.Mvc;

namespace Calm.App.Controllers.Tests
{
    public class AdminControllerTests
    {
        public AdminControllerTests()
        {
            MDelete = new Mock<IDelete>();
            MPost = new Mock<IPost>();
            MPut = new Mock<IPut>();
            MGet = new Mock<IGet>();
            Ctrl = new AdminController(MGet.Object, MPost.Object, MPut.Object, MDelete.Object);
        }

        private Mock<IPost> MPost;
        private Mock<IPut> MPut;
        private Mock<IGet> MGet;
        private Mock<IDelete> MDelete;
        private AdminController Ctrl;

        [Fact]
        public async void PostUserTest()
        {
            var user = Logic.BlankUserItem();
            MPost.Setup(x => x.AdminUser("","",user)).ReturnsAsync(user);

            var result = await Ctrl.PostUser("", "", user);

            var item = result.Result as OkObjectResult;
            Assert.True(Logic.UserItemEquality(item.Value as UserItem, user));
            MPost.Verify(x => x.AdminUser("", "", user), Times.Exactly(1));
        }

        [Fact]
        public async void SwapUserStatusTest()
        {
            MPut.Setup(x => x.SwapUserStatus("", "", "")).Returns(Task.CompletedTask);

            var result = await Ctrl.SwapUserStatus("", "", "");

            var item = result.Result as NoContentResult;
            Assert.True(item != null);
            MPut.Verify(x => x.SwapUserStatus("", "", ""), Times.Exactly(1));
        }
    }
}