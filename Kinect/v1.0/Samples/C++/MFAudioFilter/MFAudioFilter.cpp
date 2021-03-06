//------------------------------------------------------------------------------
// <copyright file="MFAudioFilter.cpp" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

// This module provides sample code used to demonstrate Kinect DMO filtering audio as part
// of a Media Foundation topology.

#include <windows.h>

// For configuring DMO properties
#include <wmcodecdsp.h>

// For discovering microphone array device
#include <MMDeviceApi.h>
#include <devicetopology.h>

// For functions and definitions used to configure Media Foundation objects
#include <mfapi.h>      // MF_MT_* constants
#include <mferror.h>    // MF_E_NOT_FOUND
#include <mfidl.h>      // IMFMediaSource, IMFMediaSink, IMFTransform
#include <wmcontainer.h>// ASF sink creation and config

// For string input,output and manipulation
#include <tchar.h>
#include <strsafe.h>
#include <conio.h>

// For CLSID_KinectAudio GUID
#include "NuiApi.h"


#define SAFE_RELEASE(p) {if (NULL != p) {(p)->Release(); (p) = NULL;}}

#define CHECK_RET(hr, message) if (FAILED(hr)) { printf("%s: %08X\n", message, hr); goto exit;}
#define CHECKHR(x) hr = x; if (FAILED(hr)) {printf("%d: %08X\n", __LINE__, hr); goto exit;}
#define CHECK_ALLOC(pb, message) if (NULL == pb) { puts(message); goto exit;}
#define CHECK_BOOL(b, message) if (!b) { hr = E_FAIL; puts(message); goto exit;}

//The KSAUDIO_MIC_ARRAY_GEOMETRY only has room for 1 mic coordinate so we manually allocate space for the other 3 
typedef struct 
{
    KSAUDIO_MIC_ARRAY_GEOMETRY geometry;
    KSAUDIO_MICROPHONE_COORDINATES coordinates[3];
} KINECT_GEOMETRY;

KINECT_GEOMETRY maKinectGeometry = {256, 0, 0, 0, -8726, 8726, 200, 7200, 4, 
{2, 0, -113, 0, 0, 0},
{{2, 0, 36, 0, 0, 0},
{2, 0, 76, 0, 0, 0},
{2, 0, 113, 0, 0, 0}}};

// Helper functions used to discover microphone array device and create a media source from it
HRESULT CreateSourceFromAudioDevice(UINT32 deviceIndex, IMFMediaSource **ppSource);
HRESULT GetMicArrayDeviceIndex(int *piDeviceIndex);
HRESULT GetJackSubtypeForEndpoint(IMMDevice* pEndpoint, GUID* pgSubtype);

// Helper functions used to set up a Media Foundation recording session
HRESULT MFRecord(IMFTransform* pFilter, IMFMediaType* pFilteredMediaType, const TCHAR* outFile);
HRESULT CreateEncoderAndSink(IMFMediaType *pFilteredMediaType, const TCHAR *szOutfileFullName, IMFTransform **ppEncoder, IMFMediaSink **ppSink);
HRESULT CreateTopology(IMFMediaSource *pSource, IMFTransform *pFilter, IMFTransform *pEncoder, IMFMediaSink *pSink, IMFTopology **ppTopology);
HRESULT RunMediaSession(IMFTopology *pTopology);

