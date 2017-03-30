using System;
using System.IO;

namespace LargeAddress
{
    public class LaaFile
    {
        private string _Path;
        public string Path
        {
            get { return _Path; }
        }

        private long _CharacteristicsOffset = -1;
        private Characteristics _Characteristics = (Characteristics)0;
        public Characteristics Characteristics
        {
            get { return _Characteristics; }
        }
        public bool LargeAddressAware
        {
            get
            {
                if ((_Characteristics & Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE) == Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE)
                    return true;
                else
                    return false;
            }
        }

        public LaaFile(string path)
        {
            _Path = path;
            ReadCharacteristics();
        }

        private bool GetOffsets()
        {
            bool output = true;

            try
            {
                FileStream fs = new FileStream(_Path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                fs.Seek(0, SeekOrigin.Begin);
                if (br.ReadByte() != 0x4D) // M
                    throw new Exception();

                if (br.ReadByte() != 0x5A) // Z
                    throw new Exception();

                fs.Seek(60, SeekOrigin.Begin);
                long pe = br.ReadUInt32();

                fs.Seek(pe, SeekOrigin.Begin);
                if (br.ReadByte() != 0x50) // P ortable
                    throw new Exception();

                if (br.ReadByte() != 0x45) // E executable
                    throw new Exception();

                fs.Seek(20, SeekOrigin.Current);
                _CharacteristicsOffset = fs.Position;
                _Characteristics = (Characteristics)br.ReadUInt16();

                br.Close();
                fs.Close();
            }
            catch
            {
                _CharacteristicsOffset = -1;
                output = false;
            }

            return output;
        }
        public bool ReadCharacteristics()
        {
            if (_CharacteristicsOffset == -1)
                return GetOffsets();
            else
            {
                bool output = true;

                try
                {
                    FileStream fs = new FileStream(_Path, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);

                    fs.Seek(_CharacteristicsOffset, SeekOrigin.Begin);
                    _Characteristics = (Characteristics)br.ReadUInt16();

                    br.Close();
                    fs.Close();
                }
                catch
                {
                    _CharacteristicsOffset = -1;
                    output = false;
                }

                return output;
            }
        }
        public bool WriteCharacteristics(Characteristics value)
        {
            if (_CharacteristicsOffset == -1)
                return false;
            else
            {
                bool output = true;

                try
                {
                    FileStream fs = new FileStream(_Path, FileMode.Open, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);

                    fs.Seek(_CharacteristicsOffset, SeekOrigin.Begin);
                    bw.Write((ushort)value);
                    bw.Flush();

                    bw.Close();
                    fs.Close();

                    _Characteristics = value; // Update the new value locally so it always matches the file.
                }
                catch { output = false; }

                return output;
            }
        }
        public bool WriteCharacteristics(bool laa)
        {
            Characteristics chars = _Characteristics;
            if (laa)
                chars = Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE | chars;
            else
                chars = ~Characteristics.IMAGE_FILE_LARGE_ADDRESS_AWARE & chars;

            return WriteCharacteristics(chars);
        }
    }
}
