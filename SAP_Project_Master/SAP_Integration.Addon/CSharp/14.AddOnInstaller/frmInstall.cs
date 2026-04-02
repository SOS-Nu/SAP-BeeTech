//  SAP MANAGE DI API 2006 SDK Sample
//****************************************************************************
//
//  File:      frmInstall.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
//****************************************************************************
// This sample creates an add-on installer for SBO.
// An installation for SBO should be build in a spesific way.
// 1) It should be able to accept a command line parameter from SBO.
//    This parameter is a string built from 2 strings devided by "|".
//    The first string is the path recommended by SBO for installation folder.
//    The second string is the location of "AddOnInstallAPI.dll".
//    For example, a command line parameter that looks like this:
//    "C:\MyAddon|C:\Program Files\SAP Manage\SAP Business One\AddOnInstallAPI.dll"
//    Means that the recommended installation folder for this addon is "C:\MyAddon"
//    and the location of "AddOnInstallAPI.dll" is - 
//                 "C:\Program Files\SAP Manage\SAP Business One\AddOnInstallAPI.dll"
// 2) When the installation is complete the installer must call the function 
//    "EndInstall" from "AddOnInstallAPI.dll" to inform SBO the installation is complete.
//    This dll contains 3 functions that can be used during the installation.
//    The functions are: 
//         1) EndInstall - Signals SBO that the installation is complete.
//         2) SetAddOnFolder - Use it if you want to change the installation folder.
//         3) RestartNeeded - Use it if your installation requires a restart, it will cause
//            the SBO application to close itself after the installation is complete.
//    All 3 functions return a 32 bit integer. There are 2 possible values for this integer.
//    0 - Success, 1 - Failure.
// 3) The installer must be one executable file.
// 4) After your installer is ready you need to create an add-on registration file.
//    In order to create it you have a utility - "Add-On Registration Data Creator"
//    you can find it in -
//       "..\SAP Manage\SAP Business One SDK\Tools\AddOnRegDataGen\AddOnRegDataGen.exe".
//    This utility creates a file with the extention 'ard', you will be asked to 
//    point to this file when you register your addon.

using System; 
using System.Runtime.InteropServices; 
using Microsoft.Win32; 


