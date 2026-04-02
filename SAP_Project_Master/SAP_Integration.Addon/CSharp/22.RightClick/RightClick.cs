//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      RightClick.cs
// 
//   Copyright (c) SAP MANAGE
// 
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// 
// ****************************************************************************
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
class RightClick  { 
    
    // **********************************************************
    //  This parameter will use us to manipulate the
    //  SAP Business One Application
    // **********************************************************
    
    private SAPbouiCOM.Application SBO_Application; 
    private SAPbouiCOM.Form oForm; 
    
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
    
    
    public RightClick() { 
        
        
        // *************************************************************
        //  set SBO_Application with an initialized application object
        // *************************************************************
        
        SetApplication(); 
        
        AddMenu(); 
        
        CreateForm(); 
        
        // events handled by SBO_Application_MenuEvent
        SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler( SBO_Application_MenuEvent ); 
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
        // events handled by SBO_Application_RightClickEvent
        SBO_Application.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler( SBO_Application_RightClickEvent ); 
    } 
    
    private void AddMenu() { 
        SAPbouiCOM.MenuItem oMenuItem = null; 
        SAPbouiCOM.Menus oMenus = null; 
        
        
        try { 
            //  Create menu popup MyUserMenu01 and add it to Tools menu
            SAPbouiCOM.MenuCreationParams oCreationPackage = null; 
            oCreationPackage = ( ( SAPbouiCOM.MenuCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams ) ) ); 
            
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP; 
            oCreationPackage.UniqueID = "AlwaysAppear"; 
            oCreationPackage.String = "Always Appear"; 
            oCreationPackage.Enabled = true; 
            
            oMenuItem = SBO_Application.Menus.Item( "1280" ); // Data'
            oMenus = oMenuItem.SubMenus; 
            oMenus.AddEx( oCreationPackage ); 
            
            // Create sub menu MySubMenu1 and add it to popup MyUserMenu01
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING; 
            oCreationPackage.UniqueID = "AlwaysPop"; 
            oCreationPackage.String = "In Data Menu and In RightClick"; 
            oCreationPackage.Enabled = true; 
            
            oMenuItem = SBO_Application.Menus.Item( "AlwaysAppear" ); 
            oMenus = oMenuItem.SubMenus; 
            oMenus.AddEx( oCreationPackage ); 
            
        } 
        catch ( Exception ex ) { 
            MessageBox.Show( ex.Message ); 
        } 
        
    } 
    
    
    private void CreateForm() { 
        
        SAPbouiCOM.FormCreationParams oCP = null; 
        SAPbouiCOM.Item oItem = null; 
        SAPbouiCOM.StaticText oStatic = null; 
        SAPbouiCOM.Button oButton = null; 
        SAPbouiCOM.EditText oEdit = null; 
        
        //  Setting the form creation params
        oCP = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
        oCP.UniqueID = "RightClk"; 
        oCP.FormType = "RightClk"; 
        oCP.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable; 
        
        //  Adding the form
        oForm = SBO_Application.Forms.AddEx( oCP ); 
        oForm.Title = "Right Click Sample"; 
        
        //  Adding a text box
        oItem = oForm.Items.Add( "EditTxt", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
        oItem.Left = 170; 
        oItem.Top = 90; 
        oItem.LinkTo = "StaticTxt"; 
        oEdit = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
        
        //  Adding a static text - Caption text
        oItem = oForm.Items.Add( "StaticTxt", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 10; 
        oItem.Top = 90; 
        oItem.Width = 150; 
        oItem.LinkTo = "EditTxt"; 
        oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStatic.Caption = "Right Click On the Text Box"; 
        
        //  Adding a static text - Explenation
        oItem = oForm.Items.Add( "ExpTxt1", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 10; 
        oItem.Top = 20; 
        oItem.Width = 350; 
        oItem.LinkTo = "EditTxt"; 
        oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStatic.Caption = "In this sample we show the RightClick usage."; 
        
        oItem = oForm.Items.Add( "ExpTxt2", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 10; 
        oItem.Top = 35; 
        oItem.Width = 350; 
        oItem.LinkTo = "EditTxt"; 
        oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStatic.Caption = "When Right-Clicking on the text box 2 additional menus will appear:"; 
        
        oItem = oForm.Items.Add( "ExpTxt3", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 10; 
        oItem.Top = 50; 
        oItem.Width = 350; 
        oItem.LinkTo = "EditTxt"; 
        oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStatic.Caption = "'In Data Menu and In RightClick' that always appear in the 'Data' Menu."; 
        
        oItem = oForm.Items.Add( "ExpTxt4", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
        oItem.Left = 10; 
        oItem.Top = 65; 
        oItem.Width = 350; 
        oItem.LinkTo = "EditTxt"; 
        oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
        oStatic.Caption = "'Only In RightClick' that appear only in a RightClick menu."; 
        
        oForm.Width = 400; 
        oForm.Height = 150; 
        
        oForm.Visible = true; 
        
    } 
    
    
    private void SBO_Application_MenuEvent( ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent ) { 

		BubbleEvent = true;
        
        // ********************************************************************************
        //  in order to activate your own forms instead of SAP Business One system forms
        //  process the menu event by your self
        //  change BubbleEvent to False so that SAP Business One won't process it
        // ********************************************************************************
        
        if ( pVal.BeforeAction == true ) { 
            
            SBO_Application.SetStatusBarMessage( "Menu item: " + pVal.MenuUID + " sent an event BEFORE SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, true ); 
            
            //  to stop SAP Business One from processing this event
            //  unmark the following statement
            
            //  BubbleEvent = False
            
        } 
        else { 
            
            // SBO_Application.SetStatusBarMessage("Menu item: " & pVal.MenuUID & " sent an event AFTER SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, True)
            
        } 
        
    } 
    
    
    private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 

		BubbleEvent = true;
        
        // *************************************************************************
        //  BubbleEvent sets the behavior of SAP Business One.
        //  False means that the application will not continue processing this event.
        // *************************************************************************
        
        if ( pVal.FormType != 0 ) { 
            
            // ************************************************************
            //  the message box form type is 0
            //  I chose not to deal with events triggered by a message box
            // ************************************************************
            
            // ************************************************************
            //  every event will open a message box with the event
            //  name and the form UID how sent it
            // ************************************************************
            SAPbouiCOM.BoEventTypes EventEnum = 0; 
            EventEnum = pVal.EventType; 
            
            // To prevent an endless loop of MessageBoxes,
            // we'll not notify et_FORM_ACTIVATE and et_FORM_LOAD events
            if ( ( EventEnum != SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE ) & ( EventEnum != SAPbouiCOM.BoEventTypes.et_FORM_LOAD ) ) { 
                // SBO_Application.MessageBox("An " & EventEnum.ToString & " has been sent by a form with the unique ID: " & FormUID)
            } 
        } 
    } 
    
    
    private void SBO_Application_RightClickEvent( ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent ) { 

		BubbleEvent = true;
        
        if ( eventInfo.FormUID == "RightClk" ) { 
            if ( ( eventInfo.BeforeAction == true ) ) { 
                SAPbouiCOM.MenuItem oMenuItem = null; 
                SAPbouiCOM.Menus oMenus = null; 
                
                
                try { 
                    SAPbouiCOM.MenuCreationParams oCreationPackage = null; 
                    oCreationPackage = ( ( SAPbouiCOM.MenuCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams ) ) ); 
                    
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING; 
                    oCreationPackage.UniqueID = "OnlyOnRC"; 
                    oCreationPackage.String = "Only On Right Click"; 
                    oCreationPackage.Enabled = true; 
                    
                    oMenuItem = SBO_Application.Menus.Item( "1280" ); // Data'
                    oMenus = oMenuItem.SubMenus; 
                    oMenus.AddEx( oCreationPackage ); 
                    
                } 
                catch ( Exception ex ) { 
                    MessageBox.Show( ex.Message ); 
                } 
            } 
            else { 
                SAPbouiCOM.MenuItem oMenuItem = null; 
                SAPbouiCOM.Menus oMenus = null; 
                
                
                try { 
                    SBO_Application.Menus.RemoveEx( "OnlyOnRC" ); 
                    
                } 
                catch ( Exception ex ) { 
                    MessageBox.Show( ex.Message ); 
                } 
                
            } 
        } 
    } 
    
} 
    
