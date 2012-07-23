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
using System.ComponentModel;

namespace MyTask
{
    // Define the task database table
    [Table]
    public class Task
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string Title { get; set; }

        [Column]
        public string DueDate { get; set; }

        [Column]
        public string Comment { get; set; }

        [Column]
        public string Type { get; set; }

        [Column]
        public bool HasDone { get; set; }

        //    #region INotifyPropertyChanged Members

        //    public event PropertyChangedEventHandler PropertyChanged;

        //    // Used to notify the page that a data context property changed
        //    private void NotifyPropertyChanged(string propertyName)
        //    {
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //        }
        //    }

        //    #endregion

        //    #region INotifyPropertyChanging Members

        //    public event PropertyChangingEventHandler PropertyChanging;

        //    // Used to notify the data context that a data context property is about to change
        //    private void NotifyPropertyChanging(string propertyName)
        //    {
        //        if (PropertyChanging != null)
        //        {
        //            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        //        }
        //    }

        //    #endregion
    }
}
