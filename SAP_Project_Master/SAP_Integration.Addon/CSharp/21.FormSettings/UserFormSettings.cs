using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace FormSettings {
    public class Form1 : System.Windows.Forms.Form { 
        private SAPbouiCOM.Application SBO_Application; 
        public SAPbouiCOM.Form oForm; 
        public bool bFirstTime; 
        public SAPbouiCOM.Matrix oMatrix; 
        public SAPbouiCOM.Columns oColumns; 
        
        #region ' Windows Form Designer generated code ' 
        
        public Form1() { 
            
            
            // This call is required by the Windows Form Designer.
            InitializeComponent(); 
            
            // Add any initialization after the InitializeComponent() call
            SetApplication(); 
            
        } 
        
        // Form overrides dispose to clean up the component list.
        protected override void Dispose( bool disposing ) { 
            if ( disposing ) { 
                if ( !( ( components == null ) ) ) { 
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
        internal System.Windows.Forms.Button BMatrixForm; 
        internal System.Windows.Forms.Button SwitchGrid; 
        [ System.Diagnostics.DebuggerStepThrough() ]
        private void InitializeComponent() { 
			this.SwitchGrid = new System.Windows.Forms.Button();
			this.BMatrixForm = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SwitchGrid
			// 
			this.SwitchGrid.Enabled = false;
			this.SwitchGrid.Location = new System.Drawing.Point(48, 80);
			this.SwitchGrid.Name = "SwitchGrid";
			this.SwitchGrid.Size = new System.Drawing.Size(152, 24);
			this.SwitchGrid.TabIndex = 2;
			this.SwitchGrid.Text = "SwitchGrid";
			this.SwitchGrid.Click += new System.EventHandler(this.SwitchGrid_Click);
			// 
			// BMatrixForm
			// 
			this.BMatrixForm.Location = new System.Drawing.Point(48, 24);
			this.BMatrixForm.Name = "BMatrixForm";
			this.BMatrixForm.Size = new System.Drawing.Size(152, 24);
			this.BMatrixForm.TabIndex = 0;
			this.BMatrixForm.Text = "MatrixForm";
			this.BMatrixForm.Click += new System.EventHandler(this.BMatrixForm_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 148);
			this.Controls.Add(this.BMatrixForm);
			this.Controls.Add(this.SwitchGrid);
			this.Name = "Form1";
			this.Text = "UserFormSettingsTester";
			this.ResumeLayout(false);

		} 
        
        
        #endregion 
        
        private void TabOrder_Click( System.Object sender, System.EventArgs e ) { 
            CreateFormCP(); 
            oForm.Freeze( true ); 
            try { 
                
                
                string EditName = null; 
                long lLeft = 0; 
                EditName = "Edit"; 
                lLeft = 400; 
                
                AddEdit( EditName + lLeft, lLeft ); 
                lLeft = lLeft - 100; 
                
                AddEdit( EditName + lLeft, lLeft ); 
                lLeft = lLeft - 100; 
                
                AddEdit( EditName + lLeft, lLeft ); 
                lLeft = lLeft - 100; 
                
                AddEdit( EditName + lLeft, lLeft ); 
                lLeft = lLeft - 100; 
                AddLables(); 
            } 
            catch  { 
                Interaction.MsgBox( Information.Err().Description, (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
                
            } 
            oForm.Freeze( false ); 
        } 
        
        
        private void SetApplication() { 
            
            // *******************************************************************
            // // Use an SboGuiApi object to establish connection
            // // with the SAP Business One application and return an
            // // initialized appliction object
            // *******************************************************************
            
            SAPbouiCOM.SboGuiApi SboGuiApi = null; 
            string sConnectionString = null; 
            
            SboGuiApi = new SAPbouiCOM.SboGuiApi(); 
            
            // // by following the steps specified above, the following
            // // statment should be suficient for either development or run mode
            
            sConnectionString = System.Convert.ToString( Environment.GetCommandLineArgs().GetValue( 1 ) ); 
            
            // // connect to a running SBO Application
            
            SboGuiApi.Connect( sConnectionString ); 
            
            // // get an initialized application object
            
            SBO_Application = SboGuiApi.GetApplication( -1 ); 
            bFirstTime = true; 
        } 
        
        private void CreateFormCP() { 
            // ////////////////////////////////////////
            SAPbouiCOM.FormCreationParams oFormCreaP = null; 
            oFormCreaP = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
            oFormCreaP.UniqueID = "MeiravForm"; 
            oFormCreaP.FormType = "MeiravFormType"; 
            oFormCreaP.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Sizable; 
            oForm = SBO_Application.Forms.AddEx( oFormCreaP ); 
            oForm.Visible = true; 
        } 
        
        private void AddLables() { 
            
            // left       = 400,300,200,100
            //  tabOrder  = 4,2,1,3
            AddNumberLabel( "Index1 ", 400 ); 
            AddNumberLabel( "Index2 ", 300 ); 
            AddNumberLabel( "Index3 ", 200 ); 
            AddNumberLabel( "Index4 ", 100 ); 
            
            AddOrderLabel( "Order4 ", 400 ); 
            AddOrderLabel( "Order2 ", 300 ); 
            AddOrderLabel( "Order1", 200 ); 
            AddOrderLabel( "Order3", 100 ); 
            
            
        } 
        
        private void AddNumberLabel( string LableName, long lLeft ) { 
            
            SAPbouiCOM.Item oItem = null; 
            oItem = oForm.Items.Add( LableName, SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
            SetItemRect( ref oItem, lLeft, 65, 50, 19 ); 
            
            SAPbouiCOM.StaticText oLba = null; 
            oLba = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
            oLba.Caption = LableName; 
        } 
        
        private void AddOrderLabel( string LableName, long lLeft ) { 
            
            SAPbouiCOM.Item oItem = null; 
            oItem = oForm.Items.Add( LableName, SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
            SetItemRect( ref oItem, lLeft, 65, 100, 19 ); 
            SAPbouiCOM.StaticText oLba = null; 
            oLba = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
            oLba.Caption = LableName; 
        } 
        
        private void AddEdit( string EditName, long lLeft ) { 
            
            SAPbouiCOM.Item oItem = null; 
            oItem = oForm.Items.Add( EditName, SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            
            SetItemRect( ref oItem, lLeft, 65, 150, 19 ); 
            SAPbouiCOM.EditText oEdit = null; 
            
            
        } 
        
        
        
        private void SetItemRect( ref SAPbouiCOM.Item oItem, long oLeft, long Width, long Top, long Height ) { 
            oItem.Left = System.Convert.ToInt32( oLeft ); 
            oItem.Width = System.Convert.ToInt32( Width ); 
            oItem.Top = System.Convert.ToInt32( Top ); 
            oItem.Height = System.Convert.ToInt32( Height ); 
            
        } 
        
        
        
        
        private void BMatrixForm_Click( System.Object sender, System.EventArgs e ) { 
            // Creates a form with 2 matrixes
            CreateVisFormWithMatrix1(); 
			SwitchGrid.Enabled = true;
			BMatrixForm.Enabled = false;
            
        } 
        
        private void CreateVisFormWithMatrix1() { 
            // The Default matrix setting will appear for the first matrix added to the form
            CreateVisFormWithMatrix3(); 
            AddMatrixToForm1( "Matrix1" ); 
            AddColumnsToMatrix( ref oMatrix ); 
            SetMatrix( ref oMatrix ); 
            AddMatrixToForm1( "Matrix2" ); 
            AddColumnsToMatrix( ref oMatrix ); 
            SetMatrix( ref oMatrix ); 
            SAPbouiCOM.DBDataSource oDBDS = null; 
            oForm.Settings.Enabled = true; 
            
            // oDBDS = oForm.DataSources.DBDataSources.Item("OCRD")
            // oDBDS.Query()
            // oMatrix.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto
            // 'oColumns = oMatrix.Columns
            // 'Dim oEditText As SAPbouiCOM.EditText
            // 'oEditText = oColumns.Item(1).Cells.Item(2).Specific()
            // 'oEditText.String = "Test"
        } 
        
        private void CreateVisFormWithMatrix3() { 
            
            SAPbouiCOM.DBDataSource oDBDS = null; 
            // // add a new form
            // // The following object is needed to create our form
            SAPbouiCOM.FormCreationParams creationPackage = null; 
            creationPackage = ( ( SAPbouiCOM.FormCreationParams )( SBO_Application.CreateObject( SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams ) ) ); 
            creationPackage.UniqueID = "UidFrmMatrix"; 
            creationPackage.FormType = "TypeFrmMatrix"; 
            
            // // Add our form to the SBO application
            oForm = SBO_Application.Forms.AddEx( creationPackage ); 
            
            
            // // set the form properties
            oForm.Title = "Matrix and Cells"; 
            oForm.Left = 336; 
            oForm.Width = 2000; 
            oForm.Top = 44; 
            oForm.Height = 250; 
            oForm.ClientHeight = 200; 
            oForm.ClientWidth = 600; 
            oForm.Visible = true; 
            oForm.PaneLevel = 0; 
            
            // //*****************************************
            // // Adding Items to the form
            // // and setting their properties
            // //*****************************************
            
            // /**********************
            // // Adding an Ok button
            // //*********************
            
            // // We get automatic event handling for
            // // the Ok and Cancel Buttons by setting
            // // their UIDs to 1 and 2 respectively
            
            SAPbouiCOM.Item oItem = null; 
            oItem = oForm.Items.Add( "1", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
            oItem.Left = 5; 
            oItem.Width = 65; 
            oItem.Top = 150; 
            oItem.Height = 19; 
            
            SAPbouiCOM.Button oButton = null; 
            oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
            oButton.Caption = "Ok"; 
            
            // //************************
            // // Adding a Cancel button
            // //***********************
            
            oItem = oForm.Items.Add( "2", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
            oItem.Left = 75; 
            oItem.Width = 65; 
            oItem.Top = 150; 
            oItem.Height = 19; 
            
            oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
            
            oButton.Caption = "Cancel"; 
            SAPbouiCOM.StaticText oStaticText = null; 
            SAPbouiCOM.EditText oEditText15 = null; 
            SAPbouiCOM.EditText oEditText = null; 
            
            AddFormDataSources(); 
        } 
        
        private void AddFormDataSources() { 
            // //***********************************
            // // Adding Culomn items to the matrix
            // //***********************************
            // oForm.DataSources.DBDataSources.Add("@MUDTMD")
            SAPbouiCOM.UserDataSources dbSRC = oForm.DataSources.UserDataSources;
            dbSRC.Add( "TabDS1", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER, 254 ); 
            dbSRC.Add( "EditDS1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            dbSRC.Add( "EditDS2", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            dbSRC.Add( "EditDS3", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            dbSRC.Add( "CheckDS2", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1 ); 
            dbSRC.Add( "ComboDS1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            dbSRC.Add( "LinkDS1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            dbSRC.Add( "PicDS1", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 254 ); 
            
        } 
        
        private void AddMatrixToForm1( string mName ) { 
            SAPbouiCOM.Item oItem = null; 
            
            oItem = oForm.Items.Add( mName, SAPbouiCOM.BoFormItemTypes.it_MATRIX ); 
            if ( ( mName.IndexOf("1") < 0 ) ) { 
                
                oItem.Left = 5; 
                oItem.Width = 200; 
                oItem.Top = 5; 
                oItem.Height = 130; 
            } 
            else { 
                oItem.Left = 400; 
                oItem.Width = 300; 
                oItem.Top = 5; 
                oItem.Height = 100; 
            } 
            
            
            oMatrix = ( ( SAPbouiCOM.Matrix )( oItem.Specific ) ); 
            oMatrix.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto; 
            oColumns = oMatrix.Columns; 
            
        } 
        
        public void AddColumnsToMatrix( ref SAPbouiCOM.Matrix oMatrix ) { 
            int i = 0; 
            SAPbouiCOM.Column oColumn = null; 
            SAPbouiCOM.LinkedButton oLinkedButton = null; 
            
            oColumn = oMatrix.Columns.Add( "col" + 0, SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            //  oColumn.Width = 15
            oColumn.TitleObject.Caption = "#"; 
            oColumn.Editable = false; 
            
            //  add edit column
            oColumn = oMatrix.Columns.Add( "col" + 1, SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON ); 
            oColumn.Width = 40; 
            // oColumn.DataBind.SetBound(True, "@MYUDT", "Code")
            oColumn.TitleObject.Caption = "LINK_choose"; 
            oColumn.BackColor = 255064; 
            oColumn.ForeColor = 640255; 
            oColumn.TextStyle = 8; 
            oColumn.FontSize = 12; 
            oLinkedButton = ( ( SAPbouiCOM.LinkedButton )( oColumn.ExtendedObject ) ); 
            //  oLinkedButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_CorrectionInvoice
            oLinkedButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner; 
            //  
            
            //  add combo column
            oColumn = oMatrix.Columns.Add( "col" + 2, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX ); 
            oColumn.Width = 40; 
            oColumn.DataBind.SetBound( true, "", "ComboDS1" ); 
            oColumn.TitleObject.Caption = "Combo"; 
            oColumn.ForeColor = 255064; 
            oColumn.BackColor = 640255; 
            
            for ( i=0; i<=3; i++ ) { 
                oColumn.ValidValues.Add( "val" + i, "desc" + i ); 
            } 
            
            //  add checkbox column
            oColumn = oMatrix.Columns.Add( "col" + 3, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX ); 
            oColumn.Width = 40; 
            oColumn.DataBind.SetBound( true, "", "CheckDS2" ); 
            oColumn.TitleObject.Caption = "Check"; 
            
            //  add picture column
            oColumn = oMatrix.Columns.Add( "col" + 4, SAPbouiCOM.BoFormItemTypes.it_PICTURE ); 
            oColumn.Editable = false; 
            oColumn.Width = 40; 
            oColumn.DataBind.SetBound( true, "", "PicDS1" ); 
            oColumn.TitleObject.Caption = "Picture"; 
            
        } 
        
        public void SetMatrix( ref SAPbouiCOM.Matrix oMatrix ) { 
            
            int i = 0; // // to be used as counter
            int CurDS = 0; 
            //  oMatrix.Clear()
            oMatrix.AddRow( 5, -1 ); 
        } 
        
        
        private void LoadFromXML_Click( System.Object sender, System.EventArgs e ) { 
            I_LoadFromXML( "ActiveForm.xml" ); 
            
        } 
        
        
        private void I_LoadFromXML( string FileName ) { 
            // //**********************************************************************
            // // Loading from XML:
            // //---------------------------------------------------------------------
            // //
            // // Add a directory  named "XML" under "C:\Program Files\SAP Manage\"
            // //
            // //**********************************************************************
            try { 
                
                
                MSXML2.DOMDocument oXMLDoc1 = null; 
                
                oXMLDoc1 = new MSXML2.DOMDocument(); 
                
                // // load the content of the XML File
                oXMLDoc1.load( @"C:\Program Files\SAP Manage\XML\" + FileName ); 
                string XMLStr = null; 
				string strTMP = oXMLDoc1.xml.ToString();
                // // load the form to the SBO application in one batch
                SBO_Application.LoadBatchActions( ref strTMP  ); 
                //  GetBatchRes()
            } 
            catch ( Exception Exc ) { 
                Interaction.MsgBox( "Load Form XML " + Exc.Message, (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
                Interaction.MsgBox( "Load Form XML " + Exc.ToString(), (Microsoft.VisualBasic.MsgBoxStyle)(0), null ); 
            } 
        } 
        
        
        private void SwitchGrid_Click( System.Object sender, System.EventArgs e ) { 
            SAPbouiCOM.Form oForm = null; 
            oForm = SBO_Application.Forms.ActiveForm; 
            SAPbouiCOM.FormSettings fSettings = null; 
            fSettings = oForm.Settings; 
            if ( fSettings.MatrixUID == "Matrix1" ) { 
                fSettings.MatrixUID = "Matrix2"; 
            } 
            else { 
                fSettings.MatrixUID = "Matrix1"; 
            } 
        } 
        
        
        [STAThread]
        public static void Main() { Application.Run( new Form1() ); }
    } 
    
    
} 
