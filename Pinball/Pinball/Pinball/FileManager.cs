using System.IO;

namespace Pinball
{
    public class FileManager
    {
        private static FileManager instance;
        public static FileManager GetInstance()
        {
            return instance ?? (instance = new FileManager());
        }

        private FileManager()
        {
        }

        public void RewriteIntInFile(int value, string filePath)
        {
            CreateNewFile(filePath);
            FileStream file = File.OpenWrite(filePath);
            TextWriter writer = new StreamWriter(Stream.Synchronized(file));
            writer.WriteLine(value);
            writer.Close();
            file.Close();
        }

        public int? ReadFromFile(string filePath)
        {
            FileStream file = File.OpenRead(filePath);
            TextReader reader = new StreamReader(Stream.Synchronized(file));

            int? result = null;
            try
            {
                result = int.Parse(reader.ReadLine());
            }
            catch
            {

            }
            finally
            {
                reader.Close();
                file.Close();
            }
            return result;
        }

        public void CreateIfNotExist(string filePath)
        {
            FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
            file.Close();
        }

        private void CreateNewFile(string filePath)
        {
            FileStream file = File.Open(filePath, FileMode.Create);
            file.Close();
        }
    }
}
