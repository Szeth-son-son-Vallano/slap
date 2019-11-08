using iTextSharp.text;
using System.Collections.Generic;

namespace Slap
{
    class SlapColor
    {
        private static readonly List<BaseColor> _color;
        static SlapColor()
        {
            _color = new List<BaseColor>
            {
                new BaseColor(128, 128, 128),

                new BaseColor(255, 0, 0),
                new BaseColor(0, 255, 0),
                new BaseColor(0, 0, 255),
                new BaseColor(255, 255, 0),
                new BaseColor(0, 255, 255),
                new BaseColor(255, 0, 255),
                new BaseColor(255, 128, 0),
                new BaseColor(0, 255, 128),
                new BaseColor(128, 0, 255),
                new BaseColor(128, 255, 0),
                new BaseColor(0, 128, 255),
                new BaseColor(255, 0, 128),
                new BaseColor(255, 128, 128),
                new BaseColor(128, 255, 255),
                new BaseColor(128, 128, 255),
                new BaseColor(255, 255, 128),
                new BaseColor(128, 255, 255),
                new BaseColor(255, 128, 255),

                new BaseColor(255, 255, 255)
            };
        }

        public static List<BaseColor> Color
        {
            get { return _color; }
        }
    }
}
