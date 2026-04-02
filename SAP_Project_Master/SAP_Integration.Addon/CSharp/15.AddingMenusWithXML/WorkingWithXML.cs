//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      WorkingWithXML.cs
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
class WorkingWithXML  { 
    
    //**********************************************************
    // This parameter will use us to manipulate the
    // SAP Business One Application
    //**********************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.Form oForm; 
    
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
    
    
    private void LoadFromXML( ref string FileName ) { 
        
        System.Xml.XmlDocument oXmlDoc = null; 
        
        oXmlDoc = new System.Xml.XmlDocument(); 
        
        // load the content of the XML File
        string sPath = null; 
        
        sPath = System.IO.Directory.GetParent( Application.StartupPath ).ToString(); 
		sPath = System.IO.Directory.GetParent(sPath).ToString(); 
        
        oXmlDoc.Load( sPath + @"\" + FileName ); 
        
        // load the form to the SBO application in one batch
		string tmpStr;
		tmpStr = oXmlDoc.InnerXml;
        SBO_Application.LoadBatchActions( ref tmpStr ); 
        sPath = SBO_Application.GetLastBatchResults(); 
        
    } 
    
    
    
    private void SaveAsXML( ref SAPbouiCOM.Form Form ) { 
        
        System.Xml.XmlDocument oXmlDoc = null; 
        string sXmlString = null; 
        
        oXmlDoc = new System.Xml.XmlDocument(); 
        
        // get the form as an XML string
        sXmlString = Form.GetAsXML(); 
        
        // load the form's XML string to the
        // XML document object
        oXmlDoc.LoadXml( sXmlString ); 
        
        string sPath = null; 
        
        sPath = System.IO.Directory.GetParent( Application.StartupPath ).ToString(); 
        
        // save the XML Document
        oXmlDoc.Save( ( @"C:\MySimpleForm.xml" ) ); 
        
    } 
    
    
    public WorkingWithXML() { 
        
        
        //*************************************************************
        // set SBO_Application with an initialized application object
        //*************************************************************
        SetApplication(); 
        
        string strTemp = "MyMenus.xml"; 
        LoadFromXML( ref strTemp ); 
        
    } 
} 
