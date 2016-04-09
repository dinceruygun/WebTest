using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using WebSiteTest.Parameters;
using WebSiteTest.Test;
using WebSiteTest.Test.Commands;
using Form = System.Windows.Forms.Form;

namespace WebSiteTest
{
    public partial class Form1 : Form
    {
        private Projects _myProjects;
        private IE _myBrowser;
        private bool _testStatus = false;
        private ListViewItem _selectCommand;
        private bool _textChangeControl = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTest();
        }

        private void btnAddHost_Click(object sender, EventArgs e)
        {
            AddHostList(txtHostName.Text);
        }

        private void AddHostList(string hostName)
        {
            if (hostName == "") return;

            _myProjects.Add(new Project {Url = hostName});

            ReloadHostList();

            txtHostName.Text = "";
            txtHostName.Focus();
        }

        private void ReloadHostList()
        {
            lstHostList.Items.Clear();

            if (_myProjects != null)
                lstHostList.Items.AddRange(_myProjects.ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _myProjects = new Projects();
            _myBrowser = new IE();
            ReadyTest();
            ReloadHostList();
        }

        private void ReadyTest()
        {
            

            cmbCommands.Items.Clear();
            lstHostList.Items.Clear();
            lstCommands.Items.Clear();

            txtHostName.Text = "";
            txtTarget.Text = "";
            txtValue.Text = "";

            if (TestManager.Commands != null)
            {
                TestManager.SetBrowser(_myBrowser);
                cmbCommands.Items.AddRange(TestManager.Commands.Select(t => t.GetCommandName()).ToArray());
            }
        }

        private void StartTest()
        {
            var selectProject = 0;
            _testStatus = true;

            foreach (var project in _myProjects)
            {
                lstHostList.SelectedIndex = selectProject;

                CallTestPage(project);

                selectProject++;
            }

            _testStatus = false;
        }

        [STAThread]
        private void CallTestPage(Project project)
        {
            foreach (var cmd in from ListViewItem item in lstCommands.Items
                select TestManager.Commands.Find(c =>
                {
                    item.Selected = true;
                    lstCommands.Select();

                    if (c.GetCommandName() == item.Text)
                    {
                        c.Target = item.SubItems[1].Text;
                        c.Value = item.SubItems[2].Text;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }))
            {

                TestManager.RunTest(project, cmd);
            }
        }





        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _myBrowser?.Close();
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            if (cmbCommands.SelectedIndex <= -1) return;

            ICommand cmd = TestManager.Commands.Find(c => c.GetCommandName() == cmbCommands.Text);
            if (cmd == null) return;

            ListViewItem item = new ListViewItem {Text = cmbCommands.Text};
            item.SubItems.Add(txtTarget.Text);
            item.SubItems.Add(txtValue.Text);

            lstCommands.Items.Add(item);

            txtTarget.Text = "";
            txtValue.Text = "";
            cmbCommands.SelectedIndex = 1;

            _selectCommand = null;
        }

        private void cmbCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_textChangeControl) return;

            if (_selectCommand != null)
            {
                _selectCommand.Text = cmbCommands.Text;
            }
            else
            {
                txtTarget.Text = "";
                txtValue.Text = "";
            }

        }

        private void saveTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTest(false);
        }

        private void SaveTest(bool saveAs)
        {
            TestParameters parameters = new TestParameters
            {
                Projects = _myProjects
            };


            for (int index = 0; index < lstCommands.Items.Count; index++)
            {
                ListViewItem item = lstCommands.Items[index];
                if (TestManager.Commands.Find(c => c.GetCommandName() == item.Text) != null)
                {
                    parameters.Commands.Add(new Command()
                    {
                        Name = item.Text,
                        Target = item.SubItems[1].Text,
                        Value = item.SubItems[2].Text
                    });
                }
            }

            Parameters.ParameterManager.SaveParameters(parameters, saveAs);
        }

        private void saveAsTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTest(true);
        }

        private void openTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenParameters();
        }

        private void OpenParameters()
        {
            TestParameters tp = Parameters.ParameterManager.OpenParameters();

            if (tp!=null)
            {
                ReadyTest();
                _myProjects = tp.Projects;
                foreach (var testCommand in tp.Commands)
                {
                    ListViewItem item = new ListViewItem {Text = testCommand.Name};
                    item.SubItems.Add(testCommand.Target);
                    item.SubItems.Add(testCommand.Value);

                    lstCommands.Items.Add(item);
                }

                ReloadHostList();
            }


        }

        private void lstCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            _textChangeControl = true;

            if (_testStatus != false || lstCommands.SelectedItems.Count == 0) return;

            txtTarget.Text = lstCommands.SelectedItems[0].SubItems[1].Text;
            txtValue.Text = lstCommands.SelectedItems[0].SubItems[2].Text;

            for (int i = 0; i < cmbCommands.Items.Count; i++)
            {
                if (cmbCommands.Items[i].ToString() == lstCommands.SelectedItems[0].Text)
                {
                    _selectCommand = lstCommands.SelectedItems[0];
                    cmbCommands.SelectedIndex = i;

                    _textChangeControl = false;

                    return;
                }
            }

            _textChangeControl = false;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (_selectCommand!= null && _textChangeControl == false)
            {
                _selectCommand.SubItems[2].Text = txtValue.Text;
            }
        }

        private void txtTarget_TextChanged(object sender, EventArgs e)
        {
            if (_selectCommand != null && _textChangeControl == false)
            {
                _selectCommand.SubItems[1].Text = txtTarget.Text;
            }
        }
    }
}
