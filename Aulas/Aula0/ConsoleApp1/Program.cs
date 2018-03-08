using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Livro> livros = new List<Livro>();
            using (var sr = new StreamReader("livros.txt"))
            {
                string line = null;
                int id = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split('|');

                    var src = parts[0];
                    var name = parts[1];

                    SaveImage(src, $"large_{id:d3}.jpg", ImageFormat.Jpeg);

                    livros.Add(new Livro(id, name, src));

                    id++;
                }
            }

            var json = JsonConvert.SerializeObject(livros);
            File.WriteAllText("livros.json", json);
        }

        public static void SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
                bitmap.Save(filename, format);

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
    }

    internal class Livro
    {

        public Livro(int id, string name, string src)
        {
            this.Id = id;
            this.Name = name;
            this.Src = src;
        }

        public int Id { get; }
        public string Name { get; }
        public string Src { get; }
    }
}
