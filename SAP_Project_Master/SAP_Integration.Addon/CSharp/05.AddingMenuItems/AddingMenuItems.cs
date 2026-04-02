//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      AddingMenuItems.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
//****************************************************************************

using System;
using System.Windows.Forms;

class AddingMenuItems  { 
    
    
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
        
        try { 
            // If there's no active application the connection will fail
			SboGuiApi.Connect( sConnectionString ); 
        } 
        catch  { //  Connection failed
            System.Windows.Forms.MessageBox.Show( "No SAP Business One Application was found" ); 
            System.Environment.Exit( 0 ); 
        } 
        // get an initialized application object
        
        SBO_Application = SboGuiApi.GetApplication( -1 );
    } 
    
    private void AddMenuItems() { 
        //******************************************************************
        // Let's add a separator, a pop-up menu item and a string menu item
        //******************************************************************
        
        SAPbouiCOM.Menus oMenus = null; 
        SAPbouiCOM.MenuItem oMenuItem = null; 
        
        int i = 0; // to be used as counter
        int lAddAfter = 0; 
        string sXML = null; 
        
        // Get the menus collection from the application
        oMenus = SBO_Application.Menus; 
        // --------------------------------------------
        // Save an XML file containing the menus...
        // --------------------------------------------
        // sXML = SBO_Application.Menus.GetAsXML
        // Dim xmlD As System.Xml.XmlDocument
        // xmlD = New System.Xml.XmlDocument
        // xmlD.LoadXml(sXML)
        // xmlD.Save("c:\\mnu.xml")
        // --------------------------------------------
        
        
        SAPbouiCOM.MenuCreationParams oCreationPackage = null; 
        oCreationPackage = ( ( SAPbouiCOM.MenuCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams ) ) ); 
        oMenuItem = SBO_Application.Menus.Item( "43520" ); // moudles'
        
        string sPath = null; 
        
        sPath = Application.StartupPath; 
        sPath = sPath.Remove( sPath.Length - 9, 9 ); 
        
        // find the place in wich you want to add your menu item
        // in this example I chose to add my menu item under
        // SAP Business One.
        oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP; 
        oCreationPackage.UniqueID = "MyMenu01"; 
        oCreationPackage.String = "Sample Menu"; 
        oCreationPackage.Enabled = true; 
        oCreationPackage.Image = sPath + "UI.bmp"; 
        oCreationPackage.Position = 15; 
        
        oMenus = oMenuItem.SubMenus; 
        
        try { 
            //  If the manu already exists this code will fail
			oMenus.AddEx( oCreationPackage ); 
            
            // Get the menu collection of the newly added pop-up item
            oMenuItem = SBO_Application.Menus.Item( "MyMenu01" ); 
            oMenus = oMenuItem.SubMenus; 
            
            // Create s sub menu
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING; 
            oCreationPackage.UniqueID = "MySubMenu"; 
            oCreationPackage.String = "Sample Sub Menu"; 
            oMenus.AddEx( oCreationPackage ); 
        } 
        catch ( Exception er ) { //  Menu already exists
            SBO_Application.MessageBox( "Menu Already Exists", 1, "Ok", "", "" ); 
        } 
        
    } 
    
    private void AddMenuItemsToForm( SAPbouiCOM.Form oMenuForm ) { 
        
        SAPbouiCOM.MenuCreationParams oCreationPackage = null; 
        oCreationPackage = ( ( SAPbouiCOM.MenuCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams ) ) ); 
        string sPath = null; 
        
        sPath = System.IO.Directory.GetParent( Application.StartupPath ).ToString(); 
        sPath = System.IO.Directory.GetParent( sPath ).ToString() + "\\"; 
        oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING; 
        oCreationPackage.UniqueID = "MyGoToMenu"; 
        oCreationPackage.String = "Form Related Menu"; 
        oCreationPackage.Image = sPath + "UI1.bmp"; 
        
        
        try { 
            //  If the manu already exists this code will fail
			oMenuForm.Menu.AddEx( oCreationPackage ); 
        } 
        catch  { //  Menu already exists
            SBO_Application.MessageBox( "Menu Already Exists", 1, "Ok", "", "" ); 
        } 
        
        oCreationPackage.UniqueID = "MySecondGoToMenu"; 
        oCreationPackage.String = "Second Form Related Menu"; 
        oCreationPackage.Image = sPath + "UI2.bmp"; 
        
        try { 
            //  If the manu already exists this code will fail
			oMenuForm.Menu.AddEx( oCreationPackage ); 
        } 
        catch  { //  Menu already exists
            SBO_Application.MessageBox( "Menu Already Exists", 1, "Ok", "", "" ); 
        } 
    } 
    
    
    public AddingMenuItems() : base() { 
        
        
        //*************************************************************
        // set SBO_Application with an initialized application object
        //*************************************************************
        
        SetApplication(); 
        
        //*************************************************************
        // Check the SBO application lanuage
        //*************************************************************
        
        if ( !( ( SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Cy | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Gb | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Sg ) ) ) { 
            
            SBO_Application.MessageBox( "AddingMenuItems Add On:" + Environment.NewLine + "The menu items will be added only for English language settings", 1, "Ok", "", "" ); 
            
            return; 
            
        } 
        
        //*************************************************************
        // add the menu items
        //*************************************************************
        
        AddMenuItems(); 
        // events handled by SBO_Application_MenuEvent
        SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler( SBO_Application_MenuEvent ); 
        // events handled by SBO_Application_AppEvent
        SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler( SBO_Application_AppEvent ); 
    } 
    
    
    private void SBO_Application_MenuEvent( ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent ) { 
        BubbleEvent = true;
        if ( ( pVal.MenuUID == "MySubMenu" ) & ( pVal.BeforeAction == false ) ) { 
            
            
            SBO_Application.MessageBox( "My sub menu item was clicked", 1, "Ok", "", "" ); 
            //*************************************************************
            // Create a form to be launched in response to a click on the 
            // new sub menu item
            //*************************************************************
            
            try { 
                oForm = SBO_Application.Forms.Item( "MyMenuForm" ); 
                SBO_Application.MessageBox( "The Form already exists", 1, "Ok", "", "" ); 
            } 
            catch  { 
                oForm = null; 
                oForm = SBO_Application.Forms.Add( "MyMenuForm", (SAPbouiCOM.BoFormTypes)(0), -1 ); 
                oForm.Title = "New Sub Menu Item"; 
                oForm.Left = 400; 
                oForm.Top = 100; 
                oForm.Visible = true; 
                AddMenuItemsToForm( oForm ); 
            } 
        } 
        
        if ( ( pVal.MenuUID == "MyGoToMenu" ) & ( pVal.BeforeAction == false ) ) { 
            SBO_Application.MessageBox( "My GoTo Menu was clicked", 1, "Ok", "", "" ); 
        } 
        if ( ( pVal.MenuUID == "MySecondGoToMenu" ) & ( pVal.BeforeAction == false ) ) { 
            SBO_Application.MessageBox( "My Second GoTo Menu was clicked", 1, "Ok", "", "" ); 
        } 
    } 
    
    
    private void SBO_Application_AppEvent( SAPbouiCOM.BoAppEventTypes EventType ) { 
        
        switch ( EventType ) {
            case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                
                SBO_Application.MessageBox( "A Shut Down Event has been caught" + Environment.NewLine + "Terminating 'Add Menu Item' Add On...", 1, "Ok", "", "" ); 
                // terminating the Add On
                System.Environment.Exit( 0 ); 
                
                break;
            case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                
                if ( SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Cy | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Gb | SBO_Application.Language == SAPbouiCOM.BoLanguages.ln_English_Sg ) { 
                    
                    AddMenuItems(); 
                    
                } 
                break;
        }  
    }   
} 

