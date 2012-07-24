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
    public class Task : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _id;
        /*
         * Column - 将属性标记为数据表中的一个字段
         *     IsPrimaryKey - 是否是主键
         *     IsDbGenerated - 数据是否由数据库自动生成，如自增列
         *     DbType = "INT NOT NULL Identity" - int类型，不能为null，标识列
         *     CanBeNull - 是否可以为 null
         *     AutoSync = AutoSync.OnInsert - 标记此值的作用是：当数据添加完成后，此属性的值会自动同步为数据库自增后的值
         */
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("ID");
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _title;
        [Column(CanBeNull = false)]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    NotifyPropertyChanging("Title");
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _dueDate;
        [Column]
        public string DueDate
        {
            get { return _dueDate; }
            set
            {
                if (_dueDate != value)
                {
                    NotifyPropertyChanging("DueDate");
                    _dueDate = value;
                    NotifyPropertyChanged("DueDate");
                }
            }
        }

        private string _comment;
        [Column]
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    NotifyPropertyChanging("Comment");
                    _comment = value;
                    NotifyPropertyChanged("Comment");
                }
            }
        }

        private bool _isComplete;
        [Column]
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }

        private string _type;
        [Column]
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    NotifyPropertyChanging("Type");
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private Binary _version;
        [Column(IsVersion = true)]
        public Binary Version
        {
            get { return _version; }
            set
            {
                _version = value;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion

        //   

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
