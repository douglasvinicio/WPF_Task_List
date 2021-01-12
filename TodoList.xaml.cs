using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Session5
{
    /// <summary>
    /// Interaction logic for TodoList.xaml
    /// </summary>
    public partial class TodoList : Window
    {

        const string DataFileName = @"..\..\tasks.txt";
        List<Task> taskList = new List<Task>();
        public TodoList()
        {
            InitializeComponent();
            LoadDataFromFile();
            cldDueDate.SelectedDate = DateTime.Now.AddDays(1);
        }
        private void LoadDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                string[] taskInfo = File.ReadAllLines(DataFileName);
                foreach (string taskLine in taskInfo)
                {
                    string[] task = taskLine.Split(';');
                    string taskDescription = task[0];
                    string difficulty = task[1];
                    string dueDate = task[2];
                    string status = task[3];

                    Task t = new Task(taskDescription,difficulty,dueDate,status);
                    taskList.Add(t);
                }
            }
               lvTaskList.ItemsSource = taskList;
         }

        // Addding new Task method
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {

            string taskDescription = txtTask.Text;
            string difficulty = lblDifficulty.Content.ToString();
            string dueDate = txtDateDue.Text;
            string status = cmbStatus.Text;

            Task task = new Task(taskDescription,difficulty,dueDate,status);
            taskList.Add(task);

            ResetFields();
        }
         // Updating Method
        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            string newTaskDesc = txtTask.Text;
            string newDifficultyLvl = sliderDifficulty.Value.ToString();
            string newDueDate = txtDateDue.Text;
            string newStatus = cmbStatus.Text;

            Task taskUpdate = (Task)lvTaskList.SelectedItem;

            taskUpdate.TaskDescription = newTaskDesc;
            taskUpdate.Difficulty = newDifficultyLvl;
            taskUpdate.DueDate = newDueDate;
            taskUpdate.Status = newStatus;

            ResetFields();            
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lvTaskList.SelectedIndex == -1)
            {
                MessageBox.Show("You must select at least one item");
            }

            Task taskToBeDeleted = (Task)lvTaskList.SelectedItem;
            taskList.Remove(taskToBeDeleted);

            ResetFields();
        }

        private void lvTaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Activating buttons when Item in the list is selected
            btnUpdateTask.IsEnabled = true;
            btnDeleteTask.IsEnabled = true;


            var selectedTask = lvTaskList.SelectedItem;

            if (selectedTask is Task)
            {
                Task task = (Task)lvTaskList.SelectedItem;


                // Assigning values as an object inside the application
                txtTask.Text = task.TaskDescription;
                sliderDifficulty.Value = double.Parse(task.Difficulty);
                txtDateDue.Text = task.DueDate;
                cmbStatus.Text = task.Status;
            }    
        }

        private void ResetFields()
        {
            lvTaskList.Items.Refresh();
            lvTaskList.SelectedIndex = -1;
        }

        private void btnExportFile_Click(object sender, RoutedEventArgs e)
        {
            // Here method to Export Everything to a file

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (Task task in taskList)
                    {
                        writer.WriteLine(task.ToDataString());
                    }
                }
            }
          }
       }
    }
