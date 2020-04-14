using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;

namespace WinForm_MySql_NHibernate
{
    public class NHibernateConnect
    {
        public ISession session;

        public NHibernateConnect()
        {
            Configuration config = new Configuration();
            //String dir = System.Environment.CurrentDirectory;
            //MessageBox.Show(dir);
            config.Configure(@"C:\Users\JeongJin\Desktop\winform_mysql_nhibernate-master\WinForm_MySql_NHibernate_UnitTest\Mapping\Student.cfg.xml");
            ISessionFactory factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
    }
}