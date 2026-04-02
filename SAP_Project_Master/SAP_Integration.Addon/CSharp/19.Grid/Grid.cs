//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      Grid.cs
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
class GridSample  { 
    
    // **********************************************************
    //  This parameter will use us to manipulate the
    //  SAP Business One Application
    // **********************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.Form oForm; 
    private SAPbouiCOM.Item oItem; 
    // Private oGroup As SAPbouiCOM.CollapsibleColumns
    private SAPbouiCOM.Grid oGrid; 
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
        
        sPath = System.IO.Directory.GetParent( Application.StartupPath).ToString(); 
        sPath = System.IO.Directory.GetParent(sPath).ToString(); 
        oXmlDoc.Load( sPath + @"\" + FileName ); 
        
        //  load the form to the SBO application in one batch
		string strXML = oXmlDoc.InnerXml.ToString();
        SBO_Application.LoadBatchActions(ref strXML ); 
        
    } 
    
    
    public GridSample() { 
        
        
        // *************************************************************
        //  set SBO_Application with an initialized application object
        // *************************************************************
        SetApplication(); 
        
        //  Load the Tab Order form
        // LoadFromXML("TabOrder.srf")
        CreateForm(); 
        
        //  Get the added form object by using the form's UID
        oForm = SBO_Application.Forms.Item( "frmGrid" ); 
        
        
        //  Show the form
        oForm.Visible = true; 
        
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
    } 
    
