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

namespace MyTask
{
    public partial class MainPage : PhoneApplicationPage
    {

        //IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication();

        // IsoloatedStroage file 
        //string fileName = @"MyTasks.xml";
        private const string connectionString = @"isostore:/TaskDB.sdf";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Create the database if it does not yet exist.
            using (TaskDataContext context = new TaskDataContext(connectionString))
            {
                if (context.DatabaseExists() == false)
                {
                    // Create the database.
                    context.CreateDatabase();
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            lsbTaskList.ItemsSource = GetTasks();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask(txtAddTask.Text.Trim());

            lsbTaskList.ItemsSource = GetTasks();

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
        private void AddTask(string title)
        {
            using (TaskDataContext context = new TaskDataContext(connectionString))
            {
                Task task = new Task();

                task.Title = title;
                task.DueDate = "";
                task.Comment = "";
                task.Type = "";
                task.HasDone = false;

                context.SubmitChanges();
            }
        }

        private IList<Task> GetTasks()
        {
            IList<Task> taskList = null;

            using (TaskDataContext context = new TaskDataContext(connectionString))
            {
                IQueryable<Task> query = from c in context.Tasks
                                         select c;
                taskList = query.ToList();
            }

            return taskList;
        }

        private void UpdateTask(string id, string title)
        {
            using (TaskDataContext context = new TaskDataContext(connectionString))
            {
                // find a task to update
                IQueryable<Task> taskQuery = from c in context.Tasks
                                             where c.ID == Convert.ToInt32(id)
                                             select c;
                Task taskToUpdate = taskQuery.FirstOrDefault();

                taskToUpdate.Title = title;

                context.SubmitChanges();
            }
        }

        private void DeleteTask(string id)
        {
            using (TaskDataContext context = new TaskDataContext(connectionString))
            {
                IQueryable<Task> taskQuery = from c in context.Tasks
                                             where c.ID == Convert.ToInt32(id)
                                             select c;
                Task taskToDelete = taskQuery.FirstOrDefault();

                context.Tasks.DeleteOnSubmit(taskToDelete);

                context.SubmitChanges();
            }
        }
        #endregion
    }
}