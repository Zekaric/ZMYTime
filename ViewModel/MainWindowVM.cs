using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ZUtility;

namespace ZMuse.ViewModel
{
    internal class MainWindowVM : ZMVVM_VM
    {
        // Variable/Property //////////////////////////////////////////////////////////////////////
        // Public /////////////////////////////////////////////////////////////////////////////////

        public ICommand         CmdTask1Start   { get; private set; }
        public ICommand         CmdTask2Start   { get; private set; }
        public ICommand         CmdTask3Start   { get; private set; }
        public ICommand         CmdTask4Start   { get; private set; }
        public ICommand         CmdTask5Start   { get; private set; }
        public ICommand         CmdTask6Start   { get; private set; }
        public ICommand         CmdTask7Start   { get; private set; }
        public ICommand         CmdTask8Start   { get; private set; }
        public ICommand         CmdTask9Start   { get; private set; }
        public ICommand         CmdTask10Start  { get; private set; }
        public ICommand         CmdTaskStop     { get; private set; }
        public ICommand         CmdTimeReset    { get; private set; }
        public ICommand         CmdCloseApp     { get; private set; }

        public String           Task1Name       { get { return this._taskName[0]; } set { this._taskName[0] = value; } }
        public String           Task2Name       { get { return this._taskName[1]; } set { this._taskName[1] = value; } }
        public String           Task3Name       { get { return this._taskName[2]; } set { this._taskName[2] = value; } }
        public String           Task4Name       { get { return this._taskName[3]; } set { this._taskName[3] = value; } }
        public String           Task5Name       { get { return this._taskName[4]; } set { this._taskName[4] = value; } }
        public String           Task6Name       { get { return this._taskName[5]; } set { this._taskName[5] = value; } }
        public String           Task7Name       { get { return this._taskName[6]; } set { this._taskName[6] = value; } }
        public String           Task8Name       { get { return this._taskName[7]; } set { this._taskName[7] = value; } }
        public String           Task9Name       { get { return this._taskName[8]; } set { this._taskName[8] = value; } }
        public String           Task10Name      { get { return this._taskName[9]; } set { this._taskName[9] = value; } }
        public String           Task1Time       { get { return this._taskTime[0]; } set { this._taskTime[0] = value; } }
        public String           Task2Time       { get { return this._taskTime[1]; } set { this._taskTime[1] = value; } }
        public String           Task3Time       { get { return this._taskTime[2]; } set { this._taskTime[2] = value; } }
        public String           Task4Time       { get { return this._taskTime[3]; } set { this._taskTime[3] = value; } }
        public String           Task5Time       { get { return this._taskTime[4]; } set { this._taskTime[4] = value; } }
        public String           Task6Time       { get { return this._taskTime[5]; } set { this._taskTime[5] = value; } }
        public String           Task7Time       { get { return this._taskTime[6]; } set { this._taskTime[6] = value; } }
        public String           Task8Time       { get { return this._taskTime[7]; } set { this._taskTime[7] = value; } }
        public String           Task9Time       { get { return this._taskTime[8]; } set { this._taskTime[8] = value; } }
        public String           Task10Time      { get { return this._taskTime[9]; } set { this._taskTime[9] = value; } }
        public Int32            CurrentTask     { get; private set; } = -1;

        // private variables.

        private String[]        _taskName       = new String[10];
        private String[]        _taskTime       = new String[10];
        private DateTime[]      _taskClock      = new DateTime[10];
        private readonly Timer  _timer          = null;
        private readonly Window _view           = null;
        private DateTime        _startTime,
                                _stopTime;

        // Function ///////////////////////////////////////////////////////////////////////////////
        // Public /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM(Window view)
        {
            Int32 index;

            this._SettingLoad();

            this.CmdTask1Start      = new ZMVVM_Command(null, this._ExeTask1Start);
            this.CmdTask2Start      = new ZMVVM_Command(null, this._ExeTask2Start);
            this.CmdTask3Start      = new ZMVVM_Command(null, this._ExeTask3Start);
            this.CmdTask4Start      = new ZMVVM_Command(null, this._ExeTask4Start);
            this.CmdTask5Start      = new ZMVVM_Command(null, this._ExeTask5Start);
            this.CmdTask6Start      = new ZMVVM_Command(null, this._ExeTask6Start);
            this.CmdTask7Start      = new ZMVVM_Command(null, this._ExeTask7Start);
            this.CmdTask8Start      = new ZMVVM_Command(null, this._ExeTask8Start);
            this.CmdTask9Start      = new ZMVVM_Command(null, this._ExeTask9Start);
            this.CmdTask10Start     = new ZMVVM_Command(null, this._ExeTask10Start);
            this.CmdTaskStop        = new ZMVVM_Command(null, this._ExeTaskStop);
            this.CmdTimeReset       = new ZMVVM_Command(null, this._ExeTimeReset);
            this.CmdCloseApp        = new ZMVVM_Command(null, this._ExeCloseApp);

            for (index = 0; index < 10; index++)
            {
                this._taskClock[index] = new DateTime();
                this._taskTime[index]  = this._taskClock[index].ToString("T");
            }
            this._startTime = new DateTime();
            this._stopTime  = new DateTime();

            this._view = view;

            this._timer             = new Timer();
            this._timer.Tick       += new EventHandler(_TimerTick);
            this._timer.Interval    = 1000;
            this._timer.Start();
        }

