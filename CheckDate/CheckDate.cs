using NetEti.Globals;
using System;
using System.Threading;
using Vishnu.Interchange;

namespace CheckDate
{
    /// <summary>
    /// Liefert den aktuellen Zeitpunkt (Datum plus Uhrzeit).
    /// Implementiert die Schnittstelle 'INodeChecker' aus 'LogicalTaskTree.dll', über
    /// die der LogicalTaskTree von 'Vishnu' den Checker ausführen, dessen Ergebnis
    /// abfragen und sich ggf. in das Event 'NodeProgressChanged' einhängen kann.
    /// </summary>
    /// <remarks>
    /// File: CheckDate.cs
    /// Autor: Erik Nagel
    ///
    /// 27.05.2013 Erik Nagel: erstellt
    /// </remarks>
    public class CheckDate : INodeChecker
    {
        #region public members

        #region INodeChecker Implementation

        /// <summary>
        /// Wird ausgelöst, wenn sich der Verarbeitungsfortschritt dieses INodeCheckers
        /// geändert hat.
        /// </summary>
        public event CommonProgressChangedEventHandler NodeProgressChanged;

        /// <summary>
        /// Das Ergebnis (aktueller Zeitpunkt). Vergleiche führt Vishnu typgerecht über
        /// die Klasse 'NodeResultComparer' aus; bekannte Typen sind: bool, DateTime, String, int, double.
        /// Bei Verarbeitung von komplexen eigenen Typen muss die ToString()-Methode des ReturnObjects
        /// einen für Vergleiche entsprechend aufbereiteteten String zurückliefern.
        /// </summary>
        public object ReturnObject
        {
            get
            {
                return this._returnObject;
            }
            set
            {
                this._returnObject = value;
            }
        }

        /// <summary>
        /// Startet den Checker - wird von einem Knoten im LogicalTaskTree aufgerufen.
        /// Checker liefern grundsätzlich true oder false zurück. Darüber hinaus können
        /// weiter gehende Informationen über das ReturnObject transportiert werden;
        /// Hier wird DateTime.Now über das ReturnObject zurückgegeben.
        /// </summary>
        /// <param name="checkerParameters">Spezifische Aufrufparameter oder null.</param>
        /// <param name="treeParameters">Für den gesamten Tree gültige Parameter oder null.</param>
        /// <param name="source">Auslösendes TreeEvent oder null.</param>
        /// <returns>True, False oder null</returns>
        public bool? Run(object checkerParameters, TreeParameters treeParameters, TreeEvent source)
        {
            string pString = (checkerParameters ?? "").ToString().Trim();
            string[] paraStrings = pString.Split('|');
            string waitMSstr = paraStrings[0].Trim();
            int waitMS = 0;
            if (!String.IsNullOrEmpty(waitMSstr))
            {
                Int32.TryParse(waitMSstr, out waitMS);
            }
            this._returnObject = null;
            this.OnNodeProgressChanged(String.Format("{0}", this.GetType().Name), 100, 0, ItemsTypes.items);
            Thread.Sleep(waitMS);
            this.OnNodeProgressChanged(String.Format("{0}", this.GetType().Name), 100, 33, ItemsTypes.items);
            Thread.Sleep(waitMS);
            this.OnNodeProgressChanged(String.Format("{0}", this.GetType().Name), 100, 66, ItemsTypes.items);
            Thread.Sleep(waitMS);
            this._returnObject = DateTime.Now;
            this.OnNodeProgressChanged(String.Format("{0}", this.GetType().Name), 100, 100, ItemsTypes.items);

            return true;
        }

        #endregion INodeChecker Implementation

        /// <summary>
        /// Nur für Debug-Zwecke des Autors, kann ersatzlos wegfallen.
        /// </summary>
        public Guid GuidId { get; private set; }

        /// <summary>
        /// Standard Konstruktor.
        /// </summary>
        public CheckDate()
        {
            this.GuidId = Guid.NewGuid(); // Nur für Debug-Zwecke des Autors, kann ersatzlos wegfallen.
        }

        #endregion public members

        #region private members

        private object _returnObject = null;

        private void OnNodeProgressChanged(string itemsName, int countAll, int countSucceeded, ItemsTypes itemsType)
        {
            if (NodeProgressChanged != null)
            {
                NodeProgressChanged(null, new CommonProgressChangedEventArgs(itemsName, countAll, countSucceeded, itemsType, null));
            }
        }

        #endregion private members
    }
}
