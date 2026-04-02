//  SAP MANAGE UI API 2007 SDK Sample
//****************************************************************************
//
//  File:      Main.cs
//
//  Copyright (c) SAP MANAGE
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
//****************************************************************************

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace ChooseFromList {
    sealed public class CFL { 
        
        public static void Main() { 
            
            ChooseFromList oChooseFromList = null; 
            
            oChooseFromList = new ChooseFromList(); 
            
            System.Windows.Forms.Application.Run(); 
            
        }         
    } 
} 
