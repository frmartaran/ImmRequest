//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmRequest.Importer.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ImporterResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ImporterResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImmRequest.Importer.Resources.ImporterResource", typeof(ImporterResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Types.
        /// </summary>
        public static string EntityToImport_Type {
            get {
                return ResourceManager.GetString("EntityToImport_Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The directory specified could not be found!.
        /// </summary>
        public static string FileLoad_DirectoryNotFound {
            get {
                return ResourceManager.GetString("FileLoad_DirectoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path of the file must not be empty!.
        /// </summary>
        public static string FileLoad_EmptyPath {
            get {
                return ResourceManager.GetString("FileLoad_EmptyPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file specified could not be found!.
        /// </summary>
        public static string FileLoad_FileNotFound {
            get {
                return ResourceManager.GetString("FileLoad_FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid format or Empty file, please correct the file.
        /// </summary>
        public static string Format_Invalid {
            get {
                return ResourceManager.GetString("Format_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Json reader is null.
        /// </summary>
        public static string Json_ReaderNull {
            get {
                return ResourceManager.GetString("Json_ReaderNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Json Serializer is Null.
        /// </summary>
        public static string Json_SerializerNull {
            get {
                return ResourceManager.GetString("Json_SerializerNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no {0} to import on this file. Please verify the formatting.
        /// </summary>
        public static string NoEntityToImport {
            get {
                return ResourceManager.GetString("NoEntityToImport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing Data Type Tag. Please verfy the formatting of the file..
        /// </summary>
        public static string XMLImporter_Type_NoDataTypeTag {
            get {
                return ResourceManager.GetString("XMLImporter_Type_NoDataTypeTag", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported DataType, please make sure the data types are either: Number, Text, DateTime or Bool.
        /// </summary>
        public static string XMLImporter_Type_UnsupportedDataType {
            get {
                return ResourceManager.GetString("XMLImporter_Type_UnsupportedDataType", resourceCulture);
            }
        }
    }
}
