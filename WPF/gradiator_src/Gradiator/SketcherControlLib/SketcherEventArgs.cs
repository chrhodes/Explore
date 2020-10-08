using System;
using System.Collections.Generic;
using System.Text;
using ShapesClassLib;

namespace SketcherControlLib
{
    public class SketcherEventArgs : EventArgs
    {
        public ShapeBase item;

        public SketcherEventArgs(ShapeBase shp)
        {
            item = shp;
        }
    }
}
