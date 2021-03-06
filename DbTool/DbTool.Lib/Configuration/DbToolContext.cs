﻿using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Exceptions;
using DotNetPrograms.Common.ExtensionMethods;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class DbToolContext
    {
        public string Name { get; set; }
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public DbToolCredentials Credentials { get; set; }
        public IList<DbToolDatabase> Databases { get; set; }
        [JsonIgnore]
        public IEnumerable<DbToolDatabase> Connections
        {
            get { return GetDefaultDatabase().ToListWith(Databases.OrderBy(d => d.Name)); }
        }

        public DbToolContext(string name) : this()
        {
            Name = name;
        }

        public DbToolContext()
        {
            Databases = new List<DbToolDatabase>();
        }
        
        public static DbToolContext Default(string name)
        {
            return new DbToolContext(name)
            {
                DatabaseType = "sqlserver",
                Host = "(local)",
            }
            .WithCredentials(new DbToolCredentials
                    {
                        IntegratedSecurity = true
                    });
        }

        public DbToolContext WithCredentials(DbToolCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }
        
        public void AddDatabase(DbToolDatabase database)
        {
            if (Databases.Any(d => d.Name.EqualsIgnoreCase(database.Name)))
            {
                throw new UserException(ExceptionType.DatabaseAlreadyExists, database.Name);
            }
            Databases.Add(database);
            database.Parent = this;
        }
        
        public void RemoveDatabase(string name)
        {
            var database = Databases.FirstOrDefault(d => d.Name.EqualsIgnoreCase(name));
            if (database == null)
            {
                return;
            }
            Databases.Remove(database);
        }

        public DbToolContext WithDatabase(DbToolDatabase database)
        {
            AddDatabase(database);
            return this;
        }

        public DbToolDatabase GetDatabase(string name)
        {
            if (name.Equals("Default"))
            {
                return GetDefaultDatabase();
            }
            return Databases.FirstOrDefault(d => d.Name.Equals(name));
        }

        public DbToolDatabase GetDefaultDatabase()
        {
            return new DbToolDatabase
                {
                    Name= "Default",
                    Credentials = Credentials,
                    DatabaseType = DatabaseType,
                    Host = Host,
                    Parent = this
                };
        }

        public ConnectionData GetDefaultConnection()
        {
            return GetDefaultDatabase().GetConnectionData();
        }
    }
}