        // Private ////////////////////////////////////////////////////////////////////////////////

        private void _ExeCloseApp(Object paramter) => this._SettingSave();

        /// <summary>
        /// Start task timer.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeTask1Start( Object parameter) => this._StartTask(0);
        private void _ExeTask2Start( Object parameter) => this._StartTask(1);
        private void _ExeTask3Start( Object parameter) => this._StartTask(2);
        private void _ExeTask4Start( Object parameter) => this._StartTask(3);
        private void _ExeTask5Start( Object parameter) => this._StartTask(4);
        private void _ExeTask6Start( Object parameter) => this._StartTask(5);
        private void _ExeTask7Start( Object parameter) => this._StartTask(6);
        private void _ExeTask8Start( Object parameter) => this._StartTask(7);
        private void _ExeTask9Start( Object parameter) => this._StartTask(8);
        private void _ExeTask10Start(Object parameter) => this._StartTask(9);
        private void _ExeTaskStop(   Object parameter) => this._StartTask(-1);

        /// <summary>
        /// Reset the time values.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeTimeReset(Object parameter)
        {
            Int32 index;

            this._StartTask(-1);

            for (index = 0; index < 10; index++)
            {
                this._taskClock[index] = new DateTime();
                this._taskTime[index]  = this._taskClock[index].ToString("T");
            }

            this.OnPropertyChanged("Task1Time");
            this.OnPropertyChanged("Task2Time");
            this.OnPropertyChanged("Task3Time");
            this.OnPropertyChanged("Task4Time");
            this.OnPropertyChanged("Task5Time");
            this.OnPropertyChanged("Task6Time");
            this.OnPropertyChanged("Task7Time");
            this.OnPropertyChanged("Task8Time");
            this.OnPropertyChanged("Task9Time");
            this.OnPropertyChanged("Task10Time");
        }

        /// <summary>
        /// Start a specific task.
        /// </summary>
        /// <param name="taskIndex"></param>
        private void _StartTask(Int32 taskIndex)
        {
            this._stopTime = DateTime.Now;

            if (this.CurrentTask != -1)
            {
                this._taskClock[CurrentTask] += this._stopTime - this._startTime;
            }

            this._startTime = this._stopTime;

            switch (this.CurrentTask)
            {
                case 0: this.OnPropertyChanged("Task1Time"); break;
                case 1: this.OnPropertyChanged("Task2Time"); break;
                case 2: this.OnPropertyChanged("Task3Time"); break;
                case 3: this.OnPropertyChanged("Task4Time"); break;
                case 4: this.OnPropertyChanged("Task5Time"); break;
                case 5: this.OnPropertyChanged("Task6Time"); break;
                case 6: this.OnPropertyChanged("Task7Time"); break;
                case 7: this.OnPropertyChanged("Task8Time"); break;
                case 8: this.OnPropertyChanged("Task9Time"); break;
                case 9: this.OnPropertyChanged("Task10Time"); break;
            }

            this.CurrentTask = taskIndex;

            this.OnPropertyChanged("CurrentTask");
        }

        /// <summary>
        /// Program settings load
        /// </summary>
        private void _SettingLoad()
        {
            Int32  index;
            String path;

            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zmytime.dat";

            if (File.Exists(path))
            {
                String   fileText;
                String[] fileLines;

                fileText = File.ReadAllText(path);

                // Break the lines apart.
                fileLines = fileText.Split('\n');

                // Set the text for the task names.
                for (index = 0; index < 10; index++)
                { 
                    this._taskName[index] = fileLines[index];
                }
            }
        }

        /// <summary>
        /// Program settings save
        /// </summary>
        private void _SettingSave()
        {
            String fileText;
            String path;

            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zmytime.dat";

            // Set the test for the file.
            fileText =
                this._taskName[0] + "\n" +
                this._taskName[1] + "\n" +
                this._taskName[2] + "\n" +
                this._taskName[3] + "\n" +
                this._taskName[4] + "\n" +
                this._taskName[5] + "\n" +
                this._taskName[6] + "\n" +
                this._taskName[7] + "\n" +
                this._taskName[8] + "\n" +
                this._taskName[9];

            File.WriteAllText(path, fileText);
        }

        /// <summary>
        /// Update the ui.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _TimerTick(object sender, EventArgs e)
        {
            this._stopTime = DateTime.Now;

            if (this.CurrentTask != -1)
            {
                DateTime value;

                value = this._taskClock[CurrentTask];

                value += this._stopTime - this._startTime;

                this._taskTime[this.CurrentTask] = value.ToString("T");

                switch (this.CurrentTask)
                {
                case 0: this.OnPropertyChanged("Task1Time");  break;
                case 1: this.OnPropertyChanged("Task2Time");  break;
                case 2: this.OnPropertyChanged("Task3Time");  break;
                case 3: this.OnPropertyChanged("Task4Time");  break;
                case 4: this.OnPropertyChanged("Task5Time");  break;
                case 5: this.OnPropertyChanged("Task6Time");  break;
                case 6: this.OnPropertyChanged("Task7Time");  break;
                case 7: this.OnPropertyChanged("Task8Time");  break;
                case 8: this.OnPropertyChanged("Task9Time");  break;
                case 9: this.OnPropertyChanged("Task10Time"); break;
                }
            }
        }
    }
}
