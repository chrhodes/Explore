//------------------------------------------------------------------------------
// <copyright file="AudioCaptureRaw.cpp" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

// Main sample function to drive audio capture.

#include "stdafx.h"
#include <wchar.h>

#include <functiondiscoverykeys.h>
#include "WASAPICapture.h"


int TargetLatency = 20;
int TargetDurationInSec = 10;


//
//  Retrieves the device friendly name for a particular device in a device collection.  
//
//  The returned string was allocated using malloc() so it should be freed using free();
//
LPWSTR GetDeviceName(IMMDeviceCollection *DeviceCollection, UINT DeviceIndex)
{
	IMMDevice *device = NULL;
    IPropertyStore *propertyStore = NULL;
	LPWSTR deviceId = NULL;
    PROPVARIANT friendlyName;
	PropVariantInit(&friendlyName);
    LPWSTR pwszName = NULL;
    LPWSTR pwszReturn = NULL;
	HRESULT hr;

	hr = DeviceCollection->Item(DeviceIndex, &device);
	if (FAILED(hr))
	{
		printf_s("Unable to get device %d: %x\n", DeviceIndex, hr);
		goto Exit;
	}
	hr = device->GetId(&deviceId);
	if (FAILED(hr))
	{
		printf_s("Unable to get device %d id: %x\n", DeviceIndex, hr);
		goto Exit;
	}

	hr = device->OpenPropertyStore(STGM_READ, &propertyStore);
	if (FAILED(hr))
	{
		printf_s("Unable to open device %d property store: %x\n", DeviceIndex, hr);
		goto Exit;
	}
	
	hr = propertyStore->GetValue(PKEY_Device_FriendlyName, &friendlyName);
	
	if (FAILED(hr))
	{
		printf_s("Unable to retrieve friendly name for device %d : %x\n", DeviceIndex, hr);
		goto Exit;
	}

    LPWSTR pwszFriendlyName = friendlyName.vt != VT_LPWSTR ? L"Unknown" : friendlyName.pwszVal;
    size_t cchFriendlyName;
    hr = StringCchLengthW(pwszFriendlyName, STRSAFE_MAX_CCH, &cchFriendlyName);
    if (FAILED(hr))
    {
        goto Exit;
    }

    size_t cchDeviceId;
    hr = StringCchLengthW(deviceId, STRSAFE_MAX_CCH, &cchDeviceId);
    if (FAILED(hr))
    {
        goto Exit;
    }

    const size_t CCH_FORMAT_OVERHEAD = 4; // One space, two parentheses + NULL character
    size_t cchName = cchFriendlyName + cchDeviceId + CCH_FORMAT_OVERHEAD;
	pwszName = static_cast<LPWSTR>(malloc(cchName*sizeof(WCHAR)));
    if (pwszName == NULL)
	{
		printf_s("Unable to allocate buffer for return\n");
		goto Exit;
	}

	hr = StringCchPrintfW(pwszName, cchName, L"%s (%s)", pwszFriendlyName, deviceId);
	if (FAILED(hr))
	{
		printf_s("Unable to format friendly name for device %d : %x\n", DeviceIndex, hr);
		goto Exit;
	}

    // Prevent cleanup code from freeing pwszName when value will be returned
	pwszReturn = pwszName;
    pwszName = NULL;

Exit:
    SafeRelease(&device);
	SafeRelease(&propertyStore);
	CoTaskMemFree(deviceId);
    PropVariantClear(&friendlyName);
    if (pwszName != NULL)
    {
        free(pwszName);
        pwszName = NULL;
    }

	return pwszReturn;
}

