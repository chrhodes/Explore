MFAudioFilter - READ ME 

Copyright (c) Microsoft Corporation. All rights reserved.

=============================
OVERVIEW  
.............................
This sample demonstrates setting up the Kinect Audio DMO in filter mode
as part of a Media Foundation topology that takes input from the Kinect
microphone array and writes filtered audio to a 1 channel wma file.

=============================
FILES   
.............................
- MFAudioFilter.cpp: Defines main function, configures Kinect DMO in
  filter mode as part of a Media Foundation topology also set up to
  record to a WMA file and then runs a Media Foundation session that
  performs all necessary processing until user decides to end session.

=============================
OPENING IN VISUAL STUDIO   
.............................
1. Launch Start/All Programs/Microsoft Kinect SDK v1.0/Kinect SDK Sample Browser
   (Start -> typing "Kinect SDK" finds it quickly)
2. In the list of samples, select this sample.
3. Click on "Install" button.
4. Provide a location to install the sample to.
5. Open the Solution file (.sln) that was installed.
