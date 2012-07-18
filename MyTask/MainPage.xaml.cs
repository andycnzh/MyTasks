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

        IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication();

        // IsoloatedStroage file 
        string fileName = "tasks.xml";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ReadTask();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            string taskMessage = "";
            taskMessage = txtAddTask.Text.Trim();

            // Write the task into the IsoloatedStroage

            XElement taskElement = new XElement("Task",
                new XElement("ID", System.Guid.NewGuid()),
                new XElement("Title", txtAddTask.Text.Trim()),
                new XElement("CreateDate", DateTime.Now.ToString()),
                new XElement("DueDate", ""),
                new XElement("Comment", ""),
                new XElement("HasDone", false)
                );

            // Retrieve the XDocument object
            XDocument taskXML = RetrieveTasks();

            taskXML.Root.Add(taskElement);

            SaveTasks(taskXML);

            ReadTask();
        }

        // Retrieve the tasks XDocument
        private XDocument RetrieveTasks()
        {

            XDocument taskXML = new XDocument();

            if (appStorage.FileExists(fileName))
            {
                using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(appStream))
                    {
                        //taskXML = XDocument.Load(sr.ReadToEnd());
                        taskXML = XDocument.Parse(sr.ReadToEnd());
                    }
                }

            }

            return taskXML;
        }

        // Save the XDocument to IsoloatedStorageFile.
        private void SaveTasks(XDocument taskXML)
        {
            if (appStorage.FileExists(fileName))
            {
                using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(appStream))
                    {
                        taskXML.Save(sw);
                    }
                }
            }
        }

        private void ReadTask()
        {
            XDocument taskXML = RetrieveTasks();

            var tasks = from query in taskXML.Descendants("Task")
                        select new Task
                        {
                            ID = query.Element("ID").Value,
                            Title = query.Element("Title").Value,
                            HasDone = Convert.ToBoolean(query.Element("HasDone").Value)
                        };

            lsbTaskList.ItemsSource = tasks;
        }

        private void ckbTaskList_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkedItem = (CheckBox)sender;

            if (checkedItem == null || checkedItem.Tag == null)
            {
                return;
            }

            // Retrieve the XDocument object
            XDocument taskXML = RetrieveTasks();

            string ID = checkedItem.Tag.ToString();

            var task = from _task in taskXML.Descendants("Task")
                       where (_task.Element("ID").Value == ID)
                       select _task;

            foreach (var el in task)
            {
                el.Element("HasDone").Value = Convert.ToBoolean(checkedItem.IsChecked).ToString();
            }

            SaveTasks(taskXML);

            ReadTask();
        }
    }
}