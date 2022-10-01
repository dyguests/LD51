using System;
using System.Collections.Generic;
using System.Text;
using Cores;
using Cores.Entities;
using Cores.Scenes.Games.Entities;
using Tools;
using UnityEngine;

namespace Ciphers
{
    public static class MapEncoder
    {
        public static string Encode(Map map)
        {
            var sb = new StringBuilder();
            sb.Append("V1"); // cipher version
            sb.Append("|");
            var size = map.Size;
            sb.Append(size.x + "x" + size.y + "x" + map.FrameLength + "x" + map.Cycle); // size
            sb.Append("|");
            sb.Append(EncodePos(map.StartPoint));
            sb.Append("|");
            sb.Append(EncodePos(map.EndPoint));
            sb.Append("|");
            sb.Append(string.Join(";", EncodeTileRings()));

            return sb.ToString();

            string EncodePos(Vector2Int pos)
            {
                return pos.x + "," + pos.y;
            }

            IEnumerable<string> EncodeTileRings()
            {
                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        var tileRings = map.GetRing(x, y);
                        if (tileRings == null) continue;
                        if (tileRings.Count == 0) continue;
                        var tileSb = new StringBuilder();
                        foreach (var tile in tileRings.Values)
                        {
                            if (tileSb.Length == 0) tileSb.Append(EncodePos(tile.Pos));

                            tileSb.Append(",");

                            var frames = tile.Frames;
                            tileSb.Append(tile.Type + frames.start + "+" + frames.length);
                        }

                        yield return tileSb.ToString();
                    }
                }
            }
        }

        public static Map Decode(string cipher)
        {
            var partsA = cipher.Split("|");
            var version = partsA[0]; // "V1"
            var whf = partsA[1].Split("x"); // size.x + "x" + size.y + "x" + map.FrameLength
            var map = new Map(Int32.Parse(whf[0]), Int32.Parse(whf[1]), Int32.Parse(whf[2]))
            {
                Cycle = whf[3].ParseInt(),
                StartPoint = DecodePos(partsA[2]),
                EndPoint = DecodePos(partsA[3])
            };
            foreach (var tileRingCipher in partsA[4].Split(";"))
            {
                InsertDecodeTileRing(tileRingCipher);
            }

            return map;

            Vector2Int DecodePos(string posCipher)
            {
                var parts = posCipher.Split(",");
                return new Vector2Int(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
            }


            void InsertDecodeTileRing(string tileRingCipher)
            {
                // 6,7,G0+1
                var parts = tileRingCipher.Split(",");
                var pos = new Vector2Int(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
                for (var i = 2; i < parts.Length; i++)
                {
                    var tileCipher = parts[i];
                    var tileType = tileCipher[0];
                    if (tileType == 'G')
                    {
                        var seg = DecodeFrames(tileCipher.Substring(1));
                        var ground = new Ground(seg.start, seg.length);

                        map.Insert(pos.x, pos.y, ground);
                    }
                    else
                    {
                        // todo 还未实现别的类型
                        throw new NotImplementedException("还未实现别的类型:" + tileType);
                    }
                }
            }
        }

        private static Seg DecodeFrames(string framesCipher)
        {
            //0+1
            var parts = framesCipher.Split("+");
            return new Seg(parts[0].ParseInt(), parts[1].ParseInt());
        }
    }
}