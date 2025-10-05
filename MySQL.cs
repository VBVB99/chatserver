using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace Server_C_Sharp
{
    class MySQL
    {
        public static IniFile fileconfig = new IniFile("config.ini");
        public static MySqlConnection connessione = new MySqlConnection("Server=" + fileconfig.Read("mysql.host", "MySQL") + ";database=" + fileconfig.Read("mysql.database", "MySQL") + ";userid=" + fileconfig.Read("mysql.username", "MySQL") + ";pwd=" + fileconfig.Read("mysql.password", "MySQL") + ";port=" + fileconfig.Read("mysql.porta", "MySQL") + ";SslMode=none"); //DA SETTARE CON .ini
        public static string query;
        public static string risultato_singolo;
        public static MySqlDataReader risultati_multipli;
        public static MySqlCommand cmd;
    }

}
