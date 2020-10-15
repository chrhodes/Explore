using System;
using System.Collections.Generic;
//using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;

namespace SystemManagement
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://life.pacificlife.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WMIInfoWS : System.Web.Services.WebService
    {

        // This method does most of the work.
        // TODO: Support Property[] syntax for properties that return arrays

        private static void GetWMIInformation(ManagementScope scope, string[] properties, ObjectQuery query, object wmiItems, string wmiItemName)
        {
            StringBuilder sb = new StringBuilder(10240);
            string outputValue;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            // Create an instance of the wmiItem we need.  Maybe move some out of this method later

            // Load the Assembly containing the class we want
            Assembly a = Assembly.Load("SystemManagement");
            // Get and create the class.  Remember to use "+" as this is a nested class
            Type wmiClass = a.GetType(string.Format("SystemManagement.WMIInfoWS+{0}", wmiItemName));
            System.Type ty = wmiItems.GetType();
            // Find the "Add" method.  The underlying type should be a List<T> which has an Add method.
            System.Reflection.MethodInfo addMethod = ty.GetMethod("Add");

            // Create an array to hold the arguments for the call
            object[] methodArgs = new object[1];

            foreach (ManagementObject m in queryCollection)
            {
                // Create a new object that can be populated with values and added to our list
                object wmiObj = Activator.CreateInstance(wmiClass);

                foreach (string prop in properties)
                {
                    // Go find the field in the structure that was selected
                    FieldInfo field = wmiClass.GetField(prop);

                    try
                    {
                        // Always use ToString() so don't have to worry about data type
                        outputValue = m[prop].ToString();
                    }
                    catch
                    {
                        outputValue =  "??";
                    }

                    field.SetValue(wmiObj, outputValue);
                }

                // Prepare the arguments for the call
                methodArgs[0] = wmiObj;
                // and finally invoke the method.  This adds a new wmiItem to the wmiItems, 
                // e.g. adds a new Win32AccountS to Win32Accounts
                addMethod.Invoke(wmiItems, methodArgs);
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return string.Format("Hello from {0} It is {1} on {2}", 
                System.Net.Dns.GetHostName(), 
                System.DateTime.Now.ToLongTimeString(),
                System.DateTime.Now.ToLongDateString());
        }

        #region Win32_Account


        [WebMethod]
        public List<Win32Account.Win32AccountS> GetWin32Account()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32Account wmiItem = new Win32Account();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32Account
        {
            // Need to keep this structure matched with the properties in the GetWin32Account()

            public struct Win32AccountS
            {
                public string Caption;
                public string Description;
                public string Domain;
                public string InstallDate;
                public string LocalAccount;
                public string Name;
                public string SID;
                public string SIDType;
                public string Status;
            };

            private List<Win32AccountS> _Win32Accounts = new List<Win32AccountS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32AccountS> Items
            {
                get { return _Win32Accounts; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32Accounts, "Win32Account+Win32AccountS");
            }
        }

        #endregion

        #region Win32_BaseBoard
        
        [WebMethod]
        public List<Win32BaseBoard.Win32BaseBoardS> GetWin32BaseBoard()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32BaseBoard wmiItem = new Win32BaseBoard();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32BaseBoard
        {
            // Need to keep this structure matched with the properties in the GetWin32BaseBoard()

            public struct Win32BaseBoardS
            {
                public string Caption; 
                //public string ConfigOptions[];
                public string CreationClassName;
                public string Depth;
                public string Description;
                public string Height;
                public string HostingBoard;
                public string HotSwappable;
                public string InstallDate;
                public string Manufacturer;
                public string Model;
                public string Name;
                public string OtherIndentifyingInfo;
                public string PartNumber;
                public string PoweredOn;
                public string Product;
                public string Removable;
                public string Replaceable;
                public string RequirementsDescription;
                public string RequiresDaughterBoard;
                public string SerialNumber;
                public string SKU;
                public string SlotLayout;
                public string SpecialRequirements;
                public string Status;
                public string Tag;
                public string Version;
                public string Weight;
                public string Width;
            };

            private List<Win32BaseBoardS> _Win32BaseBoards = new List<Win32BaseBoardS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32BaseBoardS> Items
            {
                get { return _Win32BaseBoards; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32BaseBoards, "Win32BaseBoard+Win32BaseBoardS");
            }
        }

        #endregion

        #region Win32_BIOS

        [WebMethod]
        public List<Win32BIOS.Win32BIOSS> GetWin32BIOS()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32BIOS wmiItem = new Win32BIOS();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32BIOS
        {
            // Need to keep this structure matched with the properties in the GetWin32BIOS()

            public struct Win32BIOSS
            {
                //public string BiosCharacteristics;
                //public string BIOSVersion;
                public string BuildNumber;
                public string Caption;
                public string CodeSet;
                public string CurrentLanguage;
                public string Description;
                public string IdentificationCode;
                public string InstallableLanguages;
                public string InstallDate;
                public string LanguageEdition;
                //public string ListOfLanguages;
                public string Manufacturer;
                public string Name;
                public string OtherTargetOS;
                public string PrimaryBIOS;
                public string ReleaseDate;
                public string SerialNumber;
                public string SMBIOSBIOSVersion;
                public string SMBIOSMajorVersion;
                public string SMBIOSMinorVersion;
                public string SMBIOSPresent;
                public string SoftwareElementID;
                public string SoftwareElementState;
                public string TargetOperatingSystem;
                public string Version;
            };

            private List<Win32BIOSS> _Win32BIOSs = new List<Win32BIOSS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32BIOSS> Items
            {
                get { return _Win32BIOSs; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32BIOSs, "Win32BIOS+Win32BIOSS");
            }
        }

        #endregion

        #region Win32_DiskDrive

        [WebMethod]
        public List<Win32DiskDrive.Win32DiskDriveS> GetWin32DiskDrive()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32DiskDrive wmiItem = new Win32DiskDrive();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32DiskDrive
        {
            // Need to keep this structure matched with the properties in the GetWin32DiskDrive()

            public struct Win32DiskDriveS
            {
                public string Availability; 
                public string BytesPerSector;
                //public string Capabilities[];   // TODO: Arrary
                //public string CapabilityDescriptions[]; // TODO: Array
                public string Caption;
                public string CompressionMethod;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CreationClassName;
                public string DefaultBlockSize;
                public string Description;
                public string DeviceID;
                public string ErrorCleared;
                public string ErrorDescription;
                public string ErrorMethodology;
                public string FirmwareRevision;
                public string Index;
                public string InstallDate;
                public string InterfaceType;
                public string LastErrorCode;
                public string Manufacturer;
                public string MaxBlockSize;
                public string MaxMediaSize;
                public string MeidaLoaded;
                public string MinBlockSize;
                public string Model;
                public string Name;
                public string NeedsCleaning;
                public string NumberOfMediaSupported;
                public string Partitions;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[];    // TODO: Array
                public string PowerManagementSupported;
                public string SCSIBus;
                public string SCSILogigalUnit;
                public string SCSIPort;
                public string SCSITargetId;
                public string SectorsPerTrack;
                public string SerialNumber;
                public string Signature;
                public string Size;
                public string Status;
                public string StatusInfo;
                public string SystemCreationClassName;
                public string SystemName;
                public string TotalCylinders;
                public string TotalHeads;
                public string TotalSectors;
                public string TotalTracks;
                public string TracksPerCylinder;
            };

            private List<Win32DiskDriveS> _Win32DiskDrives = new List<Win32DiskDriveS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32DiskDriveS> Items
            {
                get { return _Win32DiskDrives; }
            }

            public void GetItems(ManagementScope scope)
            {
                string[] properties =
                {
                    "Availability", 
                    "BytesPerSector",
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");

                GetWMIInformation(scope, properties, query, _Win32DiskDrives, "Win32DiskDrive+Win32DiskDriveS");
            }
        }

        #endregion

        #region Win32_Environment

        [WebMethod]
        public List<Win32Environment.Win32EnvironmentS> GetWin32Environment()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32Environment wmiItem = new Win32Environment();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32Environment
        {
            // Need to keep this structure matched with the properties in the GetWin32Environment()

            public struct Win32EnvironmentS
            {
                public string Caption;
                public string Description;
                public string InstallDate;
                public string Name;
                public string Status;
                public string SystemVariable;
                public string UserName;
                public string VaribleName;
            };

            private List<Win32EnvironmentS> _Win32Environments = new List<Win32EnvironmentS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32EnvironmentS> Items
            {
                get { return _Win32Environments; }
            }

            public void GetItems(ManagementScope scope)
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Environment");

                GetWMIInformation(scope, properties, query, _Win32Environments, "Win32Environment+Win32EnvironmentS");
            }
        }

        #endregion

        #region Win32_Group

        [WebMethod]
        public List<Win32Group.Win32GroupS> GetWin32Group()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32Group wmiItem = new Win32Group();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32Group
        {
            // Need to keep this structure matched with the properties in the GetWin32Group()

            public struct Win32GroupS
            {
                public string Caption;
                public string Description;
                public string Domain;
                public string InstallDate;
                public string LocalAccount;
                public string Name;
                public string SID;
                public string SIDType;
                public string Status;
            };

            private List<Win32GroupS> _Win32Groups = new List<Win32GroupS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32GroupS> Items
            {
                get { return _Win32Groups; }
            }

            public void GetItems(ManagementScope scope)
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Group WHERE LocalAccount=True");

                GetWMIInformation(scope, properties, query, _Win32Groups, "Win32Group+Win32GroupS");
            }
        }

        #endregion

        #region Win32_LogicalDisk

        [WebMethod]
        public List<Win32LogicalDisk.Win32LogicalDiskS> GetWin32LogicalDisk()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32LogicalDisk wmiItem = new Win32LogicalDisk();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32LogicalDisk
        {
            public struct Win32LogicalDiskS
            {
                public string Access;
                public string Availability;
                public string BlockSize;
                public string Caption;
                public string Compressed;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CreationClassName;
                public string Description;
                public string DeviceID;
                public string DriveType;
                public string ErrorCleared;
                public string ErrorDescription;
                public string ErrorMethodology;
                public string FileSystem;
                public string FreeSpace;
                public string InstallDate;
                public string LastErrorCode;
                public string MaximumComponentLength;
                public string MediaType;
                public string Name;
                public string NumberOfBlocks;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[]; // TODO: Arrary
                public string PowerManagementSupported;
                public string ProviderName;
                public string Purpose;
                public string QuotasDisabled;
                public string QuotasIncomplete;
                public string QuotasRebuilding;
                public string Size;
                public string Status;
                public string StatusInfo;
                public string SupportsDiskQuotas;
                public string SupportsFileBasedCompression;
                public string SystemCreationClassName;
                public string SystemName;
                public string VolumeDirty;
                public string VolumeName;
                public string VolumeSerialNumber;
            }

            private List<Win32LogicalDiskS> _Win32LogicalDisks = new List<Win32LogicalDiskS>();

            public List<Win32LogicalDiskS> Items
            {
                get
                {
                    return _Win32LogicalDisks;
                }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32LogicalDisks, "Win32LogicalDisk+Win32LogicalDiskS");

                //foreach (ManagementObject m in queryCollection)
                //{
                //    Win32LogicalDiskS wmiItem = new Win32LogicalDiskS();

                //    // TODO: Would be nice to pass the properties list and the wmiItem to a method
                //    // that could reflect on the properties in wmiItem and do the right thing
                //    // like the code in WMIInfo.GetWMIInformation.  For now just hard code it
                //    // and pay attention where problems may occur.

                //    wmiItem.Caption = m["Caption"].ToString();
                //    wmiItem.Description = m["Description"].ToString();
                //    wmiItem.DriveType = m["DriveType"].ToString();
                //    try
                //    {
                //        wmiItem.FileSystem = m["FileSystem"].ToString();
                //    }
                //    catch (Exception ex)
                //    {
                //        wmiItem.FileSystem ="??";
                //    }
                //    try
                //    {
                //        wmiItem.FreeSpace = m["FreeSpace"].ToString();

                //    }
                //    catch (Exception ex)
                //    {
                //        wmiItem.FreeSpace = "??";
                //    }
                //    try
                //    {
                //        wmiItem.Size = m["Size"].ToString();
                //    }
                //    catch (Exception ex)
                //    {
                //        wmiItem.Size = "??";
                //    }

                //    _Win32LogicalDisks.Add(wmiItem);
                //}
            }
        }

        #endregion

        #region Win32_MappedLogicalDisk

        [WebMethod]
        public List<Win32MappedLogicalDisk.Win32MappedLogicalDiskS> GetWin32MappedLogicalDisk()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32MappedLogicalDisk wmiItem = new Win32MappedLogicalDisk();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32MappedLogicalDisk
        {
            // Need to keep this structure matched with the properties in the GetWin32MappedLogicalDisk()

            public struct Win32MappedLogicalDiskS
            {
                public string Access;
                public string Availability; 
                public string BlockSize;
                public string Caption;
                public string Compressed;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CreationClassName;
                public string Description;
                public string DeviceID;
                public string ErrorCleared;
                public string ErrorDescription;
                public string ErrorMethodology;
                public string FileSystem;
                public string FreeSpace;
                public string InstallDate;
                public string LastErrorCode;
                public string MaximumComponentLength;
                public string Name;
                public string NumberOfBlocks;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[];    // TODO: Array
                public string PowerManagementSupported;
                public string ProviderName;
                public string Purpose;
                public string QuotasDisabled;
                public string QuotasIncomplete;
                public string QuotasRebuilding;
                public string SessionID;
                public string Size;
                public string Status;
                public string SupportsDiskQuotas;
                public string SupportsFileBasedCompression;
                public string SystemCreationClassName;
                public string SystemName;
                public string VolumeName;
                public string VolumneSerialNumber;
            };

            private List<Win32MappedLogicalDiskS> _Win32MappedLogicalDisks = new List<Win32MappedLogicalDiskS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32MappedLogicalDiskS> Items
            {
                get { return _Win32MappedLogicalDisks; }
            }

            public void GetItems(ManagementScope scope)
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_MappedLogicalDisk WHERE LocalAccount=True");

                GetWMIInformation(scope, properties, query, _Win32MappedLogicalDisks, "Win32MappedLogicalDisk+Win32MappedLogicalDiskS");
            }
        }


        #endregion

        #region Win32_MemoryDevice

        [WebMethod]
        public List<Win32MemoryDevice.Win32MemoryDeviceS> GetWin32MemoryDevice()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32MemoryDevice wmiItem = new Win32MemoryDevice();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32MemoryDevice
        {
            // Need to keep this structure matched with the properties in the GetWin32MemoryDevice()

            public struct Win32MemoryDeviceS
            {
                public string Access;
                //public string AdditionalErrorData[];
                public string Availability; 
                public string BlockSize;
                public string Caption;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CorrectableError;
                public string CreationClassName;
                public string Description;
                public string DeviceID;
                public string EndingAddress;
                public string ErrorAccess;
                public string ErrorAddress;
                public string ErrorCleared;
                //public string ErrorData[];
                public string ErrorDataOrder;
                public string ErrorDescription;
                public string ErrorGranularity;
                public string ErrorInfo;
                public string ErrorMethodology;
                public string ErrorResolution;
                public string ErrorTime;
                public string ErrorTransferSize;
                public string InstallDate;
                public string LastErrorCode;
                public string Name;
                public string NumberOfBlocks;
                public string OtherErrorDescription;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[];
                public string PowerManagementSupported;
                public string Purpose;
                public string StartingAddress;
                public string Status;
                public string StatusInfo;
                public string SystemCreationClassName;
                public string SystemLevelAddress;
                public string SystemName;
            };

            private List<Win32MemoryDeviceS> _Win32MemoryDevices = new List<Win32MemoryDeviceS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32MemoryDeviceS> Items
            {
                get { return _Win32MemoryDevices; }
            }

            public void GetItems(ManagementScope scope)
            {
                string[] properties =
                {
                    "Access",
                    //"AdditionalErrorData[]", // TODO: Arrary
                    "Availability", 
                    "BlockSize" ,
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

                GetWMIInformation(scope, properties, query, _Win32MemoryDevices, "Win32MemoryDevice+Win32MemoryDeviceS");
            }
        }

        #endregion

        #region Win32_NetworkAdapter

        [WebMethod]
        public List<Win32NetworkAdapter.Win32NetworkAdapterS> GetWin32NetworkAdapter()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32NetworkAdapter wmiItem = new Win32NetworkAdapter();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32NetworkAdapter
        {
            // Need to keep this structure matched with the properties in the GetWin32NetworkAdapter()

            public struct Win32NetworkAdapterS
            {
                public string AdapterType;
                public string AdapterTypeID;
                public string AutoSense;
                public string Availability;
                public string Caption;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CreationClassName;
                public string Description;
                public string DeviceID;
                public string ErrorCleared;
                public string ErrorDescription;
                public string GUID;
                public string Index;
                public string InstallDate;
                public string Installed;
                public string InterfaceIndex;
                public string LastErrorCode;
                public string MACAddress;
                public string Manufacturer;
                public string MaxNumberControlled;
                public string MaxSpeed;
                public string Name;
                public string NetConnectionID;
                public string NetConnectionStatus;
                public string NetEnabled;
                //public string NetworkAddresses[];
                public string PermanentAddress;
                public string PhysicalAdapter;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[];
                public string PowerManagementSupported;
                public string ProductName;
                public string ServiceName;
                public string Speed;
                public string Status;
                public string StatusInfo;
                public string SystemCreationClassName;
                public string SystemName;
                public string TimeOfLastReset; 
            };

            private List<Win32NetworkAdapterS> _Win32NetworkAdapters = new List<Win32NetworkAdapterS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32NetworkAdapterS> Items
            {
                get { return _Win32NetworkAdapters; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32NetworkAdapters, "Win32NetworkAdapter+Win32NetworkAdapterS");
            }
        }

        #endregion

        #region Win32_OperatingSystem
        
        [WebMethod]
        public List<Win32OperatingSystem.Win32OperatingSystemS> GetWin32OperatingSystem()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32OperatingSystem wmiItem = new Win32OperatingSystem();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32OperatingSystem
        {

            public struct Win32OperatingSystemS
            {
                public string BootDevice;
                public string BuildNumber;
                public string BuildType;
                public string Caption;
                public string CodeSet;
                public string CountryCode;
                public string CreatetionClassName;
                public string CSCreationClassName;
                public string CSDVersion;
                public string CSName;
                public string CurrentTimeZone;
                public string DataExecutionPrevention_Available;
                public string DataExecutionPrevention_32BitApplications;
                public string DataExecutionPrevention_Drivers;
                public string DataExecutionPrevention_SupportPolicy;
                public string Debug;
                public string Description;
                public string Distributed;
                public string EncryptionLevel;
                public string ForegroundApplicationBoost;
                public string FreePhysicalMemory;
                public string FreeSpaceInPagingFiles;
                public string FreeVirtualMemory;
                public string InstallDate;
                public string LargeSystemCache;
                public string LastBootUpTime;
                public string LocalDateTime;
                public string Locale;
                public string Manufacturer;
                public string MaxNumberOfProcesses;
                public string MaxProcessMemorySize;
                //public string MUILanguages[];
                public string Name;
                public string NumberOfLicensedUsers;
                public string NumberOfProcesses;
                public string NumberOfUsers;
                public string OperatingSystemSKU;
                public string Organization;
                public string OSArchitecture;
                public string OSLanguage;
                public string OSProductSuite;
                public string OSType;
                public string OtherTypeDescription;
                public string PAEEnabled;
                public string PlusProductId;
                public string PlusVersionNumber;
                public string Primary;
                public string ProductType;
                public string QuantumLength;
                public string QuantumType;
                public string RegisteredUser;
                public string SerialNumber;
                public string ServicePackMajorVersion;
                public string ServicePackMinorVersion;
                public string SizeStoredInPagingFiles;
                public string Status;
                public string SuiteMask;
                public string SystemDevice;
                public string TotalSwapSpaceSize;
                public string TotalVirtualMemorySize;
                public string TotalVisibleMemorySize;
                public string Version;
                public string WindowsDirectory;
            };

            private List<Win32OperatingSystemS> _Win32OperatingSystems = new List<Win32OperatingSystemS>();

            public List<Win32OperatingSystemS> Items
            {
                get { return _Win32OperatingSystems; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32OperatingSystems, "Win32OperatingSystem+Win32OperatingSystemS");
            }
        }

        #endregion

        #region Win32_PhysicalMedia

        [WebMethod]
        public List<Win32PhysicalMedia.Win32PhysicalMediaS> GetWin32PhysicalMedia()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32PhysicalMedia wmiItem = new Win32PhysicalMedia();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32PhysicalMedia
        {
            // Need to keep this structure matched with the properties in the GetWin32PhysicalMedia()

            public struct Win32PhysicalMediaS
            {
                public string Capacity;
                public string Caption;
                public string CleanerMedia;
                public string CreationClassName;
                public string Description;
                public string HotSwappable;
                public string InstallDate;
                public string Manufacturer;
                public string MediaDescription;
                public string MediaType;
                public string Model;
                public string Name;
                public string OtherIdentifyingInfo;
                public string PartNumber;
                public string PoweredOn;
                public string Removeable;
                public string Replaceable;
                public string SerialNumber;
                public string SKU;
                public string Status;
                public string Tag;
                public string Version;
                public string WriteProtectOn;
            };

            private List<Win32PhysicalMediaS> _Win32PhysicalMedias = new List<Win32PhysicalMediaS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32PhysicalMediaS> Items
            {
                get { return _Win32PhysicalMedias; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32PhysicalMedias, "Win32PhysicalMedia+Win32PhysicalMediaS");
            }
        }

        #endregion

        #region Win32_PhysicalMemory

        [WebMethod]
        public List<Win32PhysicalMemory.Win32PhysicalMemoryS> GetWin32PhysicalMemory()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32PhysicalMemory wmiItem = new Win32PhysicalMemory();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32PhysicalMemory
        {
            // Need to keep this structure matched with the properties in the GetWin32PhysicalMemory()

            public struct Win32PhysicalMemoryS
            {
                public string BankLabel;
                public string Capacity;
                public string Caption;
                public string CreationClassName;
                public string DataWidth;
                public string Description;
                public string DeviceLocator;
                public string FormFactor;
                public string HotSwappable;
                public string InstallDate;
                public string InterleaveDataDepth;
                public string InterleavePosition;
                public string Manufacturer;
                public string MemoryType;
                public string Model;
                public string Name;
                public string OtherIdentifyingInfo;
                public string PartNumber;
                public string PositionInRow;
                public string PoweredOn;
                public string Removeable;
                public string Replaceable;
                public string SerialNumber;
                public string SKU;
                public string Speed;
                public string Status;
                public string Tag;
                public string TotalWidth;
                public string TypeDetail;
                public string Version;
            };

            private List<Win32PhysicalMemoryS> _Win32PhysicalMemorys = new List<Win32PhysicalMemoryS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32PhysicalMemoryS> Items
            {
                get { return _Win32PhysicalMemorys; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32PhysicalMemorys, "Win32PhysicalMemory+Win32PhysicalMemoryS");
            }
        }

        #endregion

        #region Win32_Processor

        [WebMethod]
        public List<Win32Processor.Win32ProcessorS> GetWin32Processor()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32Processor wmiItem = new Win32Processor();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32Processor
        {
            // Need to keep this structure matched with the properties in the GetWin32Processor()

            public struct Win32ProcessorS
            {
                public string AddressWidth;
                public string Architecture;
                public string Availability;
                public string Caption;
                public string ConfigManagerErrorCode;
                public string ConfigManagerUserConfig;
                public string CpuStatus;
                public string CreationClassName;
                public string CurrentClockSpeed;
                public string CurrentVoltage;
                public string DataWidth;
                public string Description;
                public string DeviceID;
                public string ErrorCleared;
                public string ErrorDescription;
                public string ExtClock;
                public string Family;
                public string InstallDate;
                public string L2CacheSize;
                public string L2CacheSpeed;
                public string L3CacheSize;
                public string L3CacheSpeed;
                public string LastErrorCode;
                public string Level;
                public string LoadPercentage;
                public string Manufacturer;
                public string MaxClockSpeed;
                public string Name;
                public string NumberOfCores;
                public string NumberOfLogicalProcessors;
                public string OtherFamilyDescription;
                public string PNPDeviceID;
                //public string PowerManagementCapabilities[];
                public string ProcessorId;
                public string ProcessorType;
                public string Revision;
                public string Role;
                public string SocketDescription;
                public string Status;
                public string StatusInfo;
                public string Stepping;
                public string SystemCreationClassName;
                public string SystemName;
                public string UniqueId;
                public string UpgradeMethod;
                public string Version;
                public string VoltageCaps;
            };

            private List<Win32ProcessorS> _Win32Processors = new List<Win32ProcessorS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32ProcessorS> Items
            {
                get { return _Win32Processors; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32Processors, "Win32Processor+Win32ProcessorS");
            }
        }

        #endregion

        #region Win32_Service

        [WebMethod]
        public List<Win32Service.Win32ServiceS> GetWin32Service()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32Service wmiItem = new Win32Service();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32Service
        {
            // Need to keep this structure matched with the properties in the GetItems()

            public struct Win32ServiceS
            {
                public string AcceptPause;
                public string AcceptStop;
                public string Caption;
                public string CheckPoint;
                public string CreationClassName;
                public string Description;
                public string DesktopInteract;
                public string DisplayName;
                public string ErrorControl;
                public string ExitCode;
                public string InstallDate;
                public string Name;
                public string PathName;
                public string ProcessId;
                public string ServiceSpecificExitCode;
                public string ServiceType;
                public string Started;
                public string StartMode;
                public string StartName;
                public string State;
                public string Status;
                public string SystemCreationClassName;
                public string SystemName;
                public string TagId;
                public string WaitHint;
            };

            private List<Win32ServiceS> _Win32Services = new List<Win32ServiceS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32ServiceS> Items
            {
                get { return _Win32Services; }
            }

            public void GetItems(ManagementScope scope)
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

                GetWMIInformation(scope, properties, query, _Win32Services, "Win32Service+Win32ServiceS");
            }
        }

        #endregion

        #region Win32_SystemAccount
        
        [WebMethod]
        public List<Win32SystemAccount.Win32SystemAccountS> GetWin32SystemAccount()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32SystemAccount wmiItem = new Win32SystemAccount();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32SystemAccount
        {
            // Need to keep this structure matched with the properties in the GetWin32SystemAccount()

            public struct Win32SystemAccountS
            {
                public string Caption;
                public string Description;
                public string Domain;
                public string InstallDate;
                public string LocalAccount;
                public string Name;
                public string SID;
                public string SIDType;
                public string Status;
            };

            private List<Win32SystemAccountS> _Win32SystemAccounts = new List<Win32SystemAccountS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32SystemAccountS> Items
            {
                get { return _Win32SystemAccounts; }
            }

            public void GetItems(ManagementScope scope)
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemAccount WHERE LocalAccount=True");

                GetWMIInformation(scope, properties, query, _Win32SystemAccounts, "Win32SystemAccount+Win32SystemAccountS");
            }
        }

        #endregion

        #region Win32_SystemBIOS

        [WebMethod]
        public List<Win32SystemBIOS.Win32SystemBIOSS> GetWin32SystemBIOS()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32SystemBIOS wmiItem = new Win32SystemBIOS();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32SystemBIOS
        {
            // Need to keep this structure matched with the properties in the GetWin32SystemBIOS()

            public struct Win32SystemBIOSS
            {
                public string BiosCharacteristics; 
                public string BIOSVersion;
                public string BuildNumber;
                public string Caption;
                public string CodeSet;
                public string CurrentLanguage;
                public string Description;
                public string IndentificationCode;
                public string InstallableLanguages;
                public string InstallDate;
                public string LanguageEdition;
                public string ListOfLanguages;
                public string Manufacturer;
                public string Name;
                public string OtherTargetOS;
                public string PrimaryBIOS;
                public string ReleaseDate;
                public string SerialNumber;
                public string SMBIOSBIOSVersion;
                public string SMBIOSMajorVersion;
                public string SMBIOSMinorVersion;
                public string SMBIOSPresent;
                public string SoftwareElementID;
                public string SoftwareElementState;
                public string TargetOperatingSystem;
                public string Version; 
            };

            private List<Win32SystemBIOSS> _Win32SystemBIOSs = new List<Win32SystemBIOSS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32SystemBIOSS> Items
            {
                get { return _Win32SystemBIOSs; }
            }

            public void GetItems(ManagementScope scope)
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

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemBIOS WHERE LocalAccount=True");

                GetWMIInformation(scope, properties, query, _Win32SystemBIOSs, "Win32SystemBIOS+Win32SystemBIOSS");
            }
        }

        #endregion

        #region Win32_UserAccount
        
        [WebMethod]
        public List<Win32UserAccount.Win32UserAccountS> GetWin32UserAccount()
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2", options);

            Win32UserAccount wmiItem = new Win32UserAccount();
            wmiItem.GetItems(scope);
            return wmiItem.Items;
        }

        public class Win32UserAccount
        {
            // Need to keep this structure matched with the properties in the GetWin32UserAccount()

            public struct Win32UserAccountS
            {
                public string Caption;
                public string Description;
                public string Domain;
                public string InstallDate;
                public string LocalAccount;
                public string Name;
                public string SID;
                public string SIDType;
                public string Status;
            };

            private List<Win32UserAccountS> _Win32UserAccounts = new List<Win32UserAccountS>();

            // Need to implement a default iterator so can serialize the return 
            // from the Web service

            public List<Win32UserAccountS> Items
            {
                get { return _Win32UserAccounts; }
            }

            public void GetItems(ManagementScope scope)
            {
                string[] properties =
                {
                    "Caption", 
                    "Description",
                    "Domain",
                    "InstallDate",
                    "LocalUserAccount",
                    "Name",
                    "SID",
                    "SIDType",
                    "Status"
                };

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_UserAccount WHERE LocalAccount=True");

                GetWMIInformation(scope, properties, query, _Win32UserAccounts, "Win32UserAccount+Win32UserAccountS");
            }
        }

        #endregion

    }
}
