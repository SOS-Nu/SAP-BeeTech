//  SAP MANAGE UI API 2007 SDK Sample
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

using Microsoft.VisualBasic;
using System;
namespace FilterEvents {
  
    class SubMain { 
        
        public static void Main() { 
            
            // Creating an object
            EventFilter oEventsFilter = null; 
            
            oEventsFilter = new EventFilter(); 
            
            System.Windows.Forms.Application.Run();   
        }   
    }     
} 
