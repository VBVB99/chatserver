using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using Server_C_Sharp.Properties;


namespace Server_C_Sharp
{  
    class Program
    {
        static int utenti = 0; //lista utenti con gestione disconnessione
        static int ospiti = 0; //lista ospiti con gestione disconnessione
        public static bool avviato = false;

        private static void erroriAvvio()
        {
     
            try
            {
                Console.Write("Connessione al database...");
                MySQL.connessione.Open();
                Console.WriteLine("OK!");

                Console.Write("Caricamento degli utenti...");
                MySQL.query = "SELECT ID, username, email, password, data_registrazione, ultimo_accesso, ID_rank, online, mutato FROM utenti";
                MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                MySQL.cmd.ExecuteReader();
                Console.WriteLine("OK!");
                MySQL.connessione.Close();
             
                MySQL.connessione.Open();
                Console.Write("Caricamento dei ranks...");
                MySQL.query = "SELECT ID, Nome FROM ranks";
                MySQL.cmd.ExecuteNonQuery();
                Console.WriteLine("OK!");
                MySQL.connessione.Close();

                MySQL.connessione.Open();
                Console.Write("Caricamento delle relazioni tra gli utenti...");
                MySQL.query = "SELECT ID_Utente_1, ID_Utente_2, Stato, ID_Utente_Performante FROM relazioni_utenti";
                MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                MySQL.cmd.ExecuteReader();
                Console.WriteLine("OK!");
                MySQL.connessione.Close();

                MySQL.connessione.Open();
                Console.Write("Caricamento dei bans...");
                MySQL.query = "SELECT ID, username, motivo, data_inizio, data_scadenza FROM bans";
                MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                MySQL.cmd.ExecuteReader();
                Console.WriteLine("OK!");
                MySQL.connessione.Close();

                MySQL.connessione.Open();
                Console.Write("Caricamento delle news...");
                MySQL.query = "SELECT ID, titolo, descrizione, contenuto, ID_Utente  FROM news";
                MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                MySQL.cmd.ExecuteReader();
                Console.WriteLine("OK!");
                MySQL.connessione.Close();

                if(avviato == false)
                {
                    Console.Write("Caricamento del server...");
                    Server.Avvia();
                    Console.WriteLine("OK!");
                    avviato = true;
                }
                else
                {
                    Console.WriteLine("Caricamento del server...OK!");
                }
               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server avviato! - Versione: 1.0");
                Console.ResetColor();
                MySQL.connessione.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex);
                Console.Write("Premi INVIO per chiudere.....");
                Console.Read();
                Environment.Exit(0);
            
            }         
        }


 

        public static void logo() {

            Console.ResetColor();


            Console.WriteLine();
            Console.WriteLine(@"                                _____  __         __         ____                                         ");
            Console.WriteLine(@"                               / ___/ / /  ___ _ / /_       / __/ ___   ____ _  __ ___   ____             ");
            Console.WriteLine(@"                              / /__  / _ \/ _ `// __/      _\ \  / -_) / __/| |/ // -_) / __/             ");
            Console.WriteLine(@"                              \___/ /_//_/\_,_/ \__/      /___/  \__/ /_/   |___/ \__/ /_/                ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            erroriAvvio();
            Console.WriteLine();
            Console.ResetColor();
        }

        


        static void Main()
        {
                   
            bool close = false;

            
           
            /* DAL SERVER       UPDATE AUTOMATICO NUMERO ONLINE NEL TITOLO DELLA CONSOLE -> DA FARE IN MODO ASINCRONO
            
            connessione.Open();
            string query_online = "SELECT count(id) FROM utenti WHERE online = 1";
            MySqlCommand cmd_online = new MySqlCommand(query_online, connessione);

            Console.Title = "Chat Server - Utenti online: " + cmd_online.ExecuteScalar();
            connessione.Close();
            */
            // Console.Title = "Chat Server - Utenti online: ?";

            logo();
            Console.Beep();
            
            while (close == false) //ATTESA COMANDO
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Title = "Utenti online: " + utenti + " ~ Ospiti: " + (ospiti + 1);
                Console.Write("Server >> ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                String comando = Console.ReadLine();
                comando = comando.ToLower();

                switch (comando)
                {
                    case "ban":
                        Comandi.banUtente();
                        break;
                    case "permaban":  
                        Comandi.permabanUtente();
                        break;                   
                    case "unban":
                        Comandi.unbanUtente();
                        break;
                    case "kick": //SERVER
                        Comandi.cacciaUtente();
                        break;
                    case "mute": 
                        Comandi.mutaUtente();
                        break;
                    case "alert": //SERVER
                        Comandi.alert();                          
                        break;
                    case "msg": //SERVER
                        Comandi.messageUtente();
                        break;
                    case "onlinelist":
                        Comandi.onlineList();
                        break;
                    case "bannedlist":
                        Comandi.bannedList();
                        break;
                    case "rank":
                        Comandi.rankaUtente();
                        break;
                    case "close":
                        close = true;
                        break;
                    case "clear":
                        Console.Clear();
                        logo();
                        break;
                    case "comandi":
                        Comandi.listaComandi();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("");
                        Console.WriteLine("Comando inesistente, digita 'comandi' per una lista completa dei comandi");
                        Console.WriteLine("");
                        Console.ResetColor();
                        break;
                }                  
            } //END WHILE
           


        }
    }
}