///////////////////////////////////////////////////////////////////////////
// Main function
//
// Initializes COM, creates and configures a DMO object, captures sound
// from microphone array and records it to a WMA file as part of a Media
// Foundation session.
//
///////////////////////////////////////////////////////////////////////////
int __cdecl _tmain(int argc, const TCHAR ** argv)
{
    HRESULT hr = S_OK;
    CoInitializeEx(NULL, COINIT_MULTITHREADED);
    IMediaObject* pDMO = NULL;  
    IPropertyStore* pPS = NULL;
    IMFTransform* pMFT = NULL;
    HANDLE mmHandle = NULL;
    DWORD mmTaskIndex = 0;
    LPCTSTR szOutputFile = _T("MFAudioFilter.wma");
    int ch;

    // Set high priority to avoid getting preempted while capturing sound
    mmHandle = AvSetMmThreadCharacteristics(L"Audio", &mmTaskIndex);
    CHECK_BOOL(mmHandle != NULL, "failed to set thread priority\n");

    // DMO initialization
    INuiAudioBeam* pAudio = NULL;
    CHECKHR(NuiInitialize(NUI_INITIALIZE_FLAG_USES_AUDIO));
    CHECKHR(NuiGetAudioSource(&pAudio));
    CHECKHR(pAudio->QueryInterface(IID_IMediaObject, (void**)&pDMO));
    CHECKHR(pAudio->QueryInterface(IID_IPropertyStore, (void**)&pPS));
    CHECKHR(pAudio->QueryInterface(IID_IMFTransform, (void**)&pMFT));
    SAFE_RELEASE(pAudio);

    // Set MicArray DMO system mode with no echo cancellation.
    // This must be set for the DMO to work properly
    PROPVARIANT pvSysMode;
    PropVariantInit(&pvSysMode);
    pvSysMode.vt = VT_I4;
    //   SINGLE_CHANNEL_AEC = 0
    //   OPTIBEAM_ARRAY_ONLY = 2
    //   OPTIBEAM_ARRAY_AND_AEC = 4
    //   SINGLE_CHANNEL_NSAGC = 5
    pvSysMode.lVal = (LONG)(2);
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_SYSTEM_MODE, pvSysMode));
    PropVariantClear(&pvSysMode);

    // Put media object into filter mode so it can be used as a Media Foundation transform
    PROPVARIANT pvSourceMode;
    PropVariantInit(&pvSourceMode);    
    pvSourceMode.vt = VT_BOOL;
    pvSourceMode.boolVal = VARIANT_FALSE;
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_DMO_SOURCE_MODE, pvSourceMode));

    // geometry of input has to be specified in filter mode since media object can't
    // get it directly from driver as it does in source mode.
    PROPVARIANT pvGeometry;
    PropVariantInit(&pvGeometry);    
    pvGeometry.vt = VT_BLOB;
    pvGeometry.blob.cbSize = sizeof(maKinectGeometry);
    pvGeometry.blob.pBlobData = (BYTE*)&maKinectGeometry;
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_MICARRAY_DESCPTR, pvGeometry));

    // Turn on feature mode (required to change value of noise suppression and automatic gain control)
    PROPVARIANT pvFeatrModeOn;
    PropVariantInit(&pvFeatrModeOn);
    pvFeatrModeOn.vt = VT_BOOL;
    pvFeatrModeOn.boolVal = VARIANT_TRUE;
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_FEATURE_MODE, pvFeatrModeOn));
    PropVariantClear(&pvFeatrModeOn);

    // Turn on/off noise suppression
    PROPVARIANT pvNoiseSup;
    PropVariantInit(&pvNoiseSup);
    pvNoiseSup.vt = VT_I4;
    pvNoiseSup.lVal = 1;
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_FEATR_NS, pvNoiseSup));
    PropVariantClear(&pvNoiseSup);

    // Turn on AGC
    PROPVARIANT pvAGC;
    PropVariantInit(&pvAGC);
    pvAGC.vt = VT_BOOL;
    pvAGC.boolVal = VARIANT_TRUE;
    CHECKHR(pPS->SetValue(MFPKEY_WMAAECMA_FEATR_AGC, pvAGC));
    PropVariantClear(&pvAGC);
    
    // Use as a Media Foundation transform requires specifying input and output types
    IMFMediaType* pOutputMediaType;
    CHECKHR(MFCreateMediaType(&pOutputMediaType));
    CHECKHR(pOutputMediaType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio));
    CHECKHR(pOutputMediaType->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM));
    CHECKHR(pOutputMediaType->SetUINT32(MF_MT_AUDIO_NUM_CHANNELS, 1));
    CHECKHR(pOutputMediaType->SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, 16000));
    CHECKHR(pOutputMediaType->SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, 32000));
    CHECKHR(pOutputMediaType->SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, 2));
    CHECKHR(pOutputMediaType->SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, 16));
    CHECKHR(pMFT->SetOutputType(0, pOutputMediaType, 0));

    IMFMediaType* pInputMediaType;
    CHECKHR(MFCreateMediaType(&pInputMediaType));
    CHECKHR(pInputMediaType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio));
    CHECKHR(pInputMediaType->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_Float));
    CHECKHR(pInputMediaType->SetUINT32(MF_MT_AUDIO_NUM_CHANNELS, 4));
    CHECKHR(pInputMediaType->SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND, 16000));
    CHECKHR(pInputMediaType->SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, 256000));
    CHECKHR(pInputMediaType->SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT, 16));
    CHECKHR(pInputMediaType->SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE, 32));     
    CHECKHR(pMFT->SetInputType(0, pInputMediaType, 0));

    // NOTE: Need to wait 4 seconds for device to be ready right after initialization
    DWORD dwWait = 4;
    while (dwWait > 0)
    {
        _tprintf(_T("Device will be ready for recording in %d second(s).\r"), dwWait--);
        Sleep(1000);
    }
    _tprintf(_T("Device is ready. Press any key to start recording.\n"));
    ch = _getch();

    // Capture sound in microphone array while filtering it with DMO and recording to file
    hr = MFRecord(pMFT, pOutputMediaType, szOutputFile);
    CHECK_RET(hr, "Failed to record.");

