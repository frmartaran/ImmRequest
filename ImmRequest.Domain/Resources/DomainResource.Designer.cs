//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmRequest.Domain.Resources {
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
    internal class DomainResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DomainResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImmRequest.Domain.Resources.DomainResource", typeof(DomainResource).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DateTime added is not in field range..
        /// </summary>
        internal static string DateTimeNotInRangeException {
            get {
                return ResourceManager.GetString("DateTimeNotInRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bool.
        /// </summary>
        internal static string Field_Bool {
            get {
                return ResourceManager.GetString("Field_Bool", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date Field.
        /// </summary>
        internal static string Field_DateTime {
            get {
                return ResourceManager.GetString("Field_DateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid value format for &apos;{0}&apos; Field. Please provide another value.
        /// </summary>
        internal static string Field_InvalidFormat {
            get {
                return ResourceManager.GetString("Field_InvalidFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Numeric .
        /// </summary>
        internal static string Field_Numeric {
            get {
                return ResourceManager.GetString("Field_Numeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text.
        /// </summary>
        internal static string Field_Text {
            get {
                return ResourceManager.GetString("Field_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Range values can&apos;t be empty.
        /// </summary>
        internal static string FieldRange_EmptyValues {
            get {
                return ResourceManager.GetString("FieldRange_EmptyValues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid range values for {0} Field. Please insert only {1}s..
        /// </summary>
        internal static string FieldRange_InvalidFormat {
            get {
                return ResourceManager.GetString("FieldRange_InvalidFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Start value should be greater then the End value.
        /// </summary>
        internal static string FieldRange_StartGreaterThanEnd {
            get {
                return ResourceManager.GetString("FieldRange_StartGreaterThanEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only {0} are expected for {1} range..
        /// </summary>
        internal static string FieldRange_TooManyValues {
            get {
                return ResourceManager.GetString("FieldRange_TooManyValues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple Selection is not Allowed on {0}.
        /// </summary>
        internal static string MultipleSelecction_Disable {
            get {
                return ResourceManager.GetString("MultipleSelecction_Disable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number added is not in field range.
        /// </summary>
        internal static string NumberFieldNotInRangeException {
            get {
                return ResourceManager.GetString("NumberFieldNotInRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text added is not in field range..
        /// </summary>
        internal static string TextFieldNotInRangeException {
            get {
                return ResourceManager.GetString("TextFieldNotInRangeException", resourceCulture);
            }
        }
    }
}
