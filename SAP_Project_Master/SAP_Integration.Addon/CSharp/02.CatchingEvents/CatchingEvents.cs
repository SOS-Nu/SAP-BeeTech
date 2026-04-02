//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      CatchingEvents.cs
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

class CatchingEvents  { 
    
    // **********************************************************
    //  This parameter will use us to manipulate the
    //  SAP Business One Application
    // **********************************************************
        
    private SAPbouiCOM.Application SBO_Application;
    
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
    
    public CatchingEvents() { 
        
        
        // *************************************************************
        //  set SBO_Application with an initialized application object
        // *************************************************************
        
        SetApplication(); 
        
        // events handled by SBO_Application_AppEvent 
        SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler( SBO_Application_AppEvent ); 
        // events handled by SBO_Application_MenuEvent 
        SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler( SBO_Application_MenuEvent ); 
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
        // events handled by SBO_Application_ProgressBarEvent
        SBO_Application.ProgressBarEvent += new SAPbouiCOM._IApplicationEvents_ProgressBarEventEventHandler( SBO_Application_ProgressBarEvent ); 
        // events handled by SBO_Application_StatusBarEvent
        SBO_Application.StatusBarEvent += new SAPbouiCOM._IApplicationEvents_StatusBarEventEventHandler( SBO_Application_StatusBarEvent ); 
    } 
    
    private void SBO_Application_AppEvent( SAPbouiCOM.BoAppEventTypes EventType ) { 
        
        // ********************************************************************************
        //  the following are the events sent by the application
        //  (Ignore aet_ServerTermination)
        //  in order to implement your own code upon each of the events
        //  place you code instead of the matching message box statement
        // ********************************************************************************
        
        
        switch ( EventType ) {
            case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                
                SBO_Application.MessageBox( "A Shut Down Event has been caught" + Environment.NewLine + "Terminating Add On...", 1, "Ok", "", "" ); 
                
                // **************************************************************
                // 
                //  Take care of terminating your AddOn application
                // 
                // **************************************************************
                
                System.Windows.Forms.Application.Exit(); 
                
                
                break;
            case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                
                SBO_Application.MessageBox( "A Company Change Event has been caught", 1, "Ok", "", "" ); 
                
                // **************************************************************
                //  Check the new company name, if your add on was not meant for
                //  the new company terminate your AddOn
                //     If SBO_Application.Company.Name Is Not "Company1" then
                //          Close
                //     End If
                // **************************************************************
                
                break;
            case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                
                SBO_Application.MessageBox( "A Languge Change Event has been caught", 1, "Ok", "", "" ); 
                
                break;
        }        
    } 
    
    
    
    private void SBO_Application_MenuEvent( ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent ) { 
        
        // ********************************************************************************
        //  in order to activate your own forms instead of SAP Business One system forms
        //  process the menu event by your self
        //  change BubbleEvent to False so that SAP Business One won't process it
        // ********************************************************************************
        BubbleEvent = true;
        if ( pVal.BeforeAction == true ) { 
            
            SBO_Application.SetStatusBarMessage( "Menu item: " + pVal.MenuUID + " sent an event BEFORE SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, true ); 
            
            //  to stop SAP Business One from processing this event
            //  unmark the following statement
            
            //  BubbleEvent = False
            
        } 
        else { 
            
            SBO_Application.SetStatusBarMessage( "Menu item: " + pVal.MenuUID + " sent an event AFTER SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, true ); 
            
        } 

    } 
    
    
    private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
        
        // *************************************************************************
        //  BubbleEvent sets the behavior of SAP Business One.
        //  False means that the application will not continue processing this event.
        // *************************************************************************
        BubbleEvent = true;
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
                SBO_Application.MessageBox( "An " + EventEnum.ToString() + " has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
            } 
        } 
    } 
    
    
    private void SBO_Application_ProgressBarEvent( ref SAPbouiCOM.ProgressBarEvent pVal, out bool BubbleEvent) { 
        SAPbouiCOM.BoProgressBarEventTypes EventEnum = 0; 
        EventEnum = pVal.EventType; 
        BubbleEvent = true;
        SBO_Application.MessageBox( "The event " + EventEnum.ToString() + " has been sent", 1, "Ok", "", "" ); 
    } 
    
    
    private void SBO_Application_StatusBarEvent( string Text, SAPbouiCOM.BoStatusBarMessageType MessageType ) { 
        SBO_Application.MessageBox( @"Status bar event with message: """ + Text + @""" has been sent", 1, "Ok", "", "" ); 
    }     
} 

