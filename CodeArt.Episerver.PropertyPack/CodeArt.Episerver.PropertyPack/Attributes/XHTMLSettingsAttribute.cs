using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeArt.Episerver.PropertyPack.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class XhtmlSettingsAttribute : Attribute, IMetadataAware
    {
        /// <summary>
        /// Should the menu be enabled
        /// </summary>
        public bool EnableMenu { get; set; }
        /// <summary>
        /// List of (up to 3) toolbars, separated with |
        /// </summary>
        public string[] ToolBars { get; set; }
        /// <summary>
        /// List of plugins to load
        /// </summary>
        public string[] Plugins { get; set; }
        /// <summary>
        /// Height of editor
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Width of editor
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Path to CSS to use for content
        /// </summary>
        public string ContentCss { get; set; }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            ExtendedMetadata extendedMetadata = metadata as ExtendedMetadata;
            if (extendedMetadata == null) return;
            if (extendedMetadata.EditorConfiguration["settings"] is Dictionary<string, object> settings)
            {
                if (ToolBars != null) settings["toolbar"] = ToolBars.ToList();
                settings["menubar"] = EnableMenu.ToString().ToLower();
                if (Plugins != null) settings["plugins"] = Plugins.ToList();
                if (Height > 0) settings["height"] = Height;
                if (Width > 0) settings["width"] = Width;
                if (ContentCss != null) settings["epi_content_css"] = ContentCss;
            }
        }
    }

}