    private void CreateForm() { 
        SAPbouiCOM.FormCreationParams CP = null; 
        SAPbouiCOM.StaticText oStat = null; 
        SAPbouiCOM.Button oBtn = null; 
        SAPbouiCOM.OptionBtn oOpt = null; 
        
        CP = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
        CP.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable; 
        CP.FormType = "SAMPLE"; 
        CP.UniqueID = "frmGrid"; 
        
        oForm = SBO_Application.Forms.AddEx( CP ); 
        
        //  Set form width and height
        oForm.Height = 305; 
        oForm.Width = 600; 
        oForm.Title = "UI Grid sample"; 
        
        //  Add static text
        oItem = oForm.Items.Add( "StatHeader", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 20; 
        oItem.Top = 20; 
        oItem.Width = 400; 
        
        oStat = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStat.Caption = "With the new Grid you can view your data in a grouped and collapsable tree"; 
        
        oItem = oForm.Items.Add( "StatHead", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 20; 
        oItem.Top = 40; 
        oItem.Width = 400; 
        
        oStat = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStat.Caption = "In this grid you can view all the invoices in the company"; 
        
        //  Add Collapse, expand and remove group buttons
        //  Colapse
        oItem = oForm.Items.Add( "btnCol", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
        
        oItem.Top = 80; 
        oItem.Left = 480; 
        
        oBtn = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
        oBtn.Caption = "Collapse"; 
        //  Expand
        oItem = oForm.Items.Add( "btnExp", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
        
        oItem.Top = 110; 
        oItem.Left = 480; 
        
        oBtn = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
        oBtn.Caption = "Expand"; 
        
        //  Add option buttons and a static headline
        oItem = oForm.Items.Add( "StatGroup", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 470; 
        oItem.Top = 140; 
        oItem.Width = 80; 
        
        oStat = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStat.Caption = "Grouping"; 
        
        //  Option buttons
        //  Create a user Datasource for the option buttons
        userDS = oForm.DataSources.UserDataSources.Add( "OpBtnDS", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        
        //  No Grouping
        oItem = oForm.Items.Add( "optNo", SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON ); 
        oItem.Left = 480; 
        oItem.Top = 160; 
        
        oOpt = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOpt.Caption = "No grouping"; 
        oOpt.DataBind.SetBound( true, "", "OpBtnDS" ); 
        // CardCode
        oItem = oForm.Items.Add( "optCode", SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON ); 
        oItem.Left = 480; 
        oItem.Top = 180; 
        
        oOpt = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOpt.Caption = "Card Code"; 
        oOpt.GroupWith( "optNo" ); 
        oOpt.DataBind.SetBound( true, "", "OpBtnDS" ); 
        // Doc Date
        oItem = oForm.Items.Add( "optDate", SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON ); 
        oItem.Left = 480; 
        oItem.Top = 200; 
        
        oOpt = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOpt.Caption = "Doc Date"; 
        oOpt.GroupWith( "optNo" ); 
        oOpt.DataBind.SetBound( true, "", "OpBtnDS" ); 
        // Doc Status
        oItem = oForm.Items.Add( "optStatus", SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON ); 
        oItem.Left = 480; 
        oItem.Top = 220; 
        
        oOpt = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
        oOpt.Caption = "Doc Status"; 
        oOpt.GroupWith( "optNo" ); 
        oOpt.DataBind.SetBound( true, "", "OpBtnDS" ); 
        
        
        //  Add a Grid item to the form
        oItem = oForm.Items.Add( "MyGrid", SAPbouiCOM.BoFormItemTypes.it_GRID ); 
        //  Set the grid dimentions and position
        oItem.Left = 20; 
        oItem.Top = 60; 
        oItem.Width = 430; 
        oItem.Height = 200; 
        
        //  Set the grid data
        oGrid = ( ( SAPbouiCOM.Grid )( oItem.Specific ) ); 
        
        oForm.DataSources.DataTables.Add( "MyDataTable" ); 
        oForm.DataSources.DataTables.Item( 0 ).ExecuteQuery( "select CardCode, CardName, DocDate, DocNum, DocTotal, DocStatus from OINV" ); 
        oGrid.DataTable = oForm.DataSources.DataTables.Item( "MyDataTable" ); 
        
        //  Set columns size
        oGrid.Columns.Item( 0 ).Width = 50; 
        oGrid.Columns.Item( 1 ).Width = 60; 
        oGrid.Columns.Item( 2 ).Width = 130; 
        
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
        
        if ( ( FormUID == "frmGrid" ) & ( ( pVal.ItemUID == "optNo" ) | ( pVal.ItemUID == "optCode" ) | ( pVal.ItemUID == "optDate" ) | ( pVal.ItemUID == "optStatus" ) ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED ) & ( pVal.BeforeAction == false ) ) { 
            if ( ( double.Parse( userDS.Value ) == 1 ) ) { 
                oForm.Freeze( true ); 
                oForm.DataSources.DataTables.Item( 0 ).ExecuteQuery( "select CardCode, CardName, DocDate, DocNum, DocTotal, DocStatus from OINV" ); 
                oGrid.CollapseLevel = 0; 
                oForm.Freeze( false ); 
            } 
            else if ( ( double.Parse( userDS.Value ) == 2 ) ) { 
                oForm.Freeze( true ); 
                oForm.DataSources.DataTables.Item( 0 ).ExecuteQuery( "select CardCode, CardName, DocDate, DocNum, DocTotal, DocStatus from OINV" ); 
                oGrid.CollapseLevel = 1; 
                oForm.Freeze( false ); 
            } 
            else if ( ( double.Parse( userDS.Value ) == 3 ) ) { 
                oForm.Freeze( true ); 
                oForm.DataSources.DataTables.Item( 0 ).ExecuteQuery( "select DocDate, CardCode, CardName, DocNum, DocTotal, DocStatus from OINV" ); 
                oGrid.CollapseLevel = 1; 
                oForm.Freeze( false ); 
            } 
            else if ( ( double.Parse( userDS.Value ) == 4 ) ) { 
                oForm.Freeze( true ); 
                oForm.DataSources.DataTables.Item( 0 ).ExecuteQuery( "select DocStatus, DocDate, CardCode, CardName, DocNum, DocTotal from OINV" ); 
                oGrid.CollapseLevel = 2; 
                oForm.Freeze( false ); 
            } 
        } 
        
        if ( ( FormUID == "frmGrid" ) & ( pVal.BeforeAction == false ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK ) & ( ( pVal.ItemUID == "btnCol" ) | ( pVal.ItemUID == "btnExp" ) ) ) { 
            if ( ( pVal.ItemUID == "btnCol" ) ) { 
                oGrid.Rows.CollapseAll(); 
            } 
            if ( ( pVal.ItemUID == "btnExp" ) ) { 
                oGrid.Rows.ExpandAll(); 
            } 
        } 
        
        //  Close the add-on if the form closes
        if ( ( FormUID == "frmGrid" ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_CLOSE ) ) { 
            System.Windows.Forms.Application.Exit(); 
        }   
    }    
} 
