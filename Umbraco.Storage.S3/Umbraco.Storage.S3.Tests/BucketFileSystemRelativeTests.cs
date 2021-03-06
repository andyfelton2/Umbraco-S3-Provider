﻿using Amazon.S3;
using Moq;
using NUnit.Framework;
using Umbraco.Storage.S3.Services;

namespace Umbraco.Storage.S3.Tests
{
    [TestFixture]
    public class BucketFileSystemRelativeTests
    {
        private BucketFileSystem CreateProvider(Mock<IAmazonS3> mock)
        {
            var logHelperMock = new Mock<ILogHelper>();
            var mimeTypeHelper = new Mock<IMimeTypeResolver>();
            return new BucketFileSystem("test", string.Empty, "media", string.Empty) {
                ClientFactory = () => mock.Object,
                LogHelper = logHelperMock.Object,
                MimeTypeResolver = mimeTypeHelper.Object
            };
        }

        [Test]
        public void ResolveFullPath()
        {
            //Arrange
            var provider = CreateProvider(null);

            //Act
            var actual = provider.GetFullPath("1001/media.jpg");

            //Assert
            Assert.AreEqual("1001/media.jpg", actual);
        }

        [Test]
        public void ResolveUrlPath()
        {
            //Arrange
            var provider = CreateProvider(null);

            //Act
            var actual = provider.GetUrl("1001/media.jpg");

            //Assert
            Assert.AreEqual("/media/1001/media.jpg", actual);
        }

        [Test]
        public void ResolveRelativePath()
        {
            //Arrange
            var provider = CreateProvider(null);

            //Act
            var actual = provider.GetRelativePath("1001/media.jpg");

            //Assert
            Assert.AreEqual("1001/media.jpg", actual);
        }

        [Test]
        public void ResolveRelativePathPrefixed()
        {
            //Arrange
            var provider = CreateProvider(null);

            //Act
            var actual = provider.GetRelativePath("/media/1001/media.jpg");

            //Assert
            Assert.AreEqual("1001/media.jpg", actual);
        }
    }
}
