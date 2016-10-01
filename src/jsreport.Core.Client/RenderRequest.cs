﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using jsreport.Core.Client.Entities;

namespace jsreport.Core.Client
{
    public class RenderRequest
    {
        /// <summary>
        /// Report template specification, REQUIRED
        /// </summary>
        [JsonIgnore]
        public Template template { get; set; }

        [JsonProperty("template")]
        internal dynamic dynamicTemplate { get; set; }

        /// <summary>
        /// Rendering input data, any serializable object, OPTIONAL
        /// </summary>
        [JsonProperty("data")]
        public object data { get; set; }

        /// <summary>
        /// Rendering custom options, OPTIONAL
        /// </summary>
        [JsonIgnore]
        public RenderOptions options { get; set; }

        [JsonProperty("options")]
        internal dynamic dynamicOptions { get; set; }

        internal void CopyDynamicAttributes()
        {
            dynamicTemplate = new ExpandoObject();

            if (template.name != null)
                dynamicTemplate.name = template.name;
            if (template.content != null)
                dynamicTemplate.content = template.content;
            if (template.helpers != null)
                dynamicTemplate.helpers = template.helpers;
            if (template.shortid != null)
                dynamicTemplate.shortid = template.shortid;
            if (template.recipe != null)
                dynamicTemplate.recipe = template.recipe;
            if (template.engine != null)
                dynamicTemplate.engine = template.engine;
            if (template.phantom != null)
                dynamicTemplate.phantom = template.phantom;

            if (template.additional != null)
            {
                foreach (var p in template.additional.GetType().GetRuntimeProperties())
                {
                    ((IDictionary<string, object>)dynamicTemplate)[p.Name] = p.GetValue(template.additional);
                }
            }

            dynamicOptions = new ExpandoObject();
            if (options.preview != null)
                dynamicOptions.preview = options.preview;
            if (options.timeout != null)
                dynamicOptions.timeout = options.timeout;

            if (options.additional != null)
            {
                foreach (var p in options.additional.GetType().GetRuntimeProperties())
                {
                    ((IDictionary<string, object>)dynamicOptions)[p.Name] = p.GetValue(options.additional);
                }
            }
        }

        public void Validate()
        {
            if (template == null)
                throw new ArgumentNullException("template cannot be null");

            if (template.shortid == null && template.name == null && string.IsNullOrEmpty(template.recipe))
                throw new ArgumentNullException("recipe cannot be null");

            if (template.shortid == null && template.name == null && string.IsNullOrEmpty(template.content))
                throw new ArgumentNullException("template.content cannot be null");
        }
    }
}
