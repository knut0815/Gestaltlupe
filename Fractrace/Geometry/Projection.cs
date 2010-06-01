﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fractrace.Geometry {
    public class Projection: Transform3D {


        public Projection(Vec3 camera, Vec3 viewPoint) {
            this.camera = camera;
            this.viewPoint = viewPoint;
            d = camera.Dist(viewPoint);
        }


        /// <summary>
        /// Kamerapunkt des Benutzers (üblicherweise parallel vor dem Zentrum des Vierecks 
        /// </summary>
        protected Vec3 camera = null;


        /// <summary>
        /// Der Punkt, der sich vor dem Kamerapunkt befindet und der auf das Zentrum des Bildschirmes
        /// plaziert werden soll.
        /// </summary>
        protected Vec3 viewPoint = null;


        /// <summary>
        /// Abstand der Kamera zum Ansichtspunkt.
        /// </summary>
        protected double d = 0;


        /// <summary>
        /// Dies ist einer reverse Transformation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Vec3 Transform(Vec3 input) {
            Vec3 p1 = input.Diff(camera);
            double dp = p1.Norm;
            double fac = dp / d;
            Vec3 p1_p = p1.Mult(fac);
            return (p1_p.Sum(camera));
        }

    }
}
