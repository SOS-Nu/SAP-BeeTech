//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      TabOrder.cs
// 
//   Copyright (c) SAP MANAGE
// 
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// 
//  BEFORE STARTING:
//  1. Add reference to the "SAP Business One UI API"
//  2. Insert the development connection string to the "Command line argument"
// -----------------------------------------------------------------
//  1.
//     a. Project->Add Reference...
//     b. select the "SAP Business One UI API 2007" From the COM folder
// 
//  2.
//      a. Project->Properties...
//      b. choose Configuration Properties folder (place the arrow on Debugging)
//      c. place the following connection string in the 'Command line arguments' field
//  0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056
// 
// **************************************************************************************************


using System;
using System.Windows.Forms;
class TabOrder  { 
    
    // **********************************************************
    //  This parameter will use us to manipulate the
    //  SAP Business One Application
    // **********************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.Form oForm; 
    private SAPbouiCOM.Item oItem; 
    private SAPbouiCOM.OptionBtn oOptionBtnHor; 
    private SAPbouiCOM.OptionBtn oOptionBtnVer; 
    private SAPbouiCOM.UserDataSource userDS; 
    
    private void SetApplication() { 
        
        // *******************************************************************
        //  Use an SboGuiApi object to establish connection
        //  with the SAP Business One application and return an
        //  initialized appliction object
        // *******************************************************************
        
        SAPbouiCOM.SboGuiApi SboGuiApi = null; 
        string sConnectionString = null; 
        
        SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
        
        //  by following the steps specified above, the following
        //  statment should be suficient for either development or run mode
        
        sConnectionString = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
        
        //  connect to a running SBO Application
        
        SboGuiApi.Connect( sConnectionString ); 
        
        //  get an initialized application object
        
        SBO_Application = SboGuiApi.GetApplication( -1 ); 
        
    } 
    
    
    private void LoadFromXML( ref string FileName ) { 
        
        System.Xml.XmlDocument oXmlDoc = null; 
        
        oXmlDoc = new System.Xml.XmlDocument(); 
        
        //  load the content of the XML File
        string sPath = null; 
        
        sPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent( Application.StartupPath ).ToString()).ToString(); 
        
        oXmlDoc.Load( sPath + @"\" + FileName ); 
        
        //  load the form to the SBO application in one batch
		string strXML = oXmlDoc.InnerXml.ToString();
        SBO_Application.LoadBatchActions( ref strXML); 
        
    } 
    
    
    public TabOrder() { 
        
        
        // *************************************************************
        //  set SBO_Application with an initialized application object
        // *************************************************************
        SetApplication(); 
        
        //  Load the Tab Order form
        string strTmp = "TabOrder.srf"; 
        LoadFromXML( ref strTmp ); 
        
        //  Get the added form object by using the form's UID
        oForm = SBO_Application.Forms.Item( "frmTab" ); 
        
        //  Create a user Datasource for the 
        userDS = oForm.DataSources.UserDataSources.Add( "OpBtnDS", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        
        //  Set binding and grouping for the option buttons
        oItem = oForm.Items.Item( "optHor" ); 
        
        oOptionBtnHor = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOptionBtnHor.GroupWith( "optVer" ); 
        oOptionBtnHor.DataBind.SetBound( true, "", "OpBtnDS" ); 
        
        oItem = oForm.Items.Item( "optVer" ); 
        
        oOptionBtnHor = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOptionBtnHor.GroupWith( "optHor" ); 
        oOptionBtnHor.DataBind.SetBound( true, "", "OpBtnDS" ); 
        
        //  Set the default to horizontal tabbing
        oItem = oForm.Items.Item( "optHor" ); 
        oOptionBtnHor = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOptionBtnHor.Selected = true; 
		SetTabHorizontal();
        
        //  Show the form
        oForm.Visible = true; 
        
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
    } 
    private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
		BubbleEvent = true;
        
        // *************************************************************************
        //  BubbleEvent sets the behavior of SAP Business One.
        //  False means that the application will not continue processing this event.
        // *************************************************************************
        BubbleEvent = true; 
        SAPbouiCOM.BoEventTypes EventEnum = 0; 
        EventEnum = pVal.EventType; 
        
        if ( ( FormUID == "frmTab" ) & ( ( pVal.ItemUID == "optHor" ) | ( pVal.ItemUID == "optVer" ) ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED ) & ( pVal.BeforeAction == false ) ) { 
            if ( ( double.Parse( userDS.Value ) == 1 ) ) { 
                SetTabVertical(); 
            } 
            else if ( ( double.Parse( userDS.Value ) == 2 ) ) { 
                SetTabHorizontal(); 
            } 
        } 
        
        //  Close the add-on if the form closes
        if ( ( FormUID == "frmTab" ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_CLOSE ) ) { 
            System.Windows.Forms.Application.Exit(); 
        } 
        
    } 
    
    
    private void SetTabVertical() { 
        int i = 0, t = 0, diff = 0; 
        SAPbouiCOM.EditText oEdit = null; 
        
        diff = 50; 
        
        for ( i=20; i<=29; i++ ) { 
            oItem = oForm.Items.Item( i.ToString() ); 
            oEdit = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
            
            oEdit.TabOrder = i + diff; 
        } 
    } 
    
    private void SetTabHorizontal() { 
        int i = 0, t = 0, left = 0, right = 0, diff = 0; 
        SAPbouiCOM.EditText oEdit = null; 
        
        left = 1; 
        right = 2; 
        diff = 100; 
        
        for ( i=20; i<=29; i++ ) { 
            oItem = oForm.Items.Item( i.ToString() ); 
            oEdit = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
            
            if ( i < 25 ) { 
                oEdit.TabOrder = left + diff; 
                left = left + 2; 
            } 
            else { 
                oEdit.TabOrder = right + diff; 
                right = right + 2; 
            } 
        } 
        
    } 
    
} 
