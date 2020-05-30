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
        ///   Looks up a localized string similar to Finding.
        /// </summary>
        public static string Action_Find {
            get {
                return ResourceManager.GetString("Action_Find", resourceCulture);
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
        ///   Looks up a localized string similar to There is no finder for this entity. Contact support..
        /// </summary>
        public static string Developer_Exception {
            get {
                return ResourceManager.GetString("Developer_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong parameters or missing Construsctor. Please use another importer.
        /// </summary>
        public static string DeveloperException_ImporterWrongParameters {
            get {
                return ResourceManager.GetString("DeveloperException_ImporterWrongParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Area.
        /// </summary>
        public static string Entity_Area {
            get {
                return ResourceManager.GetString("Entity_Area", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Citizen Request.
        /// </summary>
        public static string Entity_Request {
            get {
                return ResourceManager.GetString("Entity_Request", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Topic.
        /// </summary>
        public static string Entity_Topic {
            get {
                return ResourceManager.GetString("Entity_Topic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type.
        /// </summary>
        public static string Entity_TopicType {
            get {
                return ResourceManager.GetString("Entity_TopicType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        public static string Field_Name {
            get {
                return ResourceManager.GetString("Field_Name", resourceCulture);
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
        ///   Looks up a localized string similar to Name in request is invalid..
        /// </summary>
        public static string ValidationError_CitizenNameIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_CitizenNameIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Status in request is invalid. Perhaps you meant Create?.
        /// </summary>
        public static string ValidationError_CreateRequestStatusIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_CreateRequestStatusIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Description in request is invalid..
        /// </summary>
        public static string ValidationError_DescriptionIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_DescriptionIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is invalid..
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
        ///   Looks up a localized string similar to {0} Must Belong to It&apos;s Parent {1}.
        /// </summary>
        public static string ValidationError_MustBelong {
            get {
                return ResourceManager.GetString("ValidationError_MustBelong", resourceCulture);
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
        ///   Looks up a localized string similar to {0} Must Contain {1}.
        /// </summary>
        public static string ValidationError_MustContainField {
            get {
                return ResourceManager.GetString("ValidationError_MustContainField", resourceCulture);
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
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid request. Status {0} cannot transition to status {1}..
        /// </summary>
        public static string ValidationError_UpdateRequestStatusIsInvalid {
            get {
                return ResourceManager.GetString("ValidationError_UpdateRequestStatusIsInvalid", resourceCulture);
            }
        }
    }
}
