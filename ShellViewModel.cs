using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WpfApplication1
{
    using Caliburn.Micro;
    using System.Windows;

    public class ShellViewModel : PropertyChangedBase
    {
        string name;

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