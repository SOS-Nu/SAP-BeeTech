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
//  File:      frmDataEvent.cs
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


namespace DataEvent
{
	public class FrmDataEvent : System.Windows.Forms.Form
	{
		
		[STAThread]
		static void Main()
		{
			Application.Run(new FrmDataEvent());
		}
		
		
		#region " Windows Form Designer generated code "
		
		public FrmDataEvent() {
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			
		}
		
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
		internal System.Windows.Forms.Label lblWatch;
		internal System.Data.DataSet EventsData;
		internal System.Data.DataColumn DataColumn1;
		internal System.Data.DataColumn DataColumn2;
		internal System.Data.DataColumn DataColumn3;
		internal System.Data.DataColumn DataColumn4;
		internal System.Data.DataColumn DataColumn5;
		internal System.Data.DataColumn DataColumn6;
		internal System.Data.DataColumn DataColumn7;
		internal System.Windows.Forms.DataGrid GridDataEvent;
		internal System.Data.DataTable tblDataEvent;
		internal System.Windows.Forms.Label Label1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent ()
		{
			this.lblWatch = new System.Windows.Forms.Label();
			base.Load += new System.EventHandler(FrmDataEvent_Load);
			base.Resize += new System.EventHandler(FrmDataEvent_Resize);
			this.GridDataEvent = new System.Windows.Forms.DataGrid();
			this.tblDataEvent = new System.Data.DataTable();
			this.DataColumn1 = new System.Data.DataColumn();
			this.DataColumn2 = new System.Data.DataColumn();
			this.DataColumn3 = new System.Data.DataColumn();
			this.DataColumn4 = new System.Data.DataColumn();
			this.DataColumn5 = new System.Data.DataColumn();
			this.DataColumn6 = new System.Data.DataColumn();
			this.DataColumn7 = new System.Data.DataColumn();
			this.EventsData = new System.Data.DataSet();
			this.Label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) this.GridDataEvent).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.tblDataEvent).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.EventsData).BeginInit();
			this.SuspendLayout();
			//
			//lblWatch
			//
			this.lblWatch.Location = new System.Drawing.Point(16, 16);
			this.lblWatch.Name = "lblWatch";
			this.lblWatch.Size = new System.Drawing.Size(552, 16);
			this.lblWatch.TabIndex = 1;
			this.lblWatch.Text = "This sample watches all the data events and loggs them in a table. ";
			//
			//GridDataEvent
			//
			this.GridDataEvent.DataMember = "";
			this.GridDataEvent.DataSource = this.tblDataEvent;
			this.GridDataEvent.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.GridDataEvent.Location = new System.Drawing.Point(0, 72);
			this.GridDataEvent.Name = "GridDataEvent";
			this.GridDataEvent.Size = new System.Drawing.Size(600, 280);
			this.GridDataEvent.TabIndex = 3;
			//
			//tblDataEvent
			//
			this.tblDataEvent.Columns.AddRange(new System.Data.DataColumn[] { this.DataColumn1, this.DataColumn2, this.DataColumn3, this.DataColumn4, this.DataColumn5, this.DataColumn6, this.DataColumn7 });
			this.tblDataEvent.TableName = "DataEventTable";
			//
			//DataColumn1
			//
			this.DataColumn1.ColumnName = "ActionSuccess";
			//
			//DataColumn2
			//
			this.DataColumn2.ColumnName = "BeforeAction";
			//
			//DataColumn3
			//
			this.DataColumn3.ColumnName = "EventType";
			//
			//DataColumn4
			//
			this.DataColumn4.ColumnName = "FormType";
			//
			//DataColumn5
			//
			this.DataColumn5.ColumnName = "FormUID";
			//
			//DataColumn6
			//
			this.DataColumn6.ColumnName = "ObjectKey";
			//
			//DataColumn7
			//
			this.DataColumn7.ColumnName = "Type";
			//
			//EventsData
			//
			this.EventsData.DataSetName = "DataEventSet";
			this.EventsData.Locale = new System.Globalization.CultureInfo("en-US");
			this.EventsData.Tables.AddRange(new System.Data.DataTable[] {this.tblDataEvent});
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(16, 32);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(552, 16);
			this.Label1.TabIndex = 4;
			this.Label1.Text = "Try to Add,Update,Delete or Load any data in SBO and watch the events being logge" + "d";
			//
			//FrmDataEvent
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 349);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.GridDataEvent);
			this.Controls.Add(this.lblWatch);
			this.Name = "FrmDataEvent";
			this.Text = "Data Event";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize) this.GridDataEvent).EndInit();
			((System.ComponentModel.ISupportInitialize) this.tblDataEvent).EndInit();
			((System.ComponentModel.ISupportInitialize) this.EventsData).EndInit();
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		//**********************************************************
		// This data member will use us to manipulate the
		// SAP Business One Application
		//**********************************************************
		
		private SAPbouiCOM.Application SBO_Application;
		
		//***********************************************************************
		// This Sub is the main idea of this sample - to use Data Events.
		// For each Data Event fired from SBO a new line is created in the grid.
		//***********************************************************************
		public void FormDataEvent (ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
		{
			BubbleEvent = true;
			int nor;
			DataRow NewRow;
			
			NewRow = EventsData.Tables["DataEventTable"].NewRow();
			
			nor = EventsData.Tables["DataEventTable"].Rows.Count;
			
			NewRow[0] = BusinessObjectInfo.ActionSuccess.ToString();
			NewRow[1] = BusinessObjectInfo.BeforeAction.ToString();
			NewRow[2] = BusinessObjectInfo.EventType.ToString();
			NewRow[3] = BusinessObjectInfo.FormTypeEx.ToString();
			NewRow[4] = BusinessObjectInfo.FormUID.ToString();
			NewRow[5] = BusinessObjectInfo.ObjectKey.ToString();
			NewRow[6] = BusinessObjectInfo.Type.ToString();
			
			EventsData.Tables["DataEventTable"].Rows.Add(NewRow);
		}
		
		private void FrmDataEvent_Load (System.Object sender, System.EventArgs e)
		{
			SetApplication();
		}
		
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
			
			sConnectionString = System.Convert.ToString(Environment.GetCommandLineArgs().GetValue(1));
			
			// connect to a running SBO Application
			
			SboGuiApi.Connect(sConnectionString);
			
			// get an initialized application object
			
			SBO_Application = SboGuiApi.GetApplication(-1);
			// events handled by SBO_Application
			SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(ref FormDataEvent);
		}
		
		private void FrmDataEvent_Resize (object sender, System.EventArgs e)
		{
			GridDataEvent.Width = this.Width - 5;
			GridDataEvent.Height = this.ClientSize.Height - 5 - GridDataEvent.Top;
		}
	}
	
}
