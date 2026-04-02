using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAP_Integration.Addon
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

            // 1. Trỏ vào menu Tools (ID: 1280)
            oMenuItem = Application.SBO_Application.Menus.Item("1280");
            oMenus = oMenuItem.SubMenus;

            // 2. Tạo Menu Cha "Custom Tools" (POPUP)
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "SAP_Integration.CustomTools_V2";
            oCreationPackage.String = "Custom Tools";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;

            try
            {
                if (!oMenus.Exists("SAP_Integration.CustomTools"))
                {
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch { }

            try
            {
                // 3. Tạo Menu Con "Sales Dashboard" (STRING)
                oMenuItem = Application.SBO_Application.Menus.Item("SAP_Integration.CustomTools");
                oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                // UniqueID này cực kỳ quan trọng, dùng để bắt sự kiện mở Form
                oCreationPackage.UniqueID = "SAP_Integration.Addon.Form1_V2";
                oCreationPackage.String = "Sales Dashboard";

                if (!oMenus.Exists("SAP_Integration.Addon.Form1_V2"))
                {
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch (Exception er)
            {
                Application.SBO_Application.SetStatusBarMessage("Menu Error: " + er.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                // Chỉ mở Form sau khi người dùng click (BeforeAction = false)
                if (!pVal.BeforeAction && pVal.MenuUID == "SAP_Integration.Addon.Form1_V2")
                {
                    Form1 activeForm = new Form1();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }
    }
}