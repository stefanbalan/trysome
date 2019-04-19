using System.Text;

namespace ts.Domain.Entities
{
//    [Table("MessageContent")]
    public class MessageContent : Securable
    {

        public MessageContent()
        {
            SecurableType = SecurableType.MessageContent;
            IsTemplate = false;
        }

        //public int ID { get; set; }

        public string Name { get; set; }

        public bool IsTemplate { get; set; }

//        [NotMapped]
        public string Content
        {
            get
            {
                if (ByteContent == null)
                {
                    return null;
                }
                else
                {
                    return Encoding.Default.GetString(ByteContent);
                }
            }
            set => ByteContent = Encoding.Default.GetBytes(value);
        }

        //[JsonIgnore]
        public byte[] ByteContent { get; set; }

        //[NotMapped]
        //[JsonIgnore]
        public byte[] CompressedContent
        {
            get
            {
                //var byteArray = Encoding.Default.GetBytes(Content);

                //using (var ms = new MemoryStream())
                //{
                //    using (var sw = new GZipStream(ms, CompressionMode.Compress))
                //    {
                //        sw.Write(byteArray, 0, byteArray.Length);
                //        sw.Close();
                //        return ms.ToArray();
                //    }
                //}
                return new byte[] { };
            }
        }


//        [NotMapped]
//        [JsonIgnore]
        public string UncompressedContentTest
        {
            get
            {
                return "";
//                using (var ms = new MemoryStream(CompressedContent))
//                {
//                    using (var sr = new GZipStream(ms, CompressionMode.Decompress))
//                    {
//                        var length = Content.Length;
//                        var byteArray = new byte[length];
//                        int rByte = sr.Read(byteArray, 0, length);
//                        return Encoding.Default.GetString(byteArray);
//                    }
//                }
            }
        }



        public override string ToString()
        {
            return Name;
        }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

    }
}

