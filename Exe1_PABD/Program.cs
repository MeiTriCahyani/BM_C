using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BM_C
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password:");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan:");
                    string db = Console.ReadLine();
                    Console.WriteLine("\n Ketik K untuk Terhubung ke Database:");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source= LAPTOP-LE37C152\\MIMITI03; " +
                                    "initial catalog = {0};" + " User ID = {1};" + " password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Hapus Data");
                                        Console.WriteLine("4. Keluar");
                                        Console.Write("\n Enter your choice(1-4):");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Peminjaman\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Peminjaman\n");
                                                    Console.WriteLine("Masukkan Id Anggota");
                                                    string id_anggota = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id buku:");
                                                    string id_buku = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Pinjam:");
                                                    string tgl_pinjam = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Kembali:");
                                                    string tgl_kembali = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id Pinjam:");
                                                    string id_pinjam = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id Petugas:");
                                                    string id_petugas = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(id_anggota,id_petugas,id_pinjam,id_buku,tgl_kembali,tgl_pinjam, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n Anda tidak memiliki akses untuk menambah data");
                                                    }


                                                }
                                                break;
                                            case '3':
                                                {

                                                }
                                                break;

                                            case '4':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n Invalid option");

                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\n Check for the value entered.");

                                    }

                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From HRD.Mahasiswa", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string id_petugas, String id_anggota, String id_buku, string id_pinjam, string tgl_pinjam, string tgl_kembali, SqlConnection con)
        {
            string str = "";
            str = " insert into Peminjaman(id_petugas,id_anggota,id_buku,id_pinjam,tgl_pinjam,tgl_kembali) values (@id_petugas,@id_anggota,@id_buku,@id_pinjam,@tgl_pinjam,@tgl_kembali) ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("id_petugas", id_petugas));
            cmd.Parameters.Add(new SqlParameter("id_anggota", id_anggota));
            cmd.Parameters.Add(new SqlParameter("id_buku", id_buku));
            cmd.Parameters.Add(new SqlParameter("id_pinjam", id_pinjam));
            cmd.Parameters.Add(new SqlParameter("tgl_pinjam", tgl_pinjam));
            cmd.Parameters.Add(new SqlParameter("tgl_kembali", tgl_kembali));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");

        }
    }
}

