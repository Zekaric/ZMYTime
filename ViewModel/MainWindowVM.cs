using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using ZMuse.Command;

namespace ZMuse.ViewModel
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        // Variable/Property //////////////////////////////////////////////////////////////////////
        // Public /////////////////////////////////////////////////////////////////////////////////

        public ICommand                         CmdTask1Start           { get; private set; }
        public ICommand                         CmdTask2Start           { get; private set; }
        public ICommand                         CmdTask3Start           { get; private set; }
        public ICommand                         CmdTask4Start           { get; private set; }
        public ICommand                         CmdTask5Start           { get; private set; }
        public ICommand                         CmdTask6Start           { get; private set; }
        public ICommand                         CmdTask7Start           { get; private set; }
        public ICommand                         CmdTask8Start           { get; private set; }
        public ICommand                         CmdTask9Start           { get; private set; }
        public ICommand                         CmdTask10Start          { get; private set; }
        public ICommand                         CmdTaskStop             { get; private set; }
        
        public String                           Task1Name               => _taskName[0];
        public String                           Task2Name               => _taskName[1];
        public String                           Task3Name               => _taskName[2];
        public String                           Task4Name               => _taskName[3];
        public String                           Task5Name               => _taskName[4];
        public String                           Task6Name               => _taskName[5];
        public String                           Task7Name               => _taskName[6];
        public String                           Task8Name               => _taskName[7];
        public String                           Task9Name               => _taskName[8];
        public String                           Task10Name              => _taskName[10];
        public String                           Task1Time               => _taskTime[0];
        public String                           Task2Time               => _taskTime[1];
        public String                           Task3Time               => _taskTime[2];
        public String                           Task4Time               => _taskTime[3];
        public String                           Task5Time               => _taskTime[4];
        public String                           Task6Time               => _taskTime[5];
        public String                           Task7Time               => _taskTime[6];
        public String                           Task8Time               => _taskTime[7];
        public String                           Task9Time               => _taskTime[8];
        public String                           Task10Time              => _taskTime[10];
        public Int32                            CurrentTask             { get; private set; } = -1;

        // private variables.

        private List<String>                    _taskName               = null;
        private List<String>                    _taskTime               = null;
        private readonly Timer                  _timer                  = null;
        private readonly Window                 _view                   = null;

        // Function ///////////////////////////////////////////////////////////////////////////////
        // Public /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM(Window view)
        {
            _SettingLoad();

            this.CmdTask1Start      = new CommandHandler(null,                this._ExeTask1Start);
            this.CmdTask2Start      = new CommandHandler(null,                this._ExeTask2Start);
            this.CmdTask3Start      = new CommandHandler(null,                this._ExeTask3Start);
            this.CmdTask4Start      = new CommandHandler(null,                this._ExeTask4Start);
            this.CmdTask5Start      = new CommandHandler(null,                this._ExeTask5Start);
            this.CmdTask6Start      = new CommandHandler(null,                this._ExeTask6Start);
            this.CmdTask7Start      = new CommandHandler(null,                this._ExeTask7Start);
            this.CmdTask8Start      = new CommandHandler(null,                this._ExeTask8Start);
            this.CmdTask9Start      = new CommandHandler(null,                this._ExeTask9Start);
            this.CmdTask10Start     = new CommandHandler(null,                this._ExeTask10Start);
            this.CmdTaskStop        = new CommandHandler(null,                this._ExeTaskStop);

            this._view              = view;

            this._timer             = new Timer();
            this._timer.Tick       += new EventHandler(_TimerTick);
            this._timer.Interval    = 1000;
            this._timer.Start();
        }

        // Private ////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Start task timer.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);
        private void _ExeTask1Start(Object parameter) => _StartTask(0);

        /// <summary>
        /// Command to add all the songs of an album
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeAddAlbum(Object parameter)
        {
            AudioFile selected;
            Int32     index;
            Int32     count;
            Boolean   isAddingToEmpty;

            isAddingToEmpty = (this.PlayList.Count == 0);

            selected = this._libraryListSelected[0];

            count = this.LibraryList.Count;
            for (index = 0; index < count; index++)
            {
                if (selected.NameArtist.Equals(this.LibraryList[index].NameArtist) &&
                    selected.NameAlbum.Equals( this.LibraryList[index].NameAlbum))
                {
                    this.PlayList.Add(LibraryList[index]);
                }
            }

            if (isAddingToEmpty)
            {
                this._SetSong(0);
            }
        }

        /// <summary>
        /// Command to add all the selected songs.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeAddSong(Object parameter)
        {
            Int32   index;
            Int32   count;
            Boolean isAddingToEmpty;

            isAddingToEmpty = (this.PlayList.Count == 0);

            count = this._libraryListSelected.Count;
            for (index = 0; index < count; index++)
            {
                this.PlayList.Add(this._libraryListSelected[index]);
            }

            this.OnPropertyChanged("PlayList");

            if (isAddingToEmpty)
            {
                this._SetSong(0);
            }
        }

        /// <summary>
        /// Command to move to the next song
        /// </summary>
        /// <param name="param"></param>
        private void _ExeNext(Object param)
        {
            Boolean playTemp;

            playTemp = this.IsPlaying;

            // Stop playing
            if (this.IsPlaying || this.IsPaused)
            {
                this._ExeStop(null);
            }

            // Change the song.
            this.SongIndex = this.SongIndex + 1;

            // Resume playing
            if (playTemp)
            {
                this._ExePlayPause(null);
            }
        }

        /// <summary>
        /// Command to select the library folder.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExePickLibraryFolder(Object parameter)
        {
            VistaFolderBrowserDialog folderBrowser;

            folderBrowser = new VistaFolderBrowserDialog();

            // Get the folder.
            if (folderBrowser.ShowDialog(this._view) == true)
            {
                // Set the folder path.  
                this._SetLibraryFolder(folderBrowser.SelectedPath);

                // Update the ui.
                this.OnPropertyChanged("LibraryList");

                this.OnPropertyChanged("PlayList");

                this.OnPropertyChanged("NameAlbum"); 
                this.OnPropertyChanged("NameArtist");
                this.OnPropertyChanged("NameFile");
                this.OnPropertyChanged("NameFileImage");
                this.OnPropertyChanged("NameSong");
                this.OnPropertyChanged("NameTrack");
                this.OnPropertyChanged("NameLength");

                this.OnPropertyChanged("LibraryFolder");

                // Save the change for next time in.
                this._SettingSave();
            }
        }

        /// <summary>
        /// Command to play / pause the current song.
        /// </summary>
        /// <param name="param"></param>
        private void _ExePlayPause(Object param)
        {
            // No song to play.
            if (this._songAudioFile == null)
            {
                return;
            }

            // We are playing
            if (!this.IsPlaying && this.IsPaused)
            {
                this.IsPaused  = false;
                this.IsPlaying = true;

                this._wmPlayer.controls.play();
            }
            else if (!this.IsPlaying && !this.IsPaused)
            {
                this.IsPaused     = false;
                this.IsPlaying    = true;
                this.SongPosition = 0;

                this._wmPlayer.URL = this.NameFile;
                this._wmPlayer.controls.play();
            }
            else if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this.IsPaused  = true;

                this._wmPlayer.controls.pause();
            }
        }

        /// <summary>
        /// Command to move to the previous song.
        /// </summary>
        /// <param name="param"></param>
        private void _ExePrev(Object param)
        {
            Boolean playTemp;

            playTemp = this.IsPlaying;

            // Stop playing
            if (this.IsPlaying || this.IsPaused)
            {
                this._ExeStop(null);
            }

            // Change the song.
            this.SongIndex = this.SongIndex - 1;

            // Resume playing
            if (playTemp)
            {
                this._ExePlayPause(null);
            }
        }

        /// <summary>
        /// Command to remove songs from the play list.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeRemoveSong(Object parameter)
        {
            Int32           index;
            Int32           count;
            List<AudioFile> ltemp;

            // When we modify the _playList, _playListSelected is volatile.
            // We need to make a copy.
            ltemp = new List<AudioFile>();

            count = this._playListSelected.Count;
            for (index = 0; index < count; index++)
            {
                ltemp.Add(this._playListSelected[index]);
            }

            for (index = 0; index < count; index++)
            {
                this.PlayList.Remove(ltemp[index]);
            }

            this.OnPropertyChanged("PlayList");

            // Stop playing when things are out of sync or empty.
            if (this.SongIndex >= this.PlayList.Count ||
                (this._songAudioFile != null &&
                 !this._songAudioFile.FileName.Equals(this.PlayList[SongIndex].FileName)))
            {
                this._ExeStop(null);
                this._SetSong(0);
            }
        }

        /// <summary>
        /// Command to randomize the play list.
        /// </summary>
        /// <param name="parameter"></param>
        private void _ExeShuffleSong(Object parameter)
        {
            List<AudioFile> playList;
            Int32           index;
            Int32           count;
            Int32           songIndex;
            Random          random;

            this._ExeStop(null);
            
            // Get the current play list.
            playList = new List<AudioFile>();
            count    = this.PlayList.Count;
            for (index = 0; index < count; index++)
            {
                playList.Add(this.PlayList[index]);
            }

            // Clear the play list.
            this.PlayList.Clear();

            // Randomly repopulate the play list.
            random = new Random((Int32) DateTime.Now.Ticks & 0xFFFF);
            for (index = 0; index < count; index++)
            {
                songIndex = random.Next(playList.Count);
                
                this.PlayList.Add(playList[songIndex]);

                playList.RemoveAt(songIndex);
            }

            this.OnPropertyChanged("PlayList");

            this._SetSong(0);
        }

        /// <summary>
        /// Command to stop playing the current song.
        /// </summary>
        /// <param name="param"></param>
        private void _ExeStop(Object param)
        {
            this.IsPlaying = false;
            this.IsPaused  = false;

            this._wmPlayer.controls.stop();
        }

        /// <summary>
        /// Callback to determine that the song has ended.
        /// </summary>
        /// <param name="NewState"></param>
        private void _SetHasSongEnded(Int32 NewState)
        {
            if (NewState == (Int32) WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                this._hasSongEnded = true;
            }
        }

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="folder"></param>
        private void _SetLibraryFolder(String folder)
        { 
            Int32 index;

            this.LibraryFolder = folder;

            this._audioFileList = new AudioFileList(this.LibraryFolder);

            this.LibraryList = new ObservableCollection<AudioFile>();
            for (index = 0; index < this._audioFileList.FileList.Count; index++)
            { 
                this.LibraryList.Add(this._audioFileList.FileList[index]);
            }

            this.PlayList = new ObservableCollection<AudioFile>();
        }

        /// <summary>
        /// Set the song to play.
        /// </summary>
        /// <param name="value"></param>
        private void _SetSong(Int32 value)
        {
            // Set the song index;
            this._songIndex = Math.Min(this.PlayList.Count - 1, Math.Max(value, 0));

            // Set the song audio file.
            this._songAudioFile = null;
            if (this.PlayList.Count > 0)
            {
                this._songAudioFile = this.PlayList[this.SongIndex];
            }

            // Update the ui.
            this.OnPropertyChanged("NameAlbum"); 
            this.OnPropertyChanged("NameArtist");
            this.OnPropertyChanged("NameFile");
            this.OnPropertyChanged("NameFileImage");
            this.OnPropertyChanged("NameSong");
            this.OnPropertyChanged("NameTrack");
            this.OnPropertyChanged("NameLength");
        }

        /// <summary>
        /// Program settings load
        /// </summary>
        private void _SettingLoad()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zmuse.dat";

            if (File.Exists(path))
            { 
                String folder;

                folder = File.ReadAllText(path);

                this._SetLibraryFolder(folder);
            }
        }

        /// <summary>
        /// Program settings save
        /// </summary>
        private void _SettingSave()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\zmuse.dat";

            File.WriteAllText(path, this.LibraryFolder);
        }

        /// <summary>
        /// Update the ui.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _TimerTick(object sender, EventArgs e)
        {
            Int32 itemp;

            this.SongPosition = 0;
            if (this._wmPlayer?.currentMedia != null)
            {
                this.SongPosition = (Int32) ((this._wmPlayer.controls.currentPosition /
                    this._wmPlayer.currentMedia.duration) * 1000.0);

                if (!this._wmPlayer.currentMedia.durationString.Equals(this.NameLength))
                {
                    this.NameLength = this._wmPlayer.currentMedia.durationString;
                }

                // We have come to the end.
                if (this._hasSongEnded)
                {
                    this.IsPlaying     = false;
                    this._hasSongEnded = false;

                    // For checking if there are any more songs.
                    itemp = this.SongIndex;

                    // Move to the next song.
                    this._SetSong(this.SongIndex + 1);

                    // SongIndex changed so there are more songs in the list.
                    if (itemp != this.SongIndex)
                    {
                        // Play the new song.
                        this._ExePlayPause(null);
                    }
                }
            }
        }

        // INotifyPropertyChanged /////////////////////////////////////////////////////////////////
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
