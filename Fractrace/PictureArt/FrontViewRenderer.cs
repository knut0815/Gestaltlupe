﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Fractrace.DataTypes;
using Fractrace.Basic;
using Fractrace.PictureArt;
using Fractrace.Geometry;

namespace Fractrace.PictureArt
{


    /// <summary>
    /// Display the points in Front view. Used in Navigation mode. 
    /// </summary>
    public class FrontViewRenderer : ScienceRendererBase
    {

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="pData"></param>
        public FrontViewRenderer(PictureData pData)
          : base(pData)
        {
        }


        protected double _expectedMinY = double.MaxValue;
        protected double _expectedMaxY = double.MinValue;
        protected double _minY = double.MaxValue;
        protected double _maxY = double.MinValue;


        /// <summary>(
        /// Erstellt das fertige Bild
        /// </summary>
        public override void Paint(Graphics grLabel)
        {
            _width = pData.Width;
            _height = pData.Height;
            PreCalculate();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Pen p = new Pen(Color.White);
                    grLabel.DrawRectangle(p, i, j, (float)0.5, (float)0.5);
                }
            }
            // ?? Eigentlich sollte width==grLabel.VisibleClipBounds.Width gelten.
            for (int i = 0; i < _width && i < grLabel.VisibleClipBounds.Width; i++)
            {
                for (int j = 0; j < grLabel.VisibleClipBounds.Height && j < _height; j++)
                {
                    Pen p = new Pen(Color.Black);
                    // Switch to front draw
                    PixelInfo pInfo = pData.Points[i, j];
                    if (pInfo != null)
                    {
                        double y = pInfo.Coord.Y;
                        if (y != 0)
                        {
                            double dheight = _height;
                            double ypos = y - _expectedMinY;
                            ypos = ypos / (_expectedMaxY - _expectedMinY);
                            ypos = dheight - dheight * ypos;
                            grLabel.DrawRectangle(p, i, (int)ypos, (float)0.5, (float)0.5);
                        }
                    }
                }
            }
            if(_height>150)
            CallPaintEnds();
        }


        /// <summary>
        /// Allgemeine Informationen werden erzeugt
        /// </summary>
        protected override void PreCalculate()
        {
            _expectedMinY = ParameterDict.Current.GetDouble("Scene.CenterY")- 0.5*ParameterDict.Current.GetDouble("Scene.Radius"); 
            _expectedMaxY = ParameterDict.Current.GetDouble("Scene.CenterY") + 0.5*ParameterDict.Current.GetDouble("Scene.Radius");
            _minY = double.MaxValue;
            _maxY = double.MinValue;
            for (int i = 0; i < pData.Width; i++)
            {
                for (int j = 0; j < pData.Height; j++)
                {
                    PixelInfo pInfo = pData.Points[i, j];
                    if (pInfo != null)
                    {
                        if (_minY > pInfo.Coord.Y && pInfo.Coord.Y != 0)
                            _minY = pInfo.Coord.Y;
                        if (_maxY < pInfo.Coord.Y && pInfo.Coord.Y != 0)
                            _maxY = pInfo.Coord.Y;
                    }
                }
            }
        }


    }
}
