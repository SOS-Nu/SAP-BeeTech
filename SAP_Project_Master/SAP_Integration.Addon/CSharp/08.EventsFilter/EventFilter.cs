//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      EventFilter.cs
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


using Microsoft.VisualBasic;
using System;
namespace FilterEvents {
    public class EventFilter  { 
        private SAPbouiCOM.Application SBO_Application; 
        
        //**********************************************************
        // declaring an Event filters container object and an
        // event filter object
        //**********************************************************
        
        public SAPbouiCOM.EventFilters oFilters; 
        
        public SAPbouiCOM.EventFilter oFilter; 
        
        
        private void SetApplication() { 
            
            // *******************************************************************
            // Use an SboGuiApi object to establish connection
            // with the SAP Business One application and return an
            // initialized appliction object
            // *******************************************************************
            
            SAPbouiCOM.SboGuiApi SboGuiApi = null; 
            string sConnectionString = null; 
            
            SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
            
            // by following the steped specified above the following
            // statment should be suficient for either development or run mode
            
            sConnectionString = Interaction.Command(); 
            
            // connect to a running SBO Application
            
            SboGuiApi.Connect( sConnectionString ); 
            
            // get an initialized application object
            
            SBO_Application = SboGuiApi.GetApplication( -1 ); 
            
        } 
        
        
        private void SetFilters() { 
            
            // Create a new EventFilters object
            oFilters = new SAPbouiCOM.EventFilters(); 
            
            // add an event type to the container
            // this method returns an EventFilter object
            oFilter = oFilters.Add( SAPbouiCOM.BoEventTypes.et_CLICK ); 
            
            // assign the form type on which the event would be processed
            oFilter.AddEx( "139" ); // Orders Form
            oFilter.AddEx( "142" ); // Purchase Form
            
            oFilter = oFilters.Add( SAPbouiCOM.BoEventTypes.et_KEY_DOWN ); 
            
            // assign the form type on which the event would be processed
            oFilter.Add( 139 ); // Orders Form
            
            // For a list of all form types see the help or use the
            // Tools -> User Tools -> Display Debug Information option
            // in the SBO application
            // then open the desired form and hover over it with the mouse
            // the form's type will apear in the lower left side of the screen
            
            
            // Setting the application with the EventFilters object
            // in this case we will process a click event for form types 142 and 139
            // and we will process a key down event for for form type 139
            
            SBO_Application.SetFilter( oFilters ); 
            
        } 
        
        public EventFilter() { 
            
            //*************************************************************
            // set SBO_Application with an initialized application object
            //*************************************************************
            SetApplication(); 
            
            //*************************************************************
            // set SBO_Application with an initialized EventFilters object
            //*************************************************************
            
            SetFilters(); 
            
            // events handled by SBO_Application_AppEvent
            SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler( SBO_Application_AppEvent ); 
            // events handled by SBO_Application_MenuEvent
            SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler( SBO_Application_MenuEvent ); 
            // events handled by SBO_Application_ItemEvent
            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
        } 
        
        private void SBO_Application_AppEvent( SAPbouiCOM.BoAppEventTypes EventType ) { 
            
            //********************************************************************************
            // the following are the events sent by the application
            // (Ignore aet_ServerTermination)
            // in order to implement your own code upon each of the events
            // place you code instead of the matching message box statement
            //********************************************************************************
            
            
            switch ( EventType ) {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    
                    SBO_Application.MessageBox( "A Shut Down Event has been caught" + Constants.vbNewLine + "Terminating Add On...", 1, "Ok", "", "" ); 
                    
                    //**************************************************************
                    //
                    // Take care of terminating your AddOn application
                    //
                    //**************************************************************
                    
                    System.Environment.Exit( 0 ); 
                    
                    
                    
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    
                    SBO_Application.MessageBox( "A Company Change Event has been caught", 1, "Ok", "", "" ); 
                    
                    //**************************************************************
                    // Check the new company name, if your add on was not meant for
                    // the new company terminate your AddOn
                    //    If SBO_Application.Company.Name Is Not "Company1" then
                    //         Close
                    //    End If
                    //**************************************************************
                    
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    
                    SBO_Application.MessageBox( "A Languge Change Event has been caught", 1, "Ok", "", "" ); 
                    
                    //**************************************************************
                    // Check the new language name, if your AddOn's items needs
                    // to be changed, take care of it at this point
                    //
                    //    Select Case SBO_Application.Language
                    //         Case ln_English:
                    //         Case ln_French:
                    //         Case ln_German:
                    //    End Select
                    //**************************************************************
                    
                    break;
            }
            
            
        } 
        
        
        
