using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leatherworks.Api.Entities;
public class LeatherworksDatabaseSettings
{
    public string ConnectionString { get; set; } = "mongo://127.0.0.1:27017"!;

    public string DatabaseName { get; set; } = null!;

    //        public string BooksCollectionName { get; set; } = null!;
}