using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlackWell.BusinessLayer.Contract
{
    public class StorageSettings
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Nomenclatura de la base de datos
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Tipo de Base de datos
        /// </summary>
        public DatabaseType DbType { get; set; }

    }

    public enum DatabaseType
    {
        [EnumMember(Value = "SqlServer")]
        SQLSERVER,
        [EnumMember(Value = "SQLite")]
        SQLITE,
        [EnumMember(Value = "MySql")]
        MYSQL,
        [EnumMember(Value = "PostgreSQL")]
        POSTGRESQL,
        [EnumMember(Value = "InMemory")]
        INMEMORY
    }
}