//
//  Pick the specified device to use.
//
bool PickDevice(IMMDevice **DeviceToUse, bool *IsDefaultDevice, ERole *DefaultDeviceRole)
{
	HRESULT hr;
	bool retValue = true;
    UINT deviceCount = 0;
    UINT deviceIndex = 0;
    wchar_t choice;

	IMMDeviceEnumerator *deviceEnumerator = NULL;
	IMMDeviceCollection *deviceCollection = NULL;
    IMMDevice *device = NULL;

	*IsDefaultDevice = false;   // Assume we're not using the default device.

	hr = CoCreateInstance(__uuidof(MMDeviceEnumerator), NULL, CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&deviceEnumerator));
	if (FAILED(hr))
	{
		printf_s("Unable to instantiate device enumerator: %x\n", hr);
		retValue = false;
		goto Exit;
	}

	//
	//  The user didn't specify an output device, prompt the user for a device and use that.
	//
	hr = deviceEnumerator->EnumAudioEndpoints(eCapture, DEVICE_STATE_ACTIVE, &deviceCollection);
	if (FAILED(hr))
	{
		printf_s("Unable to retrieve device collection: %x\n", hr);
		retValue = false;
		goto Exit;
	}

	printf_s("Select an input device:\n\n");
	hr = deviceCollection->GetCount(&deviceCount);
	if (FAILED(hr))
	{
		printf_s("Unable to get device collection length: %x\n", hr);
		retValue = false;
		goto Exit;
	}

    if (10 < deviceCount)
    {
        printf_s("10 Audio Input devices supported - %i found, truncating\n", deviceCount);
        deviceCount = 10;
    }

	for (UINT i = 0 ; i < deviceCount; i += 1)
	{
		LPWSTR deviceName;

		deviceName = GetDeviceName(deviceCollection, i);
		if (deviceName == NULL)
		{
			retValue = false;
			goto Exit;
		}
		printf_s("%d:\t%70.70S\n", i, deviceName);
		free(deviceName);
	}

    printf_s("x:\tExit\n\n");
    do
    {
	    choice = _getwch();
	    if ('x' == choice || 'X' == choice)
	    {
		    printf_s("\nExiting\n");
		    retValue = false;
		    goto Exit;
        }
	}while (choice < '0' || choice > '9');

    deviceIndex = static_cast<UINT>(choice - '0');
    if (deviceIndex >= deviceCount)
    {
        printf_s("%i is not a valid choice - exiting\n", deviceIndex);
        retValue=false;
        goto Exit;
    }
    else
    {
        printf_s("Audio Input device %i selected\n\n", deviceIndex);
    }

	hr = deviceCollection->Item(deviceIndex, &device);
	if (FAILED(hr))
	{
		printf_s("Unable to retrieve device %d: %x\n", deviceIndex, hr);
		retValue = false;
		goto Exit;
	}

	if (device == NULL)
	{        
		ERole deviceRole = eConsole;
		hr = deviceEnumerator->GetDefaultAudioEndpoint(eCapture, deviceRole, &device);
		if (FAILED(hr))
		{
			printf_s("Unable to get default device for role %d: %x\n", deviceRole, hr);
			retValue = false;
			goto Exit;
		}
		*IsDefaultDevice = true;
		*DefaultDeviceRole = deviceRole;
	}

	*DeviceToUse = device;
	retValue = true;
Exit:
	SafeRelease(&deviceCollection);
	SafeRelease(&deviceEnumerator);

	return retValue;
}

//
//  WAV file writer.
//
//  This is a VERY simple .WAV file writer.
//

//
//  A wave file consists of:
//
//  RIFF header:    8 bytes consisting of the signature "RIFF" followed by a 4 byte file length.
//  WAVE header:    4 bytes consisting of the signature "WAVE".
//  fmt header:     4 bytes consisting of the signature "fmt " followed by a WAVEFORMATEX 
//  WAVEFORMAT:     <n> bytes containing a waveformat structure.
//  DATA header:    8 bytes consisting of the signature "data" followed by a 4 byte file length.
//  wave data:      <m> bytes containing wave data.
//
//
//  Header for a WAV file - we define a structure describing the first few fields in the header for convenience.
//
struct WAVEHEADER
{
	DWORD   dwRiff;                     // "RIFF"
	DWORD   dwSize;                     // Size
	DWORD   dwWave;                     // "WAVE"
	DWORD   dwFmt;                      // "fmt "
	DWORD   dwFmtSize;                  // Wave Format Size
};

//  Static RIFF header, we'll append the format to it.
const BYTE WaveHeader[] = 
{
	'R',   'I',   'F',   'F',  0x00,  0x00,  0x00,  0x00, 'W',   'A',   'V',   'E',   'f',   'm',   't',   ' ', 0x00, 0x00, 0x00, 0x00
};

//  Static wave DATA tag.
const BYTE WaveData[] = { 'd', 'a', 't', 'a'};

//
//  Write the contents of a WAV file.  We take as input the data to write and the format of that data.
//
bool WriteWaveFile(HANDLE FileHandle, const BYTE *Buffer, const size_t BufferSize, const WAVEFORMATEX *WaveFormat)
{
	DWORD waveFileSize = sizeof(WAVEHEADER) + sizeof(WAVEFORMATEX) + WaveFormat->cbSize + sizeof(WaveData) + sizeof(DWORD) + static_cast<DWORD>(BufferSize);
	BYTE *waveFileData = new (std::nothrow) BYTE[waveFileSize];
	BYTE *waveFilePointer = waveFileData;
	WAVEHEADER *waveHeader = reinterpret_cast<WAVEHEADER *>(waveFileData);

	if (waveFileData == NULL)
	{
		printf_s("Unable to allocate %d bytes to hold output wave data\n", waveFileSize);
		return false;
	}

	//
	//  Copy in the wave header - we'll fix up the lengths later.
	//
	CopyMemory(waveFilePointer, WaveHeader, sizeof(WaveHeader));
	waveFilePointer += sizeof(WaveHeader);

	//
	//  Update the sizes in the header.
	//
	waveHeader->dwSize = waveFileSize - (2 * sizeof(DWORD));
	waveHeader->dwFmtSize = sizeof(WAVEFORMATEX) + WaveFormat->cbSize;

	//
	//  Next copy in the WaveFormatex structure.
	//
	CopyMemory(waveFilePointer, WaveFormat, sizeof(WAVEFORMATEX) + WaveFormat->cbSize);
	waveFilePointer += sizeof(WAVEFORMATEX) + WaveFormat->cbSize;


	//
	//  Then the data header.
	//
	CopyMemory(waveFilePointer, WaveData, sizeof(WaveData));
	waveFilePointer += sizeof(WaveData);
	*(reinterpret_cast<DWORD *>(waveFilePointer)) = static_cast<DWORD>(BufferSize);
	waveFilePointer += sizeof(DWORD);

	//
	//  And finally copy in the audio data.
	//
	CopyMemory(waveFilePointer, Buffer, BufferSize);

	//
	//  Last but not least, write the data to the file.
	//
	DWORD bytesWritten;
	if (!WriteFile(FileHandle, waveFileData, waveFileSize, &bytesWritten, NULL))
	{
		printf_s("Unable to write wave file: %d\n", GetLastError());
		delete []waveFileData;
		return false;
	}

	if (bytesWritten != waveFileSize)
	{
		printf_s("Failed to write entire wave file\n");
		delete []waveFileData;
		return false;
	}
	delete []waveFileData;
	return true;
}

