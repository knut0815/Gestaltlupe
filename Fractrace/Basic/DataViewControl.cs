﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fractrace.Basic {


  /// <summary>
  /// Display the Entries of the global object ParameterDict.
  /// </summary>
  public partial class DataViewControl : UserControl {


    /// <summary>
    /// Initializes a new instance of the <see cref="DataViewControl"/> class.
    /// </summary>
    public DataViewControl() {
      InitializeComponent();
    }


    protected List<DataViewElement> oldElements = new List<DataViewElement>();



    /// <summary>
    /// Some created pages. Key is the category.
    /// </summary>
    protected Dictionary<string, DataViewControlPage> mPages = new Dictionary<string, DataViewControlPage>();


    protected string oldCategory = "";



    /// <summary>
    /// A new category (as node in the tree view is selected). This control 
    /// has to display all corresponding entries.
    /// </summary>
    /// <param name="category">The category.</param>
    public void Select(string category) {
      this.SuspendLayout();
      DataViewControlPage newPage=null;
      if (mPages.ContainsKey(category)) {
        newPage = mPages[category];
        newPage.Update();
      } else {
        newPage = new DataViewControlPage(this);
        newPage.Create(category);
        mPages[category] = newPage;
      }
      if (oldCategory != category) {
        pnlMain.Controls.Clear();
        pnlMain.Controls.Add(newPage);
        this.Height = newPage.ComputedHeight;
      }
      this.ResumeLayout(true);
      oldCategory = category;
    }

    /// <summary>
    /// Inhalt des Steuerelementes wird mit den Einträgen befüllt, die category entsprechen.
    /// </summary>
    /// <param name="category"></param>
    public void Select_old(string category) {
      if (category == oldCategory) {
        Update(category);
        return;
      }

      oldCategory = category;

      this.SuspendLayout();

      // alte events abkoppeln:
      foreach (DataViewElement element in oldElements) {
        if(element!=null)
          element.ElementChanged += new ElementChangedDelegate(dElement_ElementChanged);
      }
      oldElements.Clear();

      pnlMain.Controls.Clear();
      int height = 0;

      bool elementAdded = false;
      foreach (KeyValuePair<string, string> entry in ParameterDict.Exemplar.SortedEntries) {
        if (entry.Key.StartsWith(category)) {
          if (entry.Key.Length > category.Length) {
            string tempName = entry.Key.Substring(category.Length + 1);
            if (!tempName.Contains(".")) {
              DataViewElement dElement = DataViewElementFactory.Create(entry.Key, entry.Value, "", "", true);
              dElement.ElementChanged += new ElementChangedDelegate(dElement_ElementChanged);
              oldElements.Add(dElement);
              dElement.TabIndex = oldElements.Count;
              height += DataViewElementFactory.DefaultHeight;
              elementAdded = true;
            }
          }
        }
      }

      // Wenn kein direktes Unterelement existiert, werden alle Unterelemente eingefügt.
      if (!elementAdded) {
        foreach (KeyValuePair<string, string> entry in ParameterDict.Exemplar.SortedEntries) {
          if (entry.Key.StartsWith(category)) {
              DataViewElement dElement = DataViewElementFactory.Create(entry.Key, entry.Value, "", "",false);
             // pnlMain.Controls.Add(dElement);
              dElement.ElementChanged += new ElementChangedDelegate(dElement_ElementChanged);
              oldElements.Add(dElement);
              dElement.TabIndex = oldElements.Count;
              height += DataViewElementFactory.DefaultHeight;
              elementAdded = true;
          }
        }
      }
      for (int i = oldElements.Count - 1; i >= 0; i--) {
        DataViewElement dElement = oldElements[i];
        pnlMain.Controls.Add(dElement);
      }
      this.Height = height;
      this.ResumeLayout(true);

    }


    /// <summary>
    /// Ein Unterelement hat sich geändert.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void dElement_ElementChanged(string name, string value) {
      if(ElementChanged!=null)
        ElementChanged(name,value);
      //throw new NotImplementedException();
    }

    /// <summary>
    /// Updates 
    /// </summary>
    /// <param name="category"></param>
    protected void Update(string category) {
          if (category == "Formula")
        return;
      foreach (DataViewElement element in oldElements) {
        element.Update();
      }
    }


    /// <summary>
    /// Wird aufgerufen, wenn der Nutzer eine Änderung eingegeben hat.
    /// </summary>
    public event ElementChangedDelegate ElementChanged;

  }
}
