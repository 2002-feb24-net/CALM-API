using Xunit;
using Calm.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Calm.Dtb;
using Moq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.Linq.Expressions;
using Calm.Dtb.Models;
using Calm.Lib.Items;
using System.Linq;

namespace Calm.Lib.Tests
{
    public class GetTests
    {
        private Mock<IOutput> MOutput;
        private Get Ctrl;

        public GetTests()
        {
            MOutput = new Mock<IOutput>();
            Ctrl = new Get(MOutput.Object);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true, 401)]
        public async void LoginTest(bool badLogin, int expected = 0)
        {
            var user = new User() { MapDataId = 4 };
            var map = new Mapdata() { city = "" };
            int code = 0;

            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(badLogin ? null : user);
            MOutput
                .Setup(x => x.Get<Mapdata>(4))
                .ReturnsAsync(map);
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync(new AdminInfo());

            try
            {
                var item = await Ctrl.Login("", "");

                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.Get<Mapdata>(4),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                    Times.Once);
            }
            catch(Exception ex)
            {
                code = int.Parse(ex.Message);
            }

            Assert.Equal(code, expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async void UserListTest(int length)
        {
            var users = new List<User>();
            for (int i = 0; i < length; i++)
            {
                users.Add(new User());
            }

            MOutput
                .Setup(x => x.Get<User>())
                .ReturnsAsync(users);
            MOutput
                .Setup(x => x.Get<Mapdata>(It.IsAny<int>()))
                .ReturnsAsync(new Mapdata() { city = "" });
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync(new AdminInfo());

            var item = await Ctrl.UserList();

            MOutput
                .Verify(x => x.Get<User>(),
                Times.Once);
            MOutput
                .Verify(x => x.Get<Mapdata>(It.IsAny<int>()),
                Times.Exactly(length));
            MOutput
                .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                Times.Exactly(length));
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(5,5)]
        [InlineData(100,100)]
        [InlineData(50, 100)]
        [InlineData(100, 50)]
        public async void ListGatheringsTest(int length, int width)
        {
            var gatherings = new List<Gathering>();
            for (int i = 0; i < length; i++)
            {
                gatherings.Add(new Gathering());
            }
            var links = new List<Link>();
            for (int i = 0; i < width; i++)
            {
                links.Add(new Link());
            }
            
            MOutput
                .Setup(x => x.Get<Gathering>())
                .ReturnsAsync(gatherings);
            MOutput
                .Setup(x => x.Get<User>(It.IsAny<int>()))
                .ReturnsAsync(new User());
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<Link, bool>>()))
                .ReturnsAsync(links);
            MOutput
                .Setup(x => x.Get<Mapdata>(It.IsAny<int>()))
                .ReturnsAsync(new Mapdata());
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync((AdminInfo)null);

            var result = await Ctrl.ListGatherings();

            Assert.Equal(length, result.Count());
            foreach (var item in result)
            {
                Assert.Equal(item.atendees.Count(), width);
            }

            MOutput
                .Verify(x => x.Get<Gathering>(),
                Times.Once);
            MOutput
                .Verify(x => x.Get<User>(It.IsAny<int>()),
                Times.Exactly(length * (width + 1)));
            MOutput
                .Verify(x => x.GetFilter(It.IsAny<Func<Link, bool>>()),
                Times.Exactly(length));
            MOutput
                .Verify(x => x.Get<Mapdata>(It.IsAny<int>()),
                Times.Exactly(length * (width + 2)));
            MOutput
                .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                Times.Exactly(length * (width + 1)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        public async void CityListTest(int length)
        {
            var maps = new List<Mapdata>();
            for (int i = 0; i < length; i++)
            {
                maps.Add(new Mapdata());
            }
            
            MOutput
                .Setup(x => x.Get<Mapdata>())
                .ReturnsAsync(maps);

            var result = await Ctrl.CityList();

            Assert.Equal(result.Count(), length);

            MOutput
                .Verify(x => x.Get<Mapdata>(),
                Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(5,404,false)]
        public async void CityListUsersTest(int length, int expected = 0, bool mapFound = true)
        {
            int code = 0;
            var data = new List<User>();
            for (int i = 0; i < length; i++)
            {
                data.Add(new User());
            }

            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<Mapdata, bool>>>()))
                .ReturnsAsync(mapFound ? new Mapdata() : null );
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(data);
            MOutput
                .Setup(x => x.Get<Mapdata>(It.IsAny<int>()))
                .ReturnsAsync(new Mapdata());
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync((AdminInfo)null);

            try
            {
                var result = await Ctrl.CityListUsers("");

                Assert.Equal(result.Count(), length);

                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<Mapdata, bool>>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFilter(It.IsAny<Func<User, bool>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.Get<Mapdata>(It.IsAny<int>()),
                    Times.Exactly(length));
                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                    Times.Exactly(length));
            }
            catch (Exception ex)
            {
                code = int.Parse(ex.Message);
            }

            Assert.Equal(expected, code);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        [InlineData(100, 100)]
        [InlineData(100, 50)]
        [InlineData(50, 100)]
        [InlineData(5, 5, 404, false)]
        public async void CityListGatheringsTest(int length, int width, int expected = 0, bool mapFound = true)
        {
            int code = 0;
            var data = new List<Gathering>();
            for (int i = 0; i < length; i++)
            {
                data.Add(new Gathering());
            }
            var links = new List<Link>();
            for (int i = 0; i < width; i++)
            {
                links.Add(new Link());
            }

            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<Mapdata, bool>>>()))
                .ReturnsAsync(mapFound ? new Mapdata() : null);
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<Gathering, bool>>()))
                .ReturnsAsync(data);
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<Link, bool>>()))
                .ReturnsAsync(links);
            MOutput
                .Setup(x => x.Get<User>(It.IsAny<int>()))
                .ReturnsAsync(new User());
            MOutput
                .Setup(x => x.Get<Mapdata>(It.IsAny<int>()))
                .ReturnsAsync(new Mapdata());
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync((AdminInfo)null);

            try
            {
                var result = await Ctrl.CityListGatherings("");

                Assert.Equal(result.Count(), length);

                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<Mapdata, bool>>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFilter(It.IsAny<Func<Gathering, bool>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFilter(It.IsAny<Func<Link, bool>>()),
                    Times.Exactly(length));
                MOutput
                    .Verify(x => x.Get<User>(It.IsAny<int>()),
                    Times.Exactly(length * (width + 1)));
                MOutput
                    .Verify(x => x.Get<Mapdata>(It.IsAny<int>()),
                    Times.Exactly(length * (width + 2)));
                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                    Times.Exactly(length * (width + 1)));
            }
            catch (Exception ex)
            {
                code = int.Parse(ex.Message);
            }

            Assert.Equal(expected, code);
        }
    }
}