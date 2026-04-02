using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      frmMain.cs
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


namespace CurrentPeriod
{
	public class frmPeriod : System.Windows.Forms.Form
	{
		
		[STAThread]
		static void Main()
		{
			Application.Run(new frmPeriod());
		}
		
		#region " Windows Form Designer generated code "
		
		//Form overrides dispose to clean up the component list.
		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Button cmdGetData;
		internal System.Windows.Forms.TextBox txtCurPeriod;
		internal System.Windows.Forms.Label lblCurrPeriod;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent ()
		{
			this.cmdGetData = new System.Windows.Forms.Button();
			cmdGetData.Click += new System.EventHandler(cmdGetData_Click);
			this.txtCurPeriod = new System.Windows.Forms.TextBox();
			this.lblCurrPeriod = new System.Windows.Forms.Label();
			this.SuspendLayout();
			//
			//cmdGetData
			//
			this.cmdGetData.Location = new System.Drawing.Point(24, 24);
			this.cmdGetData.Name = "cmdGetData";
			this.cmdGetData.Size = new System.Drawing.Size(152, 40);
			this.cmdGetData.TabIndex = 0;
			this.cmdGetData.Text = "Get Current Period";
			//
			//txtCurPeriod
			//
			this.txtCurPeriod.Enabled = false;
			this.txtCurPeriod.Location = new System.Drawing.Point(24, 112);
			this.txtCurPeriod.Name = "txtCurPeriod";
			this.txtCurPeriod.Size = new System.Drawing.Size(152, 20);
			this.txtCurPeriod.TabIndex = 1;
			this.txtCurPeriod.Text = "";
			//
			//lblCurrPeriod
			//
			this.lblCurrPeriod.Location = new System.Drawing.Point(24, 88);
			this.lblCurrPeriod.Name = "lblCurrPeriod";
			this.lblCurrPeriod.Size = new System.Drawing.Size(100, 16);
			this.lblCurrPeriod.TabIndex = 2;
			this.lblCurrPeriod.Text = "Current Period";
			//
			//frmPeriod
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(200, 157);
			this.Controls.Add(this.lblCurrPeriod);
			this.Controls.Add(this.txtCurPeriod);
			this.Controls.Add(this.cmdGetData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPeriod";
			this.Text = "Current Period";
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		private SAPbouiCOM.Application SBO_Application;
		
		
		private void SetApplication ()
		{
			
			//*******************************************************************
			// Use an SboGuiApi object to establish connection
			// with the SAP Business One application and return an
			// initialized appliction object
			//*******************************************************************
			
			SAPbouiCOM.SboGuiApi SboGuiApi;
			string sConnectionString;
			
			SboGuiApi = new SAPbouiCOM.SboGuiApi();
			
			// by following the steps specified above, the following
			// statment should be suficient for either development or run mode
			
			sConnectionString = Environment.GetCommandLineArgs().GetValue(1).ToString();
			
			// connect to a running SBO Application
			
			SboGuiApi.Connect(sConnectionString);
			
			// get an initialized application object
			
			SBO_Application = SboGuiApi.GetApplication(-1);
			
		}
		
		
		public frmPeriod() {
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			
			
			//*************************************************************
			// set SBO_Application with an initialized application object
			//*************************************************************
			
			SetApplication();
			
		}
		
		private void cmdGetData_Click (System.Object sender, System.EventArgs e)
		{
			txtCurPeriod.Text = SBO_Application.Company.CurrentPeriod.ToString();
		}
	}
	
}