exit:
    puts("Press any key to continue");
    ch = _getch();
    SAFE_RELEASE(pDMO);
    SAFE_RELEASE(pPS);

    AvRevertMmThreadCharacteristics(mmHandle);
    NuiShutdown();
    CoUninitialize();
}


///////////////////////////////////////////////////////////////////////////
// MFRecord
//
// Uses the DMO in filter mode, as a IMFTransform object, to perform noise
// suppression on a IMFMediaSource that captures audio from mic array.
// The processed output is sent to a IMFTransform that encodes it as WMA
// and sends it to an IMFMediaSink that writes it to a WMA file.
//
///////////////////////////////////////////////////////////////////////////
HRESULT MFRecord(IMFTransform* pFilter, IMFMediaType* pFilteredMediaType, const TCHAR* szOutfile)
{
    printf("\nRecording using Media Foundation\n");

    HRESULT hr;
    IMFTransform *pEncoder = NULL;
    IMFMediaSink *pSink = NULL;
    IMFMediaSource *pMicSource = NULL;
    IMFTopology *pTopology = NULL;
    int iMicDevIdx = -1;
    TCHAR szOutfileFullName[MAX_PATH];
    
    CHECKHR(MFStartup(MF_VERSION));
    
    DWORD dwRet = GetFullPathName(szOutfile, (DWORD)ARRAYSIZE(szOutfileFullName), szOutfileFullName,NULL);
    CHECK_BOOL(dwRet != 0, "Could not determine name of file to write.");
    _tprintf(_T("Sound output will be written to file: %s\n"),szOutfileFullName);

    CHECKHR(CreateEncoderAndSink(pFilteredMediaType, szOutfileFullName, &pEncoder, &pSink));

    // Create the media source from mic array device
    hr = GetMicArrayDeviceIndex(&iMicDevIdx);
    CHECK_RET(hr, "Failed to find microphone array device. Make sure microphone array is properly installed.");
    CHECKHR(CreateSourceFromAudioDevice(iMicDevIdx, &pMicSource));

    // Create the topology.
    hr = CreateTopology(pMicSource, pFilter, pEncoder, pSink, &pTopology);
    CHECK_RET(hr, "Failed to create topology.");

    // Run the media session.
    hr = RunMediaSession(pTopology);
    CHECK_RET(hr, "Media session failed to run to completion");
    
exit:
    if (pMicSource) pMicSource->Shutdown();

    SAFE_RELEASE(pEncoder);
    SAFE_RELEASE(pSink);
    SAFE_RELEASE(pMicSource);
    SAFE_RELEASE(pTopology);

    MFShutdown();

    return hr;
}


