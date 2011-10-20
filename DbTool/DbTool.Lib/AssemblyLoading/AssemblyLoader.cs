﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;

namespace DbTool.Lib.AssemblyLoading
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private readonly IDbToolConfig _config;

        public AssemblyLoader(IDbToolConfig config)
        {
            _config = config;
        }

        public AssemblyHandler GetAssemblyFor(string databaseType)
        {
            var assemblyPath = _config.Settings.AssemblyMap[databaseType];
            var assembly = Assembly.LoadFile(assemblyPath);
            return new AssemblyHandler(databaseType, assembly);
        }
    }
}