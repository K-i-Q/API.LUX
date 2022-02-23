using System;
using System.Collections.Generic;

namespace Infra.LUX.Repositories.CosmosDB
{
    public class CosmosDBOptions
    {
        public string ApplicationName { get; set; }
        public string DatabaseName { get; set; }
        public string PrimaryKey { get; set; }
        public Uri Uri { get; set; }

        public List<Collection> Collections { get; set; }

        public void Deconstruct(out string databaseName, out List<Collection> collectionNames)
        {
            databaseName = DatabaseName;
            collectionNames = Collections;
        }
    }

    public class Collection
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string PartitionKeyPath { get; set; }
        public string UniqueKeys { get; set; }
    }
}