///////////////////////////////////////////////////////////////////////
// CreateEncoderAndSink
// 
// Create a WMA encoder transform that will accept the specified media
// type and a WMA media sink that will write to the specified file.
// 
/////////////////////////////////////////////////////////////////////////
HRESULT CreateEncoderAndSink(IMFMediaType *pFilteredMediaType, const TCHAR *szOutfileFullName, IMFTransform **ppEncoder, IMFMediaSink **ppSink)
{
    HRESULT hr = S_OK;

    IMFByteStream *pStream = NULL;
    IMFMediaSink *pSink = NULL;
    IMFASFContentInfo *pContentInfo = NULL;
    IMFASFProfile *pProfile = NULL;
    IMFASFStreamConfig *pStreamConfig = NULL;
    IMFTransform *pEncoder = NULL;
    
    // Create WMA encoder and set its input type
    CHECKHR(CoCreateInstance(__uuidof(CWMAEncMediaObject), NULL, CLSCTX_INPROC_SERVER, __uuidof(IMFTransform), (void**)&pEncoder));
    CHECKHR(pEncoder->SetInputType(0, pFilteredMediaType,0));

    // Get first available encoded media type that suits our needs
    IMFMediaType* pOutputMediaType = NULL;
    int iType = 0;

#pragma warning(push)
#pragma warning(disable: 4127) // conditional expression is constant

    while(true)
    {
        CHECKHR(pEncoder->GetOutputAvailableType(0,iType++,&pOutputMediaType));
        GUID guidSubtype;
        CHECKHR(pOutputMediaType->GetGUID(MF_MT_SUBTYPE, &guidSubtype));

        if (guidSubtype == MFAudioFormat_WMAudioV8)
        {
            break;
        }

        SAFE_RELEASE(pOutputMediaType);
    }

#pragma warning(pop)
    CHECKHR(pEncoder->SetOutputType(0, pOutputMediaType,0));
    
    // Create media sink to WMA file to serve as MF topology output
    CHECKHR(MFCreateFile(MF_ACCESSMODE_WRITE, MF_OPENMODE_DELETE_IF_EXIST, MF_FILEFLAGS_NONE, szOutfileFullName, &pStream));
    CHECKHR(MFCreateASFMediaSink(pStream,&pSink));
    CHECKHR(pSink->QueryInterface(__uuidof(IMFASFContentInfo), (void**)&pContentInfo));
    CHECKHR(MFCreateASFProfile(&pProfile));
    CHECKHR(pProfile->CreateStream(pOutputMediaType, &pStreamConfig));
    CHECKHR(pStreamConfig->SetStreamNumber(1));
    CHECKHR(pProfile->SetStream(pStreamConfig));
    CHECKHR(pContentInfo->SetProfile(pProfile));

    *ppSink = pSink;
    pSink = NULL;
    *ppEncoder = pEncoder;
    pEncoder = NULL;

exit:
    SAFE_RELEASE(pStream);
    SAFE_RELEASE(pSink);
    SAFE_RELEASE(pContentInfo);
    SAFE_RELEASE(pProfile);
    SAFE_RELEASE(pStreamConfig);
    SAFE_RELEASE(pEncoder);

    return hr;
}


///////////////////////////////////////////////////////////////////////
// GetStreamMajorType
// 
// Get the major media type from a stream descriptor.
// Note: To get the major media type from a stream descriptor, you need
//       to go through the stream descriptor's media type handler. Use
//       this helper function if you don't need the type handler for
//       anything else.
// 
/////////////////////////////////////////////////////////////////////////
inline HRESULT GetStreamMajorType(IMFStreamDescriptor *pSD, GUID *pguidMajorType)
{
    HRESULT hr = S_OK;
    IMFMediaTypeHandler *pHandler = NULL;

    CHECKHR(pSD->GetMediaTypeHandler(&pHandler));
    hr = pHandler->GetMajorType(pguidMajorType);

exit:
    SAFE_RELEASE(pHandler);
    return hr;
}

