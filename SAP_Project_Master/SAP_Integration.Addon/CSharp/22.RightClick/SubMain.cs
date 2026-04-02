//   SAP MANAGE UI API 2007 SDK Sample
// ****************************************************************************
// 
//   File:      SubMain.vb
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

class SubMain { 
    
    public static void Main() { 
        
        //  Creating an object
        RightClick oRightClick = null; 
        
        oRightClick = new RightClick(); 
        
        //  Starting the Application
        System.Windows.Forms.Application.Run(); 
    }   
} 