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
        Task oneTask = new Task();

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
            oneTask = new Task() { Title = txtAddTask.Text.Trim() };

            string taskMessage = "";
            taskMessage = txtAddTask.Text.Trim();

            // Write the task into the IsoloatedStroage

            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            XElement taskElement = new XElement("Task",
                new XElement("ID", System.Guid.NewGuid()),
                new XElement("Title", txtAddTask.Text.Trim()),
                new XElement("CreateDate", DateTime.Now.ToString()),
                new XElement("DueDate", ""),
                new XElement("Comment", ""),
                new XElement("HasDone", "0")
                );

            XDocument taskXML = RetrieveTasks();

            taskXML.Root.Add(taskElement);

            using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, System.IO.FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(appStream))
                {
                    taskXML.Save(sw);
                }
            }

            ReadTask();
        }

        private XDocument RetrieveTasks()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            XDocument taskXML= new XDocument();

            if (appStorage.FileExists(fileName))
            {
                using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.Open))
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

        private void ReadTask()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            XDocument taskXML;

            if (appStorage.FileExists(fileName))
            {
                using (IsolatedStorageFileStream appStream = appStorage.OpenFile(fileName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(appStream))
                    {
                        //taskXML = XDocument.Load(sr.ReadToEnd());
                        taskXML = XDocument.Parse(sr.ReadToEnd());
                    }
                }

                var tasks = from query in taskXML.Descendants("Task")
                            select new Task
                            {
                                ID = query.Element("ID").Value,
                                Title = query.Element("Title").Value,
                                HasDone = query.Element("HasDone").Value
                            };

                lsbTaskList.ItemsSource = tasks;
            }
        }
    }
}