//////////////////////////////////////////////////////////////////////
// CreateSourceNode
//
// Creates a source node for a media stream.
//
///////////////////////////////////////////////////////////////////////

HRESULT CreateSourceNode(
    IMFMediaSource *pSource,          // Media source.
    IMFPresentationDescriptor *pPD,   // Presentation descriptor.
    IMFStreamDescriptor *pSD,         // Stream descriptor.
    IMFTopologyNode **ppNode          // Receives the node pointer.
    )
{
    IMFTopologyNode *pNode = NULL;
    HRESULT hr = S_OK;
    
    // Create the node.
    CHECKHR(MFCreateTopologyNode(MF_TOPOLOGY_SOURCESTREAM_NODE, &pNode));

    // Set the attributes.
    CHECKHR(pNode->SetUnknown(MF_TOPONODE_SOURCE, pSource));
    CHECKHR(pNode->SetUnknown(MF_TOPONODE_PRESENTATION_DESCRIPTOR, pPD));
    CHECKHR(pNode->SetUnknown(MF_TOPONODE_STREAM_DESCRIPTOR, pSD));

    // Return the pointer to the caller.
    *ppNode = pNode;
    (*ppNode)->AddRef();

exit:
    SAFE_RELEASE(pNode);
    return hr;
}

///////////////////////////////////////////////////////////////////////
// CreateOutputNode
//
// Creates an output node for a stream sink.
//
///////////////////////////////////////////////////////////////////////
HRESULT CreateOutputNode(IMFMediaSink *pSink, DWORD iStream, IMFTopologyNode **ppNode)
{
    IMFTopologyNode *pNode = NULL;
    IMFStreamSink *pStream = NULL;
    HRESULT hr = S_OK;

    CHECKHR(pSink->GetStreamSinkByIndex(iStream, &pStream));    
    CHECKHR(MFCreateTopologyNode(MF_TOPOLOGY_OUTPUT_NODE, &pNode));
    CHECKHR(pNode->SetObject(pStream));

    *ppNode = pNode;
    (*ppNode)->AddRef();

exit:
    SAFE_RELEASE(pNode);
    SAFE_RELEASE(pStream);
    
    return hr;
}

///////////////////////////////////////////////////////////////////////
// CreateTransformNode
//
// Creates a transform node for a media foundation transform.
//
///////////////////////////////////////////////////////////////////////
HRESULT CreateTransformNode(IMFTransform *pMft, IMFTopologyNode **ppNode)
{
    IMFTopologyNode *pNode = NULL;
    HRESULT hr = S_OK;

    CHECKHR(MFCreateTopologyNode(MF_TOPOLOGY_TRANSFORM_NODE, &pNode));    
    CHECKHR(pNode->SetObject(pMft));

    *ppNode = pNode;
    (*ppNode)->AddRef();

exit:
    SAFE_RELEASE(pNode);
    
    return hr;
}


