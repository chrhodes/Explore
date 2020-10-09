using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Management;

namespace SystemManagement
{
    // This class is primarily to explore how the WMI classes work.  
    // The web service code is in WMIInfoWS.asmx

    public class WMIInfo
    {
        // This method does most of the work.
        // TODO: Support Property[] syntax for properties that return arrays

        private static string GetWMIInformation(ManagementScope scope, string[] properties, ObjectQuery query)
        {
            StringBuilder sb = new StringBuilder(10240);

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject m in queryCollection)
            {
                foreach (string prop in properties)
                {
                    try
                    {
                        // Always use ToString() so don't have to worry about data type
                        sb.AppendLine(String.Format("{0,30} : {1}", prop, m[prop].ToString()));
                    }
                    catch
                    {
                        sb.AppendLine(String.Format("{0,30} : {1}", prop, "??"));
                    }
                }

                sb.AppendLine("-----");
            }

            sb.AppendLine(sb.Length.ToString());

            return sb.ToString();
        }

        public static string Win32_Account(ManagementScope scope)
        {
            string[] properties =
            {
                "Caption", 
                "Description",
                "Domain",
                "InstallDate",
                "LocalAccount",
                "Name",
                "SID",
                "SIDType",
                "Status"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Account WHERE LocalAccount=True");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_BaseBoard(ManagementScope scope)
        {
             string[] properties =
            {
                "Caption", 
                //"ConfigOptions[]" ,
                "CreationClassName",
                "Depth",
                "Description",
                "Height",
                "HostingBoard",
                "HotSwappable",
                "InstallDate",
                "Manufacturer",
                "Model",
                "Name",
                "OtherIndentifyingInfo",
                "PartNumber",
                "PoweredOn",
                "Product",
                "Removable",
                "Replaceable",
                "RequirementsDescription",
                "RequiresDaughterBoard",
                "SerialNumber",
                "SKU",
                "SlotLayout",
                "SpecialRequirements",
                "Status",
                "Tag",
                "Version",
                "Weight",
                "Width"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BaseBoard");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_BIOS(ManagementScope scope)
        {
            string[] properties =
            {
                //"BiosCharacteristics[]", 
                //"BIOSVersion[]" ,
                "BuildNumber",
                "Caption",
                "CodeSet",
                "CurrentLanguage",
                "Description",
                "IdentificationCode",
                "InstallableLanguages",
                "InstallDate",
                "LanguageEdition",
                //"ListOfLanguages[]",
                "Manufacturer",
                "Name",
                "OtherTargetOS",
                "PrimaryBIOS",
                "ReleaseDate",
                "SerialNumber",
                "SMBIOSBIOSVersion",
                "SMBIOSMajorVersion",
                "SMBIOSMinorVersion",
                "SMBIOSPresent",
                "SoftwareElementID",
                "SoftwareElementState",
                "TargetOperatingSystem",
                "Version"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_DiskDrive(ManagementScope scope)
        {
            string[] properties =
            {
                "Availability", 
                "BytesPerSector" ,
                //"Capabilities[]",   // TODO: Arrary
                //"CapabilityDescriptions[]", // TODO: Array
                "Caption",
                "CompressionMethod",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "DefaultBlockSize",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "ErrorMethodology",
                "FirmwareRevision",
                "Index",
                "InstallDate",
                "InterfaceType",
                "LastErrorCode",
                "Manufacturer",
                "MaxBlockSize",
                "MaxMediaSize",
                "MeidaLoaded",
                "MinBlockSize",
                "Model",
                "Name",
                "NeedsCleaning",
                "NumberOfMediaSupported",
                "Partitions",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",    // TODO: Array
                "PowerManagementSupported",
                "SCSIBus",
                "SCSILogigalUnit",
                "SCSIPort",
                "SCSITargetId",
                "SectorsPerTrack",
                "SerialNumber",
                "Signature",
                "Size",
                "Status",
                "StatusInfo",
                "SystemCreationClassName",
                "SystemName",
                "TotalCylinders",
                "TotalHeads",
                "TotalSectors",
                "TotalTracks",
                "TracksPerCylinder"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_Environment(ManagementScope scope)
        {
            string[] properties =
            {
                "Caption",
                "Description",
                "InstallDate",
                "Name",
                "Status",
                "SystemVariable",
                "UserName",
                "VariableName"
            };

            ObjectQuery query = new SelectQuery("SELECT * from Win32_Environment");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_Group(ManagementScope scope)
        {
            string[] properties =
            {
                "Caption",
                "Description",
                "Domain",
                "InstallDate",
                "LocalAccount",
                "Name",
                "SID",
                "SIDType",
                "Status"
            };

            // NOTE: Leaving off the WHERE gives you a lot of stuff!

            ObjectQuery query = new SelectQuery("SELECT * from Win32_Group WHERE LocalAccount=True");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_GroupUser(ManagementScope scope)
        {
            StringBuilder sb = new StringBuilder(2048);

            // To get information about Logical Disks on Computer 
            //ObjectQuery query = new SelectQuery("SELECT * from Win32_GroupUser");
            ObjectQuery query = new SelectQuery("SELECT * from Win32_GroupUser WHERE GroupComponent=\"Win32_Group.Domain='LPSPA01V',Name='Administrators'\"");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject m in queryCollection)
            {
                // To display Logical Disks information
                sb.AppendLine(String.Format("GroupUser (GroupComponent): {0}", m["GroupComponent"].ToString()));
                sb.AppendLine(String.Format("GroupUser  (PartComponent): {0}", m["PartComponent"].ToString()));
            }

            return sb.ToString();
        }

        public static string Win32_LogicalDisk(ManagementScope scope)
        {
            string[] properties =
            {
                "Access",
                "Availability",
                "BlockSize",
                "Caption",
                "Compressed",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "Description",
                "DeviceID",
                "DriveType",
                "ErrorCleared",
                "ErrorDescription",
                "ErrorMethodology",
                "FileSystem",
                "FreeSpace",
                "InstallDate",
                "LastErrorCode",
                "MaximumComponentLength",
                "MediaType",
                "Name",
                "NumberOfBlocks",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]", // TODO: Arrary
                "PowerManagementSupported",
                "ProviderName",
                "Purpose",
                "QuotasDisabled",
                "QuotasIncomplete",
                "QuotasRebuilding",
                "Size",
                "Status",
                "StatusInfo",
                "SupportsDiskQuotas",
                "SupportsFileBasedCompression",
                "SystemCreationClassName",
                "SystemName",
                "VolumeDirty",
                "VolumeName",
                "VolumeSerialNumber"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_MappedLogicalDisk(ManagementScope scope)
        {
            string[] properties =
            {
                "Access",
                "Availability", 
                "BlockSize" ,
                "Caption",
                "Compressed",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "ErrorMethodology",
                "FileSystem",
                "FreeSpace",
                "InstallDate",
                "LastErrorCode",
                "MaximumComponentLength",
                "Name",
                "NumberOfBlocks",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",    // TODO: Array
                "PowerManagementSupported",
                "ProviderName",
                "Purpose",
                "QuotasDisabled",
                "QuotasIncomplete",
                "QuotasRebuilding",
                "SessionID",
                "Size",
                "Status",
                "SupportsDiskQuotas",
                "SupportsFileBasedCompression",
                "SystemCreationClassName",
                "SystemName",
                "VolumeName",
                "VolumneSerialNumber"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_MappedLogicalDisk");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_MemoryDevice(ManagementScope scope)
        {
            string[] properties =
            {
                "Access",
                //"AdditionalErrorData[]", // TODO: Arrary
                "Availability", 
                "BlockSize",
                "Caption",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CorrectableError",
                "CreationClassName",
                "Description",
                "DeviceID",
                "EndingAddress",
                "ErrorAccess",
                "ErrorAddress",
                "ErrorCleared",
                //"ErrorData[]", // TODO: Arrary
                "ErrorDataOrder",
                "ErrorDescription",
                "ErrorGranularity",
                "ErrorInfo",
                "ErrorMethodology",
                "ErrorResolution",
                "ErrorTime",
                "ErrorTransferSize",
                "InstallDate",
                "LastErrorCode",
                "Name",
                "NumberOfBlocks",
                "OtherErrorDescription",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",    // TODO: Array
                "PowerManagementSupported",
                "Purpose",
                "StartingAddress",
                "Status",
                "StatusInfo",
                "SystemCreationClassName",
                "SystemLevelAddress",
                "SystemName"
            };
  
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_MemoryDevice");

            return GetWMIInformation(scope, properties, query);
        }
        
        public static string Win32_NetworkAdapter(ManagementScope scope)
        {
            string[] properties =
            {
                "AdapterType",
                "AdapterTypeID",
                "AutoSense",
                "Availability",
                "Caption",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CreationClassName",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "GUID",
                "Index",
                "InstallDate",
                "Installed",
                "InterfaceIndex",
                "LastErrorCode",
                "MACAddress",
                "Manufacturer",
                "MaxNumberControlled",
                "MaxSpeed",
                "Name",
                "NetConnectionID",
                "NetConnectionStatus",
                "NetEnabled",
                //"NetworkAddresses[]",
                "PermanentAddress",
                "PhysicalAdapter",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",
                "PowerManagementSupported",
                "ProductName",
                "ServiceName",
                "Speed",
                "Status",
                "StatusInfo",
                "SystemCreationClassName",
                "SystemName",
                "TimeOfLastReset"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_OperatingSystem(ManagementScope scope)
        {
            string[] properties =
            {
                "BootDevice", 
                "BuildNumber",
                "BuildType",
                "Caption",
                "CodeSet",
                "CountryCode",
                "CreatetionClassName",
                "CSCreationClassName",
                "CSDVersion",
                "CSName",
                "CurrentTimeZone",
                "DataExecutionPrevention_Available",
                "DataExecutionPrevention_32BitApplications",
                "DataExecutionPrevention_Drivers",
                "DataExecutionPrevention_SupportPolicy",
                "Debug",
                "Description",
                "Distributed",
                "EncryptionLevel",
                "ForegroundApplicationBoost",
                "FreePhysicalMemory",
                "FreeSpaceInPagingFiles",
                "FreeVirtualMemory",
                "InstallDate",
                "LargeSystemCache",
                "LastBootUpTime",
                "LocalDateTime",
                "Locale",
                "Manufacturer",
                "MaxNumberOfProcesses",
                "MaxProcessMemorySize",
                //"MUILanguages[]",
                "Name",
                "NumberOfLicensedUsers",
                "NumberOfProcesses",
                "NumberOfUsers",
                "OperatingSystemSKU",
                "Organization",
                "OSArchitecture",
                "OSLanguage",
                "OSProductSuite",
                "OSType",
                "OtherTypeDescription",
                "PAEEnabled",
                "PlusProductId",
                "PlusVersionNumber",
                "Primary",
                "ProductType",
                "QuantumLength",
                "QuantumType",
                "RegisteredUser",
                "SerialNumber",
                "ServicePackMajorVersion",
                "ServicePackMinorVersion",
                "SizeStoredInPagingFiles",
                "Status",
                "SuiteMask",
                "SystemDevice",
                "TotalSwapSpaceSize",
                "TotalVirtualMemorySize",
                "TotalVisibleMemorySize",
                "Version",
                "WindowsDirectory"
            }; 

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_PhysicalMedia(ManagementScope scope)
        {
            string[] properties =
            {
                "Capacity",
                "Caption",
                "CleanerMedia",
                "CreationClassName",
                "Description",
                "HotSwappable",
                "InstallDate",
                "Manufacturer",
                "MediaDescription",
                "MediaType",
                "Model",
                "Name",
                "OtherIdentifyingInfo",
                "PartNumber",
                "PoweredOn",
                "Removeable",
                "Replaceable",
                "SerialNumber",
                "SKU",
                "Status",
                "Tag",
                "Version",
                "WriteProtectOn"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMedia");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_PhysicalMemory(ManagementScope scope)
        {
            string[] properties =
            {
                "BankLabel",
                "Capacity" ,
                "Caption",
                "CreationClassName",
                "DataWidth",
                "Description",
                "DeviceLocator",
                "FormFactor",
                "HotSwappable",
                "InstallDate",
                "InterleaveDataDepth",
                "InterleavePosition",
                "Manufacturer",
                "MemoryType",
                "Model",
                "Name",
                "OtherIdentifyingInfo",
                "PartNumber",
                "PositionInRow",
                "PoweredOn",
                "Removeable",
                "Replaceable",
                "SerialNumber",
                "SKU",
                "Speed",
                "Status",
                "Tag",
                "TotalWidth",
                "TypeDetail",
                "Version"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");

            return GetWMIInformation(scope, properties, query);
        }
        
        public static string Win32_Processor(ManagementScope scope)
        {
            string[] properties =
            {
                "AddressWidth",
                "Architecture",
                "Availability",
                "Caption",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CpuStatus",
                "CreationClassName",
                "CurrentClockSpeed",
                "CurrentVoltage",
                "DataWidth",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "ExtClock",
                "Family",
                "InstallDate",
                "L2CacheSize",
                "L2CacheSpeed",
                "L3CacheSize",
                "L3CacheSpeed",
                "LastErrorCode",
                "Level",
                "LoadPercentage",
                "Manufacturer",
                "MaxClockSpeed",
                "Name",
                "NumberOfCores",
                "NumberOfLogicalProcessors",
                "OtherFamilyDescription",
                "PNPDeviceID",
                //"PowerManagementCapabilities[]",
                "ProcessorId",
                "ProcessorType",
                "Revision",
                "Role",
                "SocketDescription",
                "Status",
                "StatusInfo",
                "Stepping",
                "SystemCreationClassName",
                "SystemName",
                "UniqueId",
                "UpgradeMethod",
                "Version",
                "VoltageCaps"
            }; 

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Processor");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_Service(ManagementScope scope)
        {
            string[] properties =
            {
                "AcceptPause", 
                "AcceptStop" ,
                "Caption",
                "CheckPoint",
                "CreationClassName",
                "Description",
                "DesktopInteract",
                "DisplayName",
                "ErrorControl",
                "ExitCode",
                "InstallDate",
                "Name",
                "PathName",
                "ProcessId",
                "ServiceSpecificExitCode",
                "ServiceType",
                "Started",
                "StartMode",
                "StartName",
                "State",
                "Status",
                "SystemCreationClassName",
                "SystemName",
                "TagId",
                "WaitHint"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_SystemAccount(ManagementScope scope)
        {
            string[] properties =
            {
                "Caption", 
                "Description",
                "Domain",
                "InstallDate",
                "LocalAccount",
                "Name",
                "SID",
                "SIDType",
                "Status"
            };

            // NOTE: Leaving off the WHERE gives you a lot of stuff!
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemAccount WHERE LocalAccount=True");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_SystemBIOS(ManagementScope scope)
        {
            string[] properties =
            {
                "BiosCharacteristics", 
                "BIOSVersion" ,
                "BuildNumber",
                "Caption",
                "CodeSet",
                "CurrentLanguage",
                "Description",
                "IndentificationCode",
                "InstallableLanguages",
                "InstallDate",
                "LanguageEdition",
                "ListOfLanguages",
                "Manufacturer",
                "Name",
                "OtherTargetOS",
                "PrimaryBIOS",
                "ReleaseDate",
                "SerialNumber",
                "SMBIOSBIOSVersion",
                "SMBIOSMajorVersion",
                "SMBIOSMinorVersion",
                "SMBIOSPresent",
                "SoftwareElementID",
                "SoftwareElementState",
                "TargetOperatingSystem",
                "Version"
            };

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemBIOS");

            return GetWMIInformation(scope, properties, query);
        }

        public static string Win32_UserAccount(ManagementScope scope)
        {
            string[] properties =
            {
                "AccountType",
                "Caption", 
                "Description",
                "Disabled",
                "Domain",
                "FullName",
                "InstallDate",
                "LocalAccount",
                "LockOut",
                "Name",
                "PasswordChangeable",
                "PasswordExpires",
                "PasswordRequired",
                "SID",
                "SIDType",
                "Status"
            };

            // NOTE: Leaving off the WHERE gives you a lot of stuff!
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_UserAccount WHERE LocalAccount=True");

            return GetWMIInformation(scope, properties, query);
        }

        public static string GetUsers(String DomainName, String GroupName)
        {
            StringBuilder sb = new StringBuilder(2048);

            #region Build WMI query using normal SQL statement
            ///<summary>
            /// Build our query
            /// Be careful in adding double quotes and single quotes
            /// Thats why I have used StringBuilder Class to avoid confusion
            ///</summary>
            //StringBuilder sBuilder = new StringBuilder("select * from Win32_GroupUser where ");
            //sBuilder.Append("GroupComponent=");
            //sBuilder.Append('"');
            //sBuilder.Append("Win32_Group.Domain='CHAKS-PC',Name='Users'");
            //sBuilder.Append('"');
            #endregion

            #region Build WMI query using SelectQuery
            ///<summary>
            /// Alternate method for building query
            /// Which I think is better approach
            ///</summary>
            StringBuilder sBuilder = new StringBuilder("GroupComponent=");
            sBuilder.Append('"');
            sBuilder.Append("Win32_Group.Domain=");
            sBuilder.Append("'");
            sBuilder.Append(DomainName);
            sBuilder.Append("'");
            sBuilder.Append(",Name=");
            sBuilder.Append("'");
            sBuilder.Append(GroupName);
            sBuilder.Append("'");
            sBuilder.Append('"');
            SelectQuery sQuery = new SelectQuery("Win32_GroupUser", sBuilder.ToString());
            #endregion

            ///<summary>
            /// Execute the query
            /// Construct a ManagementPath from the PartComponent and check for ClassName
            /// and extract the UserName
            /// Depending on which method you used to build the query,
            /// pass the String or SelectQuery object to ManagementObjectSearcher
            ///</summary>
            try
            {
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(sQuery);

                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    ManagementPath path = new ManagementPath(mObject["PartComponent"].ToString());

                    if (path.ClassName == "Win32_UserAccount")
                    {
                        String[] names = path.RelativePath.Split(',');
                        sb.AppendLine(String.Format(names[1].Substring(names[1].IndexOf("=") + 1).Replace('"', ' ').Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(String.Format(ex.ToString()));
            }

            return sb.ToString();
        }
    }
}

