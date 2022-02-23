using System;
using System.Collections.Generic;

namespace Infra.LUX.Repositories.Cosmos
{
    public class CosmosSettings
    {
        public Uri?      Uri { get; set; }
        public string?   PrimaryKey { get; set; }
        public string?   ApplicationName { get; set; }
        public string?   DatabaseName { get; set; }

        public IList<Collection>? Collections { get; set; }

        public class Collection
        {
            public string Alias { get; set; }
            public string Name { get; set; }
            public string PartitionKeyPath { get; set; }
            public string UniqueKeys { get; set; }
        }
    }
}