///////////////////////////////////////////////////////////////////////
// CreateTopologyBranch
//
// Adds a source and sink to the topology and connects them, passing
// first through the filter that performs noise suppression and
// automatic gain control and then through the WMA encoder.
//
///////////////////////////////////////////////////////////////////////
HRESULT CreateTopologyBranch(
    IMFTopology *pTopology,
    IMFMediaSource *pSource,          // Microphone array as a media source.
    IMFPresentationDescriptor *pPD,   // Presentation descriptor.
    IMFStreamDescriptor *pSD,         // Stream descriptor.
    IMFTransform *pFilter,            // Filter as a media transform
    IMFTransform *pEncoder,           // WMA encoder as a media transform
    IMFMediaSink *pSink               // WMA writer as a media sink
    )
{

    IMFTopologyNode *pSourceNode = NULL;
    IMFTopologyNode *pOutputNode = NULL;
    IMFTopologyNode *pFilterTransformNode = NULL;
    IMFTopologyNode *pEncoderTransformNode = NULL;
    HRESULT hr = S_OK;

    CHECKHR(CreateSourceNode(pSource, pPD, pSD, &pSourceNode));
    CHECKHR(CreateOutputNode(pSink, 0, &pOutputNode));
    CHECKHR(CreateTransformNode(pFilter, &pFilterTransformNode));
    CHECKHR(CreateTransformNode(pEncoder, &pEncoderTransformNode));

    CHECKHR(pTopology->AddNode(pSourceNode));
    CHECKHR(pTopology->AddNode(pOutputNode));
    CHECKHR(pTopology->AddNode(pFilterTransformNode));
    CHECKHR(pTopology->AddNode(pEncoderTransformNode));

    CHECKHR(pSourceNode->ConnectOutput(0, pFilterTransformNode, 0));
    CHECKHR(pFilterTransformNode->ConnectOutput(0, pEncoderTransformNode, 0));
    CHECKHR(pEncoderTransformNode->ConnectOutput(0, pOutputNode, 0));

exit:
    SAFE_RELEASE(pSourceNode);
    SAFE_RELEASE(pOutputNode);
    SAFE_RELEASE(pFilterTransformNode);
    SAFE_RELEASE(pEncoderTransformNode);

    return hr;
}

///////////////////////////////////////////////////////////////////////
// CreateTopology
// 
// Creates the topology.
// Note: The first audio stream is conntected to the media sink.
//       Other streams are deselected.
///////////////////////////////////////////////////////////////////////
HRESULT CreateTopology(IMFMediaSource *pSource, IMFTransform* pFilter, IMFTransform *pEncoder, IMFMediaSink *pSink, IMFTopology **ppTopology)
{
    IMFTopology *pTopology = NULL;
    IMFPresentationDescriptor *pPD = NULL;
    IMFStreamDescriptor *pSD = NULL;

    HRESULT hr = S_OK;
    DWORD cStreams = 0;

    BOOL fConnected = FALSE;
    GUID majorType = GUID_NULL;
    BOOL fSelected = FALSE;

    CHECKHR(MFCreateTopology(&pTopology));
    CHECKHR(pSource->CreatePresentationDescriptor(&pPD));
    CHECKHR(pPD->GetStreamDescriptorCount(&cStreams));

    for (DWORD iStream = 0; iStream < cStreams; iStream++)
    {
        CHECKHR(pPD->GetStreamDescriptorByIndex(iStream, &fSelected, &pSD));

        // If the stream is not selected by default, ignore it.
        if (!fSelected)
        {
            continue;
        }

        // Get the major media type.
        CHECKHR(GetStreamMajorType(pSD, &majorType));

        // If it's not audio, deselect it and continue.
        if (majorType != MFMediaType_Audio)
        {
            // Deselect this stream
            CHECKHR(pPD->DeselectStream(iStream));
            continue;
        }
            
        // It's an audio stream, so try to create the topology branch.
        CHECKHR(CreateTopologyBranch(pTopology, pSource, pPD, pSD, pFilter, pEncoder, pSink));

        // Set our status flag. 
        fConnected = TRUE;
            
        // At this point we have reached the first audio stream in the
        // source, so we can stop looking (whether we succeeded or failed).
        break;
    }

    // Even if we haven't failed so far, if we didn't connect any streams, it's a failure.
    // (For example, it might be a video-only source).
    CHECK_BOOL(fConnected, "Failed to connect to any streams.");
    
    *ppTopology = pTopology;
    (*ppTopology)->AddRef();
    
exit:
    SAFE_RELEASE(pTopology);
    SAFE_RELEASE(pPD);
    SAFE_RELEASE(pSD);

    return hr;
}

