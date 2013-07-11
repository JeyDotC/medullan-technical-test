using System.IO;
using System.Xml.Serialization;

namespace TicTacToe.Models
{
    public class Persistence
    {
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(Game));

        public void Save(Game game)
        {
            if (!Directory.Exists("TicTacToes"))
            {
                Directory.CreateDirectory("TicTacToes");
            }
            var writer = new StreamWriter(GetFileName(game.Name));
            _serializer.Serialize(writer, game);
            writer.Close();
        }

        public Game Get(string name)
        {
            var reader = new StreamReader(GetFileName(name));
            var result = _serializer.Deserialize(reader);
            reader.Close();
            return result as Game;
        }

        public bool Exists(string name)
        {
            return File.Exists(GetFileName(name));
        }

        private string GetFileName(string name)
        {
            return string.Format("TicTacToes/{0}.xml", name);
        }
    }
}