using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace AddOnInstaller {
    public class frmInstall : System.Windows.Forms.Form { 
        
        #region ' Windows Form Designer generated code ' 
        
        public frmInstall() { 
            
            
            // This call is required by the Windows Form Designer.
            InitializeComponent(); 
            
            // Add any initialization after the InitializeComponent() call
            
        } 
        
        // Form overrides dispose to clean up the component list.
        protected override void Dispose( bool disposing ) { 
            if ( disposing ) { 
                if ( !( ( components == null ) ) ) { 
                    components.Dispose(); 
                } 
            } 
            base.Dispose( disposing ); 
        } 
        
        
        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components; 
        
        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        internal System.Windows.Forms.Label lblHeadLine; 
        internal System.Windows.Forms.Label Label1; 
        internal System.Windows.Forms.Label Label2; 
        internal System.Windows.Forms.TextBox txtDest; 
        internal System.Windows.Forms.CheckBox chkRestart; 
        internal System.Windows.Forms.CheckBox chkDefaultFolder; 
        internal System.Windows.Forms.Button cmdInstall; 
        internal System.IO.FileSystemWatcher FileWatcher; 
        [ System.Diagnostics.DebuggerStepThrough() ]
        private void InitializeComponent() { 
            this.cmdInstall = new System.Windows.Forms.Button(); 
            this.lblHeadLine = new System.Windows.Forms.Label(); 
            this.Label1 = new System.Windows.Forms.Label(); 
            this.Label2 = new System.Windows.Forms.Label(); 
            this.txtDest = new System.Windows.Forms.TextBox(); 
            this.chkRestart = new System.Windows.Forms.CheckBox(); 
            this.chkDefaultFolder = new System.Windows.Forms.CheckBox(); 
            this.FileWatcher = new System.IO.FileSystemWatcher(); 
            ( ( System.ComponentModel.ISupportInitialize )( this.FileWatcher ) ).BeginInit(); 
            this.SuspendLayout(); 
            // 
            // cmdInstall
            // 
            this.cmdInstall.Location = new System.Drawing.Point( 24, 216 ); 
            this.cmdInstall.Name = "cmdInstall"; 
            this.cmdInstall.Size = new System.Drawing.Size( 96, 23 ); 
            this.cmdInstall.TabIndex = 1; 
            this.cmdInstall.Text = "Install Add-on"; 
            // 
            // lblHeadLine
            // 
            this.lblHeadLine.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte( 177 ) ); 
            this.lblHeadLine.Location = new System.Drawing.Point( 16, 16 ); 
            this.lblHeadLine.Name = "lblHeadLine"; 
            this.lblHeadLine.Size = new System.Drawing.Size( 416, 24 ); 
            this.lblHeadLine.TabIndex = 2; 
            this.lblHeadLine.Text = "This Installer is a sample for Sap Business One. "; 
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte( 177 ) ); 
            this.Label1.Location = new System.Drawing.Point( 16, 40 ); 
            this.Label1.Name = "Label1"; 
            this.Label1.Size = new System.Drawing.Size( 416, 24 ); 
            this.Label1.TabIndex = 3; 
            this.Label1.Text = @"It will install a "" & sAddonName & "" add-on"; 
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point( 24, 88 ); 
            this.Label2.Name = "Label2"; 
            this.Label2.Size = new System.Drawing.Size( 256, 23 ); 
            this.Label2.TabIndex = 4; 
            this.Label2.Text = "Installation Folder recieved from SBO application"; 
            // 
            // txtDest
            // 
            this.txtDest.Enabled = false; 
            this.txtDest.Location = new System.Drawing.Point( 24, 112 ); 
            this.txtDest.Name = "txtDest"; 
            this.txtDest.Size = new System.Drawing.Size( 472, 20 ); 
            this.txtDest.TabIndex = 5; 
            this.txtDest.Text = ""; 
            // 
            // chkRestart
            // 
            this.chkRestart.Location = new System.Drawing.Point( 24, 176 ); 
            this.chkRestart.Name = "chkRestart"; 
            this.chkRestart.TabIndex = 6; 
            this.chkRestart.Text = "Ask for a restart"; 
            // 
            // chkDefaultFolder
            // 
            this.chkDefaultFolder.Checked = true; 
            this.chkDefaultFolder.CheckState = System.Windows.Forms.CheckState.Checked; 
            this.chkDefaultFolder.Location = new System.Drawing.Point( 24, 144 ); 
            this.chkDefaultFolder.Name = "chkDefaultFolder"; 
            this.chkDefaultFolder.Size = new System.Drawing.Size( 160, 24 ); 
            this.chkDefaultFolder.TabIndex = 7; 
            this.chkDefaultFolder.Text = "Use path supplied by SBO"; 
            // 
            // FileWatcher
            // 
            this.FileWatcher.EnableRaisingEvents = true; 
            this.FileWatcher.SynchronizingObject = this; 
            // 
            // frmInstall
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 ); 
            this.ClientSize = new System.Drawing.Size( 522, 263 ); 
            this.Controls.Add( this.chkDefaultFolder ); 
            this.Controls.Add( this.chkRestart ); 
            this.Controls.Add( this.txtDest ); 
            this.Controls.Add( this.Label2 ); 
            this.Controls.Add( this.Label1 ); 
            this.Controls.Add( this.lblHeadLine ); 
            this.Controls.Add( this.cmdInstall ); 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false; 
            this.MinimizeBox = false; 
            this.Name = "frmInstall"; 
            this.Text = "Addon Installation Sample"; 
            ( ( System.ComponentModel.ISupportInitialize )( this.FileWatcher ) ).EndInit(); 
            this.ResumeLayout( false ); 
            
            // events handled by frmInstall_Load
            base.Load += new System.EventHandler( frmInstall_Load ); 
            // events handled by cmdInstall_Click
            cmdInstall.Click += new System.EventHandler( cmdInstall_Click ); 
            // events handled by chkDefaultFolder_CheckedChanged
            chkDefaultFolder.CheckedChanged += new System.EventHandler( chkDefaultFolder_CheckedChanged ); 
            // events handled by FileWatcher_Renamed
            FileWatcher.Renamed += new System.IO.RenamedEventHandler( FileWatcher_Renamed ); 
        } 
        
        
        #endregion 
        
        #region 'Data members' 
        private string sAddonName = "HelloWorld"; 
        private string strDll; //  The path of "AddOnInstallAPI.dll"
        private string strDest; //  Installation target path
        private bool bFileCreated; //  True if the file was created
        #endregion 
        
        #region 'Declarations' 

		//  Declaring the functions inside "AddOnInstallAPI.dll"
        [DllImport("AddOnInstallAPI.dll")]
		static extern Int32 EndInstall();

		// EndInstall - Signals SBO that the installation is complete.
        // SetAddOnFolder - Use it if you want to change the installation folder.
		[DllImport("AddOnInstallAPI.dll")]
		static extern Int32 SetAddOnFolder(string srrPath);

		// RestartNeeded - Use it if your installation requires a restart, it will cause
        // the SBO application to close itself after the installation is complete.
		[DllImport("AddOnInstallAPI.dll")]
		static extern Int32 RestartNeeded();
        
		#endregion 
        
        #region 'Methods' 
        
        //  Read the addon path from the registry
        public string ReadPath() { 
            string readPathReturn = null;
            string sAns = null; 
            string sErr = ""; 
            
            sAns = RegValue( RegistryHive.LocalMachine, "SOFTWARE", "HelloWorld", ref sErr ); 
            readPathReturn = sAns; 
            if ( !( ( sAns != "" ) ) ) { 
                MessageBox.Show( "This error occurred: " + sErr ); 
            } 
            return readPathReturn;
        } 
        
        
        //  This Function reads values to the registry
        public string RegValue( RegistryHive Hive, string Key, string ValueName, [System.Runtime.InteropServices.Optional] ref string ErrInfo  ) { 
            
            RegistryKey objParent = null; 
            RegistryKey objSubkey = null; 
            string sAns = null; 
            switch ( Hive ) {
                case RegistryHive.ClassesRoot:
                    objParent = Registry.ClassesRoot; 
                    break;
                case RegistryHive.CurrentConfig:
                    objParent = Registry.CurrentConfig; 
                    break;
                case RegistryHive.CurrentUser:
                    objParent = Registry.CurrentUser; 
                    break;
                case RegistryHive.DynData:
                    objParent = Registry.DynData; 
                    break;
                case RegistryHive.LocalMachine:
                    objParent = Registry.LocalMachine; 
                    break;
                case RegistryHive.PerformanceData:
                    objParent = Registry.PerformanceData; 
                    break;
                case RegistryHive.Users:
                    objParent = Registry.Users; 
                    
                    break;
            }
            
            
            try { 
                objSubkey = objParent.OpenSubKey( Key ); 
                // if can't be found, object is not initialized
                if ( !( objSubkey == null ) ) { 
                    sAns = System.Convert.ToString( ( objSubkey.GetValue( ValueName ) ) ); 
                } 
                
            } 
            catch ( Exception ex ) { 
                
                ErrInfo = ex.Message; 
            } 
            finally { 
                
                // if no error but value is empty, populate errinfo
                if ( ErrInfo == "" & sAns == "" ) { 
                    ErrInfo = "No value found for requested registry key"; 
                } 
            } 
            return sAns; 
        } 
        
        
        //  This Function writes values to the registry
        public bool WriteToRegistry( RegistryHive ParentKeyHive, string SubKeyName, string ValueName, object Value ) { 
            
            RegistryKey objSubKey = null; 
            string sException = null; 
            RegistryKey objParentKey = null; 
            bool bAns = false; 
            
            try { 
                
                switch ( ParentKeyHive ) {
                    case RegistryHive.ClassesRoot:
                        objParentKey = Registry.ClassesRoot; 
                        break;
                    case RegistryHive.CurrentConfig:
                        objParentKey = Registry.CurrentConfig; 
                        break;
                    case RegistryHive.CurrentUser:
                        objParentKey = Registry.CurrentUser; 
                        break;
                    case RegistryHive.DynData:
                        objParentKey = Registry.DynData; 
                        break;
                    case RegistryHive.LocalMachine:
                        objParentKey = Registry.LocalMachine; 
                        break;
                    case RegistryHive.PerformanceData:
                        objParentKey = Registry.PerformanceData; 
                        break;
                    case RegistryHive.Users:
                        objParentKey = Registry.Users; 
                        break;
                }
                
                
                // Open 
                objSubKey = objParentKey.OpenSubKey( SubKeyName, true ); 
                // create if doesn't exist
                if ( objSubKey == null ) { 
                    objSubKey = objParentKey.CreateSubKey( SubKeyName ); 
                } 
                
                
                objSubKey.SetValue( ValueName, Value ); 
                bAns = true; 
            } 
            catch ( Exception ex ) { 
                bAns = false; 
                
            } 
            
            return true; 
            
        } 
        
        
        //  This function extracts the given add-on into the path specified
        private void ExtractFile( string path ) { 
            try { 
                System.IO.FileStream AddonExeFile = null; 
                System.Reflection.Assembly thisExe = null; 
                thisExe = System.Reflection.Assembly.GetExecutingAssembly(); 
                object sTargetPath = path + @"\" + sAddonName + ".exe"; 
                object sSourcePath = path + @"\" + sAddonName + ".tmp"; 
                
                System.IO.Stream file = null; 
                
                file = thisExe.GetManifestResourceStream( "Installer." + sAddonName + ".exe" ); 
                
                //  Create a tmp file first, after file is extracted change to exe
                if ( System.IO.File.Exists( System.Convert.ToString( sSourcePath ) ) ) { 
                    System.IO.File.Delete( System.Convert.ToString( sSourcePath ) ); 
                } 
                AddonExeFile = System.IO.File.Create( System.Convert.ToString( sSourcePath ) ); 
                
                byte[] buffer = null; 
                buffer = new byte[ file.Length ]; 
                
                file.Read( buffer, 0, System.Convert.ToInt32( file.Length ) ); 
                AddonExeFile.Write( buffer, 0, System.Convert.ToInt32( file.Length ) ); 
                AddonExeFile.Close(); 
                
                if ( System.IO.File.Exists( System.Convert.ToString( sTargetPath ) ) ) { 
                    System.IO.File.Delete( System.Convert.ToString( sTargetPath ) ); 
                } 
                //  Change file extension to exe
                System.IO.File.Move( System.Convert.ToString( sSourcePath ), System.Convert.ToString( sTargetPath ) ); 
                
            } 
            catch ( Exception ex ) { 
                Interaction.MsgBox( ex.Message, (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
            } 
        } 
        
        
        //  This procedure delets the addon files
        private void UnInstall() { 
            string path = null; 
            path = ReadPath(); //  Reads the addon path from the registry
            if ( path != "" ) { 
                try { 
                    //  Delete the addon EXE file
                    if ( System.IO.File.Exists( path + @"\HelloWorld.exe" ) ) { 
                        System.IO.File.Delete( path + @"\HelloWorld.exe" ); 
                        MessageBox.Show( path + @"\HelloWorld.exe was deleted" ); 
                    } 
                    else { 
                        MessageBox.Show( path + @"\HelloWorld.exe was not found" ); 
                    } 
                } 
                catch  { 
                    MessageBox.Show( path + " - ERROR UNINSTALLING" ); 
                } 
            } 
            else { 
                MessageBox.Show( "Path not found" ); 
            } 
            //  Terminate the application
            GC.Collect(); 
            System.Environment.Exit( 0 ); 
        } 
        
        
        //  This procedure copies the addon exe file to the installation folder        
        private void Install() { 
            try { 
                Environment.CurrentDirectory = strDll; //  For Dll function calls will work
                
                if ( chkDefaultFolder.Checked == false ) { //  Change the installation folder
                    string strTemp = txtDest.Text; 
                    SetAddOnFolder( strTemp ); 
                    strDest = txtDest.Text; 
                } 
                
                if ( !( ( System.IO.Directory.Exists( strDest ) ) ) ) { 
                    System.IO.Directory.CreateDirectory( strDest ); //  Create installation folder
                } 
                
                FileWatcher.Path = strDest; 
                FileWatcher.EnableRaisingEvents = true; 
                
                ExtractFile( strDest ); //  Extract add-on to installation folder
                
                while ( bFileCreated == false ) { 
                    Application.DoEvents(); 
                    // Don't continue running until the file is copied...
                } 
                
                if ( chkRestart.Checked ) { 
                    RestartNeeded(); //  Inform SBO the restart is needed
                } 
                EndInstall(); //  Inform SBO the installation ended
                // Write installation Folder to registry
                bool bAns = false; 
                
                // WriteToRegistry(RegistryHive.LocalMachine, "SOFTWARE", "path", "c:\folder")
                bAns = WriteToRegistry( RegistryHive.LocalMachine, "SOFTWARE", "HelloWorld", strDest ); 
                
                MessageBox.Show( "Finished Installing", "Installation ended", MessageBoxButtons.OK, MessageBoxIcon.Information ); 
                System.Windows.Forms.Application.Exit(); //  Exit the installer
            } 
            catch ( Exception ex ) { 
                Interaction.MsgBox( ex.Message, MsgBoxStyle.Information, "Addon Installer" ); 
            } 
        } 
        
        
        #endregion 
        
        #region 'Events' 
        private void frmInstall_Load( System.Object sender, System.EventArgs e ) { 
            try { 
                // Dim strAppPath As String
                
                //  The command line parameters, seperated by '|' will be broken to this array
                string[] strCmdLineElements = new string[ 3 ]; 
                
                string strCmdLine = null; //  The whole command line
                
                int NumOfParams = 0; // The number of parameters in the command line (should be 2)
                
                
                NumOfParams = Environment.GetCommandLineArgs().Length; 
                
                if ( NumOfParams == 2 ) { 
                    strCmdLine = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
                    if ( strCmdLine.ToUpper() == "/U" ) { 
                        UnInstall(); 
                    } 
                    strCmdLineElements = strCmdLine.Split( char.Parse( "|" ) ); 
                    
                    //  Get Install destination Folder
                    strDest = System.Convert.ToString( strCmdLineElements.GetValue( 0 ) ); 
                    txtDest.Text = strDest; 
                    
                    //  Get the "AddOnInstallAPI.dll" path
                    strDll = System.Convert.ToString( strCmdLineElements.GetValue( 1 ) ); 
                    strDll = strDll.Remove( ( strDll.Length - 19 ), 19 ); //  Only the path is needed
                } 
                else { 
                    MessageBox.Show( "This installer must be run from Sap Business One", "Incorrect installation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation ); 
                    System.Windows.Forms.Application.Exit(); 
                } 
            } 
            catch ( Exception ex ) { 
                ShowError( ex ); 
            } 
        } 
        
        
        private void cmdInstall_Click( System.Object sender, System.EventArgs e ) { 
            Install(); 
        } 
        
        private void chkDefaultFolder_CheckedChanged( System.Object sender, System.EventArgs e ) { 
            txtDest.Enabled = !( ( chkDefaultFolder.Checked ) ); 
        } 
        
        
        //  This event happens when the addon exe file is renamed to exe extention
        private void FileWatcher_Renamed( object sender, System.IO.RenamedEventArgs e ) { 
            bFileCreated = true; 
            FileWatcher.EnableRaisingEvents = false; 
        } 
        
        
        public void ShowError( Exception ex ) { 
            Interaction.MsgBox( ex.Message + Constants.vbNewLine + "Source:" + ex.StackTrace, MsgBoxStyle.Information, "Addon Installer" ); 
        } 
        
        #endregion 
        
        [STAThread]
        public static void Main() { Application.Run( new frmInstall() ); }
    } 
    
    
    
} 