//
//  Write the captured wave data to an output file so that it can be examined later.
//
void SaveWaveData(BYTE *CaptureBuffer, size_t BufferSize, const WAVEFORMATEX *WaveFormat)
{
	wchar_t* waveFileName = L"out.wav";
	
	HANDLE waveHandle = CreateFile(waveFileName, GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_ALWAYS, 
		FILE_ATTRIBUTE_NORMAL | FILE_FLAG_SEQUENTIAL_SCAN, 
		NULL);
	if (waveHandle != INVALID_HANDLE_VALUE)
	{
		if (WriteWaveFile(waveHandle, CaptureBuffer, BufferSize, WaveFormat))
		{
			printf_s("Successfully wrote WAVE data to %S\n", waveFileName);
		}
		else
		{
			printf_s("Unable to write wave file\n");
		}
		CloseHandle(waveHandle);
	}
	else
	{
		printf_s("Unable to open output WAV file %S: %d\n", waveFileName, GetLastError());
	}
}

//
//  The core of the sample.
//
//  Pick an audio device then capture data from that device.
//  When done, write the data to a file.
//
int wmain()
{
	int result = 0;
	IMMDevice *device = NULL;
	bool isDefaultDevice;
	ERole role;

	printf_s("WASAPI Capture Shared Timer Driven Sample\n");
	printf_s("Copyright (c) Microsoft.  All Rights Reserved\n");
	printf_s("\n");

	//
	//  A GUI application should use COINIT_APARTMENTTHREADED instead of COINIT_MULTITHREADED.
	//
	HRESULT hr = CoInitializeEx(NULL, COINIT_MULTITHREADED);
	if (FAILED(hr))
	{
		printf_s("Unable to initialize COM: %x\n", hr);
		result = hr;
		goto Exit;
	}

	//
	//  Now that we've parsed our command line, pick the device to capture.
	//
	if (!PickDevice(&device, &isDefaultDevice, &role))
	{
		result = -1;
		goto Exit;
	}

	printf_s("Capture audio data for %d seconds\n", TargetDurationInSec);

	//
	//  Instantiate a capturer and capture sounds for TargetDuration seconds
	//	
	CWASAPICapture *capturer = new (std::nothrow) CWASAPICapture(device, role);
	if (capturer == NULL)
	{
		printf_s("Unable to allocate capturer\n");
		return -1;
	}

	if (capturer->Initialize(TargetLatency))
	{
		//
		//  We've initialized the capturer.  Once we've done that, we know some information about the
		//  mix format and we can allocate the buffer that we're going to capture.
		//
		//
		//  The buffer is going to contain "TargetDuration" seconds worth of PCM data.  That means 
		//  we're going to have TargetDuration*samples/second frames multiplied by the frame size.
		//
		size_t captureBufferSize = capturer->SamplesPerSecond() * TargetDurationInSec * capturer->FrameSize();
		BYTE *captureBuffer = new (std::nothrow) BYTE[captureBufferSize];

		if (captureBuffer == NULL)
		{
			printf_s("Unable to allocate capture buffer\n");
			return -1;
		}

		if (capturer->Start(captureBuffer, captureBufferSize))
		{
			do
			{
				printf_s("  \r%d\r", TargetDurationInSec);
				Sleep(1000);
			} while (--TargetDurationInSec);
			printf_s("\n");

			capturer->Stop();

			//
			//  We've now captured our wave data.  Now write it out in a wave file.
			//
			SaveWaveData(captureBuffer, capturer->BytesCaptured(), capturer->MixFormat());


			//
			//  Now shut down the capturer and delete it
			//
			capturer->Shutdown();
			delete capturer;
		}

		delete []captureBuffer;
	}
	

Exit:
	SafeRelease(&device);	
	CoUninitialize();
	return 0;
}
