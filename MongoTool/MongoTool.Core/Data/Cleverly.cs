﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MongoTool.Core.Data
{
    public class Cleverly
    {
        private static readonly IDictionary<Type, PropertyInfo> IdProperties = new Dictionary<Type, PropertyInfo>();

        public static object GetIdFrom(object item)
        {
            var id = GetIdProperty(item);
            return id.GetValue(item);
        }

        private static PropertyInfo GetIdProperty(object item)
        {
            var type = item.GetType();
            if (!IdProperties.ContainsKey(type))
            {
                IdProperties[type] = FindIdProperty(type);
            }
            return IdProperties[type];
        }

        private static PropertyInfo FindIdProperty(Type type)
        {
            var property = GetIdCandidates(type).FirstOrDefault();
            if (property != null)
            {
                return property;
            }
            throw new InvalidOperationException(string.Format("Could not find Id property of {0}", type));
        }

        private static IEnumerable<PropertyInfo> GetIdCandidates(Type type)
        {
            return new[] { type.GetProperty("Id") }
                .Concat(type.GetProperties().Where(p => p.Name.ToLowerInvariant().Contains("id")));
        }
    }
}