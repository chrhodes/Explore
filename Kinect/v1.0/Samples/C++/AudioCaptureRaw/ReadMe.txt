AudioCaptureRaw - READ ME

Copyright (c) Microsoft Corporation.  All rights reserved.  

=============================
OVERVIEW  
.............................
This sample demonstrates capturing audio data using WASAPI, with no additional processing (such as mic array or AEC). As such, it'll produce
a 4 channel wav file when recording from a Kinect device.

The sample captures audio in shared mode using the timer driven programming model, saving the captured data to a file.

This sample was apapted from the CaptureSharedTimerDriver sample in the Platform SDK

=============================
FILES   
.............................
AudioCaptureRaw.vcproj
    This is the main project file for VC++ projects generated using an Application Wizard.
    It contains information about the version of Visual C++ that generated the file, and
    information about the platforms, configurations, and project features selected with the
    Application Wizard.

AudioCaptureRaw.cpp
    This is the main application source file.  It parses the command line and instantiates a
    CWASAPICapture object which actually performs the rendering.
    
WASAPICapture.cpp/WASAPICapture.h
    Implementation of a class which uses the WASAPI APIs to render a buffer containing audio data.
    

/////////////////////////////////////////////////////////////////////////////
Other standard files:

StdAfx.h, StdAfx.cpp
    These files are used to build a precompiled header (PCH) file
    named WASAPICaptureSharedTimerDriven.pch and a precompiled types file named StdAfx.obj.
targetver.h
    Specifies the OS target version for the sample (Vista+)


=============================
OPENING IN VISUAL STUDIO   
.............................
1. Launch Start/All Programs/Microsoft Kinect SDK v1.0/Kinect SDK Sample Browser
   (Start -> typing "Kinect SDK" finds it quickly)
2. In the list of samples, select this sample.
3. Click on "Install" button.
4. Provide a location to install the sample to.
5. Open the Solution file (.sln) that was installed.
