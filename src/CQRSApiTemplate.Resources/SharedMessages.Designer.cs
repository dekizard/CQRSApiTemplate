﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CQRSApiTemplate.Resources {
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
    public class SharedMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CQRSApiTemplate.Resources.SharedMessages", typeof(SharedMessages).Assembly);
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
        ///   Looks up a localized string similar to An error occurred while creating category..
        /// </summary>
        public static string errCreateCategory {
            get {
                return ResourceManager.GetString("errCreateCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while creating product..
        /// </summary>
        public static string errCreateProduct {
            get {
                return ResourceManager.GetString("errCreateProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while deleting category..
        /// </summary>
        public static string errDeleteCategory {
            get {
                return ResourceManager.GetString("errDeleteCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while deleting product..
        /// </summary>
        public static string errDeleteProduct {
            get {
                return ResourceManager.GetString("errDeleteProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while updating category..
        /// </summary>
        public static string errUpdateCategory {
            get {
                return ResourceManager.GetString("errUpdateCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while updating product..
        /// </summary>
        public static string errUpdateProduct {
            get {
                return ResourceManager.GetString("errUpdateProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while retrieving category with products..
        /// </summary>
        public static string GetCategoryWithProducts {
            get {
                return ResourceManager.GetString("GetCategoryWithProducts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while retrieving products..
        /// </summary>
        public static string GetProductsByCategoryId {
            get {
                return ResourceManager.GetString("GetProductsByCategoryId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no category with requested Id..
        /// </summary>
        public static string vldCategoryMissing {
            get {
                return ResourceManager.GetString("vldCategoryMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Currency is not valid..
        /// </summary>
        public static string vldCurrency {
            get {
                return ResourceManager.GetString("vldCurrency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Price value should be positive number..
        /// </summary>
        public static string vldNonPositivePrice {
            get {
                return ResourceManager.GetString("vldNonPositivePrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Model is not valid..
        /// </summary>
        public static string vldNotValidModel {
            get {
                return ResourceManager.GetString("vldNotValidModel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no product with requested Id..
        /// </summary>
        public static string vldProductMissing {
            get {
                return ResourceManager.GetString("vldProductMissing", resourceCulture);
            }
        }
    }
}
