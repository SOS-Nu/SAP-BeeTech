//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      Password.cs
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
class Password  { 
    private SAPbouiCOM.Application SBO_Application; 
    
    private void SetApplication() { 
        
        SAPbouiCOM.SboGuiApi SboGuiApi = null; 
        string sConnectionString = null; 
        
        SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
        sConnectionString = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
        
        SboGuiApi.Connect( sConnectionString ); 
        SBO_Application = SboGuiApi.GetApplication(0); 
        
    } 
    
    public Password() { 
        
        SetApplication(); 
        // events handled by SBO_Application_ItemEvent
        SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
    } 
    
    private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
		BubbleEvent = true;
        SAPbouiCOM.BoEventTypes EventEnum = 0; 
        SAPbouiCOM.Item oItem = null; 
        SAPbouiCOM.EditText oEdit = null; 
        SAPbouiCOM.Form oForm = null; 
        
        EventEnum = pVal.EventType; 
        
        //  20701 is the password setting form
        //  This is the only event handled by the addon.
        //  Items 3 and 4 are the passwoed fields
        
        if ( ( pVal.FormType == double.Parse( "20701" ) ) & ( EventEnum == SAPbouiCOM.BoEventTypes.et_VALIDATE ) & ( ( pVal.ItemUID == "3" ) | ( pVal.ItemUID == "4" ) ) ) { 
            
            oForm = SBO_Application.Forms.Item( FormUID ); 
            oItem = oForm.Items.Item( int.Parse( pVal.ItemUID ) - 1 ); //  The items index is zero based
            oEdit = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
            string sPassword = null; 
            sPassword = oEdit.String; 
            if ( !( ( ValidPassword( sPassword ) ) ) ) { 
                SBO_Application.StatusBar.SetText( "Password Length < 7 , 1 caps and 1 num", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error ); 
                BubbleEvent = false; 
            } 
            else { 
                SBO_Application.StatusBar.SetText( "Password Is Valid", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success ); 
            } 
        } 
    } 
    
    
    //  This function checks if the password is complex enough
    private bool ValidPassword( string s ) { 
        bool validPasswordReturn = false;
        string sPassword = null; 
        bool bPasswordOk = false; 
        
        object CheckLength = 7; //  The minimum length of the password
        object CheckCaps = 1; //  The minimum number of capital letters in the password
        object CheckNums = 1; //  The minimum number of numeric digits in the password
        
        object NumCaps = 0; //  The actual number of capital letters in the password
        object NumNums = 0; // The actual number of numeric digits in the password
        
        bPasswordOk = true; 
        sPassword = s; //  Get the password
        
        
        //  Check for the number of capitel letters and numeric digits in the password
        int i = 0; 
        for ( i=0; i<=sPassword.Length - 1; i++ ) { 
            char c = char.MinValue; 
            c = char.Parse( sPassword.Substring( i, 1 ) ); 
            if ( ( c >= char.Parse( "A" ) ) & ( c <= char.Parse( "Z" ) ) ) { 
                NumCaps = System.Convert.ToDouble( NumCaps ) + 1; 
            } 
            if ( ( c >= char.Parse( "0" ) ) & ( c <= char.Parse( "9" ) ) ) { 
                NumNums = System.Convert.ToDouble( NumNums ) + 1; 
            } 
        } 
        
        // If the password it not ok, change the returned value to false
        if ( ( sPassword.Length < System.Convert.ToDouble( CheckLength ) ) | ( System.Convert.ToDouble( NumCaps ) < System.Convert.ToDouble( CheckCaps ) ) | ( System.Convert.ToDouble( NumNums ) < System.Convert.ToDouble( CheckNums ) ) ) { 
            bPasswordOk = false; 
        } 
        
        validPasswordReturn = bPasswordOk; 
        return validPasswordReturn;
    } 
} 
    