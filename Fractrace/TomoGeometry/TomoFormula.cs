﻿using System;
using System.Collections.Generic;
using System.Text;

using Fractrace.Basic;

namespace Fractrace.TomoGeometry
{
    public class TomoFormula
    {


        /// <summary>
        /// Liefert den Abstand zur Objekmenge in einer diskreten Metrik.
        /// 
        /// Bei der Mandelbrotmenge z.B. ist es die Anzahl der Zyklen, die bis zur Erkennung vergehen, dass dieser Punkt nicht zur Menge
        /// gehört.
        /// 
        /// In der Computertomographie, (eigentlich eher in der Magnetresonanzstromanalyse) werden 3D-Daten nach aufwendigen Algorithmen analysiert.
        /// Zum Teil erfolgt der Umweg über dem Voxelraum. Dabei gehen aber wertvolle Informationen verloren. Es gibt Gegenden, bei denen
        /// eine genauere Analyse möglich wäre.
        /// </summary>
        /// <param name="x-Position"></param>
        /// <param name="y-Position (vom Bildschirm auf den Betrachter gerichtet)"></param>
        /// <param name="z-Position, horizontal "></param>
        /// <param name="ak"></param>
        /// <param name="br"></param>
        /// <param name="bi"></param>
        /// <param name="bj"></param>
        /// <param name="bk"></param>
        /// <param name="zkl"></param>
        /// <param name="invers"></param>
        /// <returns></returns>
        public virtual long InSet(double x, double y, double z, double br, double bi, double bj, double bk, long zkl, bool invers)
        {
            long retVal = 0;

            return retVal;
        }


        public AdditionalPointInfo additionalPointInfo = null;


        public void Set(string id, int value)
        {
            ParameterDict.Current.SetInt(id, value);
        }

        public void Set(string id, double value)
        {
            ParameterDict.Current.SetDouble(id, value);
        }


        public void Set(string id, bool value)
        {
            ParameterDict.Current.SetBool(id, value);
        }

        public double GetDouble(string id)
        {
            return ParameterDict.Current.GetDouble(id);
        }

        public double GetOrSetDouble(string id, double defaultValue = 0, string description = "",bool showInfo=true)
        {
            return ParameterDict.Current.GetOrSetDouble("Formula.Parameters."+id, defaultValue, description, showInfo);
        }

        public bool GetOrSetBool(string id, bool defaultValue = false, string description = "", bool showInfo = true)
        {
            return ParameterDict.Current.GetOrSetBool("Formula.Parameters." + id, defaultValue, description);
        }


        public string GetString(string id)
        {
            return ParameterDict.Current[id];
        }


        public void AddValue(string id, double value)
        {
            ParameterDict.Current[id] = "";
            ParameterDict.Current.SetValue(id, value.ToString(), false);
        }

        /// <summary>
        /// Delete the entry with given id complete from the Properties. 
        /// </summary>
        /// <param name="id">The id.</param>
        public void RemoveProperty(string id)
        {
            ParameterDict.Current.RemoveProperty(id);
        }


        /// <summary>
        /// Sets a bulk of parameters 
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetParameterBulk(string text)
        {
            ParameterDict.Current.AppendFromText(text);
        }


        /// <summary>
        /// Wird aufgerufen, kurz bevor der Scanalgorithmus beginnt. Zu diesem Zeitpunkt sind alle festen Parameter der
        /// </summary>
        public virtual void Init()
        {
            additionalPointInfo = new AdditionalPointInfo();
        }


    }
}
