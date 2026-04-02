using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
class SubMain { 
    //  SAP MANAGE UIDI API 2006 SDK Sample
    //****************************************************************************
    //
    //  File:      SubMain.cs
    //
    //  Copyright (c) SAP MANAGE
    //
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
    // ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
    // THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
    // PARTICULAR PURPOSE.
    //
    //****************************************************************************
    
    
    static void Main() { 
        
        ActiveXTree oActiveXTree = null; 
        
        oActiveXTree = new ActiveXTree(); 
        
        System.Windows.Forms.Application.Run(); 
    } 
    
} 
