using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> String extensions. </summary>
    public static partial class StringExtensions
    {
        private static readonly string[] TrueValues = { "true", "ok", "yes", "1" };

        private const string UserNamePattern = @"^[a-zA-Z][a-zA-Z0-9]";

        /// <summary> Checks if string is true-like (true, ok, yes, 1). </summary>
        /// <returns>True or false</returns>
        public static bool ToBoolean(this string self)
        {
            for (int i = 0; i < TrueValues.Length; ++i)
            {
                if (TrueValues[i].Equals(self.ToLowerInvariant()) == true)
                    return true;
            }

            return false;
        }

        /// <summary> Makes a byte from text with offset "0" and charcode of symbols. </summary>
        /// <param name="self">Text to work</param>
        /// <returns>Byte array that contains charcode symbols from text separated with zero offset.</returns>
        public static byte[] ToByteArray(this string self)
        {
            byte[] bytesArray = new byte[self.Length * sizeof(char)];
            Buffer.BlockCopy(self.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);

            return bytesArray;
        }

        /// <summary> Try to converts to int or 0. </summary>
        /// <param name="str">Value</param>
        /// <returns>Value or 0</returns>
        public static int ToInt(this string str)
        {
            int.TryParse(str, out int result);

            return result;
        }

        /// <summary> Try to converts to float or 0.0f. </summary>
        /// <returns>Value or 0.0f</returns>
        public static float ToFloat(this string self)
        {
            float.TryParse(self, NumberStyles.Float, CultureInfo.InvariantCulture, out float result);

            return result;
        }

        /// <summary> Try to converts to Vector2. </summary>
        /// <returns>Vector2</returns>
        public static Vector2 ToVector2(this string self)
        {
            Vector3 result = Vector3.zero;
            string[] args = self.Replace("f", string.Empty).Split(',');
            if (args.Length == 2)
            {
                float.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out result.x);
                float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out result.y);
            }

            return result;
        }

        /// <summary> Try to converts to Vector3. </summary>
        /// <returns>Vector3</returns>
        public static Vector3 ToVector3(this string self)
        {
            Vector3 result = Vector3.zero;
            string[] args = self.Replace("f", string.Empty).Split(',');
            if (args.Length == 3)
            {
                float.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out result.x);
                float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out result.y);
                float.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out result.z);
            }

            return result;
        }

        /// <summary> Try to converts to Vector4. </summary>
        /// <returns>Vector4</returns>
        public static Vector4 ToVector4(this string self)
        {
            Vector4 result = Vector4.zero;
            string[] args = self.Replace("f", string.Empty).Split(',');
            if (args.Length == 4)
            {
                float.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out result.x);
                float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out result.y);
                float.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out result.z);
                float.TryParse(args[3], NumberStyles.Float, CultureInfo.InvariantCulture, out result.w);
            }

            return result;
        }

        /// <summary> Try to converts to Quaternion. </summary>
        /// <returns>Quaternion</returns>
        public static Quaternion ToQuaternion(this string self)
        {
            Quaternion result = Quaternion.identity;
            string[] args = self.Replace("f", string.Empty).Split(',');
            if (args.Length == 4)
            {
                float.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out result.x);
                float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out result.y);
                float.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out result.z);
                float.TryParse(args[3], NumberStyles.Float, CultureInfo.InvariantCulture, out result.w);
            }

            return result;
        }

        /// <summary> Converts hex string color (0xFF00FF, #FF00FF, commas) to color. </summary>
        /// <returns>Color</returns>
        public static Color ToColor(this string self)
        {
            Color color = new Color();

            if (self.Contains("0x") == true || self.Contains("#") == true)
            {
                self = self.Replace("0x", string.Empty); // 0xFFFFFF?
                self = self.Replace("#", string.Empty); // #FFFFFF?

                byte a = 255;
                byte r = byte.Parse(self.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                byte g = byte.Parse(self.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                byte b = byte.Parse(self.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                if (self.Length == 8)
                    a = byte.Parse(self.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                color = new Color32(r, g, b, a);
            }
            else if (self.Contains(",") == true)
            {
                string[] channels = self.Substring(1, self.Length - 2).Split(',');
                if (channels.Length >= 3)
                {
                    color.r = float.Parse(channels[0]);
                    color.g = float.Parse(channels[1]);
                    color.b = float.Parse(channels[2]);

                    if (channels.Length == 4)
                        color.a = float.Parse(channels[3]);
                }
            }

            return color;
        }

        /// <summary> To hash algorithm called MD5. </summary>
        /// <returns>Hexadecimal converted value from text</returns>
        public static string ToMD5(this string self)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(self));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; ++i)
                builder.Append(data[i].ToString("x2"));

            return builder.ToString();
        }

        /// <summary> Encode to Base64 (RFC 3548, RFC 4648). </summary>
        /// <returns>UTF8 Base 64</returns>
        public static string ToBase64(this string self)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(self);

            return Convert.ToBase64String(valueBytes);
        }

        /// <summary> "Camel case string" => "CamelCaseString" </summary>
        /// <returns>Value</returns>
        public static string ToCamelCase(this string self)
        {
            self = self.Replace("-", " ").Replace("_", " ");
            self = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(self);
            self = self.Replace(" ", string.Empty);

            return self;
        }

        /// <summary> "CamelCaseString" => "Camel case string" </summary>
        /// <returns>Value</returns>
        public static string ToWords(this string self) => Regex.Replace(self, "(\\B[A-Z])", " $1").Capitalized();

        /// <summary> Decode to Base64 (RFC 3548, RFC 4648). </summary>
        /// <returns>String</returns>
        public static string FromBase64(this string self)
        {
            byte[] valueBytes = Convert.FromBase64String(self);

            return Encoding.UTF8.GetString(valueBytes);
        }

        /// <summary> Xor encryption. </summary>
        /// <param name="self">String</param>
        /// <param name="key">Key</param>
        /// <returns>De/Encrypted string</returns>
        public static string Xor(this string self, string key)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < self.Length; ++i)
                builder.Append((char)(self[i] ^ key[i % key.Length]));

            return builder.ToString();
        }

        /// <summary> "CamelCaseString" => "Camel case string" </summary>
        /// <returns>Value</returns>
        public static string FromCamelCase(this string self)
        {
            if (string.IsNullOrEmpty(self) == true)
                return self;

            string camelCase = Regex.Replace(Regex.Replace(self, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
            string firstLetter = camelCase.Substring(0, 1).ToUpperInvariant();

            if (self.Length > 1)
                return $"{firstLetter}{camelCase.Substring(1).ToLowerInvariant()}";

            return firstLetter;
        }

        /// <summary> Replaces the characters in the originalChars with characters of newChars. </summary>
        /// <param name="originalChars">Original characters.</param>
        /// <param name="newChars">Replacement characters.</param>
        /// <returns>Value</returns>
        public static string ReplaceChars(this string self, string originalChars, string newChars)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < self.Length; ++i)
            {
                int pos = originalChars.IndexOf(self.Substring(i, 1), StringComparison.InvariantCulture);

                builder.Append((-1 != pos) ? newChars.Substring(pos, 1) : self.Substring(i, 1));
            }

            return builder.ToString();
        }

        /// <summary> Index within the range of the string. </summary>
        /// <param name="index">Index</param>
        /// <returns>True/false</returns>
        public static bool IsIndexValid(this string self, int index) => self != null && index >= 0 && index < self.Length;

        /// <summary>
        /// Validates a text to be a username: the first character must be a letter,
        /// only Latin letters and numbers and of a certain length.
        /// </summary>
        /// <returns>True/false</returns>
        public static bool IsValidLatinUsername(this string self, int min = 6, int max = 12) => Regex.IsMatch(self, $"{UserNamePattern}{{{min},{max}}}");

        /// <summary> Is a valid email. </summary>
        /// <returns>True/false</returns>
        public static bool IsValidEmail(this string self) => Regex.IsMatch(self, @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");

        /// <summary> Calculate hash number from text. </summary>
        /// <returns>Hash number (ulong)</returns>
        public static ulong CalculateHash(this string self)
        {
            ulong hashedValue = 3074457345618258791ul;
            for (int i = 0; i < self.Length; ++i)
            {
                hashedValue += self[i];
                hashedValue *= 3074457345618258799ul;
            }

            return hashedValue;
        }

        /// <summary> First character or default. </summary>
        /// <returns>First character or default.</returns>
        public static char First(this string self) => string.IsNullOrEmpty(self) ? default : self[0];

        /// <summary> Last character or default. </summary>
        /// <returns>Last character or default.</returns>
        public static char Last(this string self) => string.IsNullOrEmpty(self) ? default : self[self.Length - 1];

        /// <summary> First character uppercase. </summary>
        /// <returns>Value or empty</returns>
        public static string Capitalized(this string self)
        {
            string capitalized = string.Empty;

            if (string.IsNullOrEmpty(self) == false)
                capitalized = self.Length == 1 ? char.ToUpper(self[0]).ToString() : $"{char.ToUpper(self[0])}{self.Substring(1)}";

            return capitalized;
        }

        /// <summary> Reverse the string. </summary>
        /// <returns>Reversed text</returns>
        public static string Reverse(this string self)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = self.Length; i-- > 0;)
                builder.Append(self[i]);

            return builder.ToString();
        }

        /// <summary> Remove illegal characters for files. </summary>
        /// <returns>Valid file / path</returns>
        public static string RemoveInvalidFileCharacters(this string self) => string.Concat(self.Split(Path.GetInvalidFileNameChars())).Trim();

        /// <summary> Relative to project path to absolute path. </summary>
        /// <returns>Absolute path</returns>
        public static string ToAbsolutePath(this string self)
        {
            int index = Application.dataPath.LastIndexOf("Assets");
            if (index >= 0)
            {
                return $"{Application.dataPath.Substring(0, index)}{self}";
            }

            return $"{Application.dataPath}{self}";
        }

        /// <summary> Absolute path to relative to project path. </summary>
        /// <returns>Relative path</returns>
        public static string ToRelativePath(this string self) => self.Substring(Application.dataPath.Length - "Assets".Length);

        /// <summary> Compress the text, using gzip. </summary>
        /// <returns>Compressed text</returns>
        public static string Compress(this string self)
        {
            string compressed;
            byte[] bytes = Encoding.UTF8.GetBytes(self);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(bytes, 0, bytes.Length);
                }

                memoryStream.Position = (long)0;
                byte[] numArray = new byte[checked((int)memoryStream.Length)];
                int length = memoryStream.Read(numArray, 0, numArray.Length);
                byte[] numArray1 = new byte[length + 4];
                Buffer.BlockCopy(numArray, 0, numArray1, 4, numArray.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, numArray1, 0, 4);
                compressed = Convert.ToBase64String(numArray1);
            }

            return compressed;
        }

        /// <summary> Decompress the text, using gzip. </summary>
        /// <returns>Decompressed text</returns>
        public static string Decompress(this string self)
        {
            string decompressed;
            byte[] numArray = Convert.FromBase64String(self);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(numArray, 0);
                memoryStream.Write(numArray, 4, numArray.Length - 4);
                byte[] numArray1 = new byte[num];
                memoryStream.Position = (long)0;
                using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(numArray1, 0, numArray1.Length);
                }

                decompressed = Encoding.UTF8.GetString(numArray1);
            }

            return decompressed;
        }

        /// <summary> Encrypts the string using TripleDES. </summary>
        /// <param name="passphrase">Passphrase</param>
        /// <returns>Text encrypted</returns>
        public static string Encrypt(this string self, string passphrase)
        {
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));

            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider()
            {
                Key = tdesKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            byte[] DataToEncrypt = UTF8.GetBytes(self);

            try
            {
                ICryptoTransform Encryptor = tdesAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                HashProvider.Clear();
            }

            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");

            return enc;
        }

        /// <summary> Decrypts the string using TripleDES. </summary>
        /// <param name="passphrase">Passphrase</param>
        /// <returns>Text decrypted</returns>
        public static string Decrypt(this string self, string passphrase)
        {
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));

            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider()
            {
                Key = tdesKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            self = self.Replace("KKK", "+");
            self = self.Replace("JJJ", "/");
            self = self.Replace("III", "\\");

            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(self);
                ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                Results = decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                HashProvider.Clear();
            }

            return UTF8.GetString(Results);
        }

        /// <summary> Similarity between two strings using Levenshtein algorithm. The higher, the more different are. </summary>
        /// <param name="comparison">Comparison string</param>
        /// <returns>0 if both strings are identical, otherwise a number indicating the level of difference</returns>
        public static int Similarity(this string self, string comparison)
        {
            char[] s = self.ToCharArray();
            char[] t = comparison.ToCharArray();
            int n = self.Length;
            int m = comparison.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0 || m == 0)
                return m;

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
                ;
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
                ;
            }

            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= m; ++j)
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + (t[j - 1].Equals(s[i - 1]) ? 0 : 1));
            }

            return d[n, m];
        }

        /// <summary> Rect containing the string. </summary>
        /// <param name="font">Font used</param>
        /// <param name="size">Font size</param>
        /// <param name="fontStyle">Font style</param>
        /// <returns>Rect</returns>
        public static Rect GetRect(this string self, Font font, int size = 10, FontStyle fontStyle = FontStyle.Normal)
        {
            float width = 0.0f, height = 0.0f, lineWidth = 0.0f, lineHeight = 0.0f;

            for (int i = 0; i < self.Length; ++i)
            {
                char letter = self[i];
                font.GetCharacterInfo(letter, out CharacterInfo charInfo, size, fontStyle);

                if (letter == '\n')
                {
                    if (lineHeight.NearlyEquals(0) == true)
                        lineHeight = size;

                    width = Mathf.Max(width, lineWidth);
                    height += lineHeight;
                    lineWidth = 0;
                    lineHeight = 0;
                }
                else
                {
                    lineWidth += charInfo.advance;
                    lineHeight = Mathf.Max(lineHeight, charInfo.size);
                }
            }

            width = Mathf.Max(width, lineWidth);
            height += lineHeight;

            return new Rect(0, 0, width, height);
        }
    }
}