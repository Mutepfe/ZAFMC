using System;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

namespace ZAFMC
{
    //Shared popups for every screen. Each box is modal and on top of all windows (incl. the browser),
    //so it never opens behind the browser or minimised. DefaultDesktopOnly shows the box on the
    //active desktop as a topmost window; it needs no owner window, so it also works from Timer threads.
    public static class TopMostDialogs
    {
        public static DialogResult ShowTopMost(string text)
        {
            return ShowTopMost(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowTopMost(string text, string caption)
        {
            return ShowTopMost(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowTopMost(string text, string caption, MessageBoxButtons buttons)
        {
            return ShowTopMost(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return ShowTopMost(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ShowTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            //No owner: WinForms throws if an owner is combined with DefaultDesktopOnly.
            return MessageBox.Show(text, caption, buttons, icon, defaultButton, MessageBoxOptions.DefaultDesktopOnly);
        }

        //Topmost replacement for Microsoft.VisualBasic.Interaction.InputBox.
        //Returns the typed text on OK, and "" on Cancel, Esc or close (same contract as Interaction.InputBox).
        //ASP.NET request threads are MTA; WinForms text input needs STA, so the form runs on its own STA thread.
        public static string InputBox(string prompt, string title, string defaultResponse = "")
        {
            string result = string.Empty;
            Exception error = null;

            var thread = new Thread(() =>
            {
                try
                {
                    result = ShowInputForm(prompt, title, defaultResponse);
                }
                catch (Exception ex)
                {
                    error = ex;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
            thread.Join();

            if (error != null)
            {
                ExceptionDispatchInfo.Capture(error).Throw();
            }
            return result;
        }

        private static string ShowInputForm(string prompt, string title, string defaultResponse)
        {
            using (var form = new TopMostForm())
            using (var label = new Label())
            using (var textBox = new TextBox())
            using (var okButton = new Button())
            using (var cancelButton = new Button())
            {
                form.Text = title ?? string.Empty;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = true;
                form.TopMost = true;
                form.ClientSize = new Size(400, 130);

                label.AutoSize = false;
                label.Location = new Point(12, 12);
                label.Size = new Size(376, 40);
                label.Text = prompt ?? string.Empty;

                textBox.Location = new Point(12, 58);
                textBox.Width = 376;
                textBox.Text = defaultResponse ?? string.Empty;

                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(232, 92);
                okButton.Size = new Size(75, 23);

                cancelButton.Text = "Cancel";
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Location = new Point(313, 92);
                cancelButton.Size = new Size(75, 23);

                form.Controls.AddRange(new Control[] { label, textBox, okButton, cancelButton });
                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                form.Shown += (s, e) =>
                {
                    form.Activate();
                    form.BringToFront();
                    textBox.Focus();
                    textBox.SelectAll();
                };

                return form.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
            }
        }

        //Form.TopMost alone is applied with SetWindowPos, which Windows ignores once the process
        //(e.g. IIS Express) is not in the foreground. Creating the window with WS_EX_TOPMOST keeps
        //every InputBox on top, not just the first one.
        private sealed class TopMostForm : Form
        {
            private const int WS_EX_TOPMOST = 0x00000008;

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_TOPMOST;
                    return cp;
                }
            }
        }
    }
}
