﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmRequest.WebApi.Resources {
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
    public class WebApiResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WebApiResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImmRequest.WebApi.Resources.WebApiResource", typeof(WebApiResource).Assembly);
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
        ///   Looks up a localized string similar to Created.
        /// </summary>
        public static string Action_Created {
            get {
                return ResourceManager.GetString("Action_Created", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleted.
        /// </summary>
        public static string Action_Deleted {
            get {
                return ResourceManager.GetString("Action_Deleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Updated.
        /// </summary>
        public static string Action_Updated {
            get {
                return ResourceManager.GetString("Action_Updated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid token. Please provide a valid token..
        /// </summary>
        public static string AuthorizationFilter_InvalidTokenFormat {
            get {
                return ResourceManager.GetString("AuthorizationFilter_InvalidTokenFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The authorization token is empty. Please provide a valid token.
        /// </summary>
        public static string AuthorizationFilter_TokenEmpty {
            get {
                return ResourceManager.GetString("AuthorizationFilter_TokenEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Administrator.
        /// </summary>
        public static string Entities_Administrator {
            get {
                return ResourceManager.GetString("Entities_Administrator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type.
        /// </summary>
        public static string Entities_Type {
            get {
                return ResourceManager.GetString("Entities_Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error login in. Please check your credentials..
        /// </summary>
        public static string SessionController_UserNotFound {
            get {
                return ResourceManager.GetString("SessionController_UserNotFound", resourceCulture);
            }
        }
    }
}
