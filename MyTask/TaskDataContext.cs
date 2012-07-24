using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace MyTask
{
    public class TaskDataContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/MyTaskDB.sdf";

        // Pass the connection string to the base class.
        public TaskDataContext(string connectionString)
            : base(connectionString)
        {

        }

        // Specify a single table for the task items.
        public Table<Task> Tasks;
    }
}
