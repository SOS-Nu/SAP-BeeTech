//  SAP Business One 8.8 SP1 SDK Sample
//****************************************************************************
//
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// BEFORE STARTING:
// 1. Add reference to the "SAP Business One UI API"
// 2. Insert the development connection string to the "Command line argument"
//-----------------------------------------------------------------
// 1.
//    a. Project->Add Reference...
//    b. select the "SAP Business One UI API 8.8 SP1" From the COM folder
//
// 2.
//     a. Project->Properties...
//     b. choose Configuration Properties folder (place the arrow on Debugging)
//     c. place the following connection string in the 'Command line arguments' field
// 0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056
//
//**************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CSharpUISimple
{
    public partial class Form1 : Form
    {
        private SAPbouiCOM.Application SBO_Application;
        private SAPbouiCOM.Form oSpecialFrm;
        private SAPbouiCOM.Form oForm;
        private String oString;


        private SAPbouiCOM.Item oItem;
        private SAPbouiCOM.Matrix oMatrix;
        private SAPbouiCOM.Column oColumn;
        private SAPbouiCOM.IEditText oEditText;
        private SAPbouiCOM.DBDataSource oDBDS;

        private SAPbouiCOM.UserDataSource oUsrDS;

        SAPbouiCOM.IComboBox oCombo;
        SAPbouiCOM.Cell oCell;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetApplication()
        {
            SAPbouiCOM.SboGuiApi SboGuiApi = null;
            string sConnectionString = null;
            SboGuiApi = new SAPbouiCOM.SboGuiApi();
            sConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
            SboGuiApi.Connect(sConnectionString);
            SBO_Application = SboGuiApi.GetApplication(-1);
        }

        private void CreateFirstForm()
        {
            try
            {
                oSpecialFrm = SBO_Application.Forms.Item("firstform");
                oSpecialFrm.Close();
                GC.Collect();
            }
            catch (Exception e)
            {
                oString = e.ToString();
            }

            SAPbouiCOM.FormCreationParams oFormParams;
            oFormParams = (SAPbouiCOM.FormCreationParams)SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            oFormParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable;
            oFormParams.FormType = "SpecialF";
            oFormParams.UniqueID = "SpecialF";
            oSpecialFrm = SBO_Application.Forms.AddEx(oFormParams);
            oSpecialFrm.ClientHeight = 600;
            oSpecialFrm.ClientWidth = 600;
            oSpecialFrm.Left = 300;
            oSpecialFrm.Title = "Special Lines Demo";
            oSpecialFrm.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetApplication();
            CreateFirstForm();

            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            SBO_Application.FormDataEvent +=new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
        }

        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            oString = BusinessObjectInfo.ObjectKey;
        }

        private void AddMatrix(string matrixuid)
        {
            oDBDS = oSpecialFrm.DataSources.DBDataSources.Add("OITM");
            oUsrDS = oSpecialFrm.DataSources.UserDataSources.Add("user1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 20);

            oItem = oSpecialFrm.Items.Add(matrixuid, SAPbouiCOM.BoFormItemTypes.it_MATRIX);
            oItem.Height = (int)(oSpecialFrm.Height * 0.7);
            oItem.Width = (int)(oSpecialFrm.Width * 0.9);
            oMatrix = (SAPbouiCOM.Matrix)oItem.Specific;

            oMatrix.Layout = SAPbouiCOM.BoMatrixLayoutType.mlt_Normal;
            oMatrix.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto;

            oColumn = oMatrix.Columns.Add("col0", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oColumn.Editable = false;
            oColumn.TitleObject.Caption = "#";


            oColumn = oMatrix.Columns.Add("Picture", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
            oColumn.Width = 60;
            oColumn.Description = "col1";
            oColumn.TitleObject.Caption = "Picture";
            oColumn.DisplayDesc = false;
            oColumn.DataBind.SetBound(true, "", "user1");
            SAPbouiCOM.ColumnSetting cs = oColumn.ColumnSetting;
            SAPbouiCOM.BoColumnDisplayType dt = cs.DisplayType;
            cs.DisplayType = SAPbouiCOM.BoColumnDisplayType.cdt_Picture;
            dt = cs.DisplayType;
            

            oColumn = oMatrix.Columns.Add("mergecell", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oColumn.Width = 60;
            oColumn.Description = "Mer";
            oColumn.TitleObject.Caption = "MergeCell";
            oColumn.DataBind.SetBound(true, "OITM", "U_string");
            oColumn.Editable = false;


            oColumn = oMatrix.Columns.Add("ItemName", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oColumn.Width = 60;
            oColumn.Description = "col2";
            oColumn.TitleObject.Caption = "OITM - ItemName";
            oColumn.DataBind.SetBound(true, "OITM", "ItemName");
            oColumn.Editable = false;


            oColumn = oMatrix.Columns.Add("ItemCode", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oColumn.Width = 60;
            oColumn.Description = "col3";
            oColumn.TitleObject.Caption = "OITM - ItemCode";
            oColumn.DisplayDesc = false;
            oColumn.DataBind.SetBound(true, "OITM", "ItemCode");
            oColumn.Editable = false;

            oColumn = oMatrix.Columns.Add("U_total", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oColumn.Width = 60;
            oColumn.Description = "col4";
            oColumn.TitleObject.Caption = "Total";
            oColumn.DisplayDesc = false;
            oColumn.DataBind.SetBound(true, "OITM", "U_total");
            oColumn.Editable = false;


            oMatrix.Clear();
            oMatrix.AutoResizeColumns();
            oDBDS.Query(null);

            oMatrix.LoadFromDataSource();


            oColumn = oMatrix.Columns.Item("Picture");
            oCombo = (SAPbouiCOM.ComboBox)oColumn.Cells.Item(1).Specific;

            // Put a 16x16 pixel bitmap in the specified path (file is in the project folder)
            oString = "C:\\BitMap\\Smile.bmp";

            oCombo.ValidValues.Add(oString, "picture");
            oCombo.ValidValues.Add("T", "Text");
            oCombo.ValidValues.Add("A", "Alternative");
            oCombo.ValidValues.Add("S", "SubTotal");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMatrix("SpecialMx");
        }

        private int MatrixGetLastTotalRow(int row)
        {
            int last = 0;
            for (int i = 1; i < row; i++)
            {
                oColumn = oMatrix.Columns.Item("Picture");
                oCell = oColumn.Cells.Item(i);
                oCombo = (SAPbouiCOM.ComboBox)oCell.Specific;
                if (oCombo.Selected == null)
                    continue;

                oString = oCombo.Selected.Value;
                if (oString == "S" )
                {
                    last = i;
                }
            }

            return last;
        }
        private int MatrixCalculateTotal(int row)
        {
            int total = 0;
            int tmp = 0;
            int last = MatrixGetLastTotalRow(row);
            for (int i = last + 1; i < row; i++)
            {
                oCombo = (SAPbouiCOM.ComboBox)oMatrix.Columns.Item("Picture").Cells.Item(i).Specific;
                if (oCombo.Selected != null)
                {
                    oString = oCombo.Selected.Value;
                    if (oString == "T")
                        continue;
                }     
                                
                oColumn = oMatrix.Columns.Item("U_total");
                oCell = oColumn.Cells.Item(i);
                oEditText = (SAPbouiCOM.EditText)oCell.Specific;
                oString = oEditText.Value;
                tmp = Convert.ToInt16(oString);
                total += tmp;
            }
            return total;
        }

        private void PopUp_Text_Form(string inital)
        {
            int oTop, oLeft, oHeight, oWidth;
            SAPbouiCOM.Form oMyForm;
            SAPbouiCOM.FormCreationParams oFormParams;
            oFormParams = (SAPbouiCOM.FormCreationParams)SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            oFormParams.FormType = "longtext";
            oFormParams.UniqueID = "longtext";

            try
            {
                oMyForm = SBO_Application.Forms.AddEx(oFormParams);
            }
            catch (Exception)
            {
                oMyForm = SBO_Application.Forms.Item("longtext");
                oMyForm.Close();
                oMyForm = SBO_Application.Forms.AddEx(oFormParams);
            }
            oMyForm.Top = 50;
            oMyForm.ClientHeight = 376;
            oMyForm.ClientWidth = 544;
            oMyForm.Title = "long text";
            oMyForm.Visible = true;

            oItem = oMyForm.Items.Add("multiedit", SAPbouiCOM.BoFormItemTypes.it_EXTEDIT);
            oItem.Height = 298;
            oItem.Width = 526;

            oEditText = (SAPbouiCOM.EditText)oItem.Specific;
            oEditText.Active = true;
            oEditText.Value = inital;

            oTop = oItem.Top;
            oLeft = oItem.Left;
            oWidth = oItem.Width;
            oHeight = oItem.Height;


            oItem = oMyForm.Items.Add("1", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Height = 20;
            oItem.Width = 70;
            oItem.Top = oTop + oHeight + 50;
            oItem.Left = oLeft + 3;

            oTop = oItem.Top;
            oLeft = oItem.Left;
            oWidth = oItem.Width;
            oHeight = oItem.Height;

            oItem = oMyForm.Items.Add("2", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Top = oTop;
            oItem.Height = oHeight;
            oItem.Width = oWidth;
            oItem.Left = oLeft + oWidth + 5;
        }

        bool[] bSperateLine = new bool[20] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };

        int lRowNeedToMerge = 0;
        bool bIsMatrix = true;

        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK && pVal.FormUID == "longtext" && pVal.ItemUID == "1")
            {
                SAPbouiCOM.IForm otempForm;
                otempForm = SBO_Application.Forms.Item("longtext");
                oEditText = (SAPbouiCOM.EditText)otempForm.Items.Item("multiedit").Specific;
                oString = oEditText.Value;
                otempForm.Visible = false;

                if (bIsMatrix)
                {
                    oForm = SBO_Application.Forms.Item("SpecialF");
                    oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("SpecialMx").Specific;
                    oMatrix.CommonSetting.MergeCell(lRowNeedToMerge, 2, true);
                    oColumn = oMatrix.Columns.Item("mergecell");

                    oCell = oColumn.Cells.Item(lRowNeedToMerge);
                    oEditText = (SAPbouiCOM.EditText)oCell.Specific;
                    oEditText.Value = oString;
                    oMatrix.FlushToDataSource();
                    
                }
                oForm.Update();
                otempForm.Close();
            }


            if (pVal.ItemUID == "SpecialMx")
            {
                oItem = oSpecialFrm.Items.Item(pVal.ItemUID);
                if (oItem.Type == SAPbouiCOM.BoFormItemTypes.it_MATRIX)
                {
                    bIsMatrix = true;

                    oItem = oSpecialFrm.Items.Item(pVal.ItemUID);
                    oMatrix = (SAPbouiCOM.Matrix)oItem.Specific;
                    oColumn = oMatrix.Columns.Item("Picture");

                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT && pVal.BeforeAction == false )
                    {
                        oCombo = (SAPbouiCOM.ComboBox)oColumn.Cells.Item(pVal.Row).Specific;

                        oString = oCombo.Selected.Value;

                        //text
                        if (oString == "T")

                        {
                            lRowNeedToMerge = pVal.Row;
                            //initialize by last value
                            SAPbouiCOM.EditText oTMPET = (SAPbouiCOM.EditText)oMatrix.Columns.Item("mergecell").Cells.Item(lRowNeedToMerge).Specific;
                            PopUp_Text_Form(oTMPET.String);
                        }
                        //sub total
                        else if (oString == "S")
                        {
                            oMatrix.CommonSetting.SeperateLine(pVal.Row, 11111, SAPbouiCOM.BoSeparateLineType.slt_Top);
                            int GreyColor = 222 | (223 << 8) | (222 << 16);
                            oMatrix.CommonSetting.SetRowBackColor(pVal.Row, GreyColor);

                            bSperateLine[pVal.Row] = !bSperateLine[pVal.Row];
                            int value = MatrixCalculateTotal(pVal.Row);
                            oEditText = (SAPbouiCOM.EditText)oMatrix.Columns.Item("U_total").Cells.Item(pVal.Row).Specific;
                            oEditText.Value = Convert.ToString(value);
                        }
                        oSpecialFrm.Update();
                    }                                   
                }
            }  

            if (pVal.ItemUID == "utmatrix")
            {
                oItem = oSpecialFrm.Items.Item(pVal.ItemUID);
                oMatrix = (SAPbouiCOM.Matrix)oItem.Specific;
                oColumn = oMatrix.Columns.Item("Picture");

                if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT && pVal.BeforeAction == false )
                {
                    oCombo = (SAPbouiCOM.ComboBox)oColumn.Cells.Item(pVal.Row).Specific;

                    oString = oCombo.Selected.Value;
                    //text
                    if (oString == "T")
                    {
                        oMatrix.CommonSetting.MergeCell(pVal.Row, 2, true);
                    }
                    oSpecialFrm.Update();
                }
            }
        }
    }
}
