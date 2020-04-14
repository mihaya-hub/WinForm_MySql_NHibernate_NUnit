using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NHibernate;
using NHibernate.Cfg;
using WinForm_MySql_NHibernate;

namespace WinForm_MySql_NHibernate
{
    public partial class Form1 : Form
    {
        NHibernateConnect connect;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connect = new NHibernateConnect();
            }
            catch (Exception ex)
            {
                if (connect.session != null) connect.session.Close();
                MessageBox.Show(ex.Message, "NHibernate Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Read_Click(object sender, EventArgs e)
        {
            ICriteria criteria = connect.session.CreateCriteria(typeof(Student));
            List<Student> list = new List<Student>();

            criteria.List(list);

            dataGridView1.Rows.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                object[] arr = { list[i].grade, list[i].cclass, list[i].no, list[i].name, list[i].score };
                dataGridView1.Rows.Add(arr);
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            using (connect.session.BeginTransaction())
            {
                Student student = GetRowStudent();

                connect.session.Save(student);
                connect.session.Flush();
                connect.session.Transaction.Commit();

                Read.PerformClick();
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            using (connect.session.BeginTransaction())
            {
                Student student = GetRowStudent();

                connect.session.Merge(student);
                connect.session.Flush();
                connect.session.Transaction.Commit();

                Read.PerformClick();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            using (connect.session.BeginTransaction())
            {
                Student student = connect.session.Get<Student>((int)dataGridView1.SelectedCells[0].OwningRow.Cells["no"].Value);

                connect.session.Delete(student);
                connect.session.Flush();
                connect.session.Transaction.Commit();

                Read.PerformClick();
            }
        }

        private Student GetRowStudent()
        {
            Student std = new Student
            {
                grade = System.Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells["grade"].Value),
                cclass = System.Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells["cclass"].Value),
                no = System.Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells["no"].Value),
                name = (string)dataGridView1.SelectedCells[0].OwningRow.Cells["name"].Value,
                score = (string)dataGridView1.SelectedCells[0].OwningRow.Cells["score"].Value
            };

            return std;
        }
    }
}
