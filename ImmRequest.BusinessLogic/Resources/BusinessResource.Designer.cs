﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmRequest.BusinessLogic.Resources {
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
    public class BusinessResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BusinessResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImmRequest.BusinessLogic.Resources.BusinessResource", typeof(BusinessResource).Assembly);
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
        ///   Looks up a localized string similar to Deleting.
        /// </summary>
        public static string Action_Delete {
            get {
                return ResourceManager.GetString("Action_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Retrieving.
        /// </summary>
        public static string Action_Get {
            get {
                return ResourceManager.GetString("Action_Get", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Updating.
        /// </summary>
        public static string Action_Update {
            get {
                return ResourceManager.GetString("Action_Update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There has been an Error while {0} {1}. Please make sure that the {1} exists.
        /// </summary>
        public static string LogicAction_NotFound {
            get {
                return ResourceManager.GetString("LogicAction_NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are alredy logged in. Try logging out before trying to sign in again..
        /// </summary>
        public static string ValidationError_AlreadyInSession {
            get {
                return ResourceManager.GetString("ValidationError_AlreadyInSession", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Area in request is invalid.
        /// </summary>
        public static string ValidationError_AreaIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_AreaIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email in request is invalid..
        /// </summary>
        public static string ValidationError_EmailIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_EmailIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} in request doesnt exist..
        /// </summary>
        public static string ValidationError_FieldDoesntExists {
            get {
                return ResourceManager.GetString("ValidationError_FieldDoesntExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Must have a value. .
        /// </summary>
        public static string ValidationError_IsEmpty {
            get {
                return ResourceManager.GetString("ValidationError_IsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Must be Unique. .
        /// </summary>
        public static string ValidationError_MustBeUnique {
            get {
                return ResourceManager.GetString("ValidationError_MustBeUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Phone in request is invalid..
        /// </summary>
        public static string ValidationError_PhoneIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_PhoneIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Topic in request is invalid..
        /// </summary>
        public static string ValidationError_TopicIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_TopicIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Topic type in request is invalid..
        /// </summary>
        public static string ValidationError_TopicTypeIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_TopicTypeIsInvalid", resourceCulture);
            }
        }
    }
}
