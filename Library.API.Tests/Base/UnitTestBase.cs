using Moq;
using Moq.AutoMock;

namespace Library.API.Tests.Base
{
    public class UnitTestBase<TSut> where TSut : class
    {
        public UnitTestBase()
        {
            Mocker = new AutoMocker(Moq.MockBehavior.Default, Moq.DefaultValue.Mock);
            Sut = Mocker.CreateInstance<TSut>();
        }

        public AutoMocker Mocker { get; }

        public TSut Sut { get; set; }
    }

    public class UnitTestSelfMockBase<TSut> where TSut : class
    {
        public UnitTestSelfMockBase()
        {
            Mocker = new AutoMocker(Moq.MockBehavior.Default, Moq.DefaultValue.Mock);
            Sut = Mocker.CreateSelfMock<TSut>();
            SutMock = Mock.Get(Sut);
        }

        public AutoMocker Mocker { get; }

        public TSut Sut { get; set; }

        public Mock<TSut> SutMock { get; }
    }
}
