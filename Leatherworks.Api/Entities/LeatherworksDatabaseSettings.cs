using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leatherworks.Api.Entities;
    public class LeatherworksDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
}