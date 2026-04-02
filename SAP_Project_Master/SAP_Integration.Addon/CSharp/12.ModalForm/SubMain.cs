//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      SubMain.cs
// 
//   Copyright (c) SAP MANAGE
// 
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//  ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//  THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// 
// ****************************************************************************

using System;
using System.Windows.Forms;
namespace ModalForm {
    class SubMain { 
        
        public static void Main() { 
            
            //  Creating an object
            ModalForm oModalForm = null; 
            
            oModalForm = new ModalForm(); 
            
            //  Starting the Application
            System.Windows.Forms.Application.Run(); 
        }   
    } 
} 