///////////////////////////////////////////////////////////////////////
// RunMediaSession
//
// Queues the specified topology on the media session and runs the
// media session until the MESessionEnded event is received.
//
///////////////////////////////////////////////////////////////////////
HRESULT RunMediaSession(IMFTopology *pTopology)
{
    IMFMediaSession *pSession = NULL;
    IMFMediaEvent *pEvent = NULL;
    HRESULT hr = S_OK;
    BOOL bGetAnotherEvent = TRUE;
    PROPVARIANT varStartPosition;

    PropVariantInit(&varStartPosition);

    CHECKHR(MFCreateMediaSession(NULL, &pSession));
    CHECKHR(pSession->SetTopology(0, pTopology));

    while (bGetAnotherEvent)
    {
        HRESULT hrEventStatus = S_OK;
        MediaEventType meType = MEUnknown;

        MF_TOPOSTATUS TopoStatus = MF_TOPOSTATUS_INVALID; // Used with MESessionTopologyStatus event.    
    
        CHECKHR(pSession->GetEvent(0, &pEvent));
        CHECKHR(pEvent->GetStatus(&hrEventStatus));
        CHECKHR(pEvent->GetType(&meType));
        
        
        if(FAILED(hrEventStatus))
        {
            _tprintf(_T("Event %d returnd a failed status: 0x%x\n"), meType, hrEventStatus);
            hr = hrEventStatus;
            goto exit;
        }

        switch (meType)
        {
        case MESessionTopologySet:
            _tprintf(_T("MESessionTopologySet\n"));
            break;

        case MESessionTopologyStatus:
            // Get the status code.
            CHECKHR(pEvent->GetUINT32(MF_EVENT_TOPOLOGY_STATUS, (UINT32*)&TopoStatus));
            
            switch (TopoStatus)
            {
            case MF_TOPOSTATUS_READY:
                _tprintf(_T("MESessionTopologyStatus: MF_TOPOSTATUS_READY\n"));
                hr = pSession->Start(&GUID_NULL, &varStartPosition);
                break;

            case MF_TOPOSTATUS_ENDED:
                _tprintf(_T("MESessionTopologyStatus: MF_TOPOSTATUS_ENDED\n"));
                break;
            }
            break;

        case MESessionStarted:
            _tprintf(_T("MESessionStarted\n"));
            _tprintf(_T("\nRecording. Press 's' to stop.\n"));

#pragma warning(push)
#pragma warning(disable: 4127) // conditional expression is constant

            while(TRUE)
            {
                int ch = _getch();
                if (ch == 's' || ch == 'S')
                {
                    hr = pSession->Stop();                        
                    break;
                }
            }

#pragma warning(pop)
            break;

        case MESessionEnded:
            _tprintf(_T("MESessionEnded\n"));
            hr = pSession->Stop();
            break;

        case MESessionStopped:
            _tprintf(_T("MESessionStopped.\n"));
            hr = pSession->Close();
            break;

        case MESessionClosed:
            _tprintf(_T("MESessionClosed\n"));
            bGetAnotherEvent = FALSE;
            break;

        default:
            _tprintf(_T("Media session event: %d\n"), meType);
            break;
        }

        SAFE_RELEASE(pEvent);
    }

exit:
    if (pSession != NULL)
    {
        _tprintf(_T("Shutting down the media session.\n"));
        pSession->Shutdown();
    }
    PropVariantClear(&varStartPosition);

    SAFE_RELEASE(pEvent);
    SAFE_RELEASE(pSession);
    return hr;

}



