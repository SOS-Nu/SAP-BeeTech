//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      PrintEvent.cs
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
using System.Xml; 

using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
class PrintEvent : System.Windows.Forms.Form { 
    
    #region 'Windows Form Designer generated code' 
    
    public PrintEvent() { 
        
        
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
    internal System.Windows.Forms.TextBox printXML; 
    internal System.Windows.Forms.Label Label1; 
    internal System.Windows.Forms.Label Label2; 
    [ System.Diagnostics.DebuggerStepThrough() ]

    private void InitializeComponent() { 
		SetApplication();

        this.printXML = new System.Windows.Forms.TextBox(); 
        this.Label1 = new System.Windows.Forms.Label(); 
        this.Label2 = new System.Windows.Forms.Label(); 
        this.SuspendLayout(); 
        // 
        // printXML
        // 
        this.printXML.Location = new System.Drawing.Point( 16, 120 ); 
        this.printXML.Multiline = true; 
        this.printXML.Name = "printXML"; 
        this.printXML.Size = new System.Drawing.Size( 528, 224 ); 
        this.printXML.TabIndex = 2; 
        this.printXML.Text = ""; 
        // 
        // Label1
        // 
        this.Label1.Location = new System.Drawing.Point( 16, 16 ); 
        this.Label1.Name = "Label1"; 
        this.Label1.Size = new System.Drawing.Size( 528, 48 ); 
        this.Label1.TabIndex = 3; 
        this.Label1.Text = "This sample will respond to any print event sent from SBO and respont to it. It w" + "il ask the user if to catch it or not and if to present it in SBO or not."; 
        // 
        // Label2
        // 
        this.Label2.Location = new System.Drawing.Point( 16, 88 ); 
        this.Label2.Name = "Label2"; 
        this.Label2.Size = new System.Drawing.Size( 392, 23 ); 
        this.Label2.TabIndex = 4; 
        this.Label2.Text = "A report that was caught will be displayed here"; 
        // 
        // PrintEvent
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 ); 
        this.ClientSize = new System.Drawing.Size( 562, 367 ); 
        this.Controls.Add( this.Label2 ); 
        this.Controls.Add( this.Label1 ); 
        this.Controls.Add( this.printXML ); 
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; 
        this.MaximizeBox = false; 
        this.Name = "PrintEvent"; 
        this.Text = "Print Event"; 
        this.ResumeLayout( false ); 
        
		// events handled by SBO_Application_ReportDataEvent
         
        SBO_Application.ReportDataEvent += new SAPbouiCOM._IApplicationEvents_ReportDataEventEventHandler( SBO_Application_ReportDataEvent ); 
        // events handled by SBO_Application_PrintEvent
         
        SBO_Application.PrintEvent += new SAPbouiCOM._IApplicationEvents_PrintEventEventHandler( SBO_Application_PrintEvent ); 
        // events handled by PrintEvent_Load
         
        base.Load += new System.EventHandler( PrintEvent_Load ); 
    } 
    
    
    #endregion 
    
    private SAPbouiCOM.Application SBO_Application; 
    private bool IsRegistered; 
    
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
    
    
    private void SBO_Application_ReportDataEvent( ref SAPbouiCOM.ReportDataInfo reportDataInfo, out bool BubbleEvent ) { 
		BubbleEvent = true;
        
        if ( reportDataInfo.BeforeAction == true ) { // before action
            int ans = 0; 
            ans = System.Convert.ToInt32( Interaction.MsgBox( "A Report event was sent, do you wish to register to it?", MsgBoxStyle.YesNo, null ) ); 
            IsRegistered = false; 
            
            if ( ans == System.Convert.ToDouble( Constants.vbYes ) ) { 
                reportDataInfo.RegisterForReport( true ); 
                IsRegistered = true; 
            } 
        } 
        
        if ( reportDataInfo.BeforeAction == false ) { // after action
            Interaction.MsgBox( "ReportDataEvent after", (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
            if ( IsRegistered ) { 
                if ( ( reportDataInfo.GetPageCount() >= 1 ) ) { 
                    printXML.Text = reportDataInfo.GetReportData( 1, reportDataInfo.GetPageCount(), false ); 
                } 
            } 
        } 
    } 
    
    
    private void SBO_Application_PrintEvent( ref SAPbouiCOM.PrintEventInfo printeventInfo, out bool BubbleEvent ) { 
		BubbleEvent = true;
        if ( printeventInfo.BeforeAction == true ) { // before action
            int ans = 0; 
            ans = System.Convert.ToInt32( Interaction.MsgBox( "Do you want to view it in SBO?", MsgBoxStyle.YesNo, null ) ); 
            
            if ( ans == System.Convert.ToDouble( Constants.vbNo ) ) { 
                BubbleEvent = false; 
            } 
            
        } 
        
        if ( printeventInfo.BeforeAction == false ) { // after action
            Interaction.MsgBox( "PrintEvent after", (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
        } 
        
    } 
    
    
    private void PrintEvent_Load( System.Object sender, System.EventArgs e ) { 

		SBO_Application.MessageBox ("Print Event Addon is active.",1,"","","");
        
    } 
    
    
    [STAThread]
    public static void Main() { Application.Run( new PrintEvent() ); }
} 
