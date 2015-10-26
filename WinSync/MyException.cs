using System;
using System.Windows.Forms;

namespace WinSync
{
    class MyException : Exception
    {
        public static MyException LinkAlreadyExists = new MyException("Link Already Exists", "The title of the Link already exists.\nThe title must be unique!");
        public static MyException DirectoryNotFound = new MyException("Directory not Found", "The Directory does not exist on this computer!");

        public string Title { get; set; }

        public new string Message { get; set; }

        public MyException(string title, string message)
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