///////////////////////////////////////////////////////////////////////////
// GetMicArrayDeviceIndex
//
// Obtain device index corresponding to microphone array device.
//
// Parameters: piDevice: [out] Index of microphone array device.
//
// Return: S_OK if successful
//         Failure code otherwise (e.g.: if microphone array device is not found).
//
///////////////////////////////////////////////////////////////////////////////
HRESULT GetMicArrayDeviceIndex(int *piDevice)
{
    HRESULT hr = S_OK;
    UINT index, dwCount;
    IMMDeviceEnumerator* spEnumerator = NULL;
    IMMDeviceCollection* spEndpoints = NULL;

    *piDevice = -1;

    CHECKHR(CoCreateInstance(__uuidof(MMDeviceEnumerator),  NULL, CLSCTX_ALL, __uuidof(IMMDeviceEnumerator), (void**)&spEnumerator));

    CHECKHR(spEnumerator->EnumAudioEndpoints(eCapture, DEVICE_STATE_ACTIVE, &spEndpoints));

    CHECKHR(spEndpoints->GetCount(&dwCount));

    // Iterate over all capture devices until finding one that is a microphone array
    for (index = 0; index < dwCount; index++)
    {
        IMMDevice* spDevice;

        CHECKHR(spEndpoints->Item(index, &spDevice));
        
        GUID subType = {0};
        CHECKHR(GetJackSubtypeForEndpoint(spDevice, &subType));
        if (subType == KSNODETYPE_MICROPHONE_ARRAY)
        {
            *piDevice = index;
            break;
        }
    }

    hr = (*piDevice >=0) ? S_OK : E_FAIL;

exit:
    SAFE_RELEASE(spEnumerator);
    SAFE_RELEASE(spEndpoints);    
    return hr;
}

//////////////////////////////////////////////////////////////////////////
// CreateSourceFromAudioDevice
// 
// Enumerates the audio capture devices and creates an IMFMediaSource for the given index.
// If the index is less than zero it just prints the friendly name of the capture devices
//
// Parameters: iDevice: [in] Index of microphone array device.
//             ppSource: [out] Media source returned
//
// Return: S_OK if successful
//
//////////////////////////////////////////////////////////////////////////
HRESULT CreateSourceFromAudioDevice(UINT32 iDevice, IMFMediaSource **ppSource)
{
    HRESULT hr;
    IMFAttributes* pAttrs = NULL;
    IMFActivate **ppDevices = NULL;
    UINT32 count = 0;

    CHECKHR(MFCreateAttributes(&pAttrs, 1));
    
    CHECKHR(pAttrs->SetGUID(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, 
                          MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_GUID));

    CHECKHR(MFEnumDeviceSources(pAttrs, &ppDevices, &count));


    if (count > 0 && iDevice < count)
    {
        CHECKHR(ppDevices[iDevice]->ActivateObject(IID_PPV_ARGS(ppSource)));
    }    
    else
    {
        hr = MF_E_NOT_FOUND;
    }

exit:
    if(ppDevices!=NULL)
    {
        for (DWORD i = 0; i < count; i++)
        {
            ppDevices[i]->Release();
        }
        CoTaskMemFree(ppDevices);
    }


    SAFE_RELEASE(pAttrs);
    return hr;   

}


///////////////////////////////////////////////////////////////////////////////
// GetJackSubtypeForEndpoint
//
// Gets the subtype of the jack that the specified endpoint device is plugged
// into.  E.g. if the endpoint is for an array mic, then we would expect the
// subtype of the jack to be KSNODETYPE_MICROPHONE_ARRAY
//
///////////////////////////////////////////////////////////////////////////////
HRESULT GetJackSubtypeForEndpoint(IMMDevice* pEndpoint, GUID* pgSubtype)
{
    HRESULT hr = S_OK;

    if (pEndpoint == NULL)
        return E_POINTER;
   
    IDeviceTopology*    spEndpointTopology = NULL;
    IConnector*         spPlug = NULL;
    IConnector*         spJack = NULL;
    IPart*            spJackAsPart = NULL;

    // Get the Device Topology interface
    CHECKHR(pEndpoint->Activate(__uuidof(IDeviceTopology), CLSCTX_INPROC_SERVER, 
                            NULL, (void**)&spEndpointTopology));

    CHECKHR(spEndpointTopology->GetConnector(0, &spPlug));

    CHECKHR(spPlug->GetConnectedTo(&spJack));

    CHECKHR(spJack->QueryInterface(__uuidof(IPart), (void**)&spJackAsPart));

    hr = spJackAsPart->GetSubType(pgSubtype);

exit:
   SAFE_RELEASE(spEndpointTopology);
   SAFE_RELEASE(spPlug);    
   SAFE_RELEASE(spJack);    
   SAFE_RELEASE(spJackAsPart);
   return hr;
}//GetJackSubtypeForEndpoint()
