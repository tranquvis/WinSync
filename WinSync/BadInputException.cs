using System;
using System.Windows.Forms;

namespace WinSync
{
    class BadInputException : Exception
    {
        public static BadInputException LinkAlreadyExists = new BadInputException("Link Already Exists", "The title of the Link already exists.\nThe title must be unique!");
        public static BadInputException DirectoryNotFound = new BadInputException("Directory not Found", "The Directory does not exist on this computer!");

        public string Title { get; set; }

        public new string Message { get; set; }

        public BadInputException(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public void ShowMsgBox()
        {
            MessageBox.Show(Message, Title);
        }
    }
}
