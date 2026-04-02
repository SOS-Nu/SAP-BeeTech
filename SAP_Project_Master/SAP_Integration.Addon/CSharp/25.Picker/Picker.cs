//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      Picker.cs
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
namespace Project1
{
    internal class Picker  
    { 
        
        //**********************************************************
        // This parameter will use us to manipulate the
        // SAP Business One Application
        //**********************************************************
        
        private SAPbouiCOM.Application SBO_Application; 
        public SAPbouiCOM.Form oForm; 
        
        private void SetApplication() 
        { 
            try 
            { 
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
            catch ( Exception ex ) 
            { 
                MessageBox.Show( ex.Message ); 
            }             
        }         
        
        public Picker() : base() 
        { 
            
            
            SAPbouiCOM.FormCreationParams oCreationParams; 
            SAPbouiCOM.Item oItem; 
            SAPbouiCOM.EditText oEditText; 
            SAPbouiCOM.StaticText oStaticText; 
            
            try 
            { 
                //*************************************************************
                // set SBO_Application with an initialized application object
                //*************************************************************
                
                SetApplication(); 

				// events handled by SBO_Application_ItemEvent
				SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 

                //*************************************************************
                // Draw Form
                //*************************************************************
                
                // add a new form
                
				oCreationParams = (SAPbouiCOM.FormCreationParams) SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams );
                
                oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed; 
                oCreationParams.UniqueID = "PickerForm"; 
                
                oForm = SBO_Application.Forms.AddEx( oCreationParams ); 
                
                // set the form properties
                oForm.Title = "Data Picker"; 
                oForm.Left = 400; 
                oForm.Top = 100; 
                oForm.ClientHeight = 150; 
                oForm.ClientWidth = 350; 
                
                // add User Data Sources to the form
				oForm.DataSources.UserDataSources.Add( "UDDate", SAPbouiCOM.BoDataType.dt_DATE,254); 
				oForm.DataSources.UserDataSources.Add( "UDNum", SAPbouiCOM.BoDataType.dt_PRICE,254 ); 
                oForm.DataSources.UserDataSources.Add( "UDCFL", SAPbouiCOM.BoDataType.dt_SHORT_TEXT,254 ); 
                
                AddChooseFromList(); 
                
                // Add Controls
                //**********************************
                // Adding Static Text item
                //**********************************
                
                oItem = oForm.Items.Add( "StaticHead", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
                oItem.Left = 7; 
                oItem.Width = 348; 
                oItem.Top = 8; 
                oItem.Height = 44; 
                
                oItem.LinkTo = "txtDate"; 
                oStaticText = (SAPbouiCOM.StaticText) oItem.Specific; 
                oStaticText.Caption = "This sample shows 3 different picker items."; 
                
                //  Date Picker
                // __________________________________________________________________________________________
                //*************************
                // Adding a Text Edit item
                //*************************
                
                oItem = oForm.Items.Add( "txtDate", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
                oItem.Left = 157; 
                oItem.Width = 80; 
                oItem.Top = 58; 
                oItem.Height = 14; 
                
                oEditText = (SAPbouiCOM.EditText)oItem.Specific; 
                
                // bind the text edit item to the defined used data source
                oEditText.DataBind.SetBound( true, "", "UDDate" ); 
                
                //**********************************
                // Adding Static Text item
                //**********************************
                
                oItem = oForm.Items.Add( "StaticTxt1", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
                oItem.Left = 7; 
                oItem.Width = 148; 
                oItem.Top = 58; 
                oItem.Height = 14; 
                
                oItem.LinkTo = "txtDate"; 
                oStaticText = (SAPbouiCOM.StaticText)oItem.Specific; 
                oStaticText.Caption = "Date Picker"; 
                
                //  Calc Picker
                // __________________________________________________________________________________________
                //*************************
                // Adding a Text Edit item
                //*************************
                
                oItem = oForm.Items.Add( "txtCalc", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
                oItem.Left = 157; 
                oItem.Width = 80; 
                oItem.Top = 74; 
                oItem.Height = 14; 
                
                oEditText = (SAPbouiCOM.EditText)oItem.Specific; 
                
                // bind the text edit item to the defined used data source
                oEditText.DataBind.SetBound( true, "", "UDNum" ); 
                
                //**********************************
                // Adding Static Text item
                //**********************************
                
                oItem = oForm.Items.Add( "StaticTxt2", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
                oItem.Left = 7; 
                oItem.Width = 148; 
                oItem.Top = 74; 
                oItem.Height = 14; 
                
                oItem.LinkTo = "txtCalc"; 
                oStaticText = (SAPbouiCOM.StaticText)oItem.Specific; 
                oStaticText.Caption = "Calc Picker"; 
                //  CFL Picker
                // __________________________________________________________________________________________
                //*************************
                // Adding a Text Edit item
                //*************************
                
                oItem = oForm.Items.Add( "txtCFL", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
                oItem.Left = 157; 
                oItem.Width = 80; 
                oItem.Top = 90; 
                oItem.Height = 14; 
                
                oEditText = (SAPbouiCOM.EditText)oItem.Specific; 
                
                // bind the text edit item to the defined used data source
                oEditText.DataBind.SetBound( true, "", "UDCFL" ); 
                
                oEditText.ChooseFromListUID = "CFL1"; 
                
                //**********************************
                // Adding Static Text item
                //**********************************
                
                oItem = oForm.Items.Add( "StaticTxt3", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
                oItem.Left = 7; 
                oItem.Width = 148; 
                oItem.Top = 90; 
                oItem.Height = 14; 
                
                oItem.LinkTo = "txtCFL"; 
                oStaticText = (SAPbouiCOM.StaticText)oItem.Specific; 
                oStaticText.Caption = "CFL Picker"; 
                
                oForm.Visible = true; 
                
            } 
            catch ( Exception ex ) 
            { 
                MessageBox.Show( ex.Message ); 
            } 
        } 
        
        private void AddChooseFromList() 
        { 
            try 
            { 
                
                SAPbouiCOM.ChooseFromListCollection oCFLs = null; 
                SAPbouiCOM.Conditions oCons = null; 
                SAPbouiCOM.Condition oCon = null; 
                
                oCFLs = oForm.ChooseFromLists; 
                
                SAPbouiCOM.ChooseFromList oCFL = null; 
                SAPbouiCOM.ChooseFromListCreationParams oCFLCreationParams = null; 
                oCFLCreationParams =(SAPbouiCOM.ChooseFromListCreationParams) SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams ); 
                
                //  Adding 2 CFL, one for the button and one for the edit text.
                oCFLCreationParams.MultiSelection = false; 
                oCFLCreationParams.ObjectType = "2"; 
                oCFLCreationParams.UniqueID = "CFL1"; 
                
                oCFL = oCFLs.Add( oCFLCreationParams ); 
                
                //  Adding Conditions to CFL1
                
                oCons = oCFL.GetConditions(); 
                
                oCon = oCons.Add(); 
                oCon.Alias = "CardType"; 
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL; 
                oCon.CondVal = "C"; 
                oCFL.SetConditions( oCons ); 
                
            } 
            catch ( Exception ex ) 
            { 
                MessageBox.Show( ex.Message ); 
            } 
        } 
        
        private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent )
        { 
            SAPbouiCOM.Item oItem; 
            SAPbouiCOM.EditText oEdit; 
			
			BubbleEvent = true;
			
			try 
            { 
                if ( pVal.FormUID == "PickerForm" ) 
                { 
                    if ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST ) 
                    { 
                        
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = null; 
                        oCFLEvento =(SAPbouiCOM.IChooseFromListEvent) pVal; 
                        string sCFL_ID = null; 
                        sCFL_ID = oCFLEvento.ChooseFromListUID; 
                        
                        SAPbouiCOM.ChooseFromList oCFL = null; 
                        oCFL = oForm.ChooseFromLists.Item( sCFL_ID ); 
                        
                        if ( oCFLEvento.BeforeAction == false ) 
                        { 
                            SAPbouiCOM.DataTable oDataTable = null; 
                            oDataTable = oCFLEvento.SelectedObjects; 
                            string val = null; 
                            try 
                            { 
                                val = System.Convert.ToString( oDataTable.GetValue( 0, 0 ) ); 
                            } 
                            catch ( Exception ex ) 
                            { 
                                
                            } 
                            if ( ( pVal.ItemUID == "txtCFL" ) ) 
                            { 
                                oForm.DataSources.UserDataSources.Item( "UDCFL" ).ValueEx = val; 
                            }                             
                        } 
                    }                     
                    if ( ( FormUID == "PickerForm" ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD ) ) 
                    { 
                        System.Windows.Forms.Application.Exit(); 
                    }                     
                    if ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_PICKER_CLICKED ) 
                    { 
                        oItem = oForm.Items.Item( pVal.ItemUID ); 
                        oEdit = (SAPbouiCOM.EditText)oItem.Specific;                         
                        
                        SBO_Application.StatusBar.SetText( "Picker Clicked of type " + oEdit.PickerType.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success ); 
                    } 
                    
                } 
            } 
            catch ( Exception ex ) 
            { 
                MessageBox.Show( ex.Message ); 
            }             
        }         
    }     
} 
