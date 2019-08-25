using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class SharpnessJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new List<SharpnessInfo>();

            if (reader.TokenType != JsonToken.StartArray)
                throw new FormatException("Expected start of array.");

            if (reader.Read() == false)
                throw new FormatException("Nothing to read.");

            while (true)
            {
                if (reader.TokenType == JsonToken.EndArray)
                    break;

                if (reader.TokenType != JsonToken.StartArray)
                    throw new FormatException("Expected start of array.");

                int n = 0;
                var values = new ushort[7];

                while (reader.Read() && reader.TokenType == JsonToken.Integer)
                    values[n++] = (ushort)Convert.ChangeType(reader.Value, typeof(ushort));

                if (reader.TokenType != JsonToken.EndArray)
                    throw new FormatException("Expected end of array.");

                if (reader.Read() == false)
                    throw new FormatException("Nothing to read.");

                result.Add(new SharpnessInfo(values[0], values[1], values[2], values[3], values[4], values[5], values[6]));
            }

            return result.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var values = (SharpnessInfo[])value;

            writer.WriteStartArray();

            foreach (SharpnessInfo item in values)
            {
                writer.WriteStartArray();

                Formatting writerFormatting = writer.Formatting;
                writer.Formatting = Formatting.None;

                writer.WriteValue(item.Red);
                writer.WriteValue(item.Orange);
                writer.WriteValue(item.Yellow);
                writer.WriteValue(item.Green);
                writer.WriteValue(item.Blue);
                writer.WriteValue(item.White);
                writer.WriteValue(item.Purple);

                writer.WriteEndArray();

                writer.Formatting = writerFormatting;
            }

            writer.WriteEndArray();
        }
    }

    public struct SharpnessInfo : IEquatable<SharpnessInfo>
    {
        public readonly ushort Red;
        public readonly ushort Orange;
        public readonly ushort Yellow;
        public readonly ushort Green;
        public readonly ushort Blue;
        public readonly ushort White;
        public readonly ushort Purple;

        public SharpnessInfo(ushort red, ushort orange, ushort yellow, ushort green, ushort blue, ushort white, ushort purple)
        {
            Red = red;
            Orange = orange;
            Yellow = yellow;
            Green = green;
            Blue = blue;
            White = white;
            Purple = purple;
        }

        public static SharpnessInfo FromAbsoluteValues(ushort red, ushort orange, ushort yellow, ushort green, ushort blue, ushort white, ushort purple)
        {
            return new SharpnessInfo(
                red,
                (ushort)(orange - red),
                (ushort)(yellow - orange),
                (ushort)(green - yellow),
                (ushort)(blue - green),
                (ushort)(white - blue),
                purple > 0 ? (ushort)(purple - white) : (ushort)0
            );
        }

        public bool Equals(SharpnessInfo other)
        {
            return
                Red == other.Red &&
                Orange == other.Orange &&
                Yellow == other.Yellow &&
                Green == other.Green &&
                Blue == other.Blue &&
                White == other.White &&
                Purple == other.Purple;
        }

        public ushort[] ToArray()
        {
            return new ushort[]
            {
                Red,
                Orange,
                Yellow,
                Green,
                Blue,
                White,
                Purple
            };
        }

        public void ToArray(ushort[] output)
        {
            if (output.Length < 7)
                throw new ArgumentOutOfRangeException(nameof(output), $"Argument '{nameof(output)}' must be of length 7 or more.");

            output[0] = Red;
            output[1] = Orange;
            output[2] = Yellow;
            output[3] = Green;
            output[4] = Blue;
            output[5] = White;
            output[6] = Purple;
        }

        public override string ToString()
        {
            return $"{Red}, {Orange}, {Yellow}, {Green}, {Blue}, {White}, {Purple}";
        }
    }
}