        private void SBO_Application_MenuEvent( ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent ) { 
            BubbleEvent = true;
            //********************************************************************************
            // in order to activate your own forms instead of SAP Business One system forms
            // process the menu event by your self
            // change BubbleEvent to True so that SAP Business One won't process it
            //********************************************************************************
            
            if ( pVal.BeforeAction == true ) { 
                
                SBO_Application.SetStatusBarMessage( "Menu item: " + pVal.MenuUID + " sent an event BEFORE SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, true ); 
                
                // to stop SAP Business One from processing this event
                // unmark the following statement
                
                // BubbleEvent = True
                
            } 
            else { 
                
                SBO_Application.SetStatusBarMessage( "Menu item: " + pVal.MenuUID + " sent an event AFTER SAP Business One processes it.", SAPbouiCOM.BoMessageTime.bmt_Long, true ); 
                
            } 
            
        } 
        
        
        private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
            
            BubbleEvent = true;
            //**************************************************************************
            // BubbleEvent sets the behavior of SAP Business One.
            // False means that the application will not continue processing this event
            // True is the default value
            //**************************************************************************
            
            if ( pVal.FormType != 0 & pVal.Before_Action == true ) { 
                
                //************************************************************
                // the message box form type is 0
                // I chose not to deal with events triggered by a message box
                //************************************************************
                
                switch ( pVal.EventType ) {
                    case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                        
                        // Specifies a button release (After exit).
                        
                        SBO_Application.MessageBox( "An et_ITEM_PRESSED has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_KEY_DOWN:
                        
                        // Specifies a key down event.
                        
                        SBO_Application.MessageBox( "An et_KEY_DOWN has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_GOT_FOCUS:
                        
                        // Specifies an item got focus.
                        
                        SBO_Application.MessageBox( "An et_GOT_FOCUS has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_LOST_FOCUS:
                        
                        // Specifies an item lost focus.
                        
                        SBO_Application.MessageBox( "An et_LOST_FOCUS has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                        
                        // Specifies the selection of valid value in Combo Box.
                        
                        SBO_Application.MessageBox( "An et_COMBO_SELECT has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_CLICK:
                        // Specifies Mouse Up on editable item.
                        
                        SBO_Application.MessageBox( "An et_CLICK has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK:
                        // Specifies Mouse Up on editable item in time interval define by
                        // SAP Business One as double-click.
                        
                        SBO_Application.MessageBox( "An et_DOUBLE_CLICK has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED:
                        // Specifies a link arrow within a matrix was pressed.
                        
                        SBO_Application.MessageBox( "An et_MATRIX_LINK_PRESSED has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_MATRIX_COLLAPSE_PRESSED:
                        // Specifies collapsed or expanded list within a matrix.
                        
                        SBO_Application.MessageBox( "An et_MATRIX_COLLAPSE_PRESSED has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                        //Specifies an item validation event.
                        
                        SBO_Application.MessageBox( "An et_VALIDATE has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                        // Specifies that SAP Business One application opened a form.
                        
                        SBO_Application.MessageBox( "An et_FORM_LOAD has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD:
                        // Specifies that SAP Business One application closed a form.
                        
                        SBO_Application.MessageBox( "An et_FORM_UNLOAD has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE:
                        // Specifies that the focus is set on the form.
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_DEACTIVATE:
                        // Specifies that the form lost focus.
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_CLOSE:
                        // Specifies that the form was closed by calling the Close event.
                        
                        SBO_Application.MessageBox( "An et_FORM_CLOSE has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                        // Specifies resizing of the form.
                        
                        SBO_Application.MessageBox( "An et_FORM_RESIZE has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_KEY_DOWN:
                        // Specifies a key press on an area which is not item of the form.
                        
                        SBO_Application.MessageBox( "An et_FORM_KEY_DOWN has been sent by a form with the unique ID: " + FormUID, 1, "Ok", "", "" ); 
                        
                        break;
                }      
            } 
        }   
    }  
} 
