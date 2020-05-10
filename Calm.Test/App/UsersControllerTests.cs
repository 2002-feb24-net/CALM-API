using Xunit;
using Calm.App.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Calm.Lib;
using Microsoft.AspNetCore.Http.Connections;
using Calm.Test;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calm.App.Controllers.Tests
{
    public class UsersControllerTests
    {
        public UsersControllerTests()
        {
            MDelete = new Mock<IDelete>();
            MPost = new Mock<IPost>();
            MPut = new Mock<IPut>();
            MGet = new Mock<IGet>();
            Ctrl = new UsersController(MGet.Object, MPost.Object, MPut.Object, MDelete.Object);
        }

        private Mock<IPost> MPost;
        private Mock<IPut> MPut;
        private Mock<IGet> MGet;
        private Mock<IDelete> MDelete;
        private UsersController Ctrl;

        [Fact]
        public async void UserListTest()
        {
            var users = new List<UserItem>() { Logic.BlankUserItem(), Logic.BlankUserItem() };
            MGet.Setup(x => x.UserList()).ReturnsAsync(users);

            var result = await Ctrl.UserList();

            var action = result.Result as OkObjectResult;
            var item = action.Value as List<UserItem>;
            Assert.True(Logic.CompareList(users, item, Logic.UserItemEquality));
            MGet.Verify(x => x.UserList(), Times.Exactly(1));
        }

        [Fact]
        public async void LoginTest()
        {
            var user = Logic.BlankUserItem();
            MGet.Setup(x => x.Login("", "")).ReturnsAsync(user);

            var result = await Ctrl.Login("", "");

            var action = result.Result as OkObjectResult;
            var item = action.Value as UserItem;
            Assert.True(Logic.UserItemEquality(user, item));
            MGet.Verify(x => x.Login("", ""), Times.Exactly(1));
        }

        [Fact]
        public async void PostUserTest()
        {
            var user = Logic.BlankUserItem();
            MPost.Setup(x => x.User(user)).ReturnsAsync(user);

            var result = await Ctrl.PostUser(user);

            var action = result.Result as OkObjectResult;
            var item = action.Value as UserItem;
            Assert.True(Logic.UserItemEquality(user, item));
            MPost.Verify(x => x.User(user), Times.Exactly(1));
        }

        [Fact]
        public async void SetUserTest()
        {
            var user = Logic.BlankUserItem();
            MPut.Setup(x => x.SetUser("", "", user)).Returns(Task.CompletedTask);

            var result = await Ctrl.SetUser("", "", user);

            var action = result as NoContentResult;
            Assert.True(action != null);
            MPut.Verify(x => x.SetUser("", "", user), Times.Exactly(1));
        }

        [Fact]
        public async void RemoveTest()
        {
            MDelete.Setup(x => x.RemoveUser("", "")).Returns(Task.CompletedTask);

            var result = await Ctrl.Remove("", "");

            var action = result as NoContentResult;
            Assert.True(action != null);
            MDelete.Verify(x => x.RemoveUser("", ""), Times.Exactly(1));
        }
    }
}