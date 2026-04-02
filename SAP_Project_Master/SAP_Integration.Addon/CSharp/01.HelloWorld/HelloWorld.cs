//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      HelloWorld.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
//****************************************************************************

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
class HelloWorld  { 
    
    //**********************************************************
    // This parameter will use us to manipulate the
    // SAP Business One Application
    //**********************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    
    private void SetApplication() { 
        
        // *******************************************************************
        // Use an SboGuiApi object to establish connection
        // with the SAP Business One application and return an
        // initialized appliction object
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
    
    
    public HelloWorld() { 
        
        
        //*************************************************************
        // set SBO_Application with an initialized application object
        //*************************************************************
        
        SetApplication(); 
        
        //*************************************************************
        // send an "hello world" message
        //*************************************************************
        
        SBO_Application.MessageBox( "Hello World", 1, "Ok", "", "" ); 
        
    } 
} 

