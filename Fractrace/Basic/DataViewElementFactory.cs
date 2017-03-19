﻿
namespace Fractrace.Basic
{
    class DataViewElementFactory
    {

        /// <summary>
        /// The height of all created DataViewElements.
        /// </summary>
        public static int DefaultHeight { get { return _defaultHeight; } }
        protected static int _defaultHeight = 24;


        /// <summary>
        /// Return new DataViewStringElement, DataViewBoolElement or DataViewHeadlineElement, depending of given type.
        /// The Dock property is always set to DockStyle.Top.
        /// </summary>
        public static DataViewElement Create(string name, string value, string type, string description, bool shortenName)
        {
            DataViewElement retVal = null;

            switch (type)
            {
                case "Bool":
                  retVal = new DataViewBoolElement();
                  break;

                case "Headline":
                  retVal = new DataViewHeadlineElement();
                  break;
            }

            // Use string as default datatype
            if (retVal == null)
            {
                DataViewStringElement stringElement = new DataViewStringElement();
                retVal = stringElement;
                //                stringElement.AddFillRightButton();
                bool hasButtons = false;
                if (ParameterDict.Current[name + ".PARAMETERINFO.VIEW.PlusButton"] != "")
                {
                    string buttonValue = ParameterDict.Current[name + ".PARAMETERINFO.VIEW.PlusButton"];
                    stringElement.AddPlusButton(buttonValue.Trim());
                    stringElement.AddMinusButton(buttonValue.Trim());
                    stringElement.AddAdjustButtons();
                    hasButtons = true;
                }
                if (ParameterDict.Current[name + ".PARAMETERINFO.VIEW.PlusPlusButton"] != "")
                {
                    string buttonValue = ParameterDict.Current[name + ".PARAMETERINFO.VIEW.PlusPlusButton"];
                    stringElement.AddPlusPlusButton(buttonValue.Trim());
                    stringElement.AddMinusMinusButton(buttonValue.Trim());
                    hasButtons = true;
                }
                if (ParameterDict.Current[name + ".PARAMETERINFO.VIEW.FixedButtons"] != "")
                {
                    string buttonValues = ParameterDict.Current[name + ".PARAMETERINFO.VIEW.FixedButtons"];
                    foreach (string buttonText in buttonValues.Split(' '))
                    {
                        stringElement.AddFixedValueButton(buttonText.Trim());
                        hasButtons = true;
                    }
                }
                if (hasButtons)
                   stringElement.AddFillRightButton();

            }

            retVal.Dock = System.Windows.Forms.DockStyle.Top;
            retVal.Height = _defaultHeight;
            retVal.Init(name, value, type, description, shortenName);

            return retVal;
        }


    }
}
