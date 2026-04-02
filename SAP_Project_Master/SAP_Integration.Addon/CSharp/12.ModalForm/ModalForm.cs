//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      ModalForm.cs
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


//  This class presents a modal form.
//  Only after you close the form you can open other forms.

using System;
using System.Windows.Forms;
namespace ModalForm {
    internal class ModalForm  { 
        private SAPbouiCOM.Application SBO_Application; 
        private bool bModal; //  Flag to indicate if the modal form is open.
        private SAPbouiCOM.Form oForm; 
        
        private void SetApplication() { 
            
            SAPbouiCOM.SboGuiApi SboGuiApi = null; 
            string sConnectionString = null; 
            
            bModal = false; 
            SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
            sConnectionString = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
            
            SboGuiApi.Connect( sConnectionString ); 
            SBO_Application = SboGuiApi.GetApplication( -1 ); 
            
            CreateModalForm(); 
        } 
        
        private void CreateModalForm() { 
            SAPbouiCOM.FormCreationParams cp = null; 
            SAPbouiCOM.Item oItem = null; 
            SAPbouiCOM.StaticText oStatic = null; 
            
            //  Create the form
            cp = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
            
            cp.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed; 
            cp.FormType = "Modal1"; 
            cp.UniqueID = "Modal1"; 
            
            oForm = SBO_Application.Forms.AddEx( cp ); 
            oForm.ClientHeight = 170; 
            oForm.ClientWidth = 150; 
            
            //  Create the form GUI elements
            oForm.AutoManaged = true; 
            oForm.SupportedModes = 0; 

			oItem = oForm.Items.Add( "1", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
            oItem.AffectsFormMode = false; 
            oItem.Top = 100; 
            oItem.Left = 10; 
            
            oItem = oForm.Items.Add( "MyStatic", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
            oStatic = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
            oStatic.Caption = "I am a modal form"; 
            
            oForm.Visible = true; 
            bModal = true; 
        } 
        
        public ModalForm() { 
            
            SetApplication(); 
            // events handled by SBO_Application_ItemEvent
            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
            // events handled by SBO_Application_MenuEvent
            SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler( SBO_Application_MenuEvent ); 
        } 
        
        private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
			BubbleEvent = true;
            //  Don't let the user to move to other forms
            SAPbouiCOM.BoEventTypes EventEnum = 0; 
            EventEnum = pVal.EventType; 
            
            if ( bModal & FormUID != "Modal1" ) { 
                oForm.Select(); //  Select the modal form
                BubbleEvent = false; 
            } 
            //  If the modal from is closed...
            if ( FormUID == "Modal1" & ( EventEnum == SAPbouiCOM.BoEventTypes.et_FORM_CLOSE ) & bModal ) { 
                bModal = false; 
            } 
        } 
        
        
        private void SBO_Application_MenuEvent( ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent ) { 
			BubbleEvent = true;
            //  Don't let the user to use the menus when the modal form is open.
            if ( bModal ) { 
                BubbleEvent = false; 
            } 
        }   
    }    
} 
