//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      ComplexForm.cs
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
class ComplexForm  { 
    
    //*****************************************************************
    // At the begining of every UI API project we should first
    // establish connection with a running SBO application.
    // *******************************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.Form oForm; 
    
    private void SetApplication() { 
        
        // *******************************************************************
        // Use an SboGuiApi object to establish the connection
        // with the application and return an initialized appliction object
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
    
    
    private void CreateMyComplexForm() { 
        
        SAPbouiCOM.Item oItem = null; 
        
        // *******************************************
        // we will use the following objects to set
        // the specific values of every item
        // we add.
        // this is the best way to do so
        //*********************************************
        
        SAPbouiCOM.Button oButton = null; 
        SAPbouiCOM.Folder oFolder = null; 
        SAPbouiCOM.OptionBtn oOptionBtn = null; 
        SAPbouiCOM.CheckBox oCheckBox = null; 
        SAPbouiCOM.ComboBox oComboBox = null; 
        
        short i = 0; // to be used as a counter
        
        // add a new form
        SAPbouiCOM.FormCreationParams oCreationParams = null; 
        oCreationParams = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
        
        oCreationParams.UniqueID = "MyComplexForm"; 
        oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable; 
        
        // oForm = SBO_Application.Forms.Add("MyComplexForm", SAPbouiCOM.BoFormTypes.ft_Sizable)
        oForm = SBO_Application.Forms.AddEx( oCreationParams ); 
        
        // add a user data source
        // bare in mind that every item must be connected to a data source
        
        oForm.DataSources.UserDataSources.Add( "FolderDS", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        oForm.DataSources.UserDataSources.Add( "OpBtnDS", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        oForm.DataSources.UserDataSources.Add( "CheckDS1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        oForm.DataSources.UserDataSources.Add( "CheckDS2", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        oForm.DataSources.UserDataSources.Add( "CheckDS3", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
        
        
        
        // set the form properties
        oForm.Title = "Complex Form"; 
        oForm.Left = 300; 
        oForm.ClientWidth = 200; 
        oForm.Top = 100; 
        oForm.ClientHeight = 140; 
        
        //*****************************************
        // Adding Items to the form
        // and setting their properties
        //*****************************************
        
        // /**********************
        // Adding an Ok button
        //*********************
        
        // We get automatic event handling for
        // the Ok and Cancel Buttons by setting
        // their UIDs to 1 and 2 respectively
        
        oItem = oForm.Items.Add( "1", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
        oItem.Left = 5; 
        oItem.Width = 65; 
        oItem.Top = 110; 
        oItem.Height = 19; 
        
        oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
        
        oButton.Caption = "Ok"; 
        
        //************************
        // Adding a Cancel button
        //***********************
        
        oItem = oForm.Items.Add( "2", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
        oItem.Left = 75; 
        oItem.Width = 65; 
        oItem.Top = 110; 
        oItem.Height = 19; 
        
        oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
        
        oButton.Caption = "Cancel"; 
        
        //****************************
        // Adding a Rectangle Item
        // for cosmetic purposes only
        //****************************
        
        oItem = oForm.Items.Add( "Rect1", SAPbouiCOM.BoFormItemTypes.it_RECTANGLE ); 
        oItem.Left = 0; 
        oItem.Width = 194; 
        oItem.Top = 22; 
        oItem.Height = 80; 
        
        //***************************
        // Adding Folder items
        //***************************
        
        for ( i=1; i<=2; i++ ) { 
            
            oItem = oForm.Items.Add( "Folder" + i, SAPbouiCOM.BoFormItemTypes.it_FOLDER ); 
            oItem.Left = ( i - 1 ) * 100; 
            oItem.Width = 100; 
            oItem.Top = 6; 
            oItem.Height = 19; 
            
            oFolder = ( ( SAPbouiCOM.Folder )( oItem.Specific ) ); 
            
            // set the caption
            oFolder.Caption = "Folder" + i; 
            oFolder.DataBind.SetBound( true, "", "FolderDS" ); 
            
            if ( i == 1 ) { 
                oFolder.Select(); 
            } 
            else { 
                oFolder.GroupWith( ( "Folder" + ((int)(i - 1)).ToString() ) ); 
            } 
        } 
        
        //****************************
        // Adding Option button items
        //****************************
        
        for ( i=1; i<=3; i++ ) { 
            
            oItem = oForm.Items.Add( "OpBtn" + i, SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON ); 
            oItem.Left = 20; 
            oItem.Width = 100; 
            oItem.Top = 30 + ( i - 1 ) * 19; 
            oItem.Height = 19; 
            
            
            // set the Item's Pane Level.
            // by setting the Form's pane level
            // this value will determine the Items visibility
            oItem.FromPane = 1; 
            oItem.ToPane = 1; 
            
            oOptionBtn = ( ( SAPbouiCOM.OptionBtn )( oItem.Specific ) ); 
            
            // set the caption
            oOptionBtn.Caption = "Option Button" + i; 
            
            if ( i > 1 ) { 
                oOptionBtn.GroupWith( ( "OpBtn" + ((int)(i - 1)).ToString() ) ); 
            } 
            
            oOptionBtn.DataBind.SetBound( true, "", "OpBtnDS" ); 
            
        } 
        
        //***************************
        // Adding Check Box items
        //***************************
        
        for ( i=1; i<=3; i++ ) { 
            
            oItem = oForm.Items.Add( "CheckBox" + i, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX ); 
            oItem.Left = 20; 
            oItem.Width = 100; 
            oItem.Top = 30 + ( i - 1 ) * 19; 
            oItem.Height = 19; 
            
            // set the Item's Pane Level.
            // by setting the Form's pane level
            // this value will determine the Items visibility
            oItem.FromPane = 2; 
            oItem.ToPane = 2; 
            
            oCheckBox = ( ( SAPbouiCOM.CheckBox )( oItem.Specific ) ); 
            
            // set the caption
            oCheckBox.Caption = "Check Box" + i; 
            
            // binding the Check box with a data source
            oCheckBox.DataBind.SetBound( true, "", "CheckDS" + i ); 
            
        } 
        
        //****************************************
        // Show the Form:
        // we will use the Item Event to Control
        // the behavior of this form
        //****************************************
        oForm.PaneLevel = 1; 
    } 
    
    
    public void RearrangeItems() { 
        
        // this method will update the items on the form
        // after a resize event
        
        SAPbouiCOM.Button oButton = null; 
        SAPbouiCOM.Folder oFolder = null; 
        SAPbouiCOM.OptionBtn oOptionBtn = null; 
        SAPbouiCOM.CheckBox oCheckBox = null; 
        SAPbouiCOM.Item oItem = null; 
        int i = 0; 
        
        // get the required item from the form
        // and change his attributes according to the
        // new form size
        
        //************************
        // Changing the OK button
        //***********************
        
        oItem = oForm.Items.Item( "1" ); 
        // oItem.Left = 5
        // oItem.Width = 65
        oItem.Top = oForm.Height - 50; 
        // oItem.Height = 19
        
        
        //************************
        // Changing the Cancel button
        //***********************
        
        oItem = oForm.Items.Item( "2" ); 
        // oItem.Left = 75
        // oItem.Width = 65
        oItem.Top = oForm.Height - 50; 
        // oItem.Height = 19
        
        
        //****************************
        // Changing the Rectangle Item
        //****************************
        
        oItem = oForm.Items.Item( "Rect1" ); 
        // oItem.Left = 0
        oItem.Width = oForm.Width - 8; 
        // oItem.Top = 22
        oItem.Height = oForm.Height - 80; 
        
        
        //**************************************
        // Changing the Option button items
        //**************************************
        
        for ( i=1; i<=3; i++ ) { 
            
            oItem = oForm.Items.Item( "OpBtn" + i ); 
            oItem.Left = 20; 
            oItem.Width = 100; 
            oItem.Top = 30 + ( i - 1 ) * 19; 
            oItem.Height = 19; 
        } 
        
        //******************************
        // Changing the Check Box items
        //******************************
        
        for ( i=1; i<=3; i++ ) { 
            
            oItem = oForm.Items.Item( "CheckBox" + i ); 
            oItem.Left = 20; 
            oItem.Width = 100; 
            oItem.Top = 30 + ( i - 1 ) * 19; 
            oItem.Height = 19; 
            
        } 
    } 
    
    
    public ComplexForm() { 
        
        
        //*************************************************************
        // set SBO_Application with an initialized application object
        //*************************************************************
        
        SetApplication(); 
        
        //*************************************************************
        // Create the Form
        //*************************************************************
        
        CreateMyComplexForm(); 
        
        oForm.Visible = true; 
        
        ((SAPbouiCOM.OptionBtn)(oForm.Items.Item( "OpBtn1" ).Specific)).Selected = true; 
        
        ((SAPbouiCOM.Folder)(oForm.Items.Item( "Folder1" ).Specific)).Select(); 
        
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
        // events handled by SBO_Application_AppEvent
        SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler( SBO_Application_AppEvent ); 
    } 
    
    private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 

		BubbleEvent = true;
        
        if ( FormUID == "MyComplexForm" ) { 
            
            oForm = SBO_Application.Forms.Item( FormUID ); 
            
            switch ( pVal.EventType ) {
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    
                    //************************************************************
                    // Check if the event was raised by one of the Folder items
                    // and change the form's pane level
                    //************************************************************
                    if ( pVal.ItemUID == "Folder1" ) { 
                        oForm.PaneLevel = 1; 
                    } 
                    
                    if ( pVal.ItemUID == "Folder2" ) { 
                        oForm.PaneLevel = 2; 
                    } 
                    
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    
                    // first freeze the form so that all changes
                    // would be updated at one batch
                    oForm.Freeze( true ); 
                    // rearange the the items according to the
                    // new size of the form
                    RearrangeItems(); 
                    // unfreeze the form
                    oForm.Freeze( false ); 
                    // update the changes
                    // all changes will be updated in one batch
                    oForm.Update(); 
                    
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_CLOSE:
                    
                    System.Windows.Forms.Application.Exit(); 
                    
                    break;
            }  
        }   
    } 
    
    
    private void SBO_Application_AppEvent( SAPbouiCOM.BoAppEventTypes EventType ) { 
        
        switch ( EventType ) {
            case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                
                SBO_Application.MessageBox( "A Shut Down Event has been caught" + Environment.NewLine + "Terminating 'Complex Form' Add On...", 1, "Ok", "", "" ); 
                
                //**************************************************************
                //
                // Take care of terminating your AddOn application
                //
                //**************************************************************
                
                System.Windows.Forms.Application.Exit(); 
                
                break;
        }  
    }   
} 
