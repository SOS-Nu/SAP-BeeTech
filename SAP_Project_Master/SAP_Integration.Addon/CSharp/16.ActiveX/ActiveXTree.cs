using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
class ActiveXTree  { 
//  SAP MANAGE UIDI API 2006 SDK Sample
//****************************************************************************
//
//  File:      ActiveXTree.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
//****************************************************************************



//**************************************************************************************************
// BEFORE STARTING:
// 1. Add reference to the "SAP Business One UI API"
// 2. Add reference to the "SAP Business One Objects Bridge Version 2006"
// 3. Insert the development connection string to the "Command Line Argument"
//-----------------------------------------------------------------
// 1.
//    a. Project->References
//    b. check the "SAP Business One UI API 2007" check box
//
// 2.
//    a. Project->References
//    b. check the "SAP Business One Objects Bridge Version 2006" check box
//
// 3.
//     a. Project->Properties
//     b. choose 'Make' tab folder
//     c. place the following connection string in the 'Command Line Arguments' field
// 0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056
//
//**************************************************************************************************


//**********************************************************
// This parameter will use us to manipulate the
// SAP Business One Application a
// and the Company on which we are working
//**********************************************************

private SAPbouiCOM.Application SBO_Application; 
private SAPbobsCOM.Company oCompany; 
private MSComctlLib.TreeView oTreeView; 
private SAPbouiCOM.EditText oEdit; 

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


private int SetConnectionContext() { 
    int setConnectionContextReturn = 0;
    
    string sCookie = null; 
    string sConnectionContext = null; 
    int lRetCode = 0; 
    
    // First initialize the Company object
    
    oCompany = new SAPbobsCOM.Company(); 
    
    // Acquire the connection context cookie from the DI API.
    sCookie = oCompany.GetContextCookie(); 
    
    // Retrieve the connection context string from the UI API using the
    // acquired cookie.
    sConnectionContext = SBO_Application.Company.GetConnectionContext( sCookie ); 
    
    // before setting the SBO Login Context make sure the company is not
    // connected
    
    if ( oCompany.Connected == true ) { 
        oCompany.Disconnect(); 
    } 
    
    // Set the connection context information to the DI API.
    setConnectionContextReturn = oCompany.SetSboLoginContext( sConnectionContext ); 
    
    return setConnectionContextReturn;
} 


private int ConnectToCompany() { 
    int connectToCompanyReturn = 0;
    
    // Establish the connection to the company database.
    connectToCompanyReturn = oCompany.Connect(); 
    
    return connectToCompanyReturn;
} 

private void Class_Initialize() { 
    
    //*************************************************************
    // set SBO_Application with an initialized application object
    //*************************************************************
    
    SetApplication(); 
    
    //*************************************************************
    // Set The Connection Context
    //*************************************************************
    
    if ( !( SetConnectionContext() == 0 ) ) { 
        SBO_Application.MessageBox( "Failed setting a connection to DI API", 1, "Ok", "", "" ); 
        System.Environment.Exit( 0 ); //  Terminating the Add-On Application
    } 
    
    
    //*************************************************************
    // Connect To The Company Data Base
    //*************************************************************
    
    if ( !( ConnectToCompany() == 0 ) ) { 
        SBO_Application.MessageBox( "Failed connecting to the company's Data Base", 1, "Ok", "", "" ); 
        System.Environment.Exit( 0 ); //  Terminating the Add-On Application
    } 
    
    //*************************************************************
    // send an "hello world" message
    //*************************************************************
    
    SBO_Application.MessageBox( "DI Connected To: " + oCompany.CompanyName , 1, "Ok", "", "" ); 
    
    CreateFormWithActiveX(); 
} 

private void CreateFormWithActiveX() { 
    SAPbouiCOM.FormCreationParams CP = null; 
    SAPbouiCOM.Form fTree = null; 
    SAPbouiCOM.ActiveX AcXTree = null; 
    SAPbouiCOM.Item oItem = null; 
    SAPbouiCOM.StaticText oStatic = null; 
    SAPbouiCOM.LinkedButton oLink = null; 
    
    //  Set the form creation parameters
    CP = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
    CP.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed; 
    CP.FormType = "ACXTree"; 
    CP.UniqueID = "ACTree1"; 
    
    //  Add a new form
    fTree = SBO_Application.Forms.AddEx( CP ); 
    fTree.Left = 100; 
    fTree.Top = 100; 
    fTree.Width = 300; 
    fTree.Height = 300; 
    fTree.Height = 300; 
    fTree.Title = "TreeView - ActiveX"; 
    
    //  Add the edit text and linked button
    oItem = fTree.Items.Add( "staticBP", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
    oItem.Left = 20; 
    oItem.Top = 10; 
    oItem.Width = 200; 
    oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
    oStatic.Caption = "Business Parnters with contacts"; 
    
    //  Add the edit text and linked button
    oItem = fTree.Items.Add( "txtBP", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
    oItem.Left = 20; 
    oItem.Top = 250; 
    oItem.Width = 60; 
    oEdit = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
    
    oItem = fTree.Items.Add( "BPLink", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON ); 
    oItem.Left = 80; 
    oItem.Top = 250; 
    oItem.LinkTo = "txtBP"; 
    oLink = ( ( SAPbouiCOM.LinkedButton )( oItem.Specific ) ); 
    oLink.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner; 
    
    //  Add the TreeView Control to the form
    oItem = fTree.Items.Add( "Tree", SAPbouiCOM.BoFormItemTypes.it_ACTIVE_X ); 
    oItem.Left = 20; 
    oItem.Top = 40; 
    oItem.Width = 250; 
    oItem.Height = 200; 
    
    //  Create the new activeX control
    AcXTree = ( ( SAPbouiCOM.ActiveX )( oItem.Specific ) ); 
    AcXTree.ClassID = "MSComctlLib.TreeCtrl.2"; 
    oTreeView = ( ( MSComctlLib.TreeView )( AcXTree.Object ) ); 
    
    // Loading date to Tree Nodes
    MSComctlLib.Node BPNode = null; 
    MSComctlLib.Node ContactNode = null; 
    SAPbobsCOM.Recordset oRecordSet = null; 
    SAPbobsCOM.BusinessPartners oBPs = null; 
    int iContact = 0; 
    
    //  Reference all the Business Partners
    oRecordSet = ( ( SAPbobsCOM.Recordset )( oCompany.GetBusinessObject( SAPbobsCOM.BoObjectTypes.BoRecordset ) ) ); 
    oRecordSet.DoQuery( "SELECT * FROM OCRD" ); 
    oBPs = ( ( SAPbobsCOM.BusinessPartners )( oCompany.GetBusinessObject( SAPbobsCOM.BoObjectTypes.oBusinessPartners ) ) ); 
    oBPs.Browser.Recordset = oRecordSet; 
    
    //  Iterate all the BPs
    oBPs.Browser.MoveFirst(); 

	int j;

	for (j=1;j<oBPs.Browser.RecordCount;j++)
	{ 
        object NoValue = Type.Missing;
        object strBPCode = "B" + oBPs.CardCode; 
        object strBPName = oBPs.CardName; 

			
		BPNode = oTreeView.Nodes.Add( ref NoValue, ref NoValue, ref strBPCode, ref strBPName, ref NoValue, ref NoValue ); 

		for ( iContact=0; iContact<=oBPs.ContactEmployees.Count - 1; iContact++ ) 
		{ 
            oBPs.ContactEmployees.SetCurrentLine( iContact ); 
            if ( oBPs.ContactEmployees.Name != "" ) { 
                strBPCode = "C" + oBPs.CardCode + iContact.ToString(); 
                strBPName = oBPs.ContactEmployees.Name; 
					ContactNode = oTreeView.Nodes.Add(ref NoValue,ref NoValue, ref strBPCode, ref strBPName, ref NoValue, ref NoValue ); 
					ContactNode.Parent = BPNode; 
            } 
        }
			 oBPs.Browser.MoveNext(); 	       
    } 
    //  Make the form visible
    fTree.Visible = true; 
    
} 

public ActiveXTree() { 
    
    Class_Initialize(); 
    // events handled by oTreeView_NodeClick
    oTreeView.NodeClick += new MSComctlLib.ITreeViewEvents_NodeClickEventHandler( oTreeView_NodeClick ); 
} 

private void oTreeView_NodeClick( MSComctlLib.Node Node ) { 
    if ( oTreeView.SelectedItem.Key.StartsWith( "B" ) ) { 
        oEdit.Value = oTreeView.SelectedItem.Key.TrimStart( char.Parse( "B" ) ); 
    } 
} 

} 
    
