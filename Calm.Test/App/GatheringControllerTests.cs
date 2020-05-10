using Calm.Lib;
using Calm.Lib.Items;
using Calm.Test;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Calm.App.Controllers.Tests
{
    public class GatheringControllerTests
    {
        public GatheringControllerTests()
        {
            MDelete = new Mock<IDelete>();
            MPost = new Mock<IPost>();
            MPut = new Mock<IPut>();
            MGet = new Mock<IGet>();
            Ctrl = new GatheringController(MGet.Object, MPost.Object, MPut.Object, MDelete.Object);
        }

        private Mock<IPost> MPost;
        private Mock<IPut> MPut;
        private Mock<IGet> MGet;
        private Mock<IDelete> MDelete;
        private GatheringController Ctrl;

        [Fact]
        public async Task DeleteGatheringTestAsync()
        {
            MDelete.Setup(x => x.RemoveGathering("", "", "")).Returns(Task.CompletedTask);

            var result = await Ctrl.DeleteGathering("", "", "");

            var item = result as NoContentResult;
            Assert.True(item != null);
            MDelete.Verify(x => x.RemoveGathering("", "", ""), Times.Exactly(1));
        }

        [Fact]
        public async void ListGatheringsTest()
        {
            var outList = new List<GatheringItemOut>() { Logic.BlankGatheringItemOut() }; 
            MGet.Setup(x => x.ListGatherings()).ReturnsAsync(outList);

            var result = await Ctrl.ListGatherings();

            var item = result.Result as OkObjectResult;
            Assert.True(Logic.CompareList(item.Value as List<GatheringItemOut>, outList, Logic.GatheringItemOutEquality));
            MGet.Verify(x => x.ListGatherings(), Times.Exactly(1));
        }

        [Fact]
        public async void AddGatheringTest()
        {
            var gathering = Logic.BlankGatheringItemIn();
            MPost.Setup(x => x.AddGathering("", "", gathering)).Returns(Task.CompletedTask);

            var result = await Ctrl.AddGathering("", "", gathering);

            var item = result as NoContentResult;
            Assert.True(item != null);
            MPost.Verify(x => x.AddGathering("", "", gathering), Times.Exactly(1));
        }

        [Fact]
        public async void EnrollTest()
        {
            MPost.Setup(x => x.Enter("", "", "")).Returns(Task.CompletedTask);

            var result = await Ctrl.Enroll("", "", "");

            var item = result as NoContentResult;
            Assert.True(item != null);
            MPost.Verify(x => x.Enter("", "", ""), Times.Exactly(1));
        }
    }
}