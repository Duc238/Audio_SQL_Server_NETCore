using System;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using NAudio.Wave;
namespace Audio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding=Encoding.UTF8;
            string connectionString = "Data Source=DESKTOP-460EDHU\\VOTANDUC;Initial Catalog=Audio;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileName, AudioData FROM AudioFiles WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", 1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fileName = reader["FileName"].ToString();
                            byte[] audioData = (byte[])reader["AudioData"];

                            File.WriteAllBytes(fileName, audioData);
                            Console.WriteLine($"File {fileName} đã được xuất thành công.");
                            // Phát âm thanh
                            PlayAudio(fileName);
                        }
                    }
                }

            }
        }
        static void PlayAudio(string fileName)
        {
            using (var audioFile = new AudioFileReader(fileName))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                Console.WriteLine("Đang phát âm thanh...");

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
