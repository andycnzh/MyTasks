using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MyTask
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {

        //IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication();

        // IsoloatedStroage file 
        //string fileName = @"MyTasks.xml"; 

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        // Data context for the local database
        private TaskDataContext taskDB;

        // Define an observable collection property that controls can bind to
        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != null)
                {
                    _tasks = value;
                    NotifyPropertyChanged("Tasks");
                }
            }
        }


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            taskDB = new TaskDataContext(TaskDataContext.DBConnectionString);
            this.DataContext = this;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the tasks
            var tasksInDB = from Task task in taskDB.Tasks
                            select task;

            // Execute the query and place the results into a collection
            Tasks = new ObservableCollection<Task>(tasksInDB);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            taskDB.SubmitChanges();
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //lsbTaskList.ItemsSource = GetTasks();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask(txtAddTask.Text.Trim());

            //lsbTaskList.ItemsSource = GetTasks();

            MessageBox.Show("Added task");
        }

        //// Retrieve the tasks XDocument
        //private XDocument RetrieveTasks()
        //{

        //    XDocument taskXML = new XDocument();

        //    if (appStorage.FileExists(fileName))
        //    {
        //        using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.OpenOrCreate))
        //        {
        //            using (StreamReader sr = new StreamReader(appStream))
        //            {
        //                //taskXML = XDocument.Load(sr.ReadToEnd());
        //                taskXML = XDocument.Parse(sr.ReadToEnd());
        //            }
        //        }

        //    }

        //    return taskXML;
        //}

        //// Save the XDocument to IsoloatedStorageFile.
        //private void SaveTasks(XDocument taskXML)
        //{
        //    if (appStorage.FileExists(fileName))
        //    {
        //        using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.Append))
        //        {
        //            using (StreamWriter sw = new StreamWriter(appStream))
        //            {
        //                taskXML.Save(sw);
        //            }
        //        }
        //    }
        //}

        //private void ReadTask()
        //{
        //    XDocument taskXML = XDocument.Load(fileName);

        //    var tasks = from query in taskXML.Descendants("Task")
        //                select new Task
        //                {
        //                    ID = query.Element("ID").Value,
        //                    Title = query.Element("Title").Value,
        //                    HasDone = Convert.ToBoolean(query.Element("HasDone").Value)
        //                };

        //    lsbTaskList.ItemsSource = tasks;
        //}

        private void ckbTaskList_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox checkedItem = (CheckBox)sender;

            //if (checkedItem == null || checkedItem.Tag == null)
            //{
            //    return;
            //}

            //// Retrieve the XDocument object
            //XDocument taskXML = RetrieveTasks();

            //string ID = checkedItem.Tag.ToString();

            //var task = from _task in taskXML.Descendants("Task")
            //           where (_task.Element("ID").Value == ID)
            //           select _task;

            //foreach (var el in task)
            //{
            //    el.Element("HasDone").Value = Convert.ToBoolean(checkedItem.IsChecked).ToString();
            //}

            //SaveTasks(taskXML);

            //ReadTask();
        }

        #region DataBase
        //private IList<Task> GetTasks()
        //{
        //    IList<Task> taskList = null;

        //    using (TaskDataContext context = new TaskDataContext(connectionString))
        //    {
        //        IQueryable<Task> query = from c in context.Tasks
        //                                 select c;
        //        taskList = query.ToList();
        //    }

        //    return taskList;
        //}


        private void AddTask(string title)
        {
            Task newTask = new Task();

            newTask.Title = title;
            newTask.DueDate = "";
            newTask.Comment = "";
            newTask.Type = "";
            newTask.IsComplete = false;

            Tasks.Add(newTask);

            taskDB.Tasks.InsertOnSubmit(newTask);
        }


        //private void UpdateTask(string id, string title)
        //{
        //    using (TaskDataContext context = new TaskDataContext(connectionString))
        //    {
        //        // find a task to update
        //        IQueryable<Task> taskQuery = from c in context.Tasks
        //                                     where c.ID == Convert.ToInt32(id)
        //                                     select c;
        //        Task taskToUpdate = taskQuery.FirstOrDefault();

        //        taskToUpdate.Title = title;

        //        context.SubmitChanges();
        //    }
        //}

        //private void DeleteTask(string id)
        //{
        //    using (TaskDataContext context = new TaskDataContext(connectionString))
        //    {
        //        IQueryable<Task> taskQuery = from c in context.Tasks
        //                                     where c.ID == Convert.ToInt32(id)
        //                                     select c;
        //        Task taskToDelete = taskQuery.FirstOrDefault();

        //        context.Tasks.DeleteOnSubmit(taskToDelete);

        //        context.SubmitChanges();
        //    }
        //}
        #endregion
    }
}