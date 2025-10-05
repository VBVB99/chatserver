using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace Server_C_Sharp
{
    class Comandi
    {
        public static void listaComandi()
        {
            
            String[] lista_comandi = { "ban", "permaban", "unban", "kick", "mute", "alert", "msg", "onlinelist", "bannedlist", "rank", "close", "clear", "comandi" };
            String[] descrizioni_comandi = { "-> Banna un utente",
                                             "-> Banna un utente permanentemente",
                                             "-> Sbanna un utente",
                                             "-> Caccia un utente dalla chat",
                                             "-> Muta un utente",
                                             "-> Invia un messaggio visibile a tutti",
                                             "-> Invia un messaggio privato",
                                             "-> Visualizza una lista degli utenti online",
                                             "-> Visualizza una lista degli utenti bannati",
                                             "-> Ranka un utente",
                                             "-> Spegni il server",
                                             "-> Pulisce la console",
                                             "-> Visualizza questa lista" };
       
            int conto = 0;
            Console.WriteLine();
            foreach (String x in lista_comandi)
            {
                conto++;
                Console.WriteLine(x + " " + descrizioni_comandi[conto - 1]);
            }
            Console.WriteLine();
        }


        public static void banUtente()
        {
         
            bool exit = false;
            string utente;
            string motivo;
            int durata;

            Console.WriteLine();
            while (exit == false)
            {

                Console.Write("Utente: ");
                Console.ForegroundColor = ConsoleColor.White;
                utente = Console.ReadLine();
                if (utente == ".annulla")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operazione annullata!");
                    Console.WriteLine();
                    exit = true;
                }
                else
                {
                    MySQL.query = "SELECT username FROM utenti WHERE username = '" + utente + "'";
                    MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                    //MySQL.connessione.Open();
                    MySQL.risultato_singolo = (string)MySQL.cmd.ExecuteScalar();
                    //MySQL.connessione.Close();
                    if (MySQL.risultato_singolo == null)  //L'UTENTE NON RISULTA PRESENTE TRA I REGISTRATI
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Impossibile trovare l'utente specificato!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        MySQL.query = "SELECT username FROM bans WHERE username = '" + utente + "'";
                        MySqlCommand cmd2 = new MySqlCommand(MySQL.query, MySQL.connessione);
                       // MySQL.connessione.Open();
                        MySQL.risultato_singolo = (string)cmd2.ExecuteScalar();
                        //MySQL.connessione.Close();
                        if (MySQL.risultato_singolo != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("L'utente specificato risulta già bannato!");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Motivo: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            motivo = Console.ReadLine();
                            if (motivo == ".annulla")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Operazione annullata!");
                                Console.WriteLine();
                                exit = true;
                            }
                            else
                            {
                                while (exit == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write("Durata(in giorni): ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    try
                                    {
                                        durata = Convert.ToInt32(Console.ReadLine());
                                        if (durata == 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Operazione annullata!");
                                            Console.WriteLine();
                                            exit = true;
                                        }
                                        else
                                        {
                                            DateTime data_inizio = DateTime.Now;
                                            string data_scadenza = data_inizio.AddDays(durata).ToString();

                                            MySQL.query = "INSERT INTO bans (username, motivo, data_inizio, data_scadenza) VALUES ('" + utente + "', '" + motivo + "', '" + data_inizio.ToString() + "', '" + data_scadenza + "')";
                                            MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                                            //MySQL.connessione.Open();
                                            MySQL.cmd.ExecuteNonQuery();
                                            //MySQL.connessione.Close();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("L'utente inserito è stato bannato con successo!");
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.WriteLine();
                                            exit = true;
                                        }
                                    }
                                    catch (System.FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("La durata inserita non è valida!");
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void permabanUtente()
        {     
            bool exit_permaban = false;
            string utente_permaban;
            string motivo_permaban;

            Console.WriteLine();
            while (exit_permaban == false)
            {
                Console.Write("Utente: ");
                Console.ForegroundColor = ConsoleColor.White;
                utente_permaban = Console.ReadLine();

                if (utente_permaban == ".annulla")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operazione annullata! \n");
                    exit_permaban = true;
                }
                else
                {
                    MySQL.query = "SELECT username FROM utenti WHERE username = '" + utente_permaban + "'";
                    MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                    //MySQL.connessione.Open();
                    MySQL.risultato_singolo = (string)MySQL.cmd.ExecuteScalar();
                   //MySQL.connessione.Close();
                    if (MySQL.risultato_singolo == null)  //L'UTENTE NON RISULTA PRESENTE TRA I REGISTRATI
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Impossibile trovare l'utente specificato!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        MySQL.query = "SELECT username FROM bans WHERE username = '" + utente_permaban + "'";
                        MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                        //MySQL.connessione.Open();
                        MySQL.risultato_singolo = (string)MySQL.cmd.ExecuteScalar();
                        //MySQL.connessione.Close();
                        if (MySQL.risultato_singolo != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("L'utente specificato risulta già bannato!");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Motivo: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            motivo_permaban = Console.ReadLine();
                            if (motivo_permaban == ".annulla")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Operazione annullata!");
                                Console.WriteLine();
                                exit_permaban = true;
                            }
                            else
                            {
                                DateTime data_inizio = DateTime.Now;
                                MySQL.query = "INSERT INTO bans (username, motivo, data_inizio, data_scadenza) VALUES ('" + utente_permaban + "', '" + motivo_permaban + "', '" + data_inizio.ToString() + "', '//')";
                                MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                                //MySQL.connessione.Open();
                                MySQL.cmd.ExecuteNonQuery();
                                //MySQL.connessione.Close();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("L'utente inserito è stato permanentemente bannato con successo!");
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine();
                                exit_permaban = true;
                            }
                        }
                    }
                }
            }
        }

        public static void unbanUtente()
        {

        }

        public static void cacciaUtente()
        {

        }

        public static void mutaUtente()
        {

        }

        public static void alert()
        {
        
        }

        public static void onlineList()
        {
            int n_risultati = 0;
            MySQL.query = "SELECT username from utenti where online = 1";
            //MySQL.connessione.Open();
            MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
            MySQL.risultati_multipli = MySQL.cmd.ExecuteReader();
            while (MySQL.risultati_multipli.Read())
            {
                n_risultati++;
            }
            if (n_risultati == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine("Al momento non ci sono utenti online!");
                Console.WriteLine();
                Console.ResetColor();
                //MySQL.connessione.Close();
            }
            else
            {
                Console.WriteLine();
                while (MySQL.risultati_multipli.Read())
                {
                    Console.WriteLine(MySQL.risultati_multipli["username"]);
                }
                Console.WriteLine();
            }
        }

        public static void bannedList()
        {
            int n_risultati = 0;
            MySQL.query = "SELECT username from bans";
           //MySQL.connessione.Open();
            MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
            MySQL.risultati_multipli = MySQL.cmd.ExecuteReader();
            Console.WriteLine();
            while (MySQL.risultati_multipli.Read())
            {
                n_risultati++;
                Console.WriteLine(MySQL.risultati_multipli["username"]);
            }
            if (n_risultati == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Al momento non ci sono utenti bannati!");
                Console.ResetColor();
            }
            //MySQL.connessione.Close();
            Console.WriteLine();
        }

        public static void rankaUtente()
        {
            String utente;
            String rank = "0";
            bool exit_rank = false;

            while (exit_rank == false)
            {
                Console.WriteLine();
                Console.Write("Utente: ");
                Console.ForegroundColor = ConsoleColor.White;
                utente = Console.ReadLine();
                if (utente == ".annulla")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operazione annullata!");
                    Console.WriteLine();
                    exit_rank = true;
                }
                else
                {
                    MySQL.query = "SELECT username FROM utenti WHERE username = '" + utente + "'";
                    MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                    //MySQL.connessione.Open();
                    MySQL.risultato_singolo = (string)MySQL.cmd.ExecuteScalar();
                    //MySQL.connessione.Close();
                    if (MySQL.risultato_singolo == null)  //L'UTENTE NON RISULTA PRESENTE TRA I REGISTRATI
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Impossibile trovare l'utente specificato!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Rank: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        rank = Console.ReadLine();
                        if (rank == ".annulla")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Operazione annullata!");
                            Console.WriteLine();
                            return;
                        }
                        MySQL.query = "UPDATE utenti SET ID_rank = " + rank + " WHERE username = " + "'" + utente + "'";
                        MySQL.cmd = new MySqlCommand(MySQL.query, MySQL.connessione);
                       //MySQL.connessione.Open();
                        MySQL.risultato_singolo = (string)MySQL.cmd.ExecuteScalar();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Il rank dell'utente è stato aggiornato con successo!");
                       //MySQL.connessione.Close();
                        Console.WriteLine();
                        exit_rank = true;
                    }
                }
            }

        }

        public static void messageUtente()
        {

        }

        
    }
}
