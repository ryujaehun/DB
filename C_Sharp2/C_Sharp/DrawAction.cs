using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
    public class DrawAction
    {
        public char type { get; set; }
        public System.Drawing.Rectangle rect { get; set; }
        public System.Drawing.Color color { get; set; }
        public System.Drawing.Image image { get; set; }
     

    public DrawAction(char type_, System.Drawing.Rectangle rect_, System.Drawing.Color color_)
        { type = type_; rect = rect_; color = color_; }

        public DrawAction(char type_, System.Drawing.Image image_)
        { type = type_; image = image_; }
    }
    public class DrawImage
    {
        public char type { get; set; }
        public System.Drawing.Image image { get; set; }


        public DrawImage(char type_, System.Drawing.Image image_)
        { type = type_; image = image_; }
    }

}
