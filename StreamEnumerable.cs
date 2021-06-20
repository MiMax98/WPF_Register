using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AplOkien
{
    public class StreamEnumerable<T> : IEnumerable<T> where T : new()
    {
        private readonly string _path;

        public StreamEnumerable(string path)
        {
            _path = path;
        }

        public IEnumerator<T> GetEnumerator()
        {
            using FileStream fs = new FileStream(_path, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                yield return DynamicSerializer.Load<T>(sr);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
