//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      ProgressBar.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// BEFORE STARTING:
// 1. Add reference to the "SAP Business One UI API"
// 2. Insert the development connection string to the "Command line argument"
//-----------------------------------------------------------------
// 1.
//    a. Project->Add Reference...
//    b. select the "SAP Business One UI API 2007" From the COM folder
//
// 2.
//     a. Project->Properties...
//     b. choose Configuration Properties folder (place the arrow on Debugging)
//     c. place the following connection string in the 'Command line arguments' field
// 0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056
//
//**************************************************************************************************

using System;
using System.Windows.Forms;

class ProgressBar : System.Windows.Forms.Form { 
    
    #region ' Windows Form Designer generated code ' 
    
    public ProgressBar() { 
        
        
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
    internal System.Windows.Forms.Button cmdStart; 
    internal System.Windows.Forms.Button cmdFoward; 
    internal System.Windows.Forms.Button cmdBack; 
    internal System.Windows.Forms.Button cmdStop; 
    [ System.Diagnostics.DebuggerStepThrough() ]
    private void InitializeComponent() { 
        this.cmdStart = new System.Windows.Forms.Button(); 
        this.cmdFoward = new System.Windows.Forms.Button(); 
        this.cmdBack = new System.Windows.Forms.Button(); 
        this.cmdStop = new System.Windows.Forms.Button(); 
        this.SuspendLayout(); 
        // 
        // cmdStart
        // 
        this.cmdStart.Location = new System.Drawing.Point( 72, 8 ); 
        this.cmdStart.Name = "cmdStart"; 
        this.cmdStart.TabIndex = 0; 
        this.cmdStart.Text = "Start"; 
        // 
        // cmdFoward
        // 
        this.cmdFoward.Enabled = false; 
        this.cmdFoward.Location = new System.Drawing.Point( 120, 48 ); 
        this.cmdFoward.Name = "cmdFoward"; 
        this.cmdFoward.TabIndex = 1; 
        this.cmdFoward.Text = "Foward >>"; 
        // 
        // cmdBack
        // 
        this.cmdBack.Enabled = false; 
        this.cmdBack.Location = new System.Drawing.Point( 24, 48 ); 
        this.cmdBack.Name = "cmdBack"; 
        this.cmdBack.TabIndex = 2; 
        this.cmdBack.Text = "<< Back"; 
        // 
        // cmdStop
        // 
        this.cmdStop.Enabled = false; 
        this.cmdStop.Location = new System.Drawing.Point( 72, 88 ); 
        this.cmdStop.Name = "cmdStop"; 
        this.cmdStop.TabIndex = 3; 
        this.cmdStop.Text = "Stop"; 
        // 
        // ProgressBar
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 ); 
        this.ClientSize = new System.Drawing.Size( 226, 127 ); 
        this.Controls.Add( this.cmdStop ); 
        this.Controls.Add( this.cmdBack ); 
        this.Controls.Add( this.cmdFoward ); 
        this.Controls.Add( this.cmdStart ); 
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; 
        this.MaximizeBox = false; 
        this.Name = "ProgressBar"; 
        this.Text = "ProgressBar"; 
        this.TopMost = true; 
        this.ResumeLayout( false ); 
        
		SetApplication();
        // events handled by ProgressBar_Load
        base.Load += new System.EventHandler( ProgressBar_Load ); 
        // events handled by cmdStart_Click
        cmdStart.Click += new System.EventHandler( cmdStart_Click ); 
        // events handled by cmdFoward_Click
        cmdFoward.Click += new System.EventHandler( cmdFoward_Click ); 
        // events handled by cmdBack_Click
        cmdBack.Click += new System.EventHandler( cmdBack_Click ); 
        // events handled by cmdStop_Click
        cmdStop.Click += new System.EventHandler( cmdStop_Click ); 
        // events handled by SBO_Application_ProgressBarEvent
        SBO_Application.ProgressBarEvent += new SAPbouiCOM._IApplicationEvents_ProgressBarEventEventHandler( SBO_Application_ProgressBarEvent ); 
    } 
    
    
    #endregion 
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.ProgressBar oProgBar; //  This is the progress bar
    private void ProgressBar_Load( System.Object sender, System.EventArgs e ) { 
        SetApplication(); 
    } 
    
    private void SetApplication() { 
        
        // *******************************************************************
        // Use an SboGuiApi object to establish the connection
        // with the application and rturn an initialized appliction object
        // *******************************************************************
        
        SAPbouiCOM.SboGuiApi SboGuiApi = null; 
        string sConnectionString = null; 
        
        SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
        
        // by following the steps specified above, the following
        // statment should be suficient for either development or run mode
        
        sConnectionString = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
        
        // connect to a running SBO Application
        
        SboGuiApi.Connect( sConnectionString ); 
        
        // get an initialized application object
        
        SBO_Application = SboGuiApi.GetApplication( -1 ); 
        
    } 
    
    
    private void cmdStart_Click( System.Object sender, System.EventArgs e ) { 
        // Create a Progress Bar
        oProgBar = SBO_Application.StatusBar.CreateProgressBar( "Sample Progress Bar", 27, true ); 
        
        // Enable the progress bar controls
        cmdFoward.Enabled = true; 
        cmdBack.Enabled = true; 
        cmdStop.Enabled = true; 
        
        // Disable the 'Start' button
        cmdStart.Enabled = false; 
    } 
    
    
    private void cmdFoward_Click( System.Object sender, System.EventArgs e ) { 
        oProgBar.Value += 1; 
    } 
    
    
    private void cmdBack_Click( System.Object sender, System.EventArgs e ) { 
        oProgBar.Value -= 1; 
    } 
    
    
    private void cmdStop_Click( System.Object sender, System.EventArgs e ) { 
        ReleaseBar(); 
    } 
    
    private void ReleaseBar() { 
        // Stopping the progress bar, thus loosing it's values.
        oProgBar.Stop(); 
        
        // Disable the progress bar controls
        cmdFoward.Enabled = false; 
        cmdBack.Enabled = false; 
        cmdStop.Enabled = false; 
        
        // Enable the 'Start' button
        cmdStart.Enabled = true; 
        
    } 
    
    private void SBO_Application_ProgressBarEvent( ref SAPbouiCOM.ProgressBarEvent pVal, out bool BubbleEvent ) { 
        BubbleEvent = true;
		if ( pVal.EventType == SAPbouiCOM.BoProgressBarEventTypes.pbet_ProgressBarStopped & pVal.BeforeAction ) { 
            SBO_Application.MessageBox( "Progress Bar stopped by user, releasing progress bar", 1, "Ok", "", "" ); 
            ReleaseBar(); 
        } 
    } 
    
    [STAThread]
    public static void Main() { Application.Run( new ProgressBar() ); }
} 
