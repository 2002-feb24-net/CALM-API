using Xunit;
using System.Collections.Generic;
using Moq;
using Calm.Lib;
using Microsoft.AspNetCore.Mvc;
using Calm.Test;
using Calm.Lib.Items;

namespace Calm.App.Controllers.Tests
{
    public class MapControllerTests
    {
        public MapControllerTests()
        {
            MDelete = new Mock<IDelete>();
            MPost = new Mock<IPost>();
            MPut = new Mock<IPut>();
            MGet = new Mock<IGet>();
            Ctrl = new MapController(MGet.Object, MPost.Object, MPut.Object, MDelete.Object);
        }

        private Mock<IPost> MPost;
        private Mock<IPut> MPut;
        private Mock<IGet> MGet;
        private Mock<IDelete> MDelete;
        private MapController Ctrl;

        [Fact]
        public async void GetMapsTest()
        {
            var citys = new List<string>() { "", "" };
            MGet.Setup(x => x.CityList()).ReturnsAsync(citys);

            var result = await Ctrl.GetMaps();

            var ok = result.Result as OkObjectResult;
            var output = ok.Value as List<string>;
            Assert.True(Logic.CompareList(citys, output, (a,b)=> a==b));
            MGet.Verify(x => x.CityList(), Times.Exactly(1));
        }

        [Fact]
        public async void GetMapUsersTest()
        {
            var users = new List<UserItem>() { Logic.BlankUserItem(), Logic.BlankUserItem() };
            MGet.Setup(x => x.CityListUsers("")).ReturnsAsync(users);

            var result = await Ctrl.GetMapUsers("");

            var ok = result.Result as OkObjectResult;
            var output = ok.Value as List<UserItem>;
            Assert.True(Logic.CompareList(users, output, Logic.UserItemEquality));
            MGet.Verify(x => x.CityListUsers(""), Times.Exactly(1));
        }

        [Fact]
        public async void GetMapGatheringsTest()
        {
            var gatherings = new List<GatheringItemOut>() { Logic.BlankGatheringItemOut(), Logic.BlankGatheringItemOut() };
            MGet.Setup(x => x.CityListGatherings("")).ReturnsAsync(gatherings);

            var result = await Ctrl.GetMapGatherings("");

            var ok = result.Result as OkObjectResult;
            var output = ok.Value as List<GatheringItemOut>;
            Assert.True(Logic.CompareList(gatherings, output, Logic.GatheringItemOutEquality));
            MGet.Verify(x => x.CityListGatherings(""), Times.Exactly(1));
        }
    }
}