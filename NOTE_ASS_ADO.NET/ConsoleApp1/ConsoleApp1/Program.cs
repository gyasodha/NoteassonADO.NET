
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{

    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************************NOTE APP********************************");
            while (true)
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1. Create Note");
                Console.WriteLine("2. View Note");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Note");
                Console.WriteLine("5. Delete Note");
                Console.WriteLine("6. Exit");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateNote();
                            break;
                        case 2:
                            ViewNote();
                            break;
                        case 3:
                            ViewAllNotes();
                            break;
                        case 4:
                            UpdateNote();
                            break;
                        case 5:
                            DeleteNote();
                            break;
                        
                        default:
                            Console.WriteLine("Wrong Choice Entered!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Choice Entered!");
                }
            }
            static void CreateNote()
            {
                SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=notedata;Integrated Security=true");
                SqlDataAdapter adp = new SqlDataAdapter("select * from noteapp",con);
                DataSet ds = new DataSet();
                adp.Fill(ds,"notetable");
                Console.WriteLine("Enter the title:");
                string title = Console.ReadLine();
                Console.WriteLine("Enter the note description");
                string notedescr = Console.ReadLine();
                Console.WriteLine("Enter the note date");
                string notedate = Console.ReadLine();
                var row = ds.Tables["notetable"].NewRow();
                row["title"] = title;
                row["notedescr"] = notedescr;
                row["notedate"] = notedate;

                ds.Tables["notetable"].Rows.Add(row);

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds,"notetable");
                Console.WriteLine("Database Updated");
            }
                
            static void ViewNote()
            {
                SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=notedata;Integrated Security=true");
                SqlDataAdapter adp = new SqlDataAdapter($"select * from noteapp", con);
                DataSet ds = new DataSet();
                adp.Fill(ds,"notetable");

                Console.WriteLine("Enter the particular id to view note");
                int id = Convert.ToInt32(Console.ReadLine());

                var row = ds.Tables["notetable"].Select($"id={id}");
                if (row.Length > 0)
                {
                    var rows = row[0];
                    Console.WriteLine($"Title:{rows["title"]}");
                    Console.WriteLine($"description:{rows["notedescr"]}");
                    Console.WriteLine($"date:{rows["notedate"]}");
                }
                else
                {
                    Console.WriteLine("note not found");
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds,"notetable");
                

            }
            static void ViewAllNotes()
            {
                SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=notedata;Integrated Security=true");
                SqlDataAdapter adp = new SqlDataAdapter($"select * from noteapp", con);
                DataSet ds = new DataSet();
                adp.Fill(ds,"notetable");
                for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
                {
                    for(int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["notetable"].Rows[i][j]}    | ");
                    }
                    Console.WriteLine();
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds,"notetable");
               
            }
            static void UpdateNote()
            {
                SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=notedata;Integrated Security=true");
                SqlDataAdapter adp = new SqlDataAdapter($"select * from noteapp", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataSet ds = new DataSet();
                adp.Fill(ds, "notetable");
                Console.WriteLine("Enter the particular id to update the note");
                int id = Convert.ToInt32(Console.ReadLine());
                var row = ds.Tables["notetable"].Select($"id={id}");
                if (row.Length > 0)
                {
                    var rows = row[0];
                    Console.WriteLine("Enter the update title");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter the updated description");
                    string notedescr = Console.ReadLine();
                    Console.WriteLine("Enter the updated date");
                    string notedate= Console.ReadLine();
                    rows["title"] = title;
                    rows["notedescr"] = notedescr;
                    rows["notedate"] = notedate;

                    adp.Update(ds, "notetable");
                    Console.WriteLine("Note updated successfully");


                }
                else
                {
                    Console.WriteLine("note not found");
                }

            }
            static void DeleteNote()
            {
                SqlConnection con = new SqlConnection("server=IN-8B3K9S3;database=notedata;Integrated Security=true");
                SqlDataAdapter adp = new SqlDataAdapter($"select * from noteapp", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                DataSet ds = new DataSet();
                adp.Fill(ds, "notetable");
                Console.WriteLine("Enter the particular id to delete the note");
                int id = Convert.ToInt32(Console.ReadLine());
                var row = ds.Tables["notetable"].Select($"id={id}");
                if (row.Length > 0)
                {
                    var rows = row[0];
                    rows.Delete();

                    adp.Update(ds, "notetable");

                    Console.WriteLine("Note deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Note not found.");
                }


            }
        }

    }
}
