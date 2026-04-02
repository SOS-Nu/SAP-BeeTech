using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ConsoleApplication1
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Application SBO_Application;


        static void Main(string[] args)
        {
            try
            {
                Init();

                //  Start Message Loop
                System.Windows.Forms.Application.Run();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadLine();
        }

        private static void Init()
        {
            var api = new SboGuiApi();
            api.Connect("0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056");
            log.Debug("UIAPI connected");

            SBO_Application = api.GetApplication();
            AddMenuItems(SBO_Application);
            //CreateXAppForm(app);
            SBO_Application.MenuEvent += new _IApplicationEvents_MenuEventEventHandler(MenuEvent);
            log.Debug("XApp form created");
        }

        static void MenuEvent(ref MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if ((pVal.MenuUID == "MySubMenu") & (pVal.BeforeAction == false))
            {


                //SBO_Application.MessageBox("My sub menu item was clicked", 1, "Ok", "", "");
                //*************************************************************
                // Create a form to be launched in response to a click on the 
                // new sub menu item
                //*************************************************************
                CreateXAppForm(SBO_Application);
            }

        }


        private static void AddMenuItems(IApplication app)
        {
            //******************************************************************
            // Let's add a separator, a pop-up menu item and a string menu item
            //******************************************************************

            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            int i = 0; // to be used as counter
            int lAddAfter = 0;
            string sXML = null;

            // Get the menus collection from the application
            oMenus = app.Menus;
            // --------------------------------------------
            // Save an XML file containing the menus...
            // --------------------------------------------
            // sXML = SBO_Application.Menus.GetAsXML
            // Dim xmlD As System.Xml.XmlDocument
            // xmlD = New System.Xml.XmlDocument
            // xmlD.LoadXml(sXML)
            // xmlD.Save("c:\\mnu.xml")
            // --------------------------------------------


            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(app.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = app.Menus.Item("43520"); // moudles'

            string sPath = null;

            //sPath = Application.StartupPath;
            //sPath = sPath.Remove(sPath.Length - 9, 9);

            // find the place in wich you want to add your menu item
            // in this example I chose to add my menu item under
            // SAP Business One.
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "MyMenu01";
            oCreationPackage.String = "Customer Extreme App";
            oCreationPackage.Enabled = true;
            //oCreationPackage.Image = sPath + "UI.bmp";
            oCreationPackage.Position = 15;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);

                // Get the menu collection of the newly added pop-up item
                oMenuItem = app.Menus.Item("MyMenu01");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "MySubMenu";
                oCreationPackage.String = "Business Parnters";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
                app.MessageBox("Menu Already Exists", 1, "Ok", "", "");
            }

        }

        private static void CreateXAppForm(IApplication app)
        {
            FormCreationParams param = null;
            Form form = null;
            IWebBrowser browser = null;
            Item item = null;


            //IWebBrowser2 browser = null;

            //  Set the form creation parameters
            param = ((FormCreationParams)(app.CreateObject(BoCreatableObjectType.cot_FormCreationParams)));
            param.BorderStyle = BoFormBorderStyle.fbs_Fixed;
            param.FormType = "XApp";
            param.UniqueID = "XApp" + new Random().Next(1000);

            //  Add a new form
            form = app.Forms.AddEx(param);
            form.Left = 100;
            form.Top = 100;
            form.Width = 1000;
            form.Height = 800;
            form.Title = "Customer Extreme App";

            //  Add the TreeView Control to the form
            item = form.Items.Add("Browser", BoFormItemTypes.it_WEB_BROWSER);
            item.Left = 0;
            item.Top = 0;
            item.Width = 1000;
            item.Height = 800;

            browser = item.Specific as IWebBrowser;
            // TODO: replace the XApp URL
            browser.Url = app.XSEngineBaseURL + "sap/sbo/demo/embedded.html?" + app.Company.DatabaseName;

            //  Make the form visible
            form.Visible = true;
        }
    }
}
