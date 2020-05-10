using Xunit;
using Calm.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Calm.Dtb;
using System.Linq.Expressions;
using Calm.Dtb.Models;
using System.Threading.Tasks;

namespace Calm.Lib.Tests
{
    public class DeleteTests
    {
        private Mock<IOutput> MOutput;
        private Mock<IInput> MInput;
        private Delete Ctrl;

        public DeleteTests()
        {
            MOutput = new Mock<IOutput>();
            MInput = new Mock<IInput>();
            Ctrl = new Delete(MOutput.Object, MInput.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(0, 401, true)]
        public async void RemoveUserTest(int links, int expected = 0, bool badLogin = false)
        {
            var user = new User();
            var linkList = new List<Link>();
            for (int i = 0; i < links; i++)
            {
                linkList.Add(new Link());
            }
            int code = 0;

            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(badLogin ? null : user);
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<Link, bool>>()))
                .ReturnsAsync(linkList);
            MInput
                .Setup(x => x.Remove(It.IsAny<User>()))
                .Returns(Task.CompletedTask);
            MInput
                .Setup(x => x.Remove(It.IsAny<Link>()))
                .Returns(Task.CompletedTask);

            try
            {
                await Ctrl.RemoveUser("", "");

                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()),
                    Times.Once());
                MOutput
                    .Verify(x => x.GetFilter(It.IsAny<Func<Link, bool>>()),
                    Times.Once());
                MInput
                    .Verify(x => x.Remove(It.IsAny<User>()),
                    Times.Once);
                MInput
                    .Verify(x => x.Remove(It.IsAny<Link>()),
                    Times.Exactly(links));
            }
            catch(Exception ex)
            {
                code = int.Parse(ex.Message);
                MInput
                    .Verify(x => x.Remove(It.IsAny<User>()),
                    Times.Never);
                MInput
                    .Verify(x => x.Remove(It.IsAny<Link>()),
                    Times.Never);
            }

            Assert.Equal(code, expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(0, 404, false)]
        [InlineData(0, 403, true, false)]
        [InlineData(0, 0, true, false, true)]
        [InlineData(0, 401, true, false, true, true)]
        public async void RemoveGatheringTest
            (
            int links,
            int expected = 0,
            bool foundGathering = true,
            bool isOrganiser = true,
            bool isSuperAdmin = false,
            bool badLogin = false
            )
        {
            int code = 0;
            var user = new User() { Id = 4 };
            var gathering = new Gathering();
            if (isOrganiser) gathering.organizerId = 4;
            var admins = new List<AdminInfo>();
            for (int i = 0; i < 20; i++)
            {
                admins.Add(new AdminInfo() { userId = 3 });
            }
            if (isSuperAdmin) admins.Add(new AdminInfo()
            { userId = 4, SuperAdmin = true });
            var linkList = new List<Link>();
            for (int i = 0; i < links; i++)
            {
                linkList.Add(new Link());
            }

            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(badLogin ? null : user);
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<Gathering, bool>>>()))
                .ReturnsAsync(foundGathering ? gathering : null);
            MOutput
                .Setup(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()))
                .ReturnsAsync(isSuperAdmin ? admins[20] : null);
            MOutput
                .Setup(x => x.GetFilter(It.IsAny<Func<Link, bool>>()))
                .ReturnsAsync(linkList);
            MInput.Setup(x => x.Remove(It.IsAny<Link>()));
            MInput.Setup(x => x.Remove(gathering));

            try
            {
                await Ctrl.RemoveGathering("", "", "");

                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<User, bool>>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<Gathering, bool>>>()),
                    Times.Once);
                MOutput
                    .Verify(x => x.GetFind(It.IsAny<Expression<Func<AdminInfo, bool>>>()),
                    isOrganiser ? Times.Never() : Times.Once() );
                MOutput
                    .Verify(x => x.GetFilter(It.IsAny<Func<Link, bool>>()),
                    Times.Once);
                MInput.Verify(x => x.Remove(It.IsAny<Link>()), Times.Exactly(links));
                MInput.Verify(x => x.Remove(gathering), Times.Once);
            }
            catch (Exception ex)
            {
                code = int.Parse(ex.Message);
                MInput.Verify(x => x.Remove(It.IsAny<Link>()), Times.Never);
                MInput.Verify(x => x.Remove(gathering), Times.Never);
            }

            Assert.Equal(code, expected);
        }
    }
}