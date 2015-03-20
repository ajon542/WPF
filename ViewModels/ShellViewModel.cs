using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;
using WpfApplication1.ViewModels;

namespace WpfApplication1
{
    using Caliburn.Micro;
    using System.Windows;

    // Caliburn Micro expects a particular naming convention.
    // The class name should end with ViewModel.
    // Derive from PropertyChangedBase, looks like "Conductor<ScriptEditorViewModel>.Collection.OneActive"
    // already does.
    public class ShellViewModel : Conductor<ScriptEditorViewModel>.Collection.OneActive
    {
        public ShellViewModel()
        {
            DisplayName = "SCRIPTPAD";
        }

        private int count = 50;
        private string name;

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                // Notify the property Count has changed.
                NotifyOfPropertyChange(() => Count);
                // Check whether to enable or disable the button.
                NotifyOfPropertyChange(() => CanIncrementCount);
            }
        }

        // Caliburn Micro automatically looks for a method or property called
        // Can* to determine if we can actually do something.
        public bool CanIncrementCount
        {
            get { return Count < 60; }
        }
        public void IncrementCount()
        {
            ++Count;
        }

        public string FirstName
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(FirstName); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", FirstName)); //Don't do this in real life :)
        }

        public void New()
        {
            MessageBox.Show("New");
        }

        public void Open()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "C# Scripts|*.cs",
                CheckFileExists = true,
                Multiselect = false
            };

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                var fileName = Path.GetFileName(dialog.FileName);
                var content = File.ReadAllText(dialog.FileName);
            }
        }

        public void Save()
        {
            MessageBox.Show("Save");
        }

        public void SaveAll()
        {
            MessageBox.Show("SaveAll");
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}