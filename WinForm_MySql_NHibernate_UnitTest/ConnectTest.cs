using WinForm_MySql_NHibernate;
using NUnit.Framework;

namespace WinForm_MySql_NHibernate_UnitTest
{
    [TestFixture]
    public class ConnectTest
    {
        [Test]
        public void isConnect_Connected_ResultTrue()
        {
            NHibernateConnect connect =  new NHibernateConnect();

            bool result = (connect.session != null);

            Assert.True(result);
        }

        [Test]
        public void isConnect_NotConnected_ResultFalse()
        {
            NHibernateConnect connect = new NHibernateConnect();

            bool result = (connect.session == null);

            Assert.False(result);
        }
    }
}