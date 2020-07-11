using System;
using Terminal.Gui;

namespace GitUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            var top = Application.Top;

            // Creates the top-level window to show
            var win = new Window("GitUI")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);

            var okButton = new Button(3, 14, "Ok");
            okButton.Clicked += OkButtonClicked;

            win.Add(okButton,
                new Button(10, 14, "Cancel"));

            win.Add(new OpenDialog("1", "2"));

            Application.Run();
        }

        static void OkButtonClicked()
        {
            //MessageBox.Query(15,15,"Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
            var dialog = OpenDialog.Create();
            
        }
    }
}
