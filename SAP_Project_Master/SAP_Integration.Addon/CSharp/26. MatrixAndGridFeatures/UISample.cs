//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      ProgressBar.vb
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
    public class ProgressBar : System.Windows.Forms.Form 
    { 
        
        #region '" Windows Form Designer generated code "' 
        
        public ProgressBar() : base() 
        { 
            
            
            // This call is required by the Windows Form Designer.
            InitializeComponent(); 
            
            // Add any initialization after the InitializeComponent() call
            
        } 
        
        // Form overrides dispose to clean up the component list.
        protected override void Dispose( bool disposing ) 
        { 
            if ( disposing ) 
            { 
                if ( !( ( components == null ) ) ) 
                { 
                    components.Dispose(); 
                } 
            } 
            base.Dispose( disposing ); 
        } 
        
        
        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components; 
        
        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        internal  System.Windows.Forms.Button cmdAddMat; 
        internal  System.Windows.Forms.Button cmdMatSortable; 
        internal  System.Windows.Forms.Button cmdMatUnSort; 
        internal  System.Windows.Forms.Button cmdSort; 
        internal  System.Windows.Forms.Button cmdSortDesc; 
        internal  System.Windows.Forms.Button cmdAddGrid; 
        internal  System.Windows.Forms.Button cmdGridSortable; 
        internal  System.Windows.Forms.Button cmdGridUnSort; 
        internal  System.Windows.Forms.Button cmdSortDescGrid; 
        internal  System.Windows.Forms.Button cmdSortGrid; 
        internal  System.Windows.Forms.Button cmdExpandGrid; 
        internal  System.Windows.Forms.Button cmdCollapseGrid; 
        internal  System.Windows.Forms.Label Label1; 
        internal  System.Windows.Forms.Button cmdSerXML; 
        internal  System.Windows.Forms.TextBox txtMatXML; 
        internal  System.Windows.Forms.Button Button2; 
        internal  System.Windows.Forms.ComboBox cmbSerMatXML; 
        internal  System.Windows.Forms.Button cmdSerXMLGrid; 
        internal  System.Windows.Forms.ComboBox cmbSerGridXML; 
        internal  System.Windows.Forms.TextBox txtGridXML; 
        internal  System.Windows.Forms.ComboBox cmbLoadSerGridXML;
		private AxSHDocVw.AxWebBrowser browserHelp; 
        internal  System.Windows.Forms.CheckBox chkSort; 
        [ System.Diagnostics.DebuggerStepThrough() ]
        private void InitializeComponent() 
        { 
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProgressBar));
			this.cmdAddMat = new System.Windows.Forms.Button();
			this.cmdMatSortable = new System.Windows.Forms.Button();
			this.cmdMatUnSort = new System.Windows.Forms.Button();
			this.cmdSort = new System.Windows.Forms.Button();
			this.cmdSortDesc = new System.Windows.Forms.Button();
			this.cmdAddGrid = new System.Windows.Forms.Button();
			this.cmdGridSortable = new System.Windows.Forms.Button();
			this.cmdGridUnSort = new System.Windows.Forms.Button();
			this.cmdSortDescGrid = new System.Windows.Forms.Button();
			this.cmdSortGrid = new System.Windows.Forms.Button();
			this.cmdExpandGrid = new System.Windows.Forms.Button();
			this.cmdCollapseGrid = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdSerXML = new System.Windows.Forms.Button();
			this.txtMatXML = new System.Windows.Forms.TextBox();
			this.txtGridXML = new System.Windows.Forms.TextBox();
			this.cmdSerXMLGrid = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.cmbSerMatXML = new System.Windows.Forms.ComboBox();
			this.cmbSerGridXML = new System.Windows.Forms.ComboBox();
			this.cmbLoadSerGridXML = new System.Windows.Forms.ComboBox();
			this.chkSort = new System.Windows.Forms.CheckBox();
			this.browserHelp = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.browserHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdAddMat
			// 
			this.cmdAddMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.cmdAddMat.Location = new System.Drawing.Point(16, 88);
			this.cmdAddMat.Name = "cmdAddMat";
			this.cmdAddMat.Size = new System.Drawing.Size(224, 40);
			this.cmdAddMat.TabIndex = 0;
			this.cmdAddMat.Text = "Add Matrix";
			this.cmdAddMat.Click += new System.EventHandler(this.cmdAddMat_Click);
			// 
			// cmdMatSortable
			// 
			this.cmdMatSortable.Location = new System.Drawing.Point(16, 144);
			this.cmdMatSortable.Name = "cmdMatSortable";
			this.cmdMatSortable.Size = new System.Drawing.Size(104, 40);
			this.cmdMatSortable.TabIndex = 1;
			this.cmdMatSortable.Text = "Make Matrix  columns sortable";
			this.cmdMatSortable.Click += new System.EventHandler(this.cmdMatSortable_Click);
			// 
			// cmdMatUnSort
			// 
			this.cmdMatUnSort.Location = new System.Drawing.Point(16, 200);
			this.cmdMatUnSort.Name = "cmdMatUnSort";
			this.cmdMatUnSort.Size = new System.Drawing.Size(104, 40);
			this.cmdMatUnSort.TabIndex = 2;
			this.cmdMatUnSort.Text = "Disable Matrix column sorting";
			this.cmdMatUnSort.Click += new System.EventHandler(this.cmdMatUnSort_Click);
			// 
			// cmdSort
			// 
			this.cmdSort.Location = new System.Drawing.Point(136, 144);
			this.cmdSort.Name = "cmdSort";
			this.cmdSort.Size = new System.Drawing.Size(104, 40);
			this.cmdSort.TabIndex = 3;
			this.cmdSort.Text = "Sort first column Ascending";
			this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
			// 
			// cmdSortDesc
			// 
			this.cmdSortDesc.Location = new System.Drawing.Point(136, 200);
			this.cmdSortDesc.Name = "cmdSortDesc";
			this.cmdSortDesc.Size = new System.Drawing.Size(104, 40);
			this.cmdSortDesc.TabIndex = 4;
			this.cmdSortDesc.Text = "Sort first column Descending";
			this.cmdSortDesc.Click += new System.EventHandler(this.cmdSortDesc_Click);
			// 
			// cmdAddGrid
			// 
			this.cmdAddGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.cmdAddGrid.Location = new System.Drawing.Point(16, 288);
			this.cmdAddGrid.Name = "cmdAddGrid";
			this.cmdAddGrid.Size = new System.Drawing.Size(224, 40);
			this.cmdAddGrid.TabIndex = 5;
			this.cmdAddGrid.Text = "Add Grid";
			this.cmdAddGrid.Click += new System.EventHandler(this.cmdAddGrid_Click);
			// 
			// cmdGridSortable
			// 
			this.cmdGridSortable.Location = new System.Drawing.Point(16, 344);
			this.cmdGridSortable.Name = "cmdGridSortable";
			this.cmdGridSortable.Size = new System.Drawing.Size(104, 40);
			this.cmdGridSortable.TabIndex = 6;
			this.cmdGridSortable.Text = "Make Grid columns sortable";
			this.cmdGridSortable.Click += new System.EventHandler(this.cmdGridSortable_Click);
			// 
			// cmdGridUnSort
			// 
			this.cmdGridUnSort.Location = new System.Drawing.Point(16, 400);
			this.cmdGridUnSort.Name = "cmdGridUnSort";
			this.cmdGridUnSort.Size = new System.Drawing.Size(104, 40);
			this.cmdGridUnSort.TabIndex = 7;
			this.cmdGridUnSort.Text = "Disable Grid column sorting";
			this.cmdGridUnSort.Click += new System.EventHandler(this.cmdGridUnSort_Click);
			// 
			// cmdSortDescGrid
			// 
			this.cmdSortDescGrid.Location = new System.Drawing.Point(136, 400);
			this.cmdSortDescGrid.Name = "cmdSortDescGrid";
			this.cmdSortDescGrid.Size = new System.Drawing.Size(104, 40);
			this.cmdSortDescGrid.TabIndex = 9;
			this.cmdSortDescGrid.Text = "Sort first column Descending";
			this.cmdSortDescGrid.Click += new System.EventHandler(this.cmdSortDescGrid_Click);
			// 
			// cmdSortGrid
			// 
			this.cmdSortGrid.Location = new System.Drawing.Point(136, 344);
			this.cmdSortGrid.Name = "cmdSortGrid";
			this.cmdSortGrid.Size = new System.Drawing.Size(104, 40);
			this.cmdSortGrid.TabIndex = 8;
			this.cmdSortGrid.Text = "Sort first column Ascending";
			this.cmdSortGrid.Click += new System.EventHandler(this.cmdSortGrid_Click);
			// 
			// cmdExpandGrid
			// 
			this.cmdExpandGrid.Location = new System.Drawing.Point(136, 456);
			this.cmdExpandGrid.Name = "cmdExpandGrid";
			this.cmdExpandGrid.Size = new System.Drawing.Size(104, 40);
			this.cmdExpandGrid.TabIndex = 10;
			this.cmdExpandGrid.Text = "Expand Grid";
			this.cmdExpandGrid.Click += new System.EventHandler(this.cmdExpandGrid_Click);
			// 
			// cmdCollapseGrid
			// 
			this.cmdCollapseGrid.Location = new System.Drawing.Point(16, 456);
			this.cmdCollapseGrid.Name = "cmdCollapseGrid";
			this.cmdCollapseGrid.Size = new System.Drawing.Size(104, 40);
			this.cmdCollapseGrid.TabIndex = 11;
			this.cmdCollapseGrid.Text = "Collapse Grid";
			this.cmdCollapseGrid.Click += new System.EventHandler(this.cmdCollapseGrid_Click);
			// 
			// Label1
			// 
			this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.Label1.Location = new System.Drawing.Point(16, 8);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(280, 32);
			this.Label1.TabIndex = 12;
			this.Label1.Text = "Matrix and Grid enhancements";
			// 
			// cmdSerXML
			// 
			this.cmdSerXML.Location = new System.Drawing.Point(256, 56);
			this.cmdSerXML.Name = "cmdSerXML";
			this.cmdSerXML.Size = new System.Drawing.Size(184, 23);
			this.cmdSerXML.TabIndex = 13;
			this.cmdSerXML.Text = "Serialize Matrix XML -->";
			this.cmdSerXML.Click += new System.EventHandler(this.cmdSerXML_Click);
			// 
			// txtMatXML
			// 
			this.txtMatXML.Location = new System.Drawing.Point(256, 96);
			this.txtMatXML.Multiline = true;
			this.txtMatXML.Name = "txtMatXML";
			this.txtMatXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMatXML.Size = new System.Drawing.Size(328, 152);
			this.txtMatXML.TabIndex = 14;
			this.txtMatXML.Text = "";
			// 
			// txtGridXML
			// 
			this.txtGridXML.Location = new System.Drawing.Point(256, 312);
			this.txtGridXML.Multiline = true;
			this.txtGridXML.Name = "txtGridXML";
			this.txtGridXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtGridXML.Size = new System.Drawing.Size(336, 152);
			this.txtGridXML.TabIndex = 16;
			this.txtGridXML.Text = "";
			// 
			// cmdSerXMLGrid
			// 
			this.cmdSerXMLGrid.Location = new System.Drawing.Point(256, 280);
			this.cmdSerXMLGrid.Name = "cmdSerXMLGrid";
			this.cmdSerXMLGrid.Size = new System.Drawing.Size(184, 23);
			this.cmdSerXMLGrid.TabIndex = 15;
			this.cmdSerXMLGrid.Text = "Serialize Grid XML -->";
			this.cmdSerXMLGrid.Click += new System.EventHandler(this.cmdSerXMLGrid_Click);
			// 
			// Button2
			// 
			this.Button2.Location = new System.Drawing.Point(256, 472);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(184, 23);
			this.Button2.TabIndex = 17;
			this.Button2.Text = "<-- Load Serialized XML to Grid";
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// cmbSerMatXML
			// 
			this.cmbSerMatXML.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSerMatXML.Items.AddRange(new object[] {
															  "Metadata and data",
															  "Metadata"});
			this.cmbSerMatXML.Location = new System.Drawing.Point(456, 56);
			this.cmbSerMatXML.Name = "cmbSerMatXML";
			this.cmbSerMatXML.Size = new System.Drawing.Size(121, 21);
			this.cmbSerMatXML.TabIndex = 18;
			// 
			// cmbSerGridXML
			// 
			this.cmbSerGridXML.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSerGridXML.Items.AddRange(new object[] {
															   "Metadata and data",
															   "Metadata",
															   "Data"});
			this.cmbSerGridXML.Location = new System.Drawing.Point(456, 280);
			this.cmbSerGridXML.Name = "cmbSerGridXML";
			this.cmbSerGridXML.Size = new System.Drawing.Size(121, 21);
			this.cmbSerGridXML.TabIndex = 19;
			// 
			// cmbLoadSerGridXML
			// 
			this.cmbLoadSerGridXML.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLoadSerGridXML.Items.AddRange(new object[] {
																   "Metadata and data",
																   "Metadata",
																   "Data"});
			this.cmbLoadSerGridXML.Location = new System.Drawing.Point(456, 472);
			this.cmbLoadSerGridXML.Name = "cmbLoadSerGridXML";
			this.cmbLoadSerGridXML.Size = new System.Drawing.Size(121, 21);
			this.cmbLoadSerGridXML.TabIndex = 20;
			// 
			// chkSort
			// 
			this.chkSort.Location = new System.Drawing.Point(16, 248);
			this.chkSort.Name = "chkSort";
			this.chkSort.Size = new System.Drawing.Size(144, 24);
			this.chkSort.TabIndex = 22;
			this.chkSort.Text = "Listen to sort events";
			// 
			// browserHelp
			// 
			this.browserHelp.Enabled = true;
			this.browserHelp.Location = new System.Drawing.Point(16, 512);
			this.browserHelp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("browserHelp.OcxState")));
			this.browserHelp.Size = new System.Drawing.Size(584, 192);
			this.browserHelp.TabIndex = 23;
			// 
			// ProgressBar
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(618, 719);
			this.Controls.Add(this.browserHelp);
			this.Controls.Add(this.chkSort);
			this.Controls.Add(this.cmbLoadSerGridXML);
			this.Controls.Add(this.cmbSerGridXML);
			this.Controls.Add(this.cmbSerMatXML);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.txtGridXML);
			this.Controls.Add(this.cmdSerXMLGrid);
			this.Controls.Add(this.txtMatXML);
			this.Controls.Add(this.cmdSerXML);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.cmdCollapseGrid);
			this.Controls.Add(this.cmdExpandGrid);
			this.Controls.Add(this.cmdSortDescGrid);
			this.Controls.Add(this.cmdSortGrid);
			this.Controls.Add(this.cmdGridUnSort);
			this.Controls.Add(this.cmdGridSortable);
			this.Controls.Add(this.cmdAddGrid);
			this.Controls.Add(this.cmdSortDesc);
			this.Controls.Add(this.cmdSort);
			this.Controls.Add(this.cmdMatUnSort);
			this.Controls.Add(this.cmdMatSortable);
			this.Controls.Add(this.cmdAddMat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ProgressBar";
			this.Text = "UI Sample";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ProgressBar_Load);
			((System.ComponentModel.ISupportInitialize)(this.browserHelp)).EndInit();
			this.ResumeLayout(false);

		} 
        
        
        #endregion 
        
        private  SAPbouiCOM.Application SBO_Application; 
        private SAPbouiCOM.IForm oForm; 
        public SAPbouiCOM.DBDataSource oDBDataSource; 
        public SAPbouiCOM.DBDataSource oDBDataSource1; 
        public SAPbouiCOM.DBDataSource oDBDataSource2; 
        public SAPbouiCOM.DBDataSource oDBDataSource3; 
        public SAPbouiCOM.IItem oItem; 
        public SAPbouiCOM.IMatrix oMatrix; 
        public SAPbouiCOM.IColumn oColumn; 
        
        public SAPbouiCOM.IGrid oGrid; 
        public SAPbouiCOM.IDataTable oDataTable; 
        
        
        private void ProgressBar_Load( System.Object sender, System.EventArgs e ) 
        { 			
            SetApplication(); 
			// events handled by SBO_Application_ItemEvent
			SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 

            CreateForm(); 
            cmbSerMatXML.SelectedIndex = 0; 
            cmbSerGridXML.SelectedIndex = 0; 
            cmbLoadSerGridXML.SelectedIndex = 0; 
            
            string sFilePath = null; 
            sFilePath = System.Environment.CurrentDirectory; 
            sFilePath = sFilePath.Remove( sFilePath.Length - 9, 9 ); 
            sFilePath = sFilePath + "MatrixSerializeAsXML.htm"; 
            
            browserHelp.Navigate(sFilePath);
            
        } 
        
        
        private void CreateForm() 
        { 
            SAPbouiCOM.BoLanguages olan = 0; 
            olan = SBO_Application.Language; 
            
            SAPbouiCOM.FormCreationParams oFormParams = null; 
            oFormParams = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
            oFormParams.FormType = "MyForm"; 
            oFormParams.UniqueID = "MyForm"; 
            
            try 
            { 
				oForm = SBO_Application.Forms.AddEx( oFormParams); 
			} 
            catch  
            { 
                oForm = SBO_Application.Forms.Item( "MyForm" ); 
                oForm.Close(); 
                oForm = SBO_Application.Forms.AddEx( oFormParams ); 
            } 
            SAPbouiCOM.IForm tmpForm = oForm;
            tmpForm.Top = 50; 
            tmpForm.ClientHeight = 600; 
            tmpForm.ClientWidth = 400; 
            tmpForm.Title = "UI Sample Form"; 
            tmpForm.Visible = true; 
            
            
        } 
        
        
        private void SetApplication() 
        { 
            
            // *******************************************************************
            // Use an SboGuiApi object to establish the connection
            // with the application and rturn an initialized appliction object
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
        
        
        private void cmdAddMat_Click( System.Object sender, System.EventArgs e ) 
        { 
            SAPbouiCOM.IColumn oColumn = null; 
            SAPbouiCOM.ILinkedButton oLinkButton = null; 
            
            oDBDataSource = oForm.DataSources.DBDataSources.Add( "OCRD" ); 
            oDBDataSource1 = oForm.DataSources.DBDataSources.Add( "OITM" ); 
            oDBDataSource2 = oForm.DataSources.DBDataSources.Add( "OINV" ); 
            oDBDataSource3 = oForm.DataSources.DBDataSources.Add( "INV1" ); 
            
            
            oItem = oForm.Items.Add( "Matrix", SAPbouiCOM.BoFormItemTypes.it_MATRIX ); 
            oItem.Left = 10; 
            oItem.Top = 10; 
            oItem.Height = System.Convert.ToInt32( ( oForm.ClientHeight / 2 ) - 10 ); 
            oItem.Width = oForm.ClientWidth - 10; 
            
            oMatrix = ( ( SAPbouiCOM.IMatrix )( oItem.Specific ) ); 
            
            oMatrix.Layout = SAPbouiCOM.BoMatrixLayoutType.mlt_Normal; 
            oMatrix.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto; 
            
            oColumn = oMatrix.Columns.Add( "col0", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oColumn.Editable = false; 
            oColumn.ForeColor = 1; 
            oColumn.TitleObject.Caption = "#"; 
            
            oColumn = oMatrix.Columns.Add( "CardCode", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oColumn.Width = 30; 
            oColumn.Description = "col1"; 
            oColumn.TitleObject.Caption = "CardCode"; 
            oColumn.DisplayDesc = false; 
            oColumn.DataBind.SetBound( true, "OCRD", "CardCode" ); 
            
            oColumn = oMatrix.Columns.Add( "ItemCode", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oColumn.Width = 30; 
            oColumn.Description = "col2"; 
            oColumn.TitleObject.Caption = "OITM - ItemCode"; 
            oColumn.DisplayDesc = false; 
            oColumn.DataBind.SetBound( true, "OITM", "ItemCode" ); 
            
            oColumn = oMatrix.Columns.Add( "DocDate", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oColumn.Width = 30; 
            oColumn.Description = "col2"; 
            oColumn.TitleObject.Caption = "Date"; 
            oColumn.DisplayDesc = false; 
            oColumn.DataBind.SetBound( true, "OINV", "DocDate" ); 
            
            oColumn = oMatrix.Columns.Add( "LineTotal", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oColumn.Width = 30; 
            oColumn.Description = "col2"; 
            oColumn.TitleObject.Caption = "Number"; 
            oColumn.DisplayDesc = false; 
            oColumn.DataBind.SetBound( true, "INV1", "LineTotal" ); 
            
            oMatrix.Clear(); 
            oMatrix.AutoResizeColumns(); 
            oDBDataSource.Query( null ); 
            oDBDataSource1.Query( null ); 
            oDBDataSource2.Query( null ); 
            oDBDataSource3.Query( null ); 
            
            oMatrix.LoadFromDataSource(); 
            
        } 
        
        
        private void cmdMatSortable_Click( System.Object sender, System.EventArgs e ) 
        { 
            oMatrix = ( ( SAPbouiCOM.IMatrix )( oForm.Items.Item( "Matrix" ).Specific ) ); 
            
            oColumn = oMatrix.Columns.Item( "CardCode" ); 
            oColumn.TitleObject.Sortable = true; 
            
            oColumn = oMatrix.Columns.Item( "ItemCode" ); 
            oColumn.TitleObject.Sortable = true; 
            
            oColumn = oMatrix.Columns.Item( "DocDate" ); 
            oColumn.TitleObject.Sortable = true; 
            
            oColumn = oMatrix.Columns.Item( "LineTotal" ); 
            oColumn.TitleObject.Sortable = true; 
            
        } 
        
        
        private void cmdMatUnSort_Click( System.Object sender, System.EventArgs e ) 
        { 
            oMatrix = ( ( SAPbouiCOM.IMatrix )( oForm.Items.Item( "Matrix" ).Specific ) ); 
            
            oColumn = oMatrix.Columns.Item( "CardCode" ); 
            oColumn.TitleObject.Sortable = false; 
            
            oColumn = oMatrix.Columns.Item( "ItemCode" ); 
            oColumn.TitleObject.Sortable = false; 
            
            oColumn = oMatrix.Columns.Item( "DocDate" ); 
            oColumn.TitleObject.Sortable = false; 
            
            oColumn = oMatrix.Columns.Item( "LineTotal" ); 
            oColumn.TitleObject.Sortable = false; 
        } 
        
        
        private void cmdSort_Click( System.Object sender, System.EventArgs e ) 
        { 
            
            oMatrix = ( ( SAPbouiCOM.IMatrix )( oForm.Items.Item( "Matrix" ).Specific ) ); 
            
            oColumn = oMatrix.Columns.Item( "CardCode" ); 
            try 
            { 
                oColumn.TitleObject.Sort( SAPbouiCOM.BoGridSortType.gst_Ascending ); 
            } 
            catch ( Exception ex ) 
            { 
                SBO_Application.MessageBox( "Column is unsortable", 1, "Ok", "", "" ); 
            } 
            
        } 
        
        
        private void cmdSortDesc_Click( System.Object sender, System.EventArgs e ) 
        { 
            
            oMatrix = ( ( SAPbouiCOM.IMatrix )( oForm.Items.Item( "Matrix" ).Specific ) ); 
            
            oColumn = oMatrix.Columns.Item( "CardCode" ); 
            try 
            { 
                oColumn.TitleObject.Sort( SAPbouiCOM.BoGridSortType.gst_Descending ); 
            } 
            catch ( Exception ex ) 
            { 
                SBO_Application.MessageBox( "Column is unsortable", 1, "Ok", "", "" ); 
            } 
            
        } 
        
        
        private void cmdAddGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            
            string queryStr = null; 
            SAPbouiCOM.IEditTextColumn oEditColumn = null; 
            SAPbouiCOM.IComboBoxColumn oComboColumn = null; 
            
            oItem = oForm.Items.Add( "MyGrid", SAPbouiCOM.BoFormItemTypes.it_GRID ); 
            oItem.Left = 10; 
            
            oItem.Top = System.Convert.ToInt32( ( oForm.ClientHeight / 2 ) ); 
            oItem.Height = System.Convert.ToInt32( ( oForm.ClientHeight / 2 ) - 10 ); 
            oItem.Width = oForm.ClientWidth - 10; 
            
            
            oGrid = ( ( SAPbouiCOM.IGrid )( oItem.Specific ) ); 
            oGrid.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_None; 
            oDataTable = oForm.DataSources.DataTables.Add( "MyDataTable" ); 
            
            oDataTable.ExecuteQuery( "Select CardCode, CardName, DocDate, ExcRefDate from OINV" ); 
            oGrid.DataTable = ( ( SAPbouiCOM.DataTable )( oDataTable ) ); 
            
        } 
        
        
        private void cmdGridSortable_Click( System.Object sender, System.EventArgs e ) 
        { 
            
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            
            oGrid.Columns.Item( 0 ).TitleObject.Sortable = true; 
            oGrid.Columns.Item( 1 ).TitleObject.Sortable = true; 
            oGrid.Columns.Item( 2 ).TitleObject.Sortable = true; 
            oGrid.Columns.Item( 3 ).TitleObject.Sortable = true; 
            
        } 
        
        
        private void cmdGridUnSort_Click( System.Object sender, System.EventArgs e ) 
        { 
            
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            
            oGrid.Columns.Item( 0 ).TitleObject.Sortable = false; 
            oGrid.Columns.Item( 1 ).TitleObject.Sortable = false; 
            oGrid.Columns.Item( 2 ).TitleObject.Sortable = false; 
            oGrid.Columns.Item( 3 ).TitleObject.Sortable = false; 
            
        } 
        
        
        private void cmdSortGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            try 
            { 
                oGrid.Columns.Item( 0 ).TitleObject.Sort( SAPbouiCOM.BoGridSortType.gst_Ascending ); 
                
            } 
            catch ( Exception ex ) 
            { 
                if ( oGrid.Columns.Item( 0 ).TitleObject.Sortable == true ) 
                { 
                    SBO_Application.MessageBox( "Cannot sort when grid is collapsed", 1, "Ok", "", "" ); 
                } 
                else 
                { 
                    SBO_Application.MessageBox( "Column is unsortable", 1, "Ok", "", "" ); 
                } 
            } 
        } 
        
        
        private void cmdSortDescGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            try 
            { 
                oGrid.Columns.Item( 0 ).TitleObject.Sort( SAPbouiCOM.BoGridSortType.gst_Descending ); 
            } 
            catch ( Exception ex ) 
            { 
                if ( oGrid.Columns.Item( 0 ).TitleObject.Sortable == true ) 
                { 
                    SBO_Application.MessageBox( "Cannot sort when grid is collapsed", 1, "Ok", "", "" ); 
                } 
                else 
                { 
                    SBO_Application.MessageBox( "Column is unsortable", 1, "Ok", "", "" ); 
                } 
            }             
        } 
        
        
        private void cmdCollapseGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            oGrid.CollapseLevel = 1; 
        } 
        
        
        private void cmdExpandGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
            oGrid.CollapseLevel = 0; 
        } 
        
        
        private void cmdSerXML_Click( System.Object sender, System.EventArgs e ) 
        { 
            try 
            { 
                string sXML = null; 
                
                oMatrix = ( ( SAPbouiCOM.IMatrix )( oForm.Items.Item( "Matrix" ).Specific ) ); 
                
                if ( cmbSerMatXML.SelectedIndex == 0 ) 
                { 
                    sXML = oMatrix.SerializeAsXML( SAPbouiCOM.BoMatrixXmlSelect.mxs_All ); 
                } 
                else 
                { 
                    sXML = oMatrix.SerializeAsXML( SAPbouiCOM.BoMatrixXmlSelect.mxs_MetaData ); 
                }                 
                txtMatXML.Text = sXML;                 
            } 
            catch ( Exception ex ) 
            { 
                MessageBox.Show( ex.Message ); 
            }                        
        } 
        
        
        private void cmdSerXMLGrid_Click( System.Object sender, System.EventArgs e ) 
        { 
            try 
            { 
                string sXML = null; 
                
                oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
                
                if ( cmbSerGridXML.SelectedIndex == 0 ) 
                { 
                    sXML = oGrid.DataTable.SerializeAsXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_All ); 
                } 
                else if ( cmbSerGridXML.SelectedIndex == 1 ) 
                { 
                    sXML = oGrid.DataTable.SerializeAsXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_MetaData ); 
                } 
                else 
                { 
                    sXML = oGrid.DataTable.SerializeAsXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly ); 
                } 
                
                txtGridXML.Text = sXML;                 
            } 
            catch ( Exception ex ) 
            {                 
                MessageBox.Show( ex.Message ); 
            }             
        } 
        
        
        private void Button2_Click( System.Object sender, System.EventArgs e ) 
        { 
            try 
            { 
                string sXML = null; 
                
                oGrid = ( ( SAPbouiCOM.IGrid )( oForm.Items.Item( "MyGrid" ).Specific ) ); 
                sXML = txtGridXML.Text; 
                
                if ( cmbLoadSerGridXML.SelectedIndex == 0 ) 
                { 
                    oGrid.DataTable.LoadSerializedXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_All, sXML ); 
                } 
                else if ( cmbLoadSerGridXML.SelectedIndex == 1 ) 
                { 
                    oGrid.DataTable.LoadSerializedXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_MetaData, sXML ); 
                } 
                else 
                { 
                    oGrid.DataTable.LoadSerializedXML( SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly, sXML ); 
                }                 
            } 
            catch ( Exception ex ) 
            {                 
                MessageBox.Show( ex.Message ); 
            }             
        } 
        
		private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) 		     
        { 
			BubbleEvent = true;
			if ( ( chkSort.Checked == true ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_GRID_SORT ) & ( pVal.Before_Action == true ) ) 
            { 
                SBO_Application.MessageBox( "Sort event was fired.", 1, "Ok", "", "" ); 
            } 
        } 
              
        [STAThread]
        public static void Main() { Application.Run( new ProgressBar() ); }
    }     
} 
