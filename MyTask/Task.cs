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

namespace MyTask
{
    public class Task
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string DueDate { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
        public bool HasDone { get; set; }
    